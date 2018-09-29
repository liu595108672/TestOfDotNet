using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestThread
{
    public class ThreadTestOfFunctions
    {
        public Thread thread;


        public void TestStart()
        {
            thread = new Thread(Loop10)
            {
                Name = "testLoop",
                Priority = ThreadPriority.Lowest
            };

            thread.Start();
        }

        public void ChangeThread()
        {
            thread = new Thread(Loop10)
            {
                Name = "TestLoop2",
                Priority = ThreadPriority.Lowest
            };

        }

        public void TestStop()
        {
            try
            {

                thread.Abort();
                thread.Join();
                thread = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message: {0}", e.Message);
            }
        }

        public void Loop10()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Thread ID: "+ Thread.CurrentThread.ManagedThreadId +" Loop: "+ i);
                Thread.Sleep(200);
            }
        }
    }
}
