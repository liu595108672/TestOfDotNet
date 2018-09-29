using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace TestTelnetCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Process iProcess = new Process();
            iProcess.StartInfo.UseShellExecute = false;
            iProcess.StartInfo.FileName = "cmd.exe";
            iProcess.StartInfo.RedirectStandardInput = true;
            iProcess.StartInfo.RedirectStandardOutput = true;
            iProcess.StartInfo.RedirectStandardError = true;
            iProcess.StartInfo.CreateNoWindow = true;

            //Console.WriteLine(iProcess.ProcessName);

            iProcess.Start();
            //iProcess.StandardInput.WriteLine(@"TASKKILL /PID 17252  /F");
            iProcess.StandardInput.WriteLine(@"ping -n 5 www.baidu.com");
            

            Console.WriteLine(iProcess.ProcessName);

            iProcess.StandardInput.WriteLine(@"exit");
            var result = iProcess.StandardOutput.ReadToEnd();
            Console.WriteLine(result);

            Console.Read();
        }
    }
}
