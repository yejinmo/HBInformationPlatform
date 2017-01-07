using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace HBInformationPlatform_Server_Transfer
{
    class Server
    {
        public TcpClient client { get; private set; }
        public BinaryReader br { get; private set; }
        public BinaryWriter bw { get; private set; }

        /// <summary>
        /// 是否已关闭
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 是否正常登录
        /// </summary>
        public bool IsLogin { get; set; }

        /// <summary>
        /// 数据区
        /// </summary>
        public string data { get; set; }

        public Server(TcpClient client)
        {
            IsClosed = false;
            IsLocked = false;
            this.client = client;
            NetworkStream networkStream = client.GetStream();
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
        }

        public void Close()
        {
            br.Close();
            bw.Close();
            client.Close();
            IsClosed = true;
        }

        /// <summary>
        /// 负载
        /// </summary>
        public int BusyDegree = 0;
    }
}
