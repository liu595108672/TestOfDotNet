using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Net;
using WxUtilities.Extensions;
using System.Net.Sockets;
using System.Diagnostics;
using NLog;

namespace TestForAsaki
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            var bCommond = new byte[] { 0X4B, 0x0D, 0x0A };

            #region Create a new process to telnet to device
            logger.Debug("Try to config a process to telnet...");
            Process iProcess = new Process();
            iProcess.StartInfo.RedirectStandardError = false;
            iProcess.StartInfo.RedirectStandardInput = false;
            iProcess.StartInfo.RedirectStandardOutput = false;
            iProcess.StartInfo.CreateNoWindow = false;
            iProcess.StartInfo.UseShellExecute = false;
            iProcess.StartInfo.FileName = "telnet.exe";
            logger.Debug("Process configuration completed!");

            logger.Debug("Read telnet remote point config.json...");
            AsakiConfig asakiConfig = new AsakiConfig();
            logger.Debug("Read telnet remote point config completed!");

            iProcess.StartInfo.Arguments = " "+asakiConfig.Config.IP + " " + asakiConfig.Config.TelnetPort;

            try
            {
                logger.Debug("Try to start process to telnet remote point...");
                bool flag =  iProcess.Start();
                logger.Debug("Start proces succeed!");
                
            }
            catch (Exception e)
            {
                logger.Error("Unexpected excetpion occurs while start process! Exception message is: {0}", e.Message);
            }

            #endregion



            logger.Info("**********************************************");
            logger.Info("**********************************************");
            logger.Info("**********************************************");
            Console.WriteLine("Press Enter to continue!");
            Console.ReadLine();


            #region Create a tcpClient to send command to device and receive message returns
            try
            {
                logger.Debug("Start tcpClient to connect device...");
                var tcpClient = new TcpClient(asakiConfig.Config.IP, asakiConfig.Config.TcpServerPort);
                logger.Debug("Connect to device succeed!");
                int _count = 0;
                while (_count++<10)
                {

                    logger.Debug("Try to send commond to device...");
                    var tmpStream = tcpClient.GetStream();
                    tmpStream.Write(bCommond, 0, bCommond.Length);
                    logger.Debug("Send message succeed!");



                    logger.Debug("Try to read message from device...");
                    if (tmpStream.CanRead && tcpClient.Available > 0)
                    {
                        logger.Debug("Can read and tcpClient.Available is larger than zero");

                        logger.Debug("Initialize string buffer..");
                        var bResult = new byte[tcpClient.Available];

                        logger.Debug("Reading string from stream");
                        tmpStream.Read(bResult, 0, bResult.Length);
                        string strResult = Encoding.Default.GetString(bResult);
                        logger.Debug("Read message : {0}", strResult);
                        logger.Trace("HexData: {0}", bResult.ToHex());
                    }
                    else if (!tmpStream.CanRead)
                    {
                        logger.Warn("The stream cannot be read!");
                    }
                    else
                    {
                        logger.Warn("The stream can be read, but read length is not larger than zero");
                    }

                    Thread.Sleep(3000);
                }
                logger.Debug("Closing stream and tcpClient...");
                tcpClient.GetStream().Close();
                tcpClient.Close();
                logger.Debug("Stream and tcpClient closed");
            }
            catch (Exception e)
            {
                logger.Error("Exception occurs while sending and receiving message to remote point via tcpclient! Exception message is :{0}", e.Message);
            }
            #endregion

           
            
            try
            {
                logger.Debug("Stop the first process...");
                iProcess.Close();
                logger.Debug("Stop the first process succeed");
            }
            catch (Exception e)
            {
                logger.Error("Unexpected exception occurs while stopping the first process! Exception message is: {0}",e.Message);
            }

            logger.Info("**********************************************");
            logger.Info("**********************************************");
            logger.Info("**********************************************");
            Console.WriteLine("Press Enter to continue!");
            Console.ReadLine();


            #region  Using Process to telnet and send command
            logger.Debug("Try to config the second process to telnet...");
            Process iProcess2 = new Process();
            iProcess2.StartInfo.RedirectStandardError = true;
            iProcess2.StartInfo.RedirectStandardInput = true;
            iProcess2.StartInfo.RedirectStandardOutput = true;
            iProcess2.StartInfo.CreateNoWindow = true;
            iProcess2.StartInfo.UseShellExecute = false;
            iProcess2.StartInfo.FileName = "telnet.exe";
            logger.Debug("The second process configuration completed!");

            iProcess2.StartInfo.Arguments = " " + asakiConfig.Config.IP + " " + asakiConfig.Config.TelnetPort;

            try
            {
                logger.Debug("Try to start the second process to telnet remote point...");
                iProcess2.Start();
                logger.Debug("Start proces succeed!");

                logger.Debug("Try to write command to remote point by telnet process...");
                iProcess2.StandardInput.WriteLine(Encoding.Default.GetString(bCommond));
                logger.Debug(@"Write command 'K\r\n' succeed");

                logger.Debug("Write exit command to remote point by telnet process");
                iProcess2.StandardInput.WriteLine("Exit");

                logger.Debug("Receiving result from remote point");
                var result = iProcess2.StandardOutput.ReadToEnd();
                logger.Debug(!result.IsNullOrEmpty() ?  "Result is " + result : "No response received!");
            }
            catch (Exception e)
            {
                logger.Error("Unexpected excetpion occurs while start process! Exception message is: {0}", e.Message);
            }

            #endregion
            Console.WriteLine("Press Enter to continue!");
            Console.ReadLine();
        }

    }
}
