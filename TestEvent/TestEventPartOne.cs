using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvent.PartOne
{
    public delegate void RushBHandler();

    public class General
    {
        public event RushBHandler rushBHandler;
        
        public void GoRushB()
        {
            rushBHandler?.Invoke();
        }
    }
}
