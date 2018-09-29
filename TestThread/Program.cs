using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestThread
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadTestOfFunctions t = new ThreadTestOfFunctions();
            t.TestStart();
            Thread.Sleep(800);
            t.ChangeThread();
            t.TestStart();
            Thread.Sleep(1000);
            t.TestStop();
            Thread.Sleep(1000);


            //var t = new ThreadTestOfWhile();
        }
    }
}
