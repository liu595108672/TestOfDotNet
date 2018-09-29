using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace TestSerialPort
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSerialPortTry t = new TestSerialPortTry();
            t.TestSerialPort();

            
        }
    }
}
