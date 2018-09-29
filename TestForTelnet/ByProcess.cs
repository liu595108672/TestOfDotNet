using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NLog;
using System.Net;
using System.Net.Sockets;


namespace TestForTelnet
{
    /// <summary>
    /// 通过开启新进程的方式来进行Telnet连接
    /// 
    /// </summary>
    public class ByProcess
    {
        private Process _process;
        private readonly string _processName = "telnet.exe";
        private readonly string _IP = "192.168.2.92";
        private readonly int _port = 10091;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly byte[] _commond = new byte[] { 0x4B,0x0D,0x0A };


        private TcpClient _tcpClient;

        public void InitializeProcess()
        {
            _process = new Process();
            _process.StartInfo.FileName = _processName;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.RedirectStandardInput = true;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.Arguments = _IP+"  "+_port;

            try
            {
                _process.Start();
            }
            catch (Exception e)
            {
                logger.Error("Unexpected exception occurs while starting new process for telnet. Exception message is: {0}",e.Message);
            }

            try
            {
                _tcpClient = new TcpClient(_IP, _port);
            }
            catch (Exception e)
            {
                logger.Error("Unexpected exception occurs while initialize tcpClient. Exeption message is: {0}", e.Message);
            }

            var tmpStream = _tcpClient.GetStream();
            tmpStream.Write(_commond, 0, 3);

            try
            {
                if (tmpStream.DataAvailable)
                {
                    byte[] byy = new byte[_tcpClient.Available];
                    tmpStream.Read(byy, 0, byy.Length);
                }
            }
            catch (Exception e)
            {
                logger.Error("Unexpected exception occurs while reading data from tcpClient. Exeption message is: {0}", e.Message);
            }


        }

    }
}
