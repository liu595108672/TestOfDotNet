using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace TestForRegex
{
    public class RegexTest
    {
        public string pattern = "";

        public string GetRegexValue(string originalStr,string pattern)
        {
            string strToReturn = "";
            return Regex.Match(originalStr, pattern).Value;
        }


        public void TestGroups(string originalStr,string pattern)
        {
            var match = Regex.Match(originalStr, pattern);

            var group = match.Groups;

            foreach (var item in group)
            {
                var tmpStr = group["abc"].Value;
            }
        }
    }

    
}
