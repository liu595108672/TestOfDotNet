using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvent
{
    public delegate void MyEventHandler();
    public class DelegateTest
    {
        
        public DelegateTest()
        {
            MyEventHandler myDelegate = new MyEventHandler(SomeFunctions.BuyTicket);
            myDelegate();
            Console.WriteLine("******");
            myDelegate += SomeFunctions.BuyDrink;
            myDelegate();
            Console.WriteLine("******");
            myDelegate -= SomeFunctions.BuyTicket;
            myDelegate();
            Console.WriteLine("******");
            Console.WriteLine(myDelegate.ToString());
            Console.WriteLine("******");
            myDelegate.Invoke();

        }
    }

    public class SomeFunctions
    {
        public static void BuyTicket()
        {
            Console.WriteLine("Buy Ticket!");
        }

        public static void BuyDrink()
        {
            Console.WriteLine("Buy Drink!");
        }
    }
}
