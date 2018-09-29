using System;
using System.Threading;
using System.Collections.Generic;

namespace TestTimer
{
    class Program
    {
        static void Main(string[] args)
        {

            int a = 123;
            var bArry = new byte[] { 0x00, 0x00, 0x01, 0x01 };
            var bArry2 = new byte[] { 0x01, 0x00, 0x00, 0x00 };

            var t = BitConverter.ToInt32(bArry);
            t = BitConverter.ToInt32(bArry2);
            
            TimerTest timerTest = new TimerTest();

            while(true)
            {
                Console.WriteLine("Queue length {0}",timerTest.count);
                Thread.Sleep(500);
            }
        }
    }
}
