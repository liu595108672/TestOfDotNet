using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            Animal animal = new Animal();
            Human human = new Human();

            animal.Say();
            animal.Behavior();
            Console.WriteLine();

            human.Say();
            human.Behavior();
            Console.WriteLine();

            animal = human;
            animal.Say();
            animal.Behavior();

            Console.ReadLine();

        }
    }
}
