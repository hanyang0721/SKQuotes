namespace SKCOMTester
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.lblAccount = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listInformation = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.StatusListBox = new System.Windows.Forms.ListBox();
            this.skQuote1 = new SKCOMTester.SKQuote();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInitialize
            // 
            this.btnInitialize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnInitialize.Location = new System.Drawing.Point(222, 24);
            this.btnInitialize.Margin = new System.Windows.Forms.Padding(4);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(85, 61);
            this.btnInitialize.TabIndex = 21;
            this.btnInitialize.Text = "LogIn1";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F);
            this.lblPassword.Location = new System.Drawing.Point(7, 59);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(64, 13);
            this.lblPassword.TabIndex = 20;
            this.lblPassword.Text = "Password：";
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(72, 59);
            this.txtPassWord.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(132, 20);
            this.txtPassWord.TabIndex = 19;
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F);
            this.lblAccount.Location = new System.Drawing.Point(7, 31);
            this.lblAccount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(57, 13);
            this.lblAccount.TabIndex = 18;
            this.lblAccount.Text = "Account：";
            // 
            // txtAccount
            // 
            this.txtAccount.Font = new System.Drawing.Font("PMingLiU", 9F);
            this.txtAccount.Location = new System.Drawing.Point(72, 24);
            this.txtAccount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(132, 22);
            this.txtAccount.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnInitialize);
            this.groupBox1.Controls.Add(this.txtAccount);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.lblAccount);
            this.groupBox1.Controls.Add(this.txtPassWord);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 107);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            // 
            // listInformation
            // 
            this.listInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.listInformation.FormattingEnabled = true;
            this.listInformation.HorizontalExtent = 2;
            this.listInformation.HorizontalScrollbar = true;
            this.listInformation.Location = new System.Drawing.Point(3, 3);
            this.listInformation.Name = "listInformation";
            this.listInformation.ScrollAlwaysVisible = true;
            this.listInformation.Size = new System.Drawing.Size(906, 95);
            this.listInformation.TabIndex = 25;
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.listInformation);
            this.tabPage3.Controls.Add(this.skQuote1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(912, 516);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "報價";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 127);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(920, 542);
            this.tabControl1.TabIndex = 23;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StatusListBox
            // 
            this.StatusListBox.FormattingEnabled = true;
            this.StatusListBox.HorizontalExtent = 2;
            this.StatusListBox.Location = new System.Drawing.Point(359, 25);
            this.StatusListBox.Name = "StatusListBox";
            this.StatusListBox.ScrollAlwaysVisible = true;
            this.StatusListBox.Size = new System.Drawing.Size(569, 95);
            this.StatusListBox.TabIndex = 26;
            // 
            // skQuote1
            // 
            this.skQuote1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.skQuote1.Location = new System.Drawing.Point(3, 108);
            this.skQuote1.LoginID = "";
            this.skQuote1.LoginID2 = "";
            this.skQuote1.Name = "skQuote1";
            this.skQuote1.Size = new System.Drawing.Size(906, 405);
            this.skQuote1.SKQuoteLib = null;
            this.skQuote1.SKQuoteLib2 = null;
            this.skQuote1.TabIndex = 0;
            this.skQuote1.TickRunning = false;
            this.skQuote1.GetMessage += new SKCOMTester.SKQuote.MyMessageHandler(this.GetMessage);
            // 
            // timer2
            // 
            this.timer2.Interval = 10000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(949, 673);
            this.Controls.Add(this.StatusListBox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listInformation;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox StatusListBox;
        private System.Windows.Forms.Timer timer2;
    }
}

