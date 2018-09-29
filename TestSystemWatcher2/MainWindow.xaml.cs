using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestSystemWatcher2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow( )
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Initial();
        }




        public void Initial( )
        {
            System.IO.FileSystemWatcher fsw = new System.IO.FileSystemWatcher();
            int timeoutMilllis = 2000;
            WatcherTimer watcher = new WatcherTimer(fsw_Changed,timeoutMilllis);
            fsw.Path = @"E:\TestTmp\ISTATA";
            fsw.Filter = @"*.*";
            fsw.NotifyFilter = NotifyFilters.FileName |
                               NotifyFilters.LastWrite |
                               NotifyFilters.CreationTime;

            // Add event handlers.
            fsw.Created += new FileSystemEventHandler(watcher.OnFileChanged);
            fsw.Changed += new FileSystemEventHandler(watcher.OnFileChanged);

            // Begin watching.
            fsw.EnableRaisingEvents = true;
        }

        void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            Console.Out.WriteLine("Changed");
        }
    }
}
