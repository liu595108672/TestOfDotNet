using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestArrayCopy
{
    class TestArrayCopyOne
    {
    }
    public class TestOne
    {
        public List<byte[]> bArrayList = new List<byte[]>();
        public byte[] bArray = null;
        public TestOne()
        {
            List<byte> bList = new List<byte>();
            for (int i = 0; i < 200; i++)
            {
                bList.Add((byte)i);
            }
            bArray = bList.ToArray();
        }

        public void testOneArray()
        {
            byte[] tmpArray = null;
            DateTime startTime = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                tmpArray = bArray.Skip(10).Take(30).ToArray();
            }
            DateTime endTime = DateTime.Now;
            Console.WriteLine((endTime - startTime).Ticks);

            startTime = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                Buffer.BlockCopy(bArray, 10, tmpArray, 0, 30);
            }
            endTime = DateTime.Now;
            Console.WriteLine((endTime - startTime).Ticks);
        }
    }


    public class TestTwo
    {
        public byte[] bArray1, bArray2;
        public TestTwo()
        {
            bArray1 = new byte[] { 0x01, 0x02, 0x03, 0x04 };
            bArray2 = new byte[] { 0x05, 0x06, 0x07, 0x08 };
        }

        public void  change(byte[] b1, byte[] b2)
        {
            int length = b1.Length < b2.Length ? b1.Length : b2.Length;
            b2 = b1.Take(length).ToArray();
        }

        
    }
}
