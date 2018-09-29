using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEvent.PartOne;

namespace TestEvent.PartTwo
{
    public class TestEventPartTwo
    {
    }

    public class Soldier
    {
        public General g { set; get; }

        public Soldier()
        {

        }
        public Soldier(General g)
        {
            this.g = g;
            g.rushBHandler += RushBWithP90;
        }

        public void RushBWithP90()
        {
            Console.WriteLine("Let's rush B whit P90!");
        }
    }

    public class Scouts
    {
        public General g { set; get; }
        public Scouts()
        {

        }
        public Scouts(General general)
        {
            g = general;
            g.rushBHandler += RushBWithKr98k;
        }

        public void RushBWithKr98k()
        {
            Console.WriteLine("Let's rush B with Kr98k!");
        }
    }

}
