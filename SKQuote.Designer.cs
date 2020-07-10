namespace SKCOMTester
{
    partial class SKQuote
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnServerTime = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblServerTime = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSignal = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnIsConnected = new System.Windows.Forms.Button();
            this.ConnectedLabel = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.searchtype2 = new System.Windows.Forms.ComboBox();
            this.boxTradeSession = new System.Windows.Forms.ComboBox();
            this.boxOutType = new System.Windows.Forms.ComboBox();
            this.listKLine = new System.Windows.Forms.ListBox();
            this.btnKLine = new System.Windows.Forms.Button();
            this.boxKLine = new System.Windows.Forms.ComboBox();
            this.txtKLine = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnLiveStop = new System.Windows.Forms.Button();
            this.btnLiveTick = new System.Windows.Forms.Button();
            this.GridBest5Bid = new System.Windows.Forms.DataGridView();
            this.GridBest5Ask = new System.Windows.Forms.DataGridView();
            this.btnTickStop = new System.Windows.Forms.Button();
            this.listTicks = new System.Windows.Forms.ListBox();
            this.btnTicks = new System.Windows.Forms.Button();
            this.txtTick = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.searchtype = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UserPagetxt = new System.Windows.Forms.TextBox();
            this.gridStocks = new System.Windows.Forms.DataGridView();
            this.txtPageNo = new System.Windows.Forms.TextBox();
            this.txtStocks = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnQueryStocks = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridBest5Bid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridBest5Ask)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStocks)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(46, 31);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(96, 25);
            this.btnDisconnect.TabIndex = 46;
            this.btnDisconnect.Text = "SolaceDisconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnServerTime
            // 
            this.btnServerTime.Location = new System.Drawing.Point(488, 23);
            this.btnServerTime.Name = "btnServerTime";
            this.btnServerTime.Size = new System.Drawing.Size(87, 38);
            this.btnServerTime.TabIndex = 45;
            this.btnServerTime.Text = "ServerTime";
            this.btnServerTime.UseVisualStyleBackColor = true;
            this.btnServerTime.Click += new System.EventHandler(this.btnServerTime_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblServerTime);
            this.groupBox3.Location = new System.Drawing.Point(605, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(135, 53);
            this.groupBox3.TabIndex = 44;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Server Time";
            // 
            // lblServerTime
            // 
            this.lblServerTime.AutoSize = true;
            this.lblServerTime.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblServerTime.Location = new System.Drawing.Point(18, 18);
            this.lblServerTime.Name = "lblServerTime";
            this.lblServerTime.Size = new System.Drawing.Size(83, 19);
            this.lblServerTime.TabIndex = 0;
            this.lblServerTime.Text = "--：--：--";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSignal);
            this.groupBox1.Location = new System.Drawing.Point(170, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(126, 59);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "solace_QuoteServer";
            // 
            // lblSignal
            // 
            this.lblSignal.AutoSize = true;
            this.lblSignal.Font = new System.Drawing.Font("PMingLiU", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSignal.ForeColor = System.Drawing.Color.Red;
            this.lblSignal.Location = new System.Drawing.Point(15, 20);
            this.lblSignal.Name = "lblSignal";
            this.lblSignal.Size = new System.Drawing.Size(32, 22);
            this.lblSignal.TabIndex = 0;
            this.lblSignal.Text = "●";
            this.lblSignal.Paint += new System.Windows.Forms.PaintEventHandler(this.LblSignal_Paint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(46, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 25);
            this.button1.TabIndex = 42;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnIsConnected
            // 
            this.btnIsConnected.Location = new System.Drawing.Point(302, 18);
            this.btnIsConnected.Name = "btnIsConnected";
            this.btnIsConnected.Size = new System.Drawing.Size(75, 31);
            this.btnIsConnected.TabIndex = 48;
            this.btnIsConnected.Text = "IsConnected";
            this.btnIsConnected.UseVisualStyleBackColor = true;
            this.btnIsConnected.Click += new System.EventHandler(this.btnIsConnected_Click);
            // 
            // ConnectedLabel
            // 
            this.ConnectedLabel.AutoSize = true;
            this.ConnectedLabel.Location = new System.Drawing.Point(397, 31);
            this.ConnectedLabel.Name = "ConnectedLabel";
            this.ConnectedLabel.Size = new System.Drawing.Size(13, 13);
            this.ConnectedLabel.TabIndex = 49;
            this.ConnectedLabel.Text = "0";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.searchtype2);
            this.tabPage3.Controls.Add(this.boxTradeSession);
            this.tabPage3.Controls.Add(this.boxOutType);
            this.tabPage3.Controls.Add(this.listKLine);
            this.tabPage3.Controls.Add(this.btnKLine);
            this.tabPage3.Controls.Add(this.boxKLine);
            this.tabPage3.Controls.Add(this.txtKLine);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(891, 430);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "KLine";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // searchtype2
            // 
            this.searchtype2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchtype2.FormattingEnabled = true;
            this.searchtype2.Location = new System.Drawing.Point(133, 16);
            this.searchtype2.Name = "searchtype2";
            this.searchtype2.Size = new System.Drawing.Size(136, 23);
            this.searchtype2.TabIndex = 11;
            // 
            // boxTradeSession
            // 
            this.boxTradeSession.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxTradeSession.FormattingEnabled = true;
            this.boxTradeSession.Items.AddRange(new object[] {
            "0 = 全盤K線(國內期選用)",
            "1 = AM盤K線(國內期選用)"});
            this.boxTradeSession.Location = new System.Drawing.Point(308, 47);
            this.boxTradeSession.Name = "boxTradeSession";
            this.boxTradeSession.Size = new System.Drawing.Size(196, 23);
            this.boxTradeSession.TabIndex = 10;
            // 
            // boxOutType
            // 
            this.boxOutType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxOutType.FormattingEnabled = true;
            this.boxOutType.Items.AddRange(new object[] {
            "0 = 舊版輸出格式",
            "1 = 新版輸出格式"});
            this.boxOutType.Location = new System.Drawing.Point(308, 16);
            this.boxOutType.Name = "boxOutType";
            this.boxOutType.Size = new System.Drawing.Size(196, 23);
            this.boxOutType.TabIndex = 8;
            // 
            // listKLine
            // 
            this.listKLine.Font = new System.Drawing.Font("PMingLiU", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listKLine.FormattingEnabled = true;
            this.listKLine.ItemHeight = 17;
            this.listKLine.Location = new System.Drawing.Point(55, 76);
            this.listKLine.Name = "listKLine";
            this.listKLine.Size = new System.Drawing.Size(768, 174);
            this.listKLine.TabIndex = 7;
            // 
            // btnKLine
            // 
            this.btnKLine.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F);
            this.btnKLine.Location = new System.Drawing.Point(550, 16);
            this.btnKLine.Name = "btnKLine";
            this.btnKLine.Size = new System.Drawing.Size(273, 54);
            this.btnKLine.TabIndex = 6;
            this.btnKLine.Text = "Query (SKQuoteLib_RequestKLineAM)";
            this.btnKLine.UseVisualStyleBackColor = true;
            this.btnKLine.Click += new System.EventHandler(this.btnKLine_Click);
            // 
            // boxKLine
            // 
            this.boxKLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxKLine.FormattingEnabled = true;
            this.boxKLine.Items.AddRange(new object[] {
            "0 = 1分鐘線。",
            "需自行組 5分鐘線。)",
            "需自行組 30分鐘線。)",
            "solace未提供日線288天。",
            "4 =完整日線。",
            "5 =週線。",
            "6 =月線。"});
            this.boxKLine.Location = new System.Drawing.Point(133, 47);
            this.boxKLine.Name = "boxKLine";
            this.boxKLine.Size = new System.Drawing.Size(136, 23);
            this.boxKLine.TabIndex = 5;
            // 
            // txtKLine
            // 
            this.txtKLine.Location = new System.Drawing.Point(55, 16);
            this.txtKLine.Name = "txtKLine";
            this.txtKLine.Size = new System.Drawing.Size(63, 25);
            this.txtKLine.TabIndex = 4;
            this.txtKLine.Text = "TX00";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnLiveStop);
            this.tabPage2.Controls.Add(this.btnLiveTick);
            this.tabPage2.Controls.Add(this.GridBest5Bid);
            this.tabPage2.Controls.Add(this.GridBest5Ask);
            this.tabPage2.Controls.Add(this.btnTickStop);
            this.tabPage2.Controls.Add(this.listTicks);
            this.tabPage2.Controls.Add(this.btnTicks);
            this.tabPage2.Controls.Add(this.txtTick);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(891, 430);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TICK & BEST5";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnLiveStop
            // 
            this.btnLiveStop.Location = new System.Drawing.Point(275, 44);
            this.btnLiveStop.Name = "btnLiveStop";
            this.btnLiveStop.Size = new System.Drawing.Size(102, 27);
            this.btnLiveStop.TabIndex = 19;
            this.btnLiveStop.Text = "StopLive";
            this.btnLiveStop.UseVisualStyleBackColor = true;
            this.btnLiveStop.Click += new System.EventHandler(this.btnLiveStop_Click);
            // 
            // btnLiveTick
            // 
            this.btnLiveTick.Location = new System.Drawing.Point(144, 42);
            this.btnLiveTick.Name = "btnLiveTick";
            this.btnLiveTick.Size = new System.Drawing.Size(112, 29);
            this.btnLiveTick.TabIndex = 18;
            this.btnLiveTick.Text = "RequestLiveTick";
            this.btnLiveTick.UseVisualStyleBackColor = true;
            this.btnLiveTick.Click += new System.EventHandler(this.BtnLiveTick_Click);
            // 
            // GridBest5Bid
            // 
            this.GridBest5Bid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridBest5Bid.Location = new System.Drawing.Point(58, 95);
            this.GridBest5Bid.MultiSelect = false;
            this.GridBest5Bid.Name = "GridBest5Bid";
            this.GridBest5Bid.RowHeadersVisible = false;
            this.GridBest5Bid.RowTemplate.Height = 24;
            this.GridBest5Bid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.GridBest5Bid.Size = new System.Drawing.Size(131, 163);
            this.GridBest5Bid.TabIndex = 8;
            // 
            // GridBest5Ask
            // 
            this.GridBest5Ask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridBest5Ask.Location = new System.Drawing.Point(246, 95);
            this.GridBest5Ask.MultiSelect = false;
            this.GridBest5Ask.Name = "GridBest5Ask";
            this.GridBest5Ask.RowHeadersVisible = false;
            this.GridBest5Ask.RowTemplate.Height = 24;
            this.GridBest5Ask.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.GridBest5Ask.Size = new System.Drawing.Size(131, 163);
            this.GridBest5Ask.TabIndex = 7;
            // 
            // btnTickStop
            // 
            this.btnTickStop.Location = new System.Drawing.Point(275, 9);
            this.btnTickStop.Name = "btnTickStop";
            this.btnTickStop.Size = new System.Drawing.Size(102, 27);
            this.btnTickStop.TabIndex = 6;
            this.btnTickStop.Text = "Stop";
            this.btnTickStop.UseVisualStyleBackColor = true;
            this.btnTickStop.Click += new System.EventHandler(this.btnTickStop_Click);
            // 
            // listTicks
            // 
            this.listTicks.BackColor = System.Drawing.SystemColors.Window;
            this.listTicks.Font = new System.Drawing.Font("PMingLiU", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listTicks.FormattingEnabled = true;
            this.listTicks.HorizontalExtent = 1000;
            this.listTicks.HorizontalScrollbar = true;
            this.listTicks.ItemHeight = 17;
            this.listTicks.Location = new System.Drawing.Point(467, 11);
            this.listTicks.Name = "listTicks";
            this.listTicks.ScrollAlwaysVisible = true;
            this.listTicks.Size = new System.Drawing.Size(371, 259);
            this.listTicks.TabIndex = 5;
            // 
            // btnTicks
            // 
            this.btnTicks.Location = new System.Drawing.Point(144, 9);
            this.btnTicks.Name = "btnTicks";
            this.btnTicks.Size = new System.Drawing.Size(112, 29);
            this.btnTicks.TabIndex = 4;
            this.btnTicks.Text = "Request Tick";
            this.btnTicks.UseVisualStyleBackColor = true;
            this.btnTicks.Click += new System.EventHandler(this.btnTicks_Click);
            // 
            // txtTick
            // 
            this.txtTick.Location = new System.Drawing.Point(58, 12);
            this.txtTick.Name = "txtTick";
            this.txtTick.Size = new System.Drawing.Size(63, 25);
            this.txtTick.TabIndex = 3;
            this.txtTick.Text = "TX00";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.searchtype);
            this.tabPage1.Controls.Add(this.label26);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.UserPagetxt);
            this.tabPage1.Controls.Add(this.gridStocks);
            this.tabPage1.Controls.Add(this.txtPageNo);
            this.tabPage1.Controls.Add(this.txtStocks);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnQueryStocks);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(891, 371);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Quote";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // searchtype
            // 
            this.searchtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchtype.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.searchtype.FormattingEnabled = true;
            this.searchtype.Location = new System.Drawing.Point(17, 45);
            this.searchtype.Name = "searchtype";
            this.searchtype.Size = new System.Drawing.Size(150, 25);
            this.searchtype.TabIndex = 14;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.label26.Location = new System.Drawing.Point(14, 22);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(64, 17);
            this.label26.TabIndex = 13;
            this.label26.Text = "Search By";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(780, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "SysPage";
            // 
            // UserPagetxt
            // 
            this.UserPagetxt.Location = new System.Drawing.Point(195, 45);
            this.UserPagetxt.Name = "UserPagetxt";
            this.UserPagetxt.Size = new System.Drawing.Size(46, 25);
            this.UserPagetxt.TabIndex = 11;
            this.UserPagetxt.Text = "-1";
            this.UserPagetxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gridStocks
            // 
            this.gridStocks.AllowUserToAddRows = false;
            this.gridStocks.AllowUserToDeleteRows = false;
            this.gridStocks.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("PMingLiU", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridStocks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridStocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStocks.Location = new System.Drawing.Point(17, 89);
            this.gridStocks.Name = "gridStocks";
            this.gridStocks.ReadOnly = true;
            this.gridStocks.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("PMingLiU", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gridStocks.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridStocks.RowTemplate.Height = 24;
            this.gridStocks.Size = new System.Drawing.Size(817, 262);
            this.gridStocks.TabIndex = 10;
            this.gridStocks.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridStocks_CellPainting);
            // 
            // txtPageNo
            // 
            this.txtPageNo.Enabled = false;
            this.txtPageNo.Location = new System.Drawing.Point(783, 41);
            this.txtPageNo.Name = "txtPageNo";
            this.txtPageNo.Size = new System.Drawing.Size(46, 25);
            this.txtPageNo.TabIndex = 9;
            this.txtPageNo.Text = "-1";
            // 
            // txtStocks
            // 
            this.txtStocks.Location = new System.Drawing.Point(278, 44);
            this.txtStocks.Name = "txtStocks";
            this.txtStocks.Size = new System.Drawing.Size(243, 25);
            this.txtStocks.TabIndex = 5;
            this.txtStocks.Text = "TX00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(192, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "YourPage";
            // 
            // btnQueryStocks
            // 
            this.btnQueryStocks.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.btnQueryStocks.Location = new System.Drawing.Point(653, 42);
            this.btnQueryStocks.Name = "btnQueryStocks";
            this.btnQueryStocks.Size = new System.Drawing.Size(75, 25);
            this.btnQueryStocks.TabIndex = 7;
            this.btnQueryStocks.Text = "Query";
            this.btnQueryStocks.UseVisualStyleBackColor = true;
            this.btnQueryStocks.Click += new System.EventHandler(this.btnQueryStocks_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.label2.Location = new System.Drawing.Point(536, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "seperate by  \",\"";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.label3.Location = new System.Drawing.Point(275, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Stock No";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("PMingLiU", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControl1.Location = new System.Drawing.Point(8, 68);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(899, 400);
            this.tabControl1.TabIndex = 47;
            // 
            // SKQuote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ConnectedLabel);
            this.Controls.Add(this.btnIsConnected);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnServerTime);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "SKQuote";
            this.Size = new System.Drawing.Size(907, 466);
            this.Load += new System.EventHandler(this.SKQuote_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridBest5Bid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridBest5Ask)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStocks)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnServerTime;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblServerTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSignal;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnIsConnected;
        private System.Windows.Forms.Label ConnectedLabel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox boxOutType;
        private System.Windows.Forms.ListBox listKLine;
        private System.Windows.Forms.Button btnKLine;
        private System.Windows.Forms.ComboBox boxKLine;
        private System.Windows.Forms.TextBox txtKLine;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnLiveStop;
        private System.Windows.Forms.Button btnLiveTick;
        private System.Windows.Forms.DataGridView GridBest5Bid;
        private System.Windows.Forms.DataGridView GridBest5Ask;
        private System.Windows.Forms.Button btnTickStop;
        private System.Windows.Forms.ListBox listTicks;
        private System.Windows.Forms.Button btnTicks;
        private System.Windows.Forms.TextBox txtTick;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView gridStocks;
        private System.Windows.Forms.TextBox txtPageNo;
        private System.Windows.Forms.TextBox txtStocks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnQueryStocks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UserPagetxt;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox searchtype2;
        private System.Windows.Forms.ComboBox searchtype;
        private System.Windows.Forms.ComboBox boxTradeSession;
    }
}
