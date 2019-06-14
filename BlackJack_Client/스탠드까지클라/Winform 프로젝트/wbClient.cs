using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Winform_프로젝트
{
    public delegate void RecvDataDel(byte[] str);

    class wbClient
    {
        private Socket sock;
        private Thread recvThread;
        private RecvDataDel RecvData;

        // private MemControl con;

        //public wbClient(MemControl _con)
        public wbClient(RecvDataDel del)
        {
            // con = _con;
            RecvData = del;
        }

        public void ClientStart(string ip)
        {
            SockInit(ip);

            recvThread = new Thread(new ThreadStart(WorkThread));
            recvThread.Start();
        }

        private void SockInit(string ip)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), 7000);

            sock = new Socket(AddressFamily.InterNetwork,
                                 SocketType.Stream, ProtocolType.Tcp);

            sock.Connect(ipep);  // 127.0.0.1 서버 7000번 포트에 접속시도

            MessageBox.Show("서버에 접속...");  // 만약 서버 접속이 실패하면 예외 발생
        }

        private void WorkThread()
        {
            while (true)
            {
                byte[] msg = ReceiveData(sock);
                if (msg == null)
                    break;
                //Console.WriteLine("수신 데이터: " + Encoding.Default.GetString(msg));
                //con.RecvData(msg);
                RecvData(msg);
            }
            MessageBox.Show("서버가 종료되었습니다");
        }

        public void SendData(byte[] msg)
        {
            SendData(sock, msg);
        }

        public void ClientStop()
        {
            sock.Close();
        }

        private void SendData(Socket sock, byte[] data)
        {
            try
            {
                int total = 0;
                int size = data.Length;
                int left_data = size;
                int send_data = 0;

                // 전송할 데이터의 크기 전달
                byte[] data_size = new byte[4];
                data_size = BitConverter.GetBytes(size);
                send_data = sock.Send(data_size);



                // 실제 데이터 전송
                while (total < size)
                {
                    send_data = sock.Send(data, total, left_data, SocketFlags.None);
                    total += send_data;
                    left_data -= send_data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private byte[] ReceiveData(Socket sock)
        {
            try
            {
                int total = 0;
                int size = 0;
                int left_data = 0;
                int recv_data = 0;

                // 1) 수신할 데이터 크기 알아내기 
                byte[] data_size = new byte[4];
                recv_data = sock.Receive(data_size, 0, 4, SocketFlags.None);
                size = BitConverter.ToInt32(data_size, 0);
                //================================================

                left_data = size;
                byte[] data = new byte[size];

                // 실제 데이터 수신
                while (total < size)
                {
                    recv_data = sock.Receive(data, total, left_data, 0);
                    if (recv_data == 0) break;
                    total += recv_data;
                    left_data -= recv_data;
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}