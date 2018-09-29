using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace OneTryOfCore
{
    public class OneTryOfFile
    {
        
        public OneTryOfFile(string filePath)
        {
            try
            {
                var fInfo = new FileInfo(filePath);
                Console.WriteLine(fInfo.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
