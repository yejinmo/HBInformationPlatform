namespace HBInformationPlatform_Client
{
    partial class Client
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.GroupBoxPort = new System.Windows.Forms.GroupBox();
            this.TextPort = new System.Windows.Forms.TextBox();
            this.GroupBoxIP = new System.Windows.Forms.GroupBox();
            this.TextIP = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextPar2 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TextPar1 = new System.Windows.Forms.TextBox();
            this.GroupBoxMain = new System.Windows.Forms.GroupBox();
            this.txt_Message = new System.Windows.Forms.TextBox();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.GroupBoxPort.SuspendLayout();
            this.GroupBoxIP.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.GroupBoxMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxPort
            // 
            this.GroupBoxPort.Controls.Add(this.TextPort);
            this.GroupBoxPort.Location = new System.Drawing.Point(218, 12);
            this.GroupBoxPort.Name = "GroupBoxPort";
            this.GroupBoxPort.Size = new System.Drawing.Size(105, 43);
            this.GroupBoxPort.TabIndex = 4;
            this.GroupBoxPort.TabStop = false;
            this.GroupBoxPort.Text = "服务器端口";
            // 
            // TextPort
            // 
            this.TextPort.Location = new System.Drawing.Point(0, 20);
            this.TextPort.Name = "TextPort";
            this.TextPort.Size = new System.Drawing.Size(105, 21);
            this.TextPort.TabIndex = 0;
            this.TextPort.Text = "3213";
            // 
            // GroupBoxIP
            // 
            this.GroupBoxIP.Controls.Add(this.TextIP);
            this.GroupBoxIP.Location = new System.Drawing.Point(12, 12);
            this.GroupBoxIP.Name = "GroupBoxIP";
            this.GroupBoxIP.Size = new System.Drawing.Size(200, 43);
            this.GroupBoxIP.TabIndex = 3;
            this.GroupBoxIP.TabStop = false;
            this.GroupBoxIP.Text = "服务器地址";
            // 
            // TextIP
            // 
            this.TextIP.Location = new System.Drawing.Point(0, 20);
            this.TextIP.Name = "TextIP";
            this.TextIP.Size = new System.Drawing.Size(200, 21);
            this.TextIP.TabIndex = 0;
            this.TextIP.Text = "127.0.0.1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextPar2);
            this.groupBox1.Location = new System.Drawing.Point(12, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(783, 43);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数二";
            // 
            // TextPar2
            // 
            this.TextPar2.Location = new System.Drawing.Point(0, 20);
            this.TextPar2.Name = "TextPar2";
            this.TextPar2.Size = new System.Drawing.Size(783, 21);
            this.TextPar2.TabIndex = 0;
            this.TextPar2.Text = "username=&password=";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TextPar1);
            this.groupBox2.Location = new System.Drawing.Point(12, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(783, 43);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数一";
            // 
            // TextPar1
            // 
            this.TextPar1.Location = new System.Drawing.Point(0, 20);
            this.TextPar1.Name = "TextPar1";
            this.TextPar1.Size = new System.Drawing.Size(783, 21);
            this.TextPar1.TabIndex = 0;
            this.TextPar1.Text = "Get_ECardSystem";
            // 
            // GroupBoxMain
            // 
            this.GroupBoxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxMain.Controls.Add(this.txt_Message);
            this.GroupBoxMain.Location = new System.Drawing.Point(12, 157);
            this.GroupBoxMain.Name = "GroupBoxMain";
            this.GroupBoxMain.Size = new System.Drawing.Size(783, 338);
            this.GroupBoxMain.TabIndex = 7;
            this.GroupBoxMain.TabStop = false;
            this.GroupBoxMain.Text = "服务器返回值";
            // 
            // txt_Message
            // 
            this.txt_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Message.Location = new System.Drawing.Point(1, 20);
            this.txt_Message.Multiline = true;
            this.txt_Message.Name = "txt_Message";
            this.txt_Message.ReadOnly = true;
            this.txt_Message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Message.Size = new System.Drawing.Size(782, 318);
            this.txt_Message.TabIndex = 0;
            // 
            // ButtonStop
            // 
            this.ButtonStop.Location = new System.Drawing.Point(440, 12);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(105, 43);
            this.ButtonStop.TabIndex = 9;
            this.ButtonStop.Text = "发送请求";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(329, 12);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(105, 43);
            this.ButtonStart.TabIndex = 8;
            this.ButtonStart.Text = "连接服务器";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 507);
            this.Controls.Add(this.ButtonStop);
            this.Controls.Add(this.ButtonStart);
            this.Controls.Add(this.GroupBoxMain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.GroupBoxPort);
            this.Controls.Add(this.GroupBoxIP);
            this.Name = "Client";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Client_Load);
            this.GroupBoxPort.ResumeLayout(false);
            this.GroupBoxPort.PerformLayout();
            this.GroupBoxIP.ResumeLayout(false);
            this.GroupBoxIP.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.GroupBoxMain.ResumeLayout(false);
            this.GroupBoxMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxPort;
        private System.Windows.Forms.TextBox TextPort;
        private System.Windows.Forms.GroupBox GroupBoxIP;
        private System.Windows.Forms.TextBox TextIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TextPar2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TextPar1;
        private System.Windows.Forms.GroupBox GroupBoxMain;
        private System.Windows.Forms.TextBox txt_Message;
        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.Button ButtonStart;
    }
}

