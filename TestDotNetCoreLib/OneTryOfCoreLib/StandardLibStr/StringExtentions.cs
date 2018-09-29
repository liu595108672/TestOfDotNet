using System;

namespace StandardLibStr
{
    public static class StringExtentions
    {
        public static bool IfIsStartedWithUpper(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            return char.IsUpper(str[0]);
        }
    }
}
