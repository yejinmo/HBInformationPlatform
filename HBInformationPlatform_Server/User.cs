using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace HBInformationPlatform_Server
{
    class User
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
        /// 数据区
        /// </summary>
        public string data { get; set; }

        public User(TcpClient client)
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

    }
}
