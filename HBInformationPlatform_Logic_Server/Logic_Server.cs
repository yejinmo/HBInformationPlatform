using InformationEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBInformationPlatform_Logic_Server
{
    public partial class Logic_Server : Form
    {

        /// <summary>
        /// 是否启用自动部署
        /// </summary>
        bool AutoServer = true;

        #region UI
        
        public Logic_Server()
        {
            InitializeComponent();
        }

        private void Logic_Server_Load(object sender, EventArgs e)
        {
            if (AutoServer)
            {
                try
                {
                    //Visible = false;
                    var transfer_server_IP = Dns.GetHostAddresses("transfer-server.hbinformation.app.yejinmo.com");
                    TextIP.Text = transfer_server_IP[0].ToString();
                }
                catch
                {

                }
            }
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            ServerIP = TextIP.Text;
            port = int.Parse(TextPort.Text);
            try
            {
                client = new TcpClient();
                client.Connect(IPAddress.Parse(ServerIP), port);
                AddTalkMessage("连接成功");
            }
            catch (Exception ex)
            {
                AddTalkMessage("连接失败，原因：" + ex.Message);
                return;
            }
            //获取网络流
            NetworkStream networkStream = client.GetStream();
            //将网络流作为二进制读写对象
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            //SendMessage("Login," + txt_UserName.Text);
            Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
            threadReceive.IsBackground = true;
            threadReceive.Start();
        }

        private delegate void AddTalkMessageDelegate(string message);
        /// <summary>
        /// 在聊天对话框（txt_Message）中追加聊天信息
        /// </summary>
        /// <param name="message"></param>
        private void AddTalkMessage(string message)
        {
            if (txt_Message.InvokeRequired)
            {
                AddTalkMessageDelegate d = new AddTalkMessageDelegate(AddTalkMessage);
                txt_Message.Invoke(d, new object[] { message });
            }
            else
            {
                txt_Message.AppendText(message + Environment.NewLine);
                txt_Message.ScrollToCaret();
            }
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            SendMessage(TextPar1.Text + "," + TextPar2.Text);
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            SendMessage("Login");
        }

        #endregion

        #region Global

        private string ServerIP; //IP
        private int port;   //端口
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;

        class MissionInfo
        {
            public string MissionID = string.Empty;
            public string MissionData = string.Empty;
        }

        #endregion

        #region Listener

        /// <summary>
        /// 向服务端发送消息
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(string message)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                bw.Write(message);
                bw.Flush();
            }
            catch
            {
                AddTalkMessage("发送失败");
            }
        }

        /// <summary>
        /// 处理服务器信息
        /// </summary>
        private void ReceiveData()
        {
            string receiveString = null;
            while (isExit == false)
            {
                try
                {
                    receiveString = br.ReadString();
                    if (string.IsNullOrEmpty(receiveString))
                        continue;
                    //任务类别,任务编号,任务描述
                    string[] splitString = receiveString.Split(',');
                    if (splitString.Length != 3)
                    {
                        AddTalkMessage("收到未知请求 [" + receiveString + "]");
                        continue;
                    }
                    switch (splitString[0])
                    {
                        case "Get_ECardSystem":
                            Thread Thread_Get_ECardSystem = new Thread(Get_ECardSystem);
                            Thread_Get_ECardSystem.Start(new MissionInfo { MissionID = splitString[1], MissionData = splitString[2] });
                            break;
                        case "Get_EducationSystem":
                            Thread Thread_Get_EducationSystem = new Thread(Get_EducationSystem);
                            Thread_Get_EducationSystem.Start(new MissionInfo { MissionID = splitString[1], MissionData = splitString[2] });
                            break;
                        case "Get_LibrarySystem":
                            Thread Thread_Get_LibrarySystem = new Thread(Get_LibrarySystem);
                            Thread_Get_LibrarySystem.Start(new MissionInfo { MissionID = splitString[1], MissionData = splitString[2] });
                            break;
                        case "Get_LibrarySystemBookInfo":
                            Thread Thread_Get_LibrarySystemBookInfo = new Thread(Get_LibrarySystemBookInfo);
                            Thread_Get_LibrarySystemBookInfo.Start(new MissionInfo { MissionID = splitString[1], MissionData = splitString[2] });
                            break;
                        default:
                            AddTalkMessage("收到未知请求 [" + receiveString + "]");
                            break;
                    }
                }
                catch
                {
                    if (isExit == false)
                    {
                        MessageBox.Show("与服务器失去连接");
                    }
                    break;
                }
            }
        }

        #endregion

        #region Logic

        #region 查询一卡通系统信息

        private void Get_ECardSystem(object obj)
        {
            MissionInfo info = (MissionInfo)obj;
            string username = string.Empty;
            string password = string.Empty;

            string data = info.MissionData;
            string res = string.Empty;
            if (string.IsNullOrEmpty(data))
                data = string.Empty;
            Regex reg = new Regex(@"username=(.+?)&password=(.+)");
            MatchCollection mcResul = reg.Matches(data);
            if (mcResul.Count > 0)
            {
                username = mcResul[0].Groups[1].Value;
                password = mcResul[0].Groups[2].Value;
            }
            AddTalkMessage("收到 [" + username + "] 请求一卡通消费记录");
            res = new ECardSystem().Get(username, password);
            SendMessage(info.MissionID + "," + res);
            AddTalkMessage("已向 [" + username + "] 发送一卡通消费记录结果");
        }

        #endregion

        #region 查询教务系统信息

        private void Get_EducationSystem(object obj)
        {
            MissionInfo info = (MissionInfo)obj;
            string username = string.Empty;
            string password = string.Empty;

            string data = info.MissionData;
            string res = string.Empty;
            if (string.IsNullOrEmpty(data))
                data = string.Empty;
            Regex reg = new Regex(@"username=(.+?)&password=(.+)");
            MatchCollection mcResul = reg.Matches(data);
            if (mcResul.Count > 0)
            {
                username = mcResul[0].Groups[1].Value;
                password = mcResul[0].Groups[2].Value;
            }
            AddTalkMessage("收到 [" + username + "] 请求教务系统查询");
            res = new EducationSystem().Get(username, password);
            SendMessage(info.MissionID + "," + res);
            AddTalkMessage("已向 [" + username + "] 发送教务系统查询结果");

        }

        #endregion

        #region 查询图书馆系统信息

        private void Get_LibrarySystem(object obj)
        {
            MissionInfo info = (MissionInfo)obj;
            string data = info.MissionData;
            string username = string.Empty;
            string keyword = string.Empty;
            string page = string.Empty;
            string res = string.Empty;
            if (string.IsNullOrEmpty(data))
                data = string.Empty;
            Regex reg = new Regex(@"username=(.+?)&keyword=(.+?)&page=(.+)");
            MatchCollection mcResul = reg.Matches(data);
            if (mcResul.Count > 0)
            {
                username = mcResul[0].Groups[1].Value;
                keyword = mcResul[0].Groups[2].Value;
                page = mcResul[0].Groups[3].Value;
            }
            AddTalkMessage("收到 [" + username + "] 请求图书馆信息检索");
            res = new LibrarySystem().Get(username, keyword, page);
            SendMessage(info.MissionID + "," + res);
            AddTalkMessage("已向 [" + username + "] 发送图书馆信息检索结果");
        }

        #endregion

        #region 查询图书馆系统详细信息
        private void Get_LibrarySystemBookInfo(object obj)
        {
            MissionInfo info = (MissionInfo)obj;
            string data = info.MissionData;
            string username = string.Empty;
            string url = string.Empty;
            string res = string.Empty;
            if (string.IsNullOrEmpty(data))
                data = string.Empty;
            Regex reg = new Regex(@"username=(.+?)&url=(.+)");
            MatchCollection mcResul = reg.Matches(data);
            if (mcResul.Count > 0)
            {
                username = mcResul[0].Groups[1].Value;
                url = mcResul[0].Groups[2].Value;
            }
            AddTalkMessage("收到 [" + username + "] 请求图书馆详细信息检索");
            res = new LibrarySystemBookInfo().Get(username, url);
            SendMessage(info.MissionID + "," + res);
            AddTalkMessage("已向 [" + username + "] 发送图书馆详细信息检索结果");
        }

        #endregion

        #endregion

        private void txt_Message_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
