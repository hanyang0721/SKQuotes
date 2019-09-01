using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SKCOMLib;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

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
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("SKQuote");
            if (processes.Length > 1)
            {
                MessageBox.Show("SKQuote is already open", "Warning", MessageBoxButtons.OK);
                //processes[0].CloseMainWindow();
                Application.Exit();
            }
            else
            {
                KLineProcessCount = 0;

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

            //listInformation.HorizontalScrollbar = true;

            // Create a Graphics object to use when determining the size of the largest item in the ListBox.
            Graphics g = listInformation.CreateGraphics();

            // Determine the size for HorizontalExtent using the MeasureString method using the last item in the list.
            int hzSize = (int)g.MeasureString(listInformation.Items[listInformation.Items.Count - 1].ToString(), listInformation.Font).Width;
            // Set the HorizontalExtent property.
            listInformation.HorizontalExtent = hzSize;
        }

        public void WriteMessage(int nCode)
        {
            listInformation.Items.Add( m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) );

            listInformation.SelectedIndex = listInformation.Items.Count - 1;

            //listInformation.HorizontalScrollbar = true;

            // Create a Graphics object to use when determining the size of the largest item in the ListBox.
            Graphics g = listInformation.CreateGraphics();

            // Determine the size for HorizontalExtent using the MeasureString method using the last item in the list.
            int hzSize = (int)g.MeasureString(listInformation.Items[listInformation.Items.Count - 1].ToString(), listInformation.Font).Width;
            // Set the HorizontalExtent property.
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
            Button getbtnTick = skQuote1.Controls.Find("btnTicks", true).FirstOrDefault() as Button;
            getbtnTick.PerformClick();
            if (skQuote1.b_TickRunning)
            {
                timer1.Enabled = false;//Tick come first, then check minite, daily table
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           int returnval = 1;

           if (KLineProcessCount > 1)
           {
                timer2.Enabled = false;
                TabControl gettabcontrol = skQuote1.Controls.Find("tabControl1", true).FirstOrDefault() as TabControl;
                gettabcontrol.SelectedIndex = 1;
                WriteMessage("【KLine】 Minute and Daily table check complete");
            }
           else if (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) >= TimeSpan.Parse("08:45") && TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) <= TimeSpan.Parse("08:50"))
           {
                TabControl gettabcontrol = skQuote1.Controls.Find("tabControl1", true).FirstOrDefault() as TabControl;
                gettabcontrol.SelectedIndex=2;
                int KLineType=0;
                if(KLineProcessCount==0)//Check if daily table has latest K bar
                {
                    using (SqlConnection conn = new SqlConnection(connectionstr))
                    {
                        SqlCommand sqlcmd = new SqlCommand();
                        sqlcmd.CommandText = "EXEC Stock.dbo.sp_ChkLatest_KLine @Chktype=0";//Chktype check daily
                        sqlcmd.Connection = conn;
                        sqlcmd.CommandType = CommandType.Text;
                        try
                        {
                            conn.Open();
                            returnval = (int)sqlcmd.ExecuteScalar();
                            KLineType = 4;
                        }
                        catch (Exception ex)
                        {}
                    }
                }
                else  //Check if minute table has latest K bar
                {
                    using (SqlConnection conn = new SqlConnection(connectionstr))
                    {
                        SqlCommand sqlcmd = new SqlCommand();
                        sqlcmd.CommandText = "EXEC Stock.dbo.sp_ChkLatest_KLine @Chktype=1";//Chktype check minute
                        sqlcmd.Connection = conn;
                        sqlcmd.CommandType = CommandType.Text;
                        try
                        {
                            conn.Open();
                            returnval = (int)sqlcmd.ExecuteScalar();
                            KLineType = 0;
                        }
                        catch (Exception ex)
                        {}
                    }
                }
                //If it doens't have the latest k bar, trigger the button to download
                if (returnval == 1)
                {
                    ComboBox getCombo1 = skQuote1.Controls.Find("searchtype2", true).FirstOrDefault() as ComboBox;
                    ComboBox getCombo2 = skQuote1.Controls.Find("boxKLine", true).FirstOrDefault() as ComboBox;
                    ComboBox getCombo3 = skQuote1.Controls.Find("boxOutType", true).FirstOrDefault() as ComboBox;
                    ComboBox getCombo4 = skQuote1.Controls.Find("boxTradeSession", true).FirstOrDefault() as ComboBox;
                    getCombo1.SelectedIndex = 0;
                    getCombo2.SelectedIndex = KLineType;
                    getCombo3.SelectedIndex = 0;
                    getCombo4.SelectedIndex = 1;
                    Button getbtnTick = skQuote1.Controls.Find("btnKLine", true).FirstOrDefault() as Button;
                    getbtnTick.PerformClick();
                    WriteMessage("【KLine】 Downaloaded " + (KLineType == 0 ? "Minute" : "Daily") + " Complete" );
                }
            }
           KLineProcessCount++;
        }
    }
}
