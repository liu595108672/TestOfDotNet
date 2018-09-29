using System;
using System.Net.Sockets;
using System.Net;
using NLog;

namespace TestSocket
{
    /// <summary>
    /// 提供两种方式 client : server   需要进行明确。
    /// 基础的通信功能和心跳检测
    /// 不提供内置的重连机构，如果需要进行重连 在外部实现。
    /// </summary>
    public class TcpReceiver
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public TcpListener tcpListener = null;
        public TcpClient tcpClient = null;
        private TcpWorkModel _workModel = TcpWorkModel.NOTINITIALIZED;

        public TcpReceiver() { }
        public TcpReceiver(TcpListener tcpListener)
        {
            TcpChangeServer(tcpListener);
        }

        public TcpReceiver(TcpClient tcpClient)
        {
            TcpChangeClient(tcpClient);
        }

        public TcpReceiver(string workModel, string tcpIp, int tcpPort)
        {
            switch (workModel.ToUpper())
            {
                case "SERVER":
                    _workModel = TcpWorkModel.SERVER;
                    TcpChangeServer(new TcpListener(tcpPort));
                    break;
                case "CLIENT":
                    _workModel = TcpWorkModel.CLIENT;
                    TcpChangeClient(new TcpClient(tcpIp, tcpPort));
                    break;
                default:
                    _workModel = TcpWorkModel.NOTINITIALIZED;
                    
                    break;
            }
        }

        public TcpReceiver(TcpWorkModel workModel, string tcpIp, int tcpPort)
        {
            _workModel = workModel;
            switch (workModel)
            {
                case TcpWorkModel.SERVER:
                    TcpChangeServer(new TcpListener(IPAddress.Parse(tcpIp),tcpPort));
                    break;
                case TcpWorkModel.CLIENT:
                    TcpChangeClient(new TcpClient(tcpIp, tcpPort));
                    break;
                default:
                    logger.Debug("Not correct WorkModel for ctr");
                    break;
            }
        }

        public bool ClosePort()
        {
            throw new NotImplementedException();
        }

        public bool OpenPort()
        {
            throw new NotImplementedException();
        }

        public bool TcpChangeServer(TcpListener tcpListener)
        {
            bool result = false;
            try
            {
                this.tcpListener = tcpListener;
                this.tcpClient = null;
                _workModel = TcpWorkModel.SERVER;
                result = true;
            }
            catch (Exception e)
            {
                logger.Error("Unexpected exception occurs while changing TcpServer! Exception message is {0}", 
                    e.Message);
                _workModel = TcpWorkModel.ERROR;
                result = false;
            }
            return result;
        }

        public void TcpKeepAlive() { throw new NotImplementedException(); }

        public bool TcpChangeClient(TcpClient tcpClient)
        {
            bool result = false;
            try
            {
                this.tcpClient = tcpClient;
                this.tcpListener = null;
                _workModel = TcpWorkModel.CLIENT;
                result = true;
            }
            catch (Exception e)
            {
                logger.Error("Unexpected exception occurs while changing TcpClient! Exception message is {0}",
                    e.Message);
                _workModel = TcpWorkModel.ERROR;
                result = false;
            }
            return result;
        }


        public bool WriteToPort(byte[] msg)
        {
            throw new NotImplementedException();
        }

        public bool ServerReceiver()
        {
            bool result = false;
            if (null == tcpListener)
            {
                logger.Error("TcpListener is not initialized!");
            }
            try
            {
                tcpListener.Start();
            }
            catch (Exception e)
            {
                logger.Error("Unexpected ecxeption occurs while opening tcp listener! Exception message is: {0}", e.Message);
                result = false; 
            }

            return result;
        }


        #region For TcpServer
        #endregion

        #region For TcpClient
        #endregion

        #region Tool Function
        public byte[] ReadFromPort()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public enum TcpWorkModel
    {
        SERVER = 1,
        CLIENT = 2,
        NOTINITIALIZED = 3,
        ERROR = 0
    }
}
