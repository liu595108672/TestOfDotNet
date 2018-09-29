using System;
using System.Messaging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NLog;

namespace TestTopshelf
{
    public class MyServiceCodeOfReceive
    {
        private string path = @".\private$\mymessagequeue";
        private Thread t;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public MyServiceCodeOfReceive()
        {
            logger.Debug("Create default MyServiceCodeOfReceive.");
        }

        public void DoSomeMSMQ()
        {
            if (MessageQueue.Exists(path))
            {
                logger.Debug("Using MSMQ {0}",path);
                MessageQueue queue = new MessageQueue(path);
              
                queue.ReceiveCompleted += MyReceive;
                logger.Debug("Begin receive...");
                queue.BeginReceive();
            }
            else
            {
                logger.Error("MSMQ does not exist!");
            }
        }

        private void MyReceive(object sender, ReceiveCompletedEventArgs args)
        {
            try
            {
                logger.Debug("step into MyReceive");
                MessageQueue msgQueue = (MessageQueue)sender;
                msgQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                Message msg = msgQueue.EndReceive(args.AsyncResult);


                string strData = JsonConvert.SerializeObject(msg.Body);
                string strData2 = msg.Body.ToString();
                

                logger.Debug("Get data from MSMQ. Data is: " + strData);

                try
                {
                    var testData0 = JsonConvert.DeserializeObject(strData2);
                    var testData1 = JsonConvert.DeserializeObject<TimeCounter>(strData2);
                }
                catch (Exception e)
                {
                    logger.Error("exception message : {0}", e.Message);
                }

                msgQueue.BeginReceive();
            }
            catch (Exception exception)
            {
                logger.Error("exception: " + exception.Message);
                throw;
            }

        }

        public void Start()
        {
            Console.WriteLine("coming into start");
            //DoSomething();

            t = new Thread(DoSomeMSMQ);
            t.IsBackground = true;
            t.Start();


        }


        public void Stop()
        {
            t.Abort();
        }
    }
}
