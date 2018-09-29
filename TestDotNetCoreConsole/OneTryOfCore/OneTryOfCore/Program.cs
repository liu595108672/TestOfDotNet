using System;
using System.IO;
using StandardLibStr;

namespace OneTryOfCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var otof = new OneTryOfFile(@"E:\tmp\Release\license.lic");
            Console.WriteLine("abcde".IfIsStartedWithUpper());
            Console.WriteLine("Abcde".IfIsStartedWithUpper());
            Console.Read();
        }
    }
}
