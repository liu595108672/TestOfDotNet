using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEvent.PartOne;
using TestEvent.PartTwo;
using System.Threading;
namespace TestEvent
{
    public class ThreadEventTest
    {
        public General general = new General();
        public Thread t;
        private bool _isRunning =false;
        private Soldier s;
        private Scouts s2;
        public ThreadEventTest(General g)
        {
            general = g;
            Start();
            int count = 0;
            while (count++<10)
            {
                g.GoRushB();
                Thread.Sleep(1000);
            }

            Stop();
            Console.WriteLine("Stop succeed!");
            g.GoRushB();
            Console.ReadLine();
        }

        public void Stop()
        {
            _isRunning = false;
            Thread.Sleep(1000);
            t.Abort();
        }
        public void Start()
        {
            _isRunning = true;
            t = new Thread(CreateSoldiers);
            t.Start();
        }

        public void CreateSoldiers()
        {
            s = new Soldier(general);
            s2 =new Scouts(general);

            while (_isRunning)
            {
                
            }
            //!!!!! 必须要手动 UNRegist
            s.g.rushBHandler -= s.RushBWithP90;
            s2.g.rushBHandler -= s2.RushBWithKr98k;
        }
    }
}
