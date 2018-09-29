using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEvent.PartOne;
using TestEvent.PartTwo;
using System.Threading;

namespace TestEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadEventTest threadEventTest = new  ThreadEventTest(new General());
            
            Console.ReadLine();
        }
    }
}
