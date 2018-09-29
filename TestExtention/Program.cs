using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExtention
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person("Jay Liu", 23, 1);
            Console.WriteLine(p.ToString());
            Console.WriteLine(p.ToFormatedString());

            Console.Read();
        }
    }
}
