using System.Text;
using System.Net;
using System.Net.Sockets;
using System;
using System.Threading;

namespace TestSocket
{
    public class SocketClinet
    {
        private static int _port = 10045;
        private string _IP = "127.0.0.1";
        private IPAddress _IPAdaress;
        private TcpClient tcpClient;
        public SocketClinet()
        {
            //tcpClient = new TcpClient(_IP, _port);
            //tcpClient = new TcpClient();
            //tcpClient.Connect(_IP, _port);
            tcpClient = new TcpClient();
            tcpClient.BeginConnect(_IP, _port,new AsyncCallback(communication),null);

            //SendMessage();
        }

        private void communication(IAsyncResult ar)
        {
            var tmpStream = tcpClient.GetStream();
            int count = 0;
            while (true)
            {
                var sMessage = string.Format("Hello {0}", count++);
                byte[] bArr = new byte[] { 0x30, 0x31, 0x33 };
                tmpStream.Write(bArr,0,bArr.Length);
                Thread.Sleep(3000);
            }
            
        }

        public bool Connect()
        {
            tcpClient.Close();
            tcpClient.Connect(_IP, _port);
            return true;
        }
        public void SendMessage()
        {
            var tmpStream = tcpClient.GetStream();



            while (true)
            {
                string tmpMessage = "request data!"+DateTime.Now;
                tmpStream.Write(Encoding.ASCII.GetBytes(tmpMessage), 0, tmpMessage.Length);

                if (tcpClient.Available>0)
                {
                    byte[] bBuffer = new byte[tcpClient.Available];
                    var tmpLength = tmpStream.Read(bBuffer, 0, bBuffer.Length);

                    tmpMessage = "Receive data from server. Data length is : " + tmpLength;
                    tmpStream.Write(Encoding.ASCII.GetBytes(tmpMessage), 0, tmpMessage.Length);
                    Console.WriteLine("Read message is :"+Encoding.ASCII.GetString(bBuffer));
                }
                else
                {
                    Console.WriteLine("Read Nothing from net port!");
                }
                Thread.Sleep(700);
            }
        }
    }
}
