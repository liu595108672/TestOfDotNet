using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace TestForMSMQ
{
    public class ListenMSMQ
    {
        public string MSMQName = "MSMQ_Name";
        public MessageQueue queue;
        public ListenMSMQ()
        {

        }

        public ListenMSMQ(string name)
        {
            MSMQName = name;
        }

        public bool Create()
        {
            queue = new MessageQueue(@".\Private$\MyMessageQueue");
            queue.ReceiveCompleted += ParseData;
            return false;
        }

        public void ParseData(object sender,ReceiveCompletedEventArgs a)
        {
            Console.WriteLine(a.Message);
        }
    }
}
