using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTopshelf
{
    class Program
    {
        static void Main(string[] args)
        {
            string abc = "a|b|c,d;e";
            var tmp = abc.Split(",|;".ToCharArray());
            //MSMQServiceTest msmqTest = new MSMQServiceTest();
            //msmqTest.ReceiveFromMSMQ(@".\Private$\icis_ttttt_mh_dbmonitor_testforpc0505");

            TopshelfHost t = new TopshelfHost();

            //t.CreateSendMSMQService();
            t.CreateReceiveMSMQService();
        }

    }
}
