using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSomethingAboutString
{
    public class TestSubString
    {
        public string s = "abcdef";
        public string TestForSubString()
        {
            var iEnd = s.IndexOf("f");
            var tmpStr = s.Substring(iEnd+1);
            return tmpStr;
        }
    }
}
