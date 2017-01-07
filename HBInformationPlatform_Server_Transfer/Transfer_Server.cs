using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace HBInformationPlatform_Server_Transfer
{
    public partial class Transfer_Server : Form
    {

        #region UI及初始化
        private void Transfer_Server_Load(object sender, EventArgs e)
        {
            try
            {
                TextIP.Text = Dns.GetHostAddresses("transfer-server.hbinformation.app.yejinmo.com")[0].ToString();
            }
            catch
            {

            }
        }

        public Transfer_Server()
        {
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            ListenerIP = TextIP.Text;
            Server_Port = int.Parse(Text_Server_Port.Text);
            Client_Port = int.Parse(Text_Client_Port.Text);
            StartListen();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            StopListen();
        }

        private delegate void AddContentDelegate(string message);

        /// <summary>
        /// 添加状态信息
        /// </summary>
        /// <param name="str"></param>
        private void AddContent(string str)
        {

            if (TextMain.InvokeRequired)
            {
                AddContentDelegate d = new AddContentDelegate(AddContent);
                TextMain.Invoke(d, new object[] { str });
            }
            else
            {
                string Update_Date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                string Update_Time = string.Format("{0:00}:{1:00}:{2:00}.{3:0000}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);

                TextMain.AppendText("【" + Update_Date + " " + Update_Time + "】    " + str + Environment.NewLine);
                TextMain.ScrollToCaret();
            }

        }

        /// <summary>
        /// UI锁定
        /// </summary>
        /// <param name="Enabled"></param>
        private void ChangeUIEnabled(bool Enabled = true)
        {
            ButtonStart.Enabled = Enabled;
            ButtonStop.Enabled = !Enabled;
            Text_Server_Port.ReadOnly = !Enabled;
            Text_Client_Port.ReadOnly = !Enabled;
            TextIP.ReadOnly = !Enabled;
        }

        private void Transfer_Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ButtonStop.Enabled)
            {
                MessageBox.Show(this, "请先停止监听！");
                e.Cancel = true;
            }
            else
            {
                if (Server_Listener != null || Client_Listener != null)
                    ButtonStop.PerformClick();    //引发 btn_Stop 的Click事件
            }
        }

        private void ListView_Server_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = ListView_Server.Columns[e.ColumnIndex].Width;
        }

        /// <summary>
        /// 刷新服务端状态
        /// </summary>
        private void RefreshServerListView()
        {
            Invoke((EventHandler)delegate
            {
                ListView_Server.BeginUpdate();
                ListView_Server.Items.Clear();
                foreach (Server server in Server_List)
                {
                    ListView_Server.Items.Add(new ListViewItem(new string[] 
                    {
                        server.client.Client.RemoteEndPoint.ToString(), server.BusyDegree.ToString()
                    }));
                }
                ListView_Server.EndUpdate();
            });
        }

        #endregion

        #region 全局变量

        /// <summary>
        /// 监听地址
        /// </summary>
        string ListenerIP;

        /// <summary>
        /// 客户端监听端口
        /// </summary>
        int Client_Port;

        /// <summary>
        /// 服务端监听端口
        /// </summary>
        int Server_Port;

        /// <summary>
        /// 客户端监听器
        /// </summary>
        TcpListener Client_Listener;

        /// <summary>
        /// 服务端监听器
        /// </summary>
        TcpListener Server_Listener;

        /// <summary>
        /// 客户端列表
        /// </summary>
        List<Client> Client_List = new List<Client>();

        /// <summary>
        /// 服务端列表
        /// </summary>
        List<Server> Server_List = new List<Server>();

        Dictionary<string, Client> Mission_List = new Dictionary<string, Client>();

        Dictionary<string, int> Mission_BusyDegree = new Dictionary<string, int>();

        Dictionary<string, Server> Mission_Server = new Dictionary<string, Server>();

        static int Temp_MissionID = 0;

        #endregion

        #region 监听方法

        /// <summary>
        /// 开始监听
        /// </summary>
        private void StartListen()
        {
            try
            {
                Server_Listener = new TcpListener(IPAddress.Parse(ListenerIP), Server_Port);
                Server_Listener.Start();
                Thread Server_Listener_Thread = new Thread(Listen_Server_Connect);
                Server_Listener_Thread.Start();
                AddContent(string.Format("开始监听服务端 [{0}:{1}] ", ListenerIP, Server_Port));

                Client_Listener = new TcpListener(IPAddress.Parse(ListenerIP), Client_Port);
                Client_Listener.Start();
                Thread Client_Listener_Thread = new Thread(Listen_Client_Connect);
                Client_Listener_Thread.Start();
                AddContent(string.Format("开始监听客户端 [{0}:{1}] ", ListenerIP, Client_Port));

                ChangeUIEnabled(false);
            }
            catch(Exception e)
            {
                MessageBox.Show(this, e.Message, "Error");
                ChangeUIEnabled(true);
            }
        }

        /// <summary>
        /// 停止监听
        /// </summary>
        private void StopListen()
        {
            try
            {
                AddContent("正在停止");
                for (int i = Client_List.Count - 1; i >= 0; i--)
                {
                    Remove_Client(Client_List[i]);
                }
                Client_Listener.Stop();
                AddContent(string.Format("停止监听客户端 [{0}:{1}] ", ListenerIP, Client_Port));

                for (int i = Server_List.Count - 1; i >= 0; i--)
                {
                    Remove_Server(Server_List[i]);
                }
                Server_Listener.Stop();
                AddContent(string.Format("停止监听服务端 [{0}:{1}] ", ListenerIP, Server_Port));

                ChangeUIEnabled(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error");
                ChangeUIEnabled(false);
            }

        }

        /// <summary>
        /// 移除客户端
        /// </summary>
        /// <param name="client">指定要移除的客户端</param>
        private void Remove_Client(Client client)
        {
            AddContent("已与客户端 " + client.client.Client.RemoteEndPoint + " 断开连接");
            Client_List.Remove(client);
            client.Close();
            RefreshOnlineClientCount();
        }

        private delegate void RefreshOnlineClientCountDelegate(string message);

        /// <summary>
        /// 刷新当前客户端连接数
        /// </summary>
        private void RefreshOnlineClientCount(string str = "")
        {

            str = Client_List.Count.ToString();

            if (Text_Client_Count.InvokeRequired)
            {
                RefreshOnlineClientCountDelegate d = new RefreshOnlineClientCountDelegate(RefreshOnlineClientCount);
                Text_Client_Count.Invoke(d, new object[] { str });
            }
            else
            {
                Text_Client_Count.Text = str;
            }

        }

        /// <summary>
        /// 移除服务端
        /// </summary>
        /// <param name="server">指定要移除的服务端</param>
        private void Remove_Server(Server server)
        {
            AddContent("已与服务端 " + server.client.Client.RemoteEndPoint + " 断开连接");
            Server_List.Remove(server);
            server.Close();
            RefreshServerListView();
            RefreshOnlineServerCount();
        }

        private delegate void RefreshOnlineServerCountDelegate(string message);

        /// <summary>
        /// 刷新当前服务端连接数
        /// </summary>
        private void RefreshOnlineServerCount(string str = "")
        {

            str = Server_List.Count.ToString();

            if (Text_Server_Count.InvokeRequired)
            {
                RefreshOnlineServerCountDelegate d = new RefreshOnlineServerCountDelegate(RefreshOnlineServerCount);
                Text_Server_Count.Invoke(d, new object[] { str });
            }
            else
            {
                Text_Server_Count.Text = str;
            }

        }

        /// <summary>
        /// 接收客户端连接
        /// </summary>
        private void Listen_Client_Connect()
        {
            TcpClient newClient = null;
            while (true)
            {
                try
                {
                    newClient = Client_Listener.AcceptTcpClient();
                }
                catch
                {
                    //当单击‘停止监听’或者退出此窗体时 AcceptTcpClient() 会产生异常
                    //因此可以利用此异常退出循环
                    break;
                }
                //每接收一个客户端连接，就创建一个对应的线程循环接收该客户端发来的信息；
                Client client = new Client(newClient);
                Thread Client_Receive_Thread = new Thread(Client_Receive_Data);
                Client_Receive_Thread.Start(client);
                Client_List.Add(client);
                AddContent("已与客户端 " + client.client.Client.RemoteEndPoint + " 建立连接");
                RefreshOnlineClientCount();
            }

        }

        /// <summary>
        /// 接收服务端连接
        /// </summary>
        private void Listen_Server_Connect()
        {
            TcpClient newServer = null;
            while (true)
            {
                try
                {
                    newServer = Server_Listener.AcceptTcpClient();
                }
                catch
                {
                    //当单击‘停止监听’或者退出此窗体时 AcceptTcpClient() 会产生异常
                    //因此可以利用此异常退出循环
                    break;
                }
                //每接收一个客户端连接，就创建一个对应的线程循环接收该客户端发来的信息；
                Server server = new Server(newServer);
                Thread Server_Receive_Thread = new Thread(Server_Receive_Data);
                Server_Receive_Thread.Start(server);
                Server_List.Add(server);
                AddContent("已与服务端 " + server.client.Client.RemoteEndPoint + " 建立连接");
                RefreshServerListView();
                RefreshOnlineServerCount();
            }

        }

        /// <summary>
        /// 处理接收的客户端信息
        /// </summary>
        /// <param name="userState">客户端信息</param>
        private void Client_Receive_Data(object obj)
        {
            Client client = (Client)obj;
            while (!client.IsClosed)
            {
                if (client.IsLocked)
                    break;
                try
                {
                    string receiveString = client.br.ReadString();
                    string[] splitString = receiveString.Split(',');
                    client.data = splitString[1];
                    switch (splitString[0])
                    {
                        case "Get_ECardSystem":
                            Thread Thread_Get_ECardSystem = new Thread(Get_ECardSystem);
                            Thread_Get_ECardSystem.Start(client);
                            break;
                        case "Get_EducationSystem":
                            Thread Thread_Get_EducationSystem = new Thread(Get_EducationSystem);
                            Thread_Get_EducationSystem.Start(client);
                            break;
                        case "Get_LibrarySystem":
                            Thread Thread_Get_LibrarySystem = new Thread(Get_LibrarySystem);
                            Thread_Get_LibrarySystem.Start(client);
                            break;
                        case "Get_LibrarySystemBookInfo":
                            Thread Thread_Get_LibrarySystemBookInfo = new Thread(Get_LibrarySystemBookInfo);
                            Thread_Get_LibrarySystemBookInfo.Start(client);
                            break;
                        default:
                            AddContent("收到客户端 [" + client.client.Client.RemoteEndPoint + "] 的未知请求");
                            Remove_Client(client);
                            break;
                    }
                }
                catch
                {
                    if (!client.IsClosed)
                        Remove_Client(client);
                    break;
                }
            }
        }

        /// <summary>
        /// 处理接收的服务端信息
        /// </summary>
        /// <param name="obj"></param>
        private void Server_Receive_Data(object obj)
        {
            Server server = (Server)obj;
            while (!server.IsClosed)
            {
                if (server.IsLocked)
                    break;
                try
                {
                    string receiveString = server.br.ReadString();
                    if (string.IsNullOrEmpty(receiveString))
                        continue;
                    if (receiveString == "Login")
                    {
                        server.IsLogin = true;
                        AddContent("服务端登录 " + server.client.Client.RemoteEndPoint);
                        continue;
                    }

                    Regex reg = new Regex(@"(.+?),(.+)");
                    MatchCollection mcResul = reg.Matches(receiveString);
                    if (mcResul.Count == 0)
                        continue;

                    string mission_id = mcResul[0].Groups[1].Value;
                    string data = mcResul[0].Groups[2].Value;
                    int mission_busyDegree = 0;
                    Client client = null;
                    Server child_server = null;
                    if (!Mission_List.TryGetValue(mission_id, out client))
                        continue;
                    Send_To_Client(client, data);
                    Mission_List.Remove(mission_id);
                    if (!Mission_BusyDegree.TryGetValue(mission_id, out mission_busyDegree))
                        continue;
                    Mission_BusyDegree.Remove(mission_id);
                    if (!Mission_Server.TryGetValue(mission_id, out child_server))
                        continue;
                    Mission_Server.Remove(mission_id);
                    child_server.BusyDegree -= mission_busyDegree;
                    RefreshServerListView();
                }
                catch
                {
                    if (!server.IsClosed)
                        Remove_Server(server);
                    break;
                }
            }
        }

        /// <summary>
        /// 发送 message 给 client
        /// </summary>
        /// <param name="client">指定发给哪个client</param>
        /// <param name="message">信息内容</param>
        private void Send_To_Client(Client client, string message)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                client.bw.Write(message);
                client.bw.Flush();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 发送 message 给 server
        /// </summary>
        /// <param name="server">指定发给哪个server</param>
        /// <param name="message">信息内容</param>
        private void Send_To_Server(Server server, string message)
        {
            try
            {
                server.bw.Write(message);
                server.bw.Flush();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 获取当前负载最小的服务端
        /// </summary>
        /// <returns></returns>
        private Server Get_Min_Busy_Server()
        {
            if (Server_List.Count <= 0)
                return null;
            int Min_Busy = 999999;
            Server res = null;
            foreach (Server server in Server_List)
            {
                if (server.IsLogin && server.BusyDegree < Min_Busy)
                {
                    Min_Busy = server.BusyDegree;
                    res = server;
                }
            }
            return res;
        }

        #endregion

        #region 响应方法

        private void Get_ECardSystem(object obj)
        {
            Client client = (Client)obj;
            Server ser = Get_Min_Busy_Server();
            string MissionID = Get_MissionID();
            AddContent("收到 [" + client.client.Client.RemoteEndPoint + "] 请求一卡通消费记录，已将任务下发给 [" + ser.client.Client.RemoteEndPoint + "] ，任务编号 " + MissionID);
            Send_To_Server(ser, "Get_ECardSystem," + MissionID + "," + client.data);
            ser.BusyDegree += 2;
            Mission_BusyDegree.Add(MissionID, 2);
            Mission_List.Add(MissionID, client);
            Mission_Server.Add(MissionID, ser);
            RefreshServerListView(); 
        }

        private void Get_EducationSystem(object obj)
        {
            Client client = (Client)obj;
            Server ser = Get_Min_Busy_Server();
            string MissionID = Get_MissionID();
            AddContent("收到 [" + client.client.Client.RemoteEndPoint + "] 请求教务系统记录，已将任务下发给 [" + ser.client.Client.RemoteEndPoint + "] ，任务编号 " + MissionID);
            Send_To_Server(ser, "Get_EducationSystem," + MissionID + "," + client.data);
            ser.BusyDegree += 5;
            Mission_BusyDegree.Add(MissionID, 5);
            Mission_List.Add(MissionID, client);
            Mission_Server.Add(MissionID, ser);
            RefreshServerListView();
        }

        private void Get_LibrarySystem(object obj)
        {
            Client client = (Client)obj;
            Server ser = Get_Min_Busy_Server();
            string MissionID = Get_MissionID();
            AddContent("收到 [" + client.client.Client.RemoteEndPoint + "] 请求图书馆查询，已将任务下发给 [" + ser.client.Client.RemoteEndPoint + "] ，任务编号 " + MissionID);
            Send_To_Server(ser, "Get_LibrarySystem," + MissionID + "," + client.data);
            ser.BusyDegree += 1;
            Mission_BusyDegree.Add(MissionID, 1);
            Mission_List.Add(MissionID, client);
            Mission_Server.Add(MissionID, ser);
            RefreshServerListView();
        }
        private void Get_LibrarySystemBookInfo(object obj)
        {
            Client client = (Client)obj;
            Server ser = Get_Min_Busy_Server();
            string MissionID = Get_MissionID();
            AddContent("收到 [" + client.client.Client.RemoteEndPoint + "] 请求图书馆详细信息查询，已将任务下发给 [" + ser.client.Client.RemoteEndPoint + "] ，任务编号 " + MissionID);
            Send_To_Server(ser, "Get_LibrarySystemBookInfo," + MissionID + "," + client.data);
            ser.BusyDegree += 1;
            Mission_BusyDegree.Add(MissionID, 1);
            Mission_List.Add(MissionID, client);
            Mission_Server.Add(MissionID, ser);
            RefreshServerListView();
        }

        /// <summary>
        /// 获取一个任务编号
        /// </summary>
        /// <returns></returns>
        private string Get_MissionID()
        {
            Temp_MissionID++;
            if (Temp_MissionID == 100)
                Temp_MissionID = 0;
            return DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + string.Format("{0:00}{1:00}{2:00}{3:0000}{4:00}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, Temp_MissionID);
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            AddContent("当前负载最小的服务端为 " + Get_Min_Busy_Server().client.Client.RemoteEndPoint);
        }

    }
}
