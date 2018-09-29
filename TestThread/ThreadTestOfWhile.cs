using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace TestThread
{
    public class ThreadTestOfWhile
    {
        public Thread testThread;
        public ThreadTestOfWhile()
        {
            while (true)
            {
                testThread = new Thread(TestFunction) { IsBackground = true };
                testThread.Start();
                Thread.Sleep(2000);
            }
        }

        public void TestFunction()
        {
            int a = 1;
            while (true)
            {
                a++;
                if (a>10000)
                {
                    a = 1;
                }
                Thread.Sleep(2100);
            }
        }
    }
}
