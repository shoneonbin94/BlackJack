using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace 프로젝트서버
{
    public partial class Form1 : Form
    {
        int a = 0;
        wbServer wbserver = new wbServer();
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = GetLocalIP;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                int port = int.Parse(textBox2.Text);
                wbserver.ServerStart(port);
                a = 1;
                
            }
            else if(textBox2.Text == string.Empty)
            {
                MessageBox.Show("포트를 입력하세요");
            }


        }
        public static string GetLocalIP
        {
            get
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                string ClientIP = string.Empty;
                for (int i = 0; i < host.AddressList.Length; i++)
                {
                    if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        ClientIP = host.AddressList[i].ToString();
                    }
                }
                return ClientIP;
            }
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                wbserver.ServerStop();
                a = 0;
            }
            catch
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (a == 1)
            {
                wbserver.ServerStop();
                MessageBox.Show("서버를 정지합니다.");
                a = 0;
            }
            else if (a == 0)
            {
                MessageBox.Show("서버에 연결되어있지 않습니다.");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }
    }
}