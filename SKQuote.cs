using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SKCOMLib;
using System.Data.SqlClient;
using System.Configuration;

namespace SKCOMTester
{
    public partial class SKQuote : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------
        private bool m_bfirst = true;
        public bool b_TickRunning = true;
        private int m_nCode;
        private int m_nSimulateStock;
        private string connectionstr = ConfigurationManager.AppSettings.Get("Connectionstring");

        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        public event MyMessageHandler GetMessage;

        SKCOMLib.SKQuoteLib m_SKQuoteLib = null;
        SKCOMLib.SKQuoteLib m_SKQuoteLib2 = null;
        public SKQuoteLib SKQuoteLib
        {
            get { return m_SKQuoteLib; }
            set { m_SKQuoteLib = value;}
        }
        public SKQuoteLib SKQuoteLib2
        {
            get { return m_SKQuoteLib2; }
            set { m_SKQuoteLib2 = value;}
        }

        public string m_strLoginID = "";
        public string LoginID
        {
            get { return m_strLoginID; }
            set
            {
                m_strLoginID = value;
            }
        }

        public bool TickRunning
        {
            get { return b_TickRunning; }
            set
            {
                b_TickRunning = value;
            }
        }


        public string m_strLoginID2 = "";
        public string LoginID2
        {
            get { return m_strLoginID2; }
            set
            {
                m_strLoginID2 = value;
            }
        }

        private DataTable m_dtStocks;
        private DataTable m_dtBest5Ask;
        private DataTable m_dtBest5Bid;

        SqlCommand sqlcmd = new SqlCommand();
        SqlParameter stockPara = new SqlParameter();
        SqlParameter ptrPara = new SqlParameter();
        SqlParameter datePara = new SqlParameter();
        SqlParameter timePara = new SqlParameter();
        SqlParameter lTimeMSPara = new SqlParameter();
        SqlParameter nBidpara = new SqlParameter();
        SqlParameter nAskpara = new SqlParameter();
        SqlParameter nClosepara = new SqlParameter();
        SqlParameter nQtypara = new SqlParameter();

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        public SKQuote()
        {
            InitializeComponent();
           
        }

        private void SKQuote_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;

            m_dtStocks = CreateStocksDataTable();
            m_dtBest5Ask = CreateBest5AskTable();
            m_dtBest5Bid = CreateBest5AskTable();
            SetDoubleBuffered(gridStocks);

            stockPara = sqlcmd.Parameters.Add("@stockIdx", SqlDbType.VarChar, 8);
            datePara = sqlcmd.Parameters.Add("@ndate", SqlDbType.Int);
            timePara = sqlcmd.Parameters.Add("@lTimehms", SqlDbType.Int);
            ptrPara = sqlcmd.Parameters.Add("@nptr", SqlDbType.Int);
            lTimeMSPara = sqlcmd.Parameters.Add("@lTimeMS", SqlDbType.Int);
            nBidpara = sqlcmd.Parameters.Add("@nBid", SqlDbType.Int);
            nAskpara = sqlcmd.Parameters.Add("@nAskpara", SqlDbType.Int);
            nClosepara = sqlcmd.Parameters.Add("@nClosepara", SqlDbType.Int);
            nQtypara = sqlcmd.Parameters.Add("@nQtypara", SqlDbType.Int);

            searchtype.Items.Add("Query by StockNo");
            searchtype.Items.Add("Query by PageNo ");
            searchtype2.Items.Add("Query by StockNo");
            searchtype2.Items.Add("Query by PageNo ");
        }

        #endregion

        #region Custom Method
        //----------------------------------------------------------------------
        // Custom Method
        //----------------------------------------------------------------------

        void SendReturnMessage(string strType, int nCode, string strMessage)
        {
            if (GetMessage != null)
            {
                GetMessage(strType, nCode, strMessage);
            }
        }
        #endregion


        #region Component Event
        //----------------------------------------------------------------------
        // Component Event
        //----------------------------------------------------------------------
        private  void connect()
        {
            if (m_bfirst == true)
            {
                m_SKQuoteLib.OnConnection += new _ISKQuoteLibEvents_OnConnectionEventHandler(m_SKQuoteLib_OnConnection);
                m_SKQuoteLib.OnNotifyQuote += new _ISKQuoteLibEvents_OnNotifyQuoteEventHandler(m_SKQuoteLib_OnNotifyQuote);
                m_SKQuoteLib.OnNotifyHistoryTicks += new _ISKQuoteLibEvents_OnNotifyHistoryTicksEventHandler(m_SKQuoteLib_OnNotifyHistoryTicks);
                m_SKQuoteLib.OnNotifyTicks += new _ISKQuoteLibEvents_OnNotifyTicksEventHandler(m_SKQuoteLib_OnNotifyTicks);
                m_SKQuoteLib.OnNotifyBest5 += new _ISKQuoteLibEvents_OnNotifyBest5EventHandler(m_SKQuoteLib_OnNotifyBest5);
                m_SKQuoteLib.OnNotifyKLineData += new _ISKQuoteLibEvents_OnNotifyKLineDataEventHandler(m_SKQuoteLib_OnNotifyKLineData);
                m_SKQuoteLib.OnNotifyServerTime += new _ISKQuoteLibEvents_OnNotifyServerTimeEventHandler(m_SKQuoteLib_OnNotifyServerTime);
                m_bfirst = false;
            }
            m_nCode = m_SKQuoteLib.SKQuoteLib_EnterMonitor();

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_EnterMonitor");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connect();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKQuoteLib.SKQuoteLib_LeaveMonitor();

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_LeaveMonitor");
        }

        private void deleteTickData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstr))
                {
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.CommandText = @" TRUNCATE TABLE dbo.TickData ";
                    connection.Open();
                    sqlcmd.Connection = connection;
                    sqlcmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnTicks_Click(object sender, EventArgs e)
        {
            deleteTickData();
            listTicks.Items.Clear();
            m_dtBest5Ask.Clear();
            m_dtBest5Bid.Clear();
            GridBest5Ask.DataSource = m_dtBest5Ask;
            GridBest5Bid.DataSource = m_dtBest5Bid;

            GridBest5Ask.Columns["m_nAskQty"].HeaderText = "張數";
            GridBest5Ask.Columns["m_nAskQty"].Width = 60;
            GridBest5Ask.Columns["m_nAsk"].HeaderText = "賣價";
            GridBest5Ask.Columns["m_nAsk"].Width = 60;

            GridBest5Bid.Columns["m_nAskQty"].HeaderText = "張數";
            GridBest5Bid.Columns["m_nAskQty"].Width = 60;
            GridBest5Bid.Columns["m_nAsk"].HeaderText = "買價";
            GridBest5Bid.Columns["m_nAsk"].Width = 60;

            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestTicks(0, txtTick.Text.Trim());

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestTicks");
        }

        private void btnQueryStocks_Click(object sender, EventArgs e)
        {
            short sPage;

            if (short.TryParse(txtPageNo.Text, out sPage) == false && searchtype.SelectedIndex ==-1)
                return;

            m_dtStocks.Clear();
            gridStocks.ClearSelection();

            gridStocks.DataSource = m_dtStocks;

            gridStocks.Columns["m_sStockidx"].Visible = false;
            gridStocks.Columns["m_sDecimal"].Visible = false;
            gridStocks.Columns["m_sTypeNo"].Visible = false;
            gridStocks.Columns["m_cMarketNo"].Visible = false;
            gridStocks.Columns["m_caStockNo"].HeaderText = "代碼";
            gridStocks.Columns["m_caName"].HeaderText = "名稱";
            gridStocks.Columns["m_nOpen"].HeaderText = "開盤價";
            //gridStocks.Columns["m_nHigh"].Visible = false;
            gridStocks.Columns["m_nHigh"].HeaderText = "最高";
            //gridStocks.Columns["m_nLow"].Visible = false;
            gridStocks.Columns["m_nLow"].HeaderText = "最低";
            gridStocks.Columns["m_nClose"].HeaderText = "成交價";
            gridStocks.Columns["m_nTickQty"].HeaderText = "單量";
            gridStocks.Columns["m_nRef"].HeaderText = "昨收價";
            gridStocks.Columns["m_nBid"].HeaderText = "買價";
            //gridStocks.Columns["m_nBc"].Visible = false;
            gridStocks.Columns["m_nBc"].HeaderText = "買量";
            gridStocks.Columns["m_nAsk"].HeaderText = "賣價";
            //gridStocks.Columns["m_nAc"].Visible = false;
            gridStocks.Columns["m_nAc"].HeaderText = "賣量";
            //gridStocks.Columns["m_nTBc"].Visible = false;
            gridStocks.Columns["m_nTBc"].HeaderText = "買盤量";
            //gridStocks.Columns["m_nTAc"].Visible = false;
            gridStocks.Columns["m_nTAc"].HeaderText = "賣盤量";
            gridStocks.Columns["m_nFutureOI"].Visible = false;
            //gridStocks.Columns["m_nTQty"].Visible = false;
            gridStocks.Columns["m_nTQty"].HeaderText = "總量";
            //gridStocks.Columns["m_nYQty"].Visible = false;
            gridStocks.Columns["m_nYQty"].HeaderText = "昨量";
            //gridStocks.Columns["m_nUp"].Visible = false;
            gridStocks.Columns["m_nUp"].HeaderText = "漲停";
            //gridStocks.Columns["m_nDown"].Visible = false;
            gridStocks.Columns["m_nDown"].HeaderText = "跌停";
            string stockstr = "";
            string[] Stocks= { };
            if (searchtype.SelectedIndex == 1)
            {
                using (SqlConnection connection = new SqlConnection(connectionstr))
                {
                    string[] bstrDataset = new string[5];
                    string sqlwhere = "";
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlwhere = " WHERE PageNo=" + UserPagetxt.Text.ToString();

                    connection.Open();

                    sqlcmd.CommandText = @"SELECT [StockNo] FROM [Stock].[dbo].[StockList] " + sqlwhere;
                    sqlcmd.Connection = connection;
                    sqlcmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stockstr = stockstr + "," + reader.GetValue(0).ToString();
                        }
                    }
                }
                Stocks = stockstr.Split(new Char[] { ',' });
                Stocks = Stocks.Where((source, index) => index != 0).ToArray();//remove the first element
            }
            if (searchtype.SelectedIndex == 0)
            {
                Stocks = txtStocks.Text.Trim().Split(new Char[] { ',' });
            }

            
            foreach (string s in Stocks)
            {
                SKSTOCK pSKStock = new SKSTOCK();

                int nCode = m_SKQuoteLib.SKQuoteLib_GetStockByNo(s.Trim(), ref pSKStock);

                OnUpDateDataRow(pSKStock);

                if (nCode == 0)
                {
                    OnUpDateDataRow(pSKStock);
                }
            }

            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestStocks(ref sPage, stockstr);

            txtPageNo.Text = sPage.ToString();

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestStocks");
        }

        private void btnTickStop_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestTicks(50, txtTick.Text.Trim());

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_CancelRequestTicks");
        }

        private void btnServerTime_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestServerTime();

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestServerTime");
        }

        private void btnKLine_Click(object sender, EventArgs e)
        {
            short sKLineType = short.Parse(boxKLine.SelectedIndex.ToString());
            short sOutType = short.Parse(boxOutType.SelectedIndex.ToString());

            // sKline 4=完整日線
            //0 舊版輸出

            listKLine.Items.Clear();
            if (sKLineType < 0)
            {
                MessageBox.Show("請選擇KLine類型");
                return;
            }
            if (sOutType < 0)
            {
                MessageBox.Show("請選擇輸出格式類型");
                return;
            }

            if (searchtype2.SelectedIndex==1)
            {
                using (SqlConnection connection = new SqlConnection(connectionstr))
                {
                    string[] bstrDataset = new string[5];
                    SqlCommand sqlcmd = new SqlCommand();

                    int count = 0;
                    string sqlwhere = "";
                    connection.Open();

                    
                    sqlwhere = " WHERE PageNo=" + txtKLine.Text.ToString();

                    sqlcmd.CommandText = @"SELECT [StockNo] FROM [Stock].[dbo].[StockList]" + sqlwhere;
                    sqlcmd.Connection = connection;
                    sqlcmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count++;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                m_nCode = m_SKQuoteLib.SKQuoteLib_RequestKLine(reader.GetValue(i).ToString(), sKLineType, sOutType);

                                SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestKLine");
                            }
                        }
                    }
                }
            }
            else
            {
                m_nCode = m_SKQuoteLib.SKQuoteLib_RequestKLine(txtKLine.Text.Trim(), sKLineType, sOutType);

                SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestKLine");
            }
        }

        private void gridStocks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    e.Graphics.FillRectangle(Brushes.Black, e.CellBounds);

                    if (e.Value != null)
                    {
                        string strHeaderText = ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText.ToString();

                        if (strHeaderText == "名稱")
                        {
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.SkyBlue, e.CellBounds.X, e.CellBounds.Y);
                        }
                        else if (strHeaderText == "買價" || strHeaderText == "賣價" || strHeaderText == "成交價" || strHeaderText == "開盤價" || strHeaderText == "最高" || strHeaderText == "最低")
                        {
                            double dPrc = double.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells["m_nRef"].Value.ToString());

                            double dValue = double.Parse(e.Value.ToString());

                            if (m_nSimulateStock == 1 && strHeaderText != "開盤價")            //盤前/後揭示為深灰色;
                                e.Graphics.FillRectangle(Brushes.SlateGray, e.CellBounds);

                            if (dValue > dPrc)
                            {
                                e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Red, e.CellBounds.X, e.CellBounds.Y);
                            }
                            else if (dValue < dPrc)
                            {
                                e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Lime, e.CellBounds.X, e.CellBounds.Y);
                            }
                            else
                            {
                                e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.White, e.CellBounds.X, e.CellBounds.Y);
                            }
                        }
                        else if (strHeaderText == "單量")
                        {
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Yellow, e.CellBounds.X, e.CellBounds.Y);
                        }
                        else
                        {
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.White, e.CellBounds.X, e.CellBounds.Y);
                        }
                    }
                    e.Handled = true;
                }
                catch (Exception ex)
                {

                }
            }
        }

        #endregion

        #region COM EVENT
        //----------------------------------------------------------------------
        // COM EVENT
        //----------------------------------------------------------------------
        void m_SKQuoteLib_OnConnection(int nKind, int nCode)
        {
            if (nKind == 3001)
            {

                if (nCode == 0)
                {
                    lblSignal.ForeColor = Color.Yellow;
                }
            }
            else if (nKind == 3002)
            {
                lblSignal.ForeColor = Color.Red;
            }
            else if (nKind == 3003)
            {
                lblSignal.ForeColor = Color.Green;
            }
            else if (nKind == 3021)//網路斷線
            {
                lblSignal.ForeColor = Color.DarkRed;                 
            }
        }

       /* void m_SKQuoteLib2_OnConnection(int nKind, int nCode)
        {
            if (nKind == 3001)
            {

                if (nCode == 0)
                {
                    label_2.ForeColor = Color.Yellow;
                }
            }
            else if (nKind == 3002)
            {
                label_2.ForeColor = Color.Red;
            }
            else if (nKind == 3003)
            {
                label_2.ForeColor = Color.Green;
            }
            else if (nKind == 3021)//網路斷線
            {
                label_2.ForeColor = Color.DarkRed;
            }
        }*/

        void m_SKQuoteLib_OnNotifyQuote(short sMarketNo, short sStockIdx)
        {
            SKSTOCK pSKStock = new SKSTOCK();

            m_SKQuoteLib.SKQuoteLib_GetStockByIndex(sMarketNo, sStockIdx, ref pSKStock);

            OnUpDateDataRow(pSKStock);
        }


        void m_SKQuoteLib_OnNotifyTicks(short sMarketNo, short sStockIdx, int nPtr, int nDate, int lTimehms, int lTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate)
        {
            string strData = "";
            //string strTimeNoMsMs = "";
            //int nlength = lTime.ToString().Length;
            //if (nlength >6)
            //    strTimeNoMsMs = lTime.ToString().Substring(0, nlength - 6);
            //[-1020-add for h:m:s'millissecond''microsecond][-0219-add Qty-]
            //string strData = nPtr.ToString() + "," + nTime.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
            //if (chkbox_msms.Checked == true)
            //    strData = sStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
            //else
            strData = sStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + " " + lTimemillismicros.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();

            //[揭示]//0:一般;1:試算揭示
            if (nSimulate == 0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionstr))
                    {
                        connection.Open();
                        sqlcmd.Connection = connection;
                        sqlcmd.CommandType = CommandType.Text;
                        stockPara.Value = txtTick.Text;
                        datePara.Value = nDate;
                        timePara.Value = lTimehms;
                        ptrPara.Value = nPtr;
                        lTimeMSPara.Value = lTimemillismicros;
                        nBidpara.Value = nBid;
                        nAskpara.Value = nAsk;
                        nClosepara.Value = nClose;
                        nQtypara.Value = nQty;

                        sqlcmd.CommandText = @" INSERT INTO [dbo].[TickData]([stockIdx],[Ptr], [ndate],[lTimehms],[lTimeMS],[nBid],[nAsk],[nClose],[nQty],[Source]) VALUES
                                (@stockIdx, @nptr, @ndate,@lTimehms, @lTimeMS, @nBid, @nAskpara, @nClosepara, @nQtypara, 'Live') ";
                        sqlcmd.ExecuteNonQuery();

                        TickRunning = true;
                    }
                }
                catch(Exception ex)
                {}
                finally
                {
                    listTicks.Items.Add("[OnNotifyTicks]" + strData);
                }
            }
        }

        void m_SKQuoteLib_OnNotifyHistoryTicks(short sMarketNo, short sStockIdx, int nPtr, int nDate, int lTimehms, int lTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate)
        {
            //[-0219-add Qty-]
            string strData = "";

            strData = sStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + lTimehms.ToString() + " " + lTimemillismicros.ToString() + "," + nBid.ToString() + "," + nAsk.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
                        
            //[揭示]//0:一般;1:試算揭示
            if (nSimulate == 0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionstr))
                    {
                        connection.Open();

                        sqlcmd.Connection = connection;
                        sqlcmd.CommandType = CommandType.Text;

                        stockPara.Value = txtTick.Text;
                        datePara.Value = nDate;
                        timePara.Value = lTimehms;
                        ptrPara.Value = nPtr;
                        lTimeMSPara.Value = lTimemillismicros;
                        nBidpara.Value = nBid;
                        nAskpara.Value = nAsk;
                        nClosepara.Value = nClose;
                        nQtypara.Value = nQty;

                        sqlcmd.CommandText = @" INSERT INTO [dbo].[TickData]([stockIdx],[Ptr], [ndate],[lTimehms],[lTimeMS],[nBid],[nAsk],[nClose],[nQty],[Source]) VALUES
                                (@stockIdx, @nptr, @ndate,@lTimehms, @lTimeMS, @nBid, @nAskpara, @nClosepara, @nQtypara, 'History') ";
                        sqlcmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            //if (listTicks.Items.Count < 200)
            //    listTicks.SelectedIndex = listTicks.Items.Count - 1;
            //else
            //    listTicks.Items.Clear();
        }

        void m_SKQuoteLib_OnNotifyBest5(short sMarketNo, short sStockIdx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nExtendBid, int nExtendBidQty, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5, int nExtendAsk, int nExtendAskQty, int nSimulate)
        {
            //0:一般;1:試算揭示
            if (nSimulate == 0)
            {
                GridBest5Ask.ForeColor = Color.Black;
                GridBest5Bid.ForeColor = Color.Black;
            }
            else
            {
                GridBest5Ask.ForeColor = Color.Gray;
                GridBest5Bid.ForeColor = Color.Gray;
            }

            SKSTOCK pSKStock = new SKSTOCK();
            double dDigitNum = 0.000;
            string strStockNoTick = txtTick.Text.Trim();
            int nCode = m_SKQuoteLib.SKQuoteLib_GetStockByNo(strStockNoTick, ref pSKStock);
            //[-1022-a-]
            if (nCode == 0)
                dDigitNum = (Math.Pow(10, pSKStock.sDecimal));
            else
                dDigitNum = 100.00;//default value

            if (m_dtBest5Ask.Rows.Count == 0 && m_dtBest5Bid.Rows.Count == 0)
            {
                DataRow myDataRow;

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty1;
                myDataRow["m_nAsk"] = nBestAsk1 / dDigitNum;///100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty2;
                myDataRow["m_nAsk"] = nBestAsk2 / dDigitNum;//100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty3;
                myDataRow["m_nAsk"] = nBestAsk3 / dDigitNum;//100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty4;
                myDataRow["m_nAsk"] = nBestAsk4 / dDigitNum;// 100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty5;
                myDataRow["m_nAsk"] = nBestAsk5 / dDigitNum;// 100.00;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty1;
                myDataRow["m_nAsk"] = nBestBid1 / dDigitNum;
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty2;
                myDataRow["m_nAsk"] = nBestBid2 / dDigitNum;
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty3;
                myDataRow["m_nAsk"] = nBestBid3 / dDigitNum;
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty4;
                myDataRow["m_nAsk"] = nBestBid4 / dDigitNum;
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty5;
                myDataRow["m_nAsk"] = nBestBid5 / dDigitNum;
                m_dtBest5Bid.Rows.Add(myDataRow);

            }
            else
            {
                m_dtBest5Ask.Rows[0]["m_nAskQty"] = nBestAskQty1;
                m_dtBest5Ask.Rows[0]["m_nAsk"] = nBestAsk1 / dDigitNum;

                m_dtBest5Ask.Rows[1]["m_nAskQty"] = nBestAskQty2;
                m_dtBest5Ask.Rows[1]["m_nAsk"] = nBestAsk2 / dDigitNum;

                m_dtBest5Ask.Rows[2]["m_nAskQty"] = nBestAskQty3;
                m_dtBest5Ask.Rows[2]["m_nAsk"] = nBestAsk3 / dDigitNum;

                m_dtBest5Ask.Rows[3]["m_nAskQty"] = nBestAskQty4;
                m_dtBest5Ask.Rows[3]["m_nAsk"] = nBestAsk4 / dDigitNum;

                m_dtBest5Ask.Rows[4]["m_nAskQty"] = nBestAskQty5;
                m_dtBest5Ask.Rows[4]["m_nAsk"] = nBestAsk5 / dDigitNum;

                m_dtBest5Bid.Rows[0]["m_nAskQty"] = nBestBidQty1;
                m_dtBest5Bid.Rows[0]["m_nAsk"] = nBestBid1 / dDigitNum;

                m_dtBest5Bid.Rows[1]["m_nAskQty"] = nBestBidQty2;
                m_dtBest5Bid.Rows[1]["m_nAsk"] = nBestBid2 / dDigitNum;

                m_dtBest5Bid.Rows[2]["m_nAskQty"] = nBestBidQty3;
                m_dtBest5Bid.Rows[2]["m_nAsk"] = nBestBid3 / dDigitNum;

                m_dtBest5Bid.Rows[3]["m_nAskQty"] = nBestBidQty4;
                m_dtBest5Bid.Rows[3]["m_nAsk"] = nBestBid4 / dDigitNum;

                m_dtBest5Bid.Rows[4]["m_nAskQty"] = nBestBidQty5;
                m_dtBest5Bid.Rows[4]["m_nAsk"] = nBestBid5 / dDigitNum;
            }
        }

        void m_SKQuoteLib_OnNotifyServerTime(short sHour, short sMinute, short sSecond, int nTotal)
        {
            lblServerTime.Text = sHour.ToString("D2") + ":" + sMinute.ToString("D2") + ":" + sSecond.ToString("D2");
        }

        void m_SKQuoteLib_OnNotifyKLineData(string bstrStockNo, string bstrData)
        {
            listKLine.Items.Add("[OnNotifyKLineData]" + bstrData);

            string KLineTpye;
            KLineTpye = boxKLine.SelectedIndex.ToString();

            using (SqlConnection connection = new SqlConnection(connectionstr))
            {
                SqlCommand sqlcmd = new SqlCommand();
                SqlParameter stockPara = new SqlParameter();
                SqlParameter datePara = new SqlParameter();
                SqlParameter timePara = new SqlParameter();
                SqlParameter openPara = new SqlParameter();
                SqlParameter highestPara = new SqlParameter();
                SqlParameter lowestPara = new SqlParameter();
                SqlParameter closePara = new SqlParameter();
                SqlParameter volPara = new SqlParameter();

                connection.Open();
               
                sqlcmd.Connection = connection;
                sqlcmd.CommandType = CommandType.Text;

                stockPara = sqlcmd.Parameters.Add("@stockno", SqlDbType.VarChar, 16);
                datePara = sqlcmd.Parameters.Add("@sdate", SqlDbType.VarChar, 10);
                openPara = sqlcmd.Parameters.Add("@open", SqlDbType.VarChar, 8);
                highestPara = sqlcmd.Parameters.Add("@highest", SqlDbType.VarChar, 8);
                lowestPara = sqlcmd.Parameters.Add("@lowest", SqlDbType.VarChar, 8);
                closePara = sqlcmd.Parameters.Add("@close", SqlDbType.VarChar, 8);
                volPara = sqlcmd.Parameters.Add("@vol", SqlDbType.VarChar, 8);

                stockPara.Direction = ParameterDirection.Input;
                datePara.Direction = ParameterDirection.Input;
                openPara.Direction = ParameterDirection.Input;
                highestPara.Direction = ParameterDirection.Input;
                lowestPara.Direction = ParameterDirection.Input;
                closePara.Direction = ParameterDirection.Input;
                volPara.Direction = ParameterDirection.Input;

                string[] sdata=null;
                if (KLineTpye == "0")
                {
                    //typelength = 6;
                    timePara = sqlcmd.Parameters.Add("@stime", SqlDbType.VarChar, 6);
                    timePara.Direction = ParameterDirection.Input;
                    sdata = bstrData.Split(new Char[] { ',' });

                    stockPara.Value = bstrStockNo;
                    datePara.Value = sdata[0];
                    timePara.Value = sdata[1];
                    openPara.Value = sdata[2];
                    highestPara.Value = sdata[3];
                    lowestPara.Value = sdata[4];
                    closePara.Value = sdata[5];
                    volPara.Value = sdata[6];
                    sqlcmd.CommandText = @" IF NOT EXISTS (SELECT 1 FROM [dbo].StockHisotryMin WHERE stockNo=@stockno AND sdate=@sdate AND stime=@stime)  
                                    BEGIN  INSERT INTO [dbo].[StockHisotryMin] ([stockNo],[sdate],[stime],[open],[highest],[lowest], [Close],[vol] ) VALUES
                                    (@stockno, @sdate,@stime, @open, @highest, @lowest, @close, @vol) END ";

                }
                else if(KLineTpye == "4")
                {
                    sdata = bstrData.Split(new Char[] { ',' });
                    stockPara.Value = bstrStockNo;
                    datePara.Value = sdata[0];
                    openPara.Value = sdata[1];
                    highestPara.Value = sdata[2];
                    lowestPara.Value = sdata[3];
                    closePara.Value = sdata[4];
                    volPara.Value = sdata[5];
                    sqlcmd.CommandText = @"
                    IF NOT EXISTS (SELECT 1 FROM [dbo].StockHistoryDaily WHERE stockNo=@stockno AND sdate=@sdate) 
                    BEGIN INSERT INTO [dbo].[StockHistoryDaily] ([stockNo],[sdate],[open],[highest],[lowest], [Close],[vol] )
                    VALUES
                    (@stockno, @sdate,@open, @highest, @lowest, @close, @vol) END ";

                }
                sqlcmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Custom Method
        //----------------------------------------------------------------------
        // Custom Method
        //----------------------------------------------------------------------
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession) return;

            System.Reflection.PropertyInfo aProp =
                        typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        private DataTable CreateStocksDataTable()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sStockidx";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sDecimal";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sTypeNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_cMarketNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caStockNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caName";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nOpen";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nHigh";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nLow";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nClose";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTickQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nRef";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nBid";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nBc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nAc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTBc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTAc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nFutureOI";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nYQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nUp";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nDown";
            myDataTable.Columns.Add(myDataColumn);

            myDataTable.PrimaryKey = new DataColumn[] { myDataTable.Columns["m_caStockNo"] };

            return myDataTable;
        }

        private void OnUpDateDataRow(SKSTOCK pStock)
        {

            string strStockNo = pStock.bstrStockNo;

            DataRow drFind = m_dtStocks.Rows.Find(strStockNo);
            if (drFind == null)
            {
                try
                {
                    DataRow myDataRow = m_dtStocks.NewRow();

                    myDataRow["m_sStockidx"] = pStock.sStockIdx;
                    myDataRow["m_sDecimal"] = pStock.sDecimal;
                    myDataRow["m_sTypeNo"] = pStock.sTypeNo;
                    myDataRow["m_cMarketNo"] = pStock.bstrMarketNo;
                    myDataRow["m_caStockNo"] = pStock.bstrStockNo;
                    myDataRow["m_caName"] = pStock.bstrStockName;
                    myDataRow["m_nOpen"] = pStock.nOpen / (Math.Pow(10, pStock.sDecimal));
                    myDataRow["m_nHigh"] = pStock.nHigh / (Math.Pow(10, pStock.sDecimal));
                    myDataRow["m_nLow"] = pStock.nLow / (Math.Pow(10, pStock.sDecimal));
                    myDataRow["m_nClose"] = pStock.nClose / (Math.Pow(10, pStock.sDecimal));
                    myDataRow["m_nTickQty"] = pStock.nTickQty;
                    myDataRow["m_nRef"] = pStock.nRef / (Math.Pow(10, pStock.sDecimal));
                    myDataRow["m_nBid"] = pStock.nBid / (Math.Pow(10, pStock.sDecimal));
                    myDataRow["m_nBc"] = pStock.nBc;
                    myDataRow["m_nAsk"] = pStock.nAsk / (Math.Pow(10, pStock.sDecimal));
                    m_nSimulateStock = pStock.nSimulate;                 //成交價/買價/賣價;揭示
                    myDataRow["m_nAc"] = pStock.nAc;
                    myDataRow["m_nTBc"] = pStock.nTBc;
                    myDataRow["m_nTAc"] = pStock.nTAc;
                    myDataRow["m_nFutureOI"] = pStock.nFutureOI;
                    myDataRow["m_nTQty"] = pStock.nTQty;
                    myDataRow["m_nYQty"] = pStock.nYQty;
                    myDataRow["m_nUp"] = pStock.nUp / (Math.Pow(10, pStock.sDecimal));
                    myDataRow["m_nDown"] = pStock.nDown / (Math.Pow(10, pStock.sDecimal));

                    m_dtStocks.Rows.Add(myDataRow);

                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
            else
            {
                drFind["m_sStockidx"] = pStock.sStockIdx;
                drFind["m_sDecimal"] = pStock.sDecimal;
                drFind["m_sTypeNo"] = pStock.sTypeNo;
                drFind["m_cMarketNo"] = pStock.bstrMarketNo;
                drFind["m_caStockNo"] = pStock.bstrStockNo;
                drFind["m_caName"] = pStock.bstrStockName;
                drFind["m_nOpen"] = pStock.nOpen / (Math.Pow(10, pStock.sDecimal));
                drFind["m_nHigh"] = pStock.nHigh / (Math.Pow(10, pStock.sDecimal));
                drFind["m_nLow"] = pStock.nLow / (Math.Pow(10, pStock.sDecimal));
                drFind["m_nClose"] = pStock.nClose / (Math.Pow(10, pStock.sDecimal));
                drFind["m_nTickQty"] = pStock.nTickQty;
                drFind["m_nRef"] = pStock.nRef / (Math.Pow(10, pStock.sDecimal));
                drFind["m_nBid"] = pStock.nBid / (Math.Pow(10, pStock.sDecimal));
                drFind["m_nBc"] = pStock.nBc;
                drFind["m_nAsk"] = pStock.nAsk / (Math.Pow(10, pStock.sDecimal));
                drFind["m_nAc"] = pStock.nAc;
                drFind["m_nTBc"] = pStock.nTBc;
                drFind["m_nTAc"] = pStock.nTAc;
                drFind["m_nFutureOI"] = pStock.nFutureOI;
                drFind["m_nTQty"] = pStock.nTQty;
                drFind["m_nYQty"] = pStock.nYQty;
                drFind["m_nUp"] = pStock.nUp / (Math.Pow(10, pStock.sDecimal));
                drFind["m_nDown"] = pStock.nDown / (Math.Pow(10, pStock.sDecimal));
                m_nSimulateStock = pStock.nSimulate;                 //成交價/買價/賣價;揭示
            }
        }

        private DataTable CreateBest5AskTable()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nAskQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;

        }

        #endregion

        private void update_Click(object sender, EventArgs e)
        {
            //m_nCode = m_SKQuoteLib.sk

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_MarketTrading");
        }

        private void button4_Click(object sender, EventArgs e)
        {

            m_nCode = m_SKQuoteLib.SKQuoteLib_GetMarketBuySellUpDown();
            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestMarketBuySellUpDown");
        }

        private void lblSignal_Paint(object sender, PaintEventArgs e)
        {
            if (lblSignal.ForeColor == Color.DarkRed)
                btnDisconnect_Click(this, null);     //Nework is broken收到網路已斷線
        }

    
        private void btnKLineAM_Click(object sender, EventArgs e)
        {
            listKLine.Items.Clear();

            short sKLineType = short.Parse(boxKLine.SelectedIndex.ToString());
            short sOutType = short.Parse(boxOutType.SelectedIndex.ToString());
            short sTradeSession = short.Parse(boxTradeSession.SelectedIndex.ToString());

            if (sKLineType < 0)
            {
                MessageBox.Show("請選擇KLine類型");
                return;
            }
            if (sOutType < 0)
            {
                MessageBox.Show("請選擇輸出格式類型");
                return;
            }
            if (sTradeSession < 0)
            {
                MessageBox.Show("請選擇盤別");
                return;
            }

            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestKLineAM(txtKLine.Text.Trim(), sKLineType, sOutType, sTradeSession);

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestKLineAM");
            //boxTradeSession
        }

        private void btnLiveTick_Click(object sender, EventArgs e)
        {
            listTicks.Items.Clear();
            

            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestLiveTick(2, txtTick.Text.Trim());

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_RequestLiveTick");
        }

        private void btnLiveStop_Click(object sender, EventArgs e)
        {
            //listTicks.Items.Clear();
            
            m_nCode = m_SKQuoteLib.SKQuoteLib_RequestLiveTick(50, txtTick.Text.Trim());

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib_CancelLiveTick");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*m_SKQuoteLib2.OnConnection += new _ISKQuoteLibEvents_OnConnectionEventHandler(m_SKQuoteLib2_OnConnection);

            m_nCode = m_SKQuoteLib2.SKQuoteLib_EnterMonitor();

            SendReturnMessage("Quote", m_nCode, "SKQuoteLib2_EnterMonitor");*/
        }


        private void btnIsConnected_Click(object sender, EventArgs e)
        {
            int nConnected = m_SKQuoteLib.SKQuoteLib_IsConnected();

            if (nConnected == 0)
            {
                ConnectedLabel.Text = "False";
                ConnectedLabel.BackColor = Color.Red;
            }
            else if (nConnected == 1)
            {
                ConnectedLabel.Text = "True";
                ConnectedLabel.BackColor = Color.Green;
            }
            else if (nConnected == 2)
            {
                ConnectedLabel.Text = "False";
                ConnectedLabel.BackColor = Color.Yellow;
            }
            else
            {
                ConnectedLabel.Text = "False";
                ConnectedLabel.BackColor = Color.DarkRed;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

 
    }   
}