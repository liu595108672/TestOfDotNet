using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInheritance
{
    class Human : Animal
    {
        public override void Behavior()
        {
            Console.WriteLine("Human Behavior");
        }

        public void Say()
        {
            Console.WriteLine("Human Say");
        }
    }
}
