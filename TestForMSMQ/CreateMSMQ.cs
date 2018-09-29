using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace TestForMSMQ
{
    public class CreateMSMQ
    {
        public string msmqPath = @".\Private$\MyMessageQueue";

        public CreateMSMQ()
        {
            
        }
        public CreateMSMQ(string path)
        {
            msmqPath = path;
            
        }
        public bool Create()
        {
            if (!MessageQueue.Exists(msmqPath))
            {
                MessageQueue.Create(msmqPath);
            }
            return false;
        }
    }
}
