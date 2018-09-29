using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestLambda
{
    public class TLambda
    {
        private delegate int IntDelegate(int a, int b);
        public void MainTest()
        {
            IntDelegate function =  (int x,int y) => x*y;
            var result = function(123, 456);
            Console.WriteLine(result);


            Func<int,int,int> tmpFunc = (int x, int y) => { var t = x + y;
                return t + x;
            };
            result = tmpFunc(1, 2);
            Console.WriteLine(result);

            Action<int, int> tmpAction = (int x, int y) => { Console.WriteLine(x + y); };
            tmpAction(33, 44);
        }
    }
}
