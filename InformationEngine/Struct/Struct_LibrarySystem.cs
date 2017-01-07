using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace InformationEngine.Struct
{
    [Serializable]
    public class Struct_LibrarySystem : Struct_Root
    {

        public Data_Info Data = new Data_Info();

        public class Data_Info
        {
            public List<Book_Info> Book = new List<Book_Info>();
        }

        public class Book_Info
        {
            /// <summary>
            /// 链接
            /// </summary>
            public string URL;
            /// <summary>
            /// 书名
            /// </summary>
            public string Name;
            /// <summary>
            /// 位置
            /// </summary>
            public string Location;
            /// <summary>
            /// 总数
            /// </summary>
            public string Total;
            /// <summary>
            /// 余量
            /// </summary>
            public string Residue_Total;
            /// <summary>
            /// 作者
            /// </summary>
            public string Author;
            /// <summary>
            /// 出版社
            /// </summary>
            public string Publisher;
        }

        public static string Serialiaze(Struct_LibrarySystem obj)
        {
            return (new JavaScriptSerializer().Serialize(obj));
        }

        public static Struct_LibrarySystem Deserialize(string json)
        {
            return new JavaScriptSerializer().Deserialize<Struct_LibrarySystem>(json);
        }


    }
}
