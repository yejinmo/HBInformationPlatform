using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBInformationPlatform
{
    static class Program
    {
        public static string result = string.Empty;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form_Main());
            //Form_Main.CanReturn.WaitOne();
            //Console.Write(result);
            //Thread.Sleep(500);
        }
    }
}
