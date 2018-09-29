using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace TestSocket
{
    public class SocketServer
    {
        private static int _port = 10043;
        private string _IP = "192.168.2.78";
        private IPAddress _IPAdaress;
        private static TcpListener _tcpServer;
        private List<TcpClient> _clientList = new List<TcpClient>();

        public SocketServer()
        {
            _IPAdaress = IPAddress.Parse(_IP);
            _tcpServer = new TcpListener(new IPEndPoint(_IPAdaress, _port));
            Thread listenerThread = new Thread(Listening) { IsBackground = true, Name = "Listenning Thread" };
            
            listenerThread.Start();
            
        }


        public void Listening()
        {
            _tcpServer.Start();
            while (true)
            {
                try
                {
                    TcpClient tmp = _tcpServer.AcceptTcpClient();
                    
                    _clientList.Add(tmp);
                    new Thread(ReceivingMessage).Start(tmp);

                    Console.WriteLine("A new client connected! Client Ip : {0}",tmp.Client.RemoteEndPoint);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
            }
        }

        

        public void ReceivingMessage(object tcpClient)
        {
            var tmp = (TcpClient)tcpClient;
            byte[] _buffer ;
            int tmpLength;
            while (true)
            {
                _buffer = new byte[tmp.Available];
                var tmpStream = tmp.GetStream();
                tmpLength = 0;

                if (tmpStream.CanRead)
                {
                    tmpLength = tmpStream.Read(_buffer, 0, tmp.Available); 
                }
                string tmpString = Encoding.ASCII.GetString(_buffer);
                if (tmpLength>0)
                {
                    Console.WriteLine(tmpString+"  "+DateTime.Now);
                    tmpStream.Write(_buffer, 0, _buffer.Length);
                    
                }
                Thread.Sleep(500);
            }
        }
    }
}
