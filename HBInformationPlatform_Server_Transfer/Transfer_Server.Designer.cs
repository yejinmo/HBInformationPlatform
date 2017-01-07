namespace HBInformationPlatform_Server_Transfer
{
    partial class Transfer_Server
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
            this.GroupBoxCount = new System.Windows.Forms.GroupBox();
            this.Text_Client_Count = new System.Windows.Forms.TextBox();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.GroupBoxPort = new System.Windows.Forms.GroupBox();
            this.Text_Client_Port = new System.Windows.Forms.TextBox();
            this.GroupBoxIP = new System.Windows.Forms.GroupBox();
            this.TextIP = new System.Windows.Forms.TextBox();
            this.GroupBoxMain = new System.Windows.Forms.GroupBox();
            this.TextMain = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Text_Server_Count = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ListView_Server = new System.Windows.Forms.ListView();
            this.ColumnHeaderServer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeaderUsing = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Text_Server_Port = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.GroupBoxCount.SuspendLayout();
            this.GroupBoxPort.SuspendLayout();
            this.GroupBoxIP.SuspendLayout();
            this.GroupBoxMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxCount
            // 
            this.GroupBoxCount.Controls.Add(this.Text_Client_Count);
            this.GroupBoxCount.Location = new System.Drawing.Point(773, 12);
            this.GroupBoxCount.Name = "GroupBoxCount";
            this.GroupBoxCount.Size = new System.Drawing.Size(105, 43);
            this.GroupBoxCount.TabIndex = 11;
            this.GroupBoxCount.TabStop = false;
            this.GroupBoxCount.Text = "客户端在线数";
            // 
            // Text_Client_Count
            // 
            this.Text_Client_Count.Location = new System.Drawing.Point(0, 20);
            this.Text_Client_Count.Name = "Text_Client_Count";
            this.Text_Client_Count.ReadOnly = true;
            this.Text_Client_Count.Size = new System.Drawing.Size(105, 21);
            this.Text_Client_Count.TabIndex = 0;
            this.Text_Client_Count.Text = "0";
            // 
            // ButtonStop
            // 
            this.ButtonStop.Enabled = false;
            this.ButtonStop.Location = new System.Drawing.Point(662, 12);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(105, 43);
            this.ButtonStop.TabIndex = 10;
            this.ButtonStop.Text = "停止监听";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(551, 12);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(105, 43);
            this.ButtonStart.TabIndex = 9;
            this.ButtonStart.Text = "开始监听";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // GroupBoxPort
            // 
            this.GroupBoxPort.Controls.Add(this.Text_Client_Port);
            this.GroupBoxPort.Location = new System.Drawing.Point(440, 12);
            this.GroupBoxPort.Name = "GroupBoxPort";
            this.GroupBoxPort.Size = new System.Drawing.Size(105, 43);
            this.GroupBoxPort.TabIndex = 8;
            this.GroupBoxPort.TabStop = false;
            this.GroupBoxPort.Text = "客户端监听端口";
            // 
            // Text_Client_Port
            // 
            this.Text_Client_Port.Location = new System.Drawing.Point(0, 20);
            this.Text_Client_Port.Name = "Text_Client_Port";
            this.Text_Client_Port.Size = new System.Drawing.Size(105, 21);
            this.Text_Client_Port.TabIndex = 0;
            this.Text_Client_Port.Text = "3213";
            // 
            // GroupBoxIP
            // 
            this.GroupBoxIP.Controls.Add(this.TextIP);
            this.GroupBoxIP.Location = new System.Drawing.Point(234, 12);
            this.GroupBoxIP.Name = "GroupBoxIP";
            this.GroupBoxIP.Size = new System.Drawing.Size(200, 43);
            this.GroupBoxIP.TabIndex = 7;
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
            // GroupBoxMain
            // 
            this.GroupBoxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxMain.Controls.Add(this.TextMain);
            this.GroupBoxMain.Location = new System.Drawing.Point(234, 61);
            this.GroupBoxMain.Name = "GroupBoxMain";
            this.GroupBoxMain.Size = new System.Drawing.Size(773, 419);
            this.GroupBoxMain.TabIndex = 6;
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
            this.TextMain.Size = new System.Drawing.Size(765, 399);
            this.TextMain.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Text_Server_Count);
            this.groupBox1.Location = new System.Drawing.Point(123, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(105, 43);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务端在线数";
            // 
            // Text_Server_Count
            // 
            this.Text_Server_Count.Location = new System.Drawing.Point(0, 20);
            this.Text_Server_Count.Name = "Text_Server_Count";
            this.Text_Server_Count.ReadOnly = true;
            this.Text_Server_Count.Size = new System.Drawing.Size(105, 21);
            this.Text_Server_Count.TabIndex = 0;
            this.Text_Server_Count.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.ListView_Server);
            this.groupBox2.Location = new System.Drawing.Point(12, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 419);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "服务端状态";
            // 
            // ListView_Server
            // 
            this.ListView_Server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ListView_Server.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeaderServer,
            this.ColumnHeaderUsing});
            this.ListView_Server.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListView_Server.Location = new System.Drawing.Point(0, 14);
            this.ListView_Server.Name = "ListView_Server";
            this.ListView_Server.Size = new System.Drawing.Size(216, 399);
            this.ListView_Server.TabIndex = 0;
            this.ListView_Server.UseCompatibleStateImageBehavior = false;
            this.ListView_Server.View = System.Windows.Forms.View.Details;
            this.ListView_Server.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_Server_ColumnWidthChanging);
            // 
            // ColumnHeaderServer
            // 
            this.ColumnHeaderServer.Text = "服务端标识";
            this.ColumnHeaderServer.Width = 155;
            // 
            // ColumnHeaderUsing
            // 
            this.ColumnHeaderUsing.Text = "负载";
            this.ColumnHeaderUsing.Width = 50;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Text_Server_Port);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(105, 43);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "服务端监听端口";
            // 
            // Text_Server_Port
            // 
            this.Text_Server_Port.Location = new System.Drawing.Point(0, 20);
            this.Text_Server_Port.Name = "Text_Server_Port";
            this.Text_Server_Port.Size = new System.Drawing.Size(105, 21);
            this.Text_Server_Port.TabIndex = 0;
            this.Text_Server_Port.Text = "3214";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(884, 32);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 18);
            this.button1.TabIndex = 15;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Transfer_Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 492);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GroupBoxCount);
            this.Controls.Add(this.ButtonStop);
            this.Controls.Add(this.ButtonStart);
            this.Controls.Add(this.GroupBoxPort);
            this.Controls.Add(this.GroupBoxIP);
            this.Controls.Add(this.GroupBoxMain);
            this.Name = "Transfer_Server";
            this.Text = "Transfer_Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Transfer_Server_FormClosing);
            this.Load += new System.EventHandler(this.Transfer_Server_Load);
            this.GroupBoxCount.ResumeLayout(false);
            this.GroupBoxCount.PerformLayout();
            this.GroupBoxPort.ResumeLayout(false);
            this.GroupBoxPort.PerformLayout();
            this.GroupBoxIP.ResumeLayout(false);
            this.GroupBoxIP.PerformLayout();
            this.GroupBoxMain.ResumeLayout(false);
            this.GroupBoxMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxCount;
        private System.Windows.Forms.TextBox Text_Client_Count;
        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.GroupBox GroupBoxPort;
        private System.Windows.Forms.TextBox Text_Client_Port;
        private System.Windows.Forms.GroupBox GroupBoxIP;
        private System.Windows.Forms.TextBox TextIP;
        private System.Windows.Forms.GroupBox GroupBoxMain;
        private System.Windows.Forms.TextBox TextMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Text_Server_Count;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox Text_Server_Port;
        private System.Windows.Forms.ListView ListView_Server;
        private System.Windows.Forms.ColumnHeader ColumnHeaderServer;
        private System.Windows.Forms.ColumnHeader ColumnHeaderUsing;
        private System.Windows.Forms.Button button1;
    }
}

