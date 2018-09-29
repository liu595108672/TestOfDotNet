using System;
using System.Linq;
using System.Collections.Generic;

namespace TestForRegex
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bArray = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            List<byte> bList = new List<byte>(bArray);
            var index = bList.FindIndex(x => x.Equals(0x02));
            index = bList.FindIndex(index,x => x.Equals(0x02));
            index = bList.FindIndex(x => x.Equals(bArray.Last()));
            var index2 = bList.FindIndex(x => x.Equals(0x09));




            RegexTest test = new RegexTest();
            string original = "abcdefghijklmnop";
            string pattern = "(?<abc>abcdef)(?<ghi>ghijkl)(?<mno>mnop)";
            test.TestGroups(original, pattern);

            Console.WriteLine("Hello World!");






        }
    }
}
