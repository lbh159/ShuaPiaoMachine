using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShuaPiaoMachine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            Random ra = new Random();
            int jiange = 5;
            try
            {
                if (string.IsNullOrEmpty(JiangeBox.Text))
                {
                    return;
                }
                jiange = Convert.ToInt32(JiangeBox.Text);
                if (jiange < 0)
                {
                    jiange = 1;
                }
            }
            catch
            {
                textBox1.Text = "请输入正确的间隔时间";
            }
            while (true)
            {
                textBox2.Text = i.ToString();
                Thread.Sleep(ra.Next(jiange  * 800, (jiange + 1) *800));
                if (i >= 1000)
                {
                    textBox1.Clear();
                }
                textBox1.AppendText(Shua.PostLogin() + ";");

                i++;
            }
        }
    }
}
