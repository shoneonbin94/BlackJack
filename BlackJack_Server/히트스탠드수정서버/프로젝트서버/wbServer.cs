using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
namespace 프로젝트서버
{
    class wbServer
    {
        private Socket server;
        private Thread Thread;
        public List<Socket> socklist = new List<Socket>();
        private Control con = new Control();

        public wbServer() { }

        public void ServerStart(int port)
        {
            SocketInit(port);

            Thread = new Thread(new ThreadStart(AcceptThread));
            Thread.Start();
            MessageBox.Show("클라이언트 접속을 대기합니다");
        }
        public void SocketInit(int port)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
            server = new Socket(AddressFamily.InterNetwork,
                                SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipep);
            server.Listen(20);
        }

        private void AcceptThread()
        {
            while (true)
            {
                if(socklist.Count == 4)
                {
                    break;
                }
                Socket client = server.Accept();
                IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
                socklist.Add(client);

     
                MessageBox.Show(ip.Address + "님이 접속하셨습니다." +"(" + socklist.Count + ")명");

                Thread thread = new Thread(
                    new ParameterizedThreadStart(MThread));
                thread.Start(client);
            }
        }

        private void MThread(object obj)
        {
            Socket sock = (Socket)obj;
            while (true)
            {
                byte[] msg = ReceiveData(sock);
                if (msg == null)
                    break;

                string str = con.RecvData(msg);
                byte[] packstr = Encoding.Default.GetBytes(str);

                SendData(sock, packstr);
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
                MessageBox.Show(ex.Message);
                return null;
            }
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
                MessageBox.Show(ex.Message);
            }
        }
        public void ServerStop()
        {
            server.Close();
            Thread.Abort();
        }
    }
}
