using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestForHttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MyHttpClient t1 = new MyHttpClient();
            t1.TestOne();

            t1.TestTwo();

            t1.TestThree();
        }
    }
}
