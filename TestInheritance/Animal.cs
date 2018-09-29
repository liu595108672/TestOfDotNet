using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInheritance
{
    class Animal
    {
        public virtual void Say()
        {
            System.Console.WriteLine("Animal Say");

        }

        public virtual void Behavior()
        {
            Console.WriteLine("Animal Behavior");
        }
    }
}
