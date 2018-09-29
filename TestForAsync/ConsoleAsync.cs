using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestForAsync
{
    public class ConsoleAsync
    {
        public void TestForAsyncWrite()
        {

        }

        public async Task<int> WriteLineHello()
        {
            var task1 = new Task<int>(a => { return (int)a + 1; }, 1);
            task1.Start();
            return 1;
        }


    }
}
