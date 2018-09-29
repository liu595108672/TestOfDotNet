using System;
using System.Collections.Generic;

namespace TestListAndDictionary
{
    class Program
    {

        /// <summary>
        /// 简单结论： Dictionary只有在 查找key值的时候才是最高效的，其他方面都不如List
        ///             因为Dictionary本质上还是延续的 HashTable
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Dictionary<string, string> testDictionary = new Dictionary<string, string>();
            List<string> testList = new List<string>();

            string[] TestKey = new string[1000000];
            string[] TestValue = new string[1000000];

            for(int i = 0; i < 1000000; i++)
            {
                TestKey[i] = i.ToString();
                TestValue[i] = i.ToString();
            }

            long timeStart = DateTime.Now.Ticks;
            for(int i = 0; i < 1000000; i++)
            {
                testDictionary[TestKey[i]]=TestValue[i]; 
            }
            long timeEnd = DateTime.Now.Ticks;

            Console.WriteLine("TestDicitonary : {0} ",timeEnd - timeStart);


            timeStart = DateTime.Now.Ticks;
            for(int i = 0; i < 1000000; i++)
            {
                testList.Add(TestValue[i]);
            }
            timeEnd = DateTime.Now.Ticks;

            Console.WriteLine("TestList : {0} ",timeEnd - timeStart);

            timeStart = DateTime.Now.Ticks;
            if(testDictionary.ContainsValue("623456"))
            {
                int tmp = 1;
                int sum = tmp + 2;
            }
            timeEnd = DateTime.Now.Ticks;
            Console.WriteLine("TestDictionary find 623456 : {0}", timeEnd - timeStart);


            timeStart = DateTime.Now.Ticks;
            if(testList.Contains("623456"))
            {
                int tmp = 1;
                int sum = tmp + 2;
            }
            timeEnd = DateTime.Now.Ticks;
            Console.WriteLine("TestList find 623456 : {0}", timeEnd - timeStart);


            timeStart = DateTime.Now.Ticks;
            testDictionary["623456"] = "00000";
            timeEnd = DateTime.Now.Ticks;
            Console.WriteLine("TestDictionary change 623456 : {0}", timeEnd - timeStart);


            timeStart = DateTime.Now.Ticks;
            testList[623456] = "00000";
            timeEnd = DateTime.Now.Ticks;
            Console.WriteLine("TestList change 623456 : {0}", timeEnd - timeStart);

        }
    }


   
}
 