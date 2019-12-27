using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SKCOMLib;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using SKQuote;

namespace SKCOMTester
{
    public partial class Form1 : Form
    {
        #region Environment Variable
        //----------------------------------------------------------------------
        // Environment Variable
        //----------------------------------------------------------------------
        int m_nCode;

        SKCenterLib m_pSKCenter;
        SKCenterLib m_pSKCenter2;

        SKQuoteLib m_pSKQuote;
        SKOSQuoteLib m_pSKOSQuote;

        public int KLineProcessCount;
        private string connectionstr = ConfigurationManager.AppSettings.Get("Connectionstring");
        //private int tradesession = Convert.ToInt16(ConfigurationManager.AppSettings.Get("TradeSession"));


        Utilties util = new Utilties();

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        private SKQuote skQuote1;

        //public UserControl userControl2 { get; set; }

        public Form1()
        {
            InitializeComponent();
            m_pSKCenter = new SKCenterLib();
            m_pSKCenter2 = new SKCenterLib();   

            m_pSKQuote = new SKQuoteLib();            
            skQuote1.SKQuoteLib = m_pSKQuote;            

            m_pSKOSQuote = new SKOSQuoteLib();
       
            m_pSKCenter2.OnShowAgreement += new _ISKCenterLibEvents_OnShowAgreementEventHandler(this.OnShowAgreement);

            txtAccount.Text = System.Configuration.ConfigurationManager.AppSettings.Get("Username");
            txtPassWord.Text = System.Configuration.ConfigurationManager.AppSettings.Get("Password");

            StatusListBox.Items.Add("DB Conn: " + connectionstr);
            StatusListBox.Items.Add("TradeSession: " + (util.GetTradeSession() == 1? "AM盤":"全盤" ));

            util.RecordLog(connectionstr, "SKQuote login, Session:"+ (util.GetTradeSession() == 1 ? "Morning session" : "Night session"));
        }

        #endregion

        //KLineProcessCount 2 means Minute and Day KLine check is completed
        private void Form1_Load(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("SKQuote");
            if (processes.Length > 1)
            {
                MessageBox.Show("SKQuote is already open", "Warning", MessageBoxButtons.OK);
                util.RecordLog(connectionstr, "SKQuote is already open");
                Application.Exit();
            }
            else
            {
                KLineProcessCount = 0;

                //  Timer 1 : Tick 
                //  Timer 2 : Import Daily, Minute KLine
                timer1.Interval = 10000;
                timer1.Enabled = true;

                timer2.Interval = 20000;
                timer2.Enabled = true;

                btnInitialize.PerformClick();//登入群益系統
                m_pSKCenter.SKCenterLib_SetLogPath(AppDomain.CurrentDomain.BaseDirectory.ToString() + "log\\");
                //this.Controls.Add(skQuote1);
                Button getbtnConnect = skQuote1.Controls.Find("button1", true).FirstOrDefault() as Button;
            
                getbtnConnect.PerformClick();//登入報價系統
            }
        }



        private void btnInitialize_Click(object sender, EventArgs e)
        {
            
            m_nCode = m_pSKCenter.SKCenterLib_Login(txtAccount.Text.Trim().ToUpper(), txtPassWord.Text.Trim());

            if (m_nCode == 0)
            {
                WriteMessage("登入成功");
                skQuote1.LoginID = txtAccount.Text.Trim().ToUpper();             
            }
            else
                WriteMessage(m_nCode);
        }

        public void WriteMessage(string strMsg)
        {
            listInformation.Items.Add(strMsg);
            listInformation.SelectedIndex = listInformation.Items.Count - 1;
            Graphics g = listInformation.CreateGraphics();
            int hzSize = (int)g.MeasureString(listInformation.Items[listInformation.Items.Count - 1].ToString(), listInformation.Font).Width;
            listInformation.HorizontalExtent = hzSize;
        }

        public void WriteMessage(int nCode)
        {
            listInformation.Items.Add( m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) );
            listInformation.SelectedIndex = listInformation.Items.Count - 1;
            Graphics g = listInformation.CreateGraphics();
            int hzSize = (int)g.MeasureString(listInformation.Items[listInformation.Items.Count - 1].ToString(), listInformation.Font).Width;
            listInformation.HorizontalExtent = hzSize;
        }

        private void GetMessage(string strType, int nCode, string strMessage)
        {
            string strInfo = "";

            if (nCode != 0)
                strInfo ="【"+ m_pSKCenter.SKCenterLib_GetLastLogInfo()+ "】";

            WriteMessage("【" + strType + "】【" + strMessage + "】【" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + "】" + strInfo);
        }

        void OnShowAgreement(string strData)
        {
            MessageBox.Show(strData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_nCode = m_pSKCenter2.SKCenterLib_Login(txtAccount.Text.Trim().ToUpper(), txtPassWord.Text.Trim());

            if (m_nCode == 0)
            {                
                //skOrder21.LoginID = txtAccount2.Text.Trim().ToUpper();
                skQuote1.LoginID2 = txtAccount.Text.Trim().ToUpper();
                WriteMessage("登入成功" );
                //skosQuote1.LoginID = txtAccount2.Text.Trim().ToUpper();
            }
            else
                WriteMessage(m_nCode);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Button getbtnTick = skQuote1.Controls.Find("btnTicks", true).FirstOrDefault() as Button;
            getbtnTick.PerformClick();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //If ticks already running, stop the timer
            if (skQuote1.TickRunning)
            {
                timer1.Enabled = false;
            }
            else
            {
                Button getbtnTick = skQuote1.Controls.Find("btnTicks", true).FirstOrDefault() as Button;
                getbtnTick.PerformClick();
                util.RecordLog(connectionstr, "Downloading Ticks");
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //0 Will not do any Kline download
           int returnval = 0;
           int KLineType = -1;//decide which Kline to be download later           
                   
           TabControl gettabcontrol = skQuote1.Controls.Find("tabControl1", true).FirstOrDefault() as TabControl;
           gettabcontrol.SelectedIndex = 2; //Switch to KLine download panel

           if (KLineProcessCount==0)//Check if daily table has latest K bar
           {
               using (SqlConnection conn = new SqlConnection(connectionstr))
               {
                   SqlCommand sqlcmd = new SqlCommand();
                   sqlcmd.CommandText = "EXEC dbo.sp_ChkLatest_KLine @Chktype=0";//Chktype check daily
                   sqlcmd.Connection = conn;
                   sqlcmd.CommandType = CommandType.Text;
                   try
                   {
                       conn.Open();
                       returnval = (int)sqlcmd.ExecuteScalar();
                       KLineType = 4;
                   }
                    catch (Exception ex)
                    {
                        util.RecordLog(connectionstr, ex.Message);
                    }
                }
           }
           else //Check if minute table has latest K bar
           {
               using (SqlConnection conn = new SqlConnection(connectionstr))
               {
                   SqlCommand sqlcmd = new SqlCommand();
                   // Chktype check minute, 1 - GetTradeSession is because I need flip 1 and 0 since Capital has a oppiste definition for trade session
                   sqlcmd.CommandText = "EXEC dbo.sp_ChkLatest_KLine @Chktype=1, @Session=" + (1- util.GetTradeSession());
                   sqlcmd.Connection = conn;
                   sqlcmd.CommandType = CommandType.Text;
                   try
                   {
                       conn.Open();
                       returnval = (int)sqlcmd.ExecuteScalar();
                       KLineType = 0;
                   }
                   catch(Exception ex)
                   {
                        util.RecordLog(connectionstr, ex.Message);
                   }
               }
           }
           //If it doens't have the latest k bar, trigger the button to download
           if (returnval == 1)
           {
               ComboBox getCombo1 = skQuote1.Controls.Find("searchtype2", true).FirstOrDefault() as ComboBox;
               ComboBox getCombo2 = skQuote1.Controls.Find("boxKLine", true).FirstOrDefault() as ComboBox;
               ComboBox getCombo3 = skQuote1.Controls.Find("boxOutType", true).FirstOrDefault() as ComboBox;
               ComboBox getCombo4 = skQuote1.Controls.Find("boxTradeSession", true).FirstOrDefault() as ComboBox;
               getCombo1.SelectedIndex = 0;//Query by stockNo
               getCombo2.SelectedIndex = KLineType;
               getCombo3.SelectedIndex = 0;//舊版格式
               getCombo4.SelectedIndex = util.GetTradeSession();//0: 全盤 1:AM盤
               Button getbtnTick = skQuote1.Controls.Find("btnKLine", true).FirstOrDefault() as Button;
               getbtnTick.PerformClick();
               WriteMessage("【KLine】 Downaloaded " + (KLineType == 0 ? "Minute" : "Daily") + " Complete" );
               util.RecordLog(connectionstr, "KLine Downaloaded " + (KLineType == 0 ? "Minute" : "Daily") + " Complete");
           }
           KLineProcessCount++;

           //Switch the tabControl back to Tick tab when KLine import is finished, 
           //Though it's not necessary to do it
           
           if (KLineProcessCount > 1)
           {
               timer2.Enabled = false;
               gettabcontrol.SelectedIndex = 1;
               WriteMessage("【KLine】 Minute and Daily table check complete");
               util.RecordLog(connectionstr, "KLine Minute and Daily table check complete");
           }
        }
    }
}
