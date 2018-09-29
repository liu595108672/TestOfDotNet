using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using Newtonsoft.Json;

namespace TestTopshelf
{
    public class MSMQServiceTest
    {
        public string msmqPath = @".\Private$\MyMessageQueue";
        
        public bool ReceiveFromMSMQ(string path)
        {
            msmqPath = path;
            MessageQueue messageQueue = new MessageQueue(msmqPath);
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MyReceiver;

            messageQueue.BeginReceive();

            return false;
        }
        public void MyReceiver(object sender,ReceiveCompletedEventArgs args)
        {
            MessageQueue msgQueue = sender as MessageQueue;
            var msg = msgQueue.EndReceive(args.AsyncResult);
            var data = msg.Body;


            try
            {
                var jp = JsonConvert.SerializeObject(data);
            }
            catch (Exception e)
            {

                throw;
            }


            msgQueue.BeginReceive();

        }
    }
}
