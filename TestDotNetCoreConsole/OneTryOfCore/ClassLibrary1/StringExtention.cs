using System;

namespace ClassLibrary1
{
    public static class StringExtention
    {
        public static bool IfStartsWithUpper(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;   
            }

            return char.IsUpper(str[0]);
        }
    }
}
