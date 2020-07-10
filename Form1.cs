﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SKCOMLib;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using SKQuote;
using System.Collections.Generic;

namespace SKCOMTester
{
    public partial class Form1 : Form
    {
        #region Environment Variable
        //----------------------------------------------------------------------
        // Environment Variable
        //----------------------------------------------------------------------
        int m_nCode;
        int retrytimes = 0;

        SKCenterLib m_pSKCenter;
        SKCenterLib m_pSKCenter2;
        SKReplyLib m_pSKReply;
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

            m_pSKReply = new SKReplyLib();
            skReply1.SKReplyLib = m_pSKReply;

            m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(this.OnAnnouncement);


            txtAccount.Text = System.Configuration.ConfigurationManager.AppSettings.Get("Username");
            txtPassWord.Text = System.Configuration.ConfigurationManager.AppSettings.Get("Password");

            StatusListBox.Items.Add("DB Conn: " + connectionstr);
            StatusListBox.Items.Add("TradeSession: " + (util.GetTradeSession() == 1? "AM盤":"全盤" ));

            util.RecordLog(connectionstr, "SKQuote login, Session:"+ (util.GetTradeSession() == 1 ? "Morning session" : "Night session"), util.INFO);
        }

        #endregion

        //KLineProcessCount 2 means Minute and Day KLine check is completed
        private void Form1_Load(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("SKQuote");
            if (processes.Length > 1)
            {
                MessageBox.Show("SKQuote is already open", "Warning", MessageBoxButtons.OK);
                util.RecordLog(connectionstr, "SKQuote is already open", util.ALARM);
                Application.Exit();
            }
            else
            {
                //Timer 1 : Tick 
                //Timer 2 : Import Daily, Minute KLine
                string[] args = Environment.GetCommandLineArgs();
                if (args.Length > 1)
                {
                    if (args[1].Equals("-KLine", StringComparison.InvariantCultureIgnoreCase))
                        KLineProcessCount = 0;
                    timer2.Interval = 60000;
                    timer2.Enabled = true;
                }
                else
                {
                    timer1.Interval = 10000;
                    timer1.Enabled = true;
                }

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

        //Mandatory to Add this after API version 2.13.17
        void OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
        {
            WriteMessage(strUserID + "_" + bstrMessage);
            nConfirmCode = -1;
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

        private void OnShowAgreement(string strData)
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
                util.RecordLog(connectionstr, "Downloading Ticks", util.INFO);
                retrytimes = retrytimes + 1;
                if(retrytimes==5)
                {
                    timer1.Enabled = false;
                }
            }
        }

        /*
         * KLineProcessCount 0: Minutes KLine, 全盤
         * KLineProcessCount 1: Daily, 全盤
         * KLineProcessCount 2: Daily, 早盤
         * */
        private void Timer2_Tick(object sender, EventArgs e)
        {
            DownloadKLine(KLineProcessCount);
            KLineProcessCount++;

            if(KLineProcessCount==3)
            {
                timer2.Enabled = false;
                util.RecordLog(connectionstr, "KLine Minute and Daily table import complete", util.INFO);
            }
        }

        private void DownloadKLine(int KLineProcessCount)
        {
            TabControl gettabcontrol = skQuote1.Controls.Find("tabControl1", true).FirstOrDefault() as TabControl;
            gettabcontrol.SelectedIndex = 2; //Switch to KLine download panel

            ComboBox getCombo1 = skQuote1.Controls.Find("searchtype2", true).FirstOrDefault() as ComboBox;
            ComboBox getCombo2 = skQuote1.Controls.Find("boxKLine", true).FirstOrDefault() as ComboBox;
            ComboBox getCombo3 = skQuote1.Controls.Find("boxOutType", true).FirstOrDefault() as ComboBox;
            ComboBox getCombo4 = skQuote1.Controls.Find("boxTradeSession", true).FirstOrDefault() as ComboBox;
            getCombo1.SelectedIndex = 0;//Query by stockNo
            getCombo2.SelectedIndex = KLineProcessCount == 0 ? 0 : 4 ;//0 minutes, 4 daily
            getCombo3.SelectedIndex = 0;//舊版格式
            //getCombo4.SelectedIndex = util.GetTradeSession();//0: 全盤 1:AM盤
            getCombo4.SelectedIndex = KLineProcessCount <= 1 ? 0 : 1; //KLineProcessCount=2 then 全盤 daily,  0: 全盤 1:AM盤
            Button getbtnTick = skQuote1.Controls.Find("btnKLine", true).FirstOrDefault() as Button;
            getbtnTick.PerformClick();
            WriteMessage("【KLine】 Downaloaded " + (KLineProcessCount == 0 ? "Minute" : "Daily") + " Complete");
            util.RecordLog(connectionstr, "KLine Downaloaded " + (KLineProcessCount == 0 ? "Minute" : "Daily") + " Complete", util.INFO);

            gettabcontrol.SelectedIndex = 1;
        }
    }
}
