using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InformationEngine;
using InformationEngine.Struct;
using System.Threading;

namespace HBInformationPlatform
{
    public partial class Form_Main : Form
    {
        public static AutoResetEvent CanReturn = new AutoResetEvent(false);

        public Form_Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            /*
            obj = new EducationSystemA("username", "password");
            obj.PropertyChanged += (s, arg) =>
            {
                if (arg.PropertyName == "Result")
                {
                    this.BeginInvoke((Action)delegate
                    {
                        Program.result =
                        ("Content-Type: text/html\n\n")
                        + ("<html><head><title>username</title></head><body>\n\n")
                        + (obj.Result)
                        + ("\n\n</body></html>");
                        obj.Dispose();
                        obj = null;
                        GC.Collect();
                        CanReturn.Set();
                        Application.Exit();
                    });
                }
            };
            obj.Get();
            //CanReturn.WaitOne();
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("开始获取 - " + DateTime.Now + '.' + DateTime.Now.Millisecond);
            ECardSystem ecs = new ECardSystem();
            textBox1.Text = ecs.Get(textBox2.Text, textBox3.Text);
            Console.WriteLine("获取完成 - " + DateTime.Now + '.' + DateTime.Now.Millisecond);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LibrarySystemBookInfo lsbi = new LibrarySystemBookInfo();
            textBox1.Text = lsbi.Get("username", "item.php?marc_no=0000081144");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("开始获取 - " + DateTime.Now + '.' + DateTime.Now.Millisecond);
            LibrarySystem ls = new LibrarySystem();
            textBox1.Text = ls.Get(textBox2.Text, "java", "1");
            Console.WriteLine("获取完成 - " + DateTime.Now + '.' + DateTime.Now.Millisecond);
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine("开始获取 - " + DateTime.Now + '.' + DateTime.Now.Millisecond);
            EducationSystem esb = new EducationSystem();
            textBox1.Text = esb.Get(textBox2.Text, textBox3.Text);
            Console.WriteLine("获取完成 - " + DateTime.Now + '.' + DateTime.Now.Millisecond);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Console.WriteLine("开始获取 - " + DateTime.Now + '.' + DateTime.Now.Millisecond);
            EducationSystem esb = new EducationSystem();
            textBox1.Text = esb.Get(textBox2.Text, textBox3.Text);
            Console.WriteLine("获取完成 - " + DateTime.Now + '.' + DateTime.Now.Millisecond);
        }

    }
}
