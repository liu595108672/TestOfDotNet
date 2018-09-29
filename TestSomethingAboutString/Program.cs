using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestSomethingAboutString
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "123456";
            Console.Out.WriteLine(GetMD5WithString(s));

            TestSubString t = new TestSubString();
            t.TestForSubString();


            TryTestIndexOf();
            Console.Read();
        }

        /// <summary>
        /// 对String进行MD5加密
        /// </summary>
        /// <param name="sDataIn"></param>
        /// <returns></returns>
        public static string GetMD5WithString(string DataIn)
        {
            string str = "";
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(DataIn);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(data);
            for (int i = 0; i < bytes.Length; i++)
            {
                str += bytes[i].ToString("x2");
            }
            return str;
        }

        public static void TryTestIndexOf()
        {
            string s = "012345667896";
            Console.WriteLine(s.IndexOf('6'));
            Console.WriteLine(s.IndexOf('6', 0));
            Console.WriteLine(s.IndexOf('6', 1));
            Console.WriteLine(s.IndexOf('6', 1,2));
            Console.WriteLine(s.IndexOf('0'));

        }
    }
}
