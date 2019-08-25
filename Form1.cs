using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SKCOMLib;
using System.Configuration;

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

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        private SKQuote skQuote1;

        public UserControl userControl2 { get; set; }

        public Form1()
        {
            InitializeComponent();
            m_pSKCenter = new SKCenterLib();
            m_pSKCenter2 = new SKCenterLib();   

            m_pSKQuote = new SKQuoteLib();            
            skQuote1.SKQuoteLib = m_pSKQuote;            

            m_pSKOSQuote = new SKOSQuoteLib();
       
            m_pSKCenter2.OnShowAgreement += new _ISKCenterLibEvents_OnShowAgreementEventHandler(this.OnShowAgreement);

            txtAccount.Text = ConfigurationManager.AppSettings.Get("Username");
            txtPassWord.Text = ConfigurationManager.AppSettings.Get("Password");
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
                timer1.Enabled = true;
                timer1.Interval = 10000;
                btnInitialize.PerformClick();//登入群益系統
                m_pSKCenter.SKCenterLib_SetLogPath(AppDomain.CurrentDomain.BaseDirectory.ToString() + "log\\");
                //this.Controls.Add(skQuote1);
                Button getbtnConnect = skQuote1.Controls.Find("button1", true).FirstOrDefault() as Button;
                Button getbtnTick = skQuote1.Controls.Find("btnTicks", true).FirstOrDefault() as Button;

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
                timer1.Enabled = false;
            }
        }
    }
}
