using System;
using System.Messaging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestTopshelf
{
    public class MyServiceCodeOfSender
    {

        private string path = @".\private$\mymessagequeue";
        private Thread t;
        public MyServiceCodeOfSender()
        {
       
        }

        public void DoSomething()
        {
            int i = 0;
            while (i<10000)
            {
                i++;
                Console.WriteLine("i: {0}", i);
                Thread.Sleep(700);
            }
            int b = i;
        }

        public void DoSomeMSMQ()
        {
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path);
            }

            MessageQueue queue = new MessageQueue(path);
            queue.Authenticate = false;
            queue.SetPermissions("jay", MessageQueueAccessRights.FullControl);
            var counter = new TimeCounter();
            var i = 0;
            while (i++<100)
            {
                counter.count = i;
                counter.dateTime = DateTime.Now; 
                queue.Send(JsonConvert.SerializeObject(counter));
                Thread.Sleep(10);
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
    public class TimeCounter
    {
        public DateTime dateTime { set; get; }
        public int count { set; get; }
    }
}
