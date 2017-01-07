using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace HBInformationPlatform_Client
{
    public partial class Client : Form
    {
        private string ServerIP; //IP
        private int port;   //端口
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        public Client()
        {
            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            ServerIP = TextIP.Text;
            port = int.Parse(TextPort.Text);
            try
            {
                //此处为方便演示，实际使用时要将Dns.GetHostName()改为服务器域名
                //IPAddress ipAd = IPAddress.Parse("182.150.193.7");
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
                    //从网络流中读出字符串
                    //此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串
                    receiveString = br.ReadString();
                }
                catch
                {
                    if (isExit == false)
                    {
                        MessageBox.Show("与服务器失去连接");
                    }
                    break;
                }
                AddTalkMessage(receiveString);
            }
            //Application.Exit();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            SendMessage(TextPar1.Text + "," + TextPar2.Text);
        }
    }
}
