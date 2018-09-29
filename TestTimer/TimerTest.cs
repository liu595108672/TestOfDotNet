using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;

namespace TestTimer
{
    public class TimerTest
    {

        public Timer t;
        public int count=0;
        public ConcurrentQueue<int> _queue = new ConcurrentQueue<int>();
        public TimerTest()
        {
            t = new Timer(TimerTick,null,0,500);
        }

        public void TimerTick(object obj)
        {
            for (int i = 0; i < 3000; i++)
            {
                _queue.Enqueue(i);
            }
            count = _queue.Count;
        }

    }

    public class Test<T> {
        public List<T> tList = new List<T>();
    }


}
