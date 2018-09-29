using System;
using System.Collections.Generic;
using System.Text;
using WxUtilities.Extensions;


namespace TestForQLabsFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            byte STX = 0x02;
            byte ETX = 0x03;
            string strCmd = "hello client";
            List<byte> completeCmd = new List<byte>();
            CRCTool tool = new CRCTool();


            Console.WriteLine("Hello Gavin Li!");
            Console.WriteLine("This is the test tool made for qLabs");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("enter the command here : ");
                strCmd = Console.ReadLine();

                completeCmd.Clear();
                completeCmd.Add(STX);
                completeCmd.AddRange(Encoding.Default.GetBytes(strCmd));
                completeCmd.Add(ETX);

                var crc = tool.GetCRC(completeCmd);
                completeCmd.AddRange(crc);

                Console.WriteLine("the complete Hex command is: {0}", completeCmd.ToArray().ToHex());
            }


        }
    }
}
