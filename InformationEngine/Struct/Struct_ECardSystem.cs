using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace InformationEngine.Struct
{
    [Serializable]
    public class Struct_ECardSystem : Struct_Root
    {
        public Data_Info Data = new Data_Info();

        public class Data_Info
        {
            public List<Records_Info> Records = new List<Records_Info>();
        }

        public class Records_Info
        {
            /// <summary>
            /// 消费时间
            /// </summary>
            public string Date;
            /// <summary>
            /// 余额
            /// </summary>
            public string Balance;
            /// <summary>
            /// 交易金额
            /// </summary>
            public string Transaction_Amount;
            /// <summary>
            /// 操作终端
            /// </summary>
            public string Terminal;
            /// <summary>
            /// 操作工作站
            /// </summary>
            public string Station;
        }

        public static string Serialiaze(Struct_ECardSystem obj)
        {
            return (new JavaScriptSerializer().Serialize(obj));
        }

        public static Struct_ECardSystem Deserialize(string json)
        {
            return new JavaScriptSerializer().Deserialize<Struct_ECardSystem>(json);
        }

    }
}
