using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestArrayCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 123;
            int b = 9;
            Console.WriteLine((a & b));

            //TestOne t = new TestOne();
            //t.testOneArray();
            //byte[] bArray1, bArray2;
            //bArray1 = new byte[] { 0x01, 0x02, 0x03, 0x04 };
            //bArray2 = new byte[] { 0x05, 0x06, 0x07, 0x08 };
            //TestTwo t2 = new TestTwo();
            //t2.change(bArray1,bArray2);
            //Console.Read();


            //byte[] bArray1, bArray2;
            //bArray1 = new byte[] { 0x01, 0x02, };
            //bArray2 = new byte[] { 0x00,0x05};

            //var intValue1 = BitConverter.ToInt16(bArray1, 0);
            //var intValue2 = BitConverter.ToInt16(bArray2, 0);


            string tmpStr = "abcdefg";
            string regex = "def";
            var startIndex = tmpStr.IndexOf(regex);
            try
            {

                string tmpStr2 = tmpStr.Substring(startIndex, 10);

            }
            catch (Exception e)
            {
                var messge = e.Message;
            }
        }
    }
}
