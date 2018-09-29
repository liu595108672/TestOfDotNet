using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;
using NLog;

namespace TestTopshelf
{
    public class TopshelfHost
    {
        Host service;
        
        public bool CreateSendMSMQService()
        {
            try
            {
                service = HostFactory.New(x =>
                {
                    x.Service<MyServiceCodeOfSender>(s =>
                    {
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                        s.ConstructUsing(name => new MyServiceCodeOfSender());
                    });
                    x.RunAsLocalService();

                    //x.SetDescription("Test For　ForwardMonitor");
                    //x.SetDisplayName("ICIS MH_ForwardMonitor_AAAAA");
                    //x.SetServiceName("MH_ForwardMonitor_AAAAA");
                    x.SetDescription("Test for msmq send service");
                    x.SetDisplayName("MSMQ Sender Service");
                    x.SetServiceName("MSMQSender");
                });

                service.Run();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.Read();   
            }
            return true;
        }


        public bool CreateReceiveMSMQService()
        {
            try
            {
                service = HostFactory.New(x =>
                {
                    x.Service<MyServiceCodeOfReceive>(s =>
                    {
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                        s.ConstructUsing(name => new MyServiceCodeOfReceive());
                    });
                    x.RunAsLocalService();

                    //x.SetDescription("Test For　ForwardMonitor");
                    //x.SetDisplayName("ICIS MH_ForwardMonitor_AAAAA");
                    //x.SetServiceName("MH_ForwardMonitor_AAAAA");
                    x.SetDescription("Test for msmq receive service");
                    x.SetDisplayName("MSMQ Receive Service");
                    x.SetServiceName("MSMQReceive");
                });

                service.Run();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.Read();
            }
            return true;
        }

        public bool RunReceiverWithoutService()
        {
            MyServiceCodeOfReceive myServiceCodeOfReceive = new MyServiceCodeOfReceive();
            myServiceCodeOfReceive.Start();

            return true;
        }
    }
}
