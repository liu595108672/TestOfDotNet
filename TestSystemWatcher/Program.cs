using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Test t = new Test();
            t.Initial();
        }

    }

    class Test
    {
        public void Initial( )
        {
            System.IO.FileSystemWatcher fsw = new System.IO.FileSystemWatcher();
            fsw.Filter = "*.*";
            fsw.NotifyFilter = NotifyFilters.FileName |
                               NotifyFilters.LastWrite |
                               NotifyFilters.CreationTime;

            // Add event handlers.
            fsw.Created += new FileSystemEventHandler(fsw_Changed);
            fsw.Changed += new FileSystemEventHandler(fsw_Changed);

            // Begin watching.
            fsw.EnableRaisingEvents = true;
        }

        void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            Console.Out.WriteLine("Changed");
        }
    }
}
