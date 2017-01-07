using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace InformationEngine.Struct
{
    public class Struct_LibrarySystemBookInfo : Struct_Root
    {

        public Data_Info Data = new Data_Info();

        public class Data_Info
        {
            /// <summary>
            /// 书名
            /// </summary>
            public string Name;
            /// <summary>
            /// 出版社
            /// </summary>
            public string Publisher;
            /// <summary>
            /// 作者
            /// </summary>
            public string Author;
            /// <summary>
            /// ISBN
            /// </summary>
            public string ISBN;
            /// <summary>
            /// 价格
            /// </summary>
            public string Price;
            /// <summary>
            /// 页码
            /// </summary>
            public string Pages;
            /// <summary>
            /// 文摘附注
            /// </summary>
            public string Remark;
            /// <summary>
            /// 借阅次数
            /// </summary>
            public string Borrow_Count;
            /// <summary>
            /// 浏览次数
            /// </summary>
            public string Browse_Count;
            /// <summary>
            /// 索引列表
            /// </summary>
            public List<BorrowList_Info> BorrowList = new List<BorrowList_Info>();
        }

        public class BorrowList_Info
        {
            /// <summary>
            /// 索书号
            /// </summary>
            public string Location;
            /// <summary>
            /// 条码号
            /// </summary>
            public string BarCode;
            /// <summary>
            /// 馆藏地
            /// </summary>
            public string Building;
            /// <summary>
            /// 书刊状态
            /// </summary>
            public string State;
        }

        public static string Serialiaze(Struct_LibrarySystemBookInfo obj)
        {
            return (new JavaScriptSerializer().Serialize(obj));
        }

        public static Struct_LibrarySystemBookInfo Deserialize(string json)
        {
            return new JavaScriptSerializer().Deserialize<Struct_LibrarySystemBookInfo>(json);
        }


    }
}
