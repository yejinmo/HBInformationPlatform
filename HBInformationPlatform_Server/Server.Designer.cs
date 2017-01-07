namespace HBInformationPlatform_Server
{
    partial class Server
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
            this.GroupBoxMain = new System.Windows.Forms.GroupBox();
            this.TextMain = new System.Windows.Forms.TextBox();
            this.GroupBoxIP = new System.Windows.Forms.GroupBox();
            this.TextIP = new System.Windows.Forms.TextBox();
            this.GroupBoxPort = new System.Windows.Forms.GroupBox();
            this.TextPort = new System.Windows.Forms.TextBox();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.GroupBoxCount = new System.Windows.Forms.GroupBox();
            this.TextCount = new System.Windows.Forms.TextBox();
            this.GroupBoxMain.SuspendLayout();
            this.GroupBoxIP.SuspendLayout();
            this.GroupBoxPort.SuspendLayout();
            this.GroupBoxCount.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxMain
            // 
            this.GroupBoxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxMain.Controls.Add(this.TextMain);
            this.GroupBoxMain.Location = new System.Drawing.Point(12, 60);
            this.GroupBoxMain.Name = "GroupBoxMain";
            this.GroupBoxMain.Size = new System.Drawing.Size(898, 451);
            this.GroupBoxMain.TabIndex = 0;
            this.GroupBoxMain.TabStop = false;
            this.GroupBoxMain.Text = "状态信息";
            // 
            // TextMain
            // 
            this.TextMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextMain.Location = new System.Drawing.Point(2, 14);
            this.TextMain.Multiline = true;
            this.TextMain.Name = "TextMain";
            this.TextMain.ReadOnly = true;
            this.TextMain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextMain.Size = new System.Drawing.Size(897, 431);
            this.TextMain.TabIndex = 0;
            // 
            // GroupBoxIP
            // 
            this.GroupBoxIP.Controls.Add(this.TextIP);
            this.GroupBoxIP.Location = new System.Drawing.Point(13, 13);
            this.GroupBoxIP.Name = "GroupBoxIP";
            this.GroupBoxIP.Size = new System.Drawing.Size(200, 43);
            this.GroupBoxIP.TabIndex = 1;
            this.GroupBoxIP.TabStop = false;
            this.GroupBoxIP.Text = "监听地址";
            // 
            // TextIP
            // 
            this.TextIP.Location = new System.Drawing.Point(0, 20);
            this.TextIP.Name = "TextIP";
            this.TextIP.Size = new System.Drawing.Size(200, 21);
            this.TextIP.TabIndex = 0;
            this.TextIP.Text = "127.0.0.1";
            // 
            // GroupBoxPort
            // 
            this.GroupBoxPort.Controls.Add(this.TextPort);
            this.GroupBoxPort.Location = new System.Drawing.Point(219, 13);
            this.GroupBoxPort.Name = "GroupBoxPort";
            this.GroupBoxPort.Size = new System.Drawing.Size(105, 43);
            this.GroupBoxPort.TabIndex = 2;
            this.GroupBoxPort.TabStop = false;
            this.GroupBoxPort.Text = "监听端口";
            // 
            // TextPort
            // 
            this.TextPort.Location = new System.Drawing.Point(0, 20);
            this.TextPort.Name = "TextPort";
            this.TextPort.Size = new System.Drawing.Size(105, 21);
            this.TextPort.TabIndex = 0;
            this.TextPort.Text = "3213";
            // 
            // ButtonStart
            // 
            this.ButtonStart.Enabled = false;
            this.ButtonStart.Location = new System.Drawing.Point(330, 11);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(105, 43);
            this.ButtonStart.TabIndex = 3;
            this.ButtonStart.Text = "开始监听";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // ButtonStop
            // 
            this.ButtonStop.Enabled = false;
            this.ButtonStop.Location = new System.Drawing.Point(441, 11);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(105, 43);
            this.ButtonStop.TabIndex = 4;
            this.ButtonStop.Text = "停止监听";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // GroupBoxCount
            // 
            this.GroupBoxCount.Controls.Add(this.TextCount);
            this.GroupBoxCount.Location = new System.Drawing.Point(552, 11);
            this.GroupBoxCount.Name = "GroupBoxCount";
            this.GroupBoxCount.Size = new System.Drawing.Size(105, 43);
            this.GroupBoxCount.TabIndex = 5;
            this.GroupBoxCount.TabStop = false;
            this.GroupBoxCount.Text = "当前连接数";
            // 
            // TextCount
            // 
            this.TextCount.Location = new System.Drawing.Point(0, 20);
            this.TextCount.Name = "TextCount";
            this.TextCount.ReadOnly = true;
            this.TextCount.Size = new System.Drawing.Size(105, 21);
            this.TextCount.TabIndex = 0;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 523);
            this.Controls.Add(this.GroupBoxCount);
            this.Controls.Add(this.ButtonStop);
            this.Controls.Add(this.ButtonStart);
            this.Controls.Add(this.GroupBoxPort);
            this.Controls.Add(this.GroupBoxIP);
            this.Controls.Add(this.GroupBoxMain);
            this.Name = "Server";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Server_FormClosing);
            this.Load += new System.EventHandler(this.Server_Load);
            this.GroupBoxMain.ResumeLayout(false);
            this.GroupBoxMain.PerformLayout();
            this.GroupBoxIP.ResumeLayout(false);
            this.GroupBoxIP.PerformLayout();
            this.GroupBoxPort.ResumeLayout(false);
            this.GroupBoxPort.PerformLayout();
            this.GroupBoxCount.ResumeLayout(false);
            this.GroupBoxCount.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxMain;
        private System.Windows.Forms.GroupBox GroupBoxIP;
        private System.Windows.Forms.TextBox TextIP;
        private System.Windows.Forms.GroupBox GroupBoxPort;
        private System.Windows.Forms.TextBox TextPort;
        private System.Windows.Forms.TextBox TextMain;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.GroupBox GroupBoxCount;
        private System.Windows.Forms.TextBox TextCount;
    }
}

