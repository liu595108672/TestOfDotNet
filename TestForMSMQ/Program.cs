using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace TestForMSMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = Environment.CurrentDirectory;
            var parentDirName = Regex.Match(@"E:\GitLab\DGW\Dgw-3.0.2.1\Debug\Monitors\MH_ForwardMonitor_nnnn", @"(?<=\\)\w+_ForwardMonitor_\w+").Value;
            
            var msmqArray = MessageQueue.GetPrivateQueuesByMachine(".");



            var dir2 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;



            List<MessageQueue> msmqList = new List<MessageQueue>();

            foreach (var tmpMsmq in msmqArray)
            {
                if (Regex.Match(tmpMsmq.QueueName, "ForwardMonitor".ToLower()).Success)
                {
                    msmqList.Add(tmpMsmq);
                }
            }

            //string path = @".\Private$\icis_ttttt_mh_dbmonitor_testforpc0505";
            string path = @".\Private$\mymessagequeue";
            //var c = new CreateMSMQ(path);
            //c.Create();
            //var p = "abc";
            //var q = "cde";
            if (MessageQueue.Exists(path))
            {
                MessageQueue.ClearConnectionCache();
                MessageQueue.Delete(path);
                MessageQueue.Create(path);
            }

            var queue = new MessageQueue(path);


            var person = new Person { Name = "name", Age = 12, Gender = "F" };
            var data = JsonConvert.SerializeObject(person);


            queue.Send(person);



            //queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Person)});
            //var msg = queue.Receive();
            //var personR = (Person)msg.Body;

            //queue.ReceiveCompleted += new ReceiveCompletedEventHandler( MyDataReceive);
            //queue.BeginReceive();

            Console.Read();
        }

       

        public static void MyDataReceive(object sender,ReceiveCompletedEventArgs args)
        {
            MessageQueue messageQueue = (MessageQueue)sender;
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Person) });

            Message msg = messageQueue.EndReceive(args.AsyncResult);

            Console.WriteLine(msg.Body);
            var data = msg.Body;
            messageQueue.BeginReceive();
           
        }

    }


    public class Person
    {
        public string Name { set; get; }    
        public string Gender { set; get; }
        public int Age { set; get; }
    }
}
