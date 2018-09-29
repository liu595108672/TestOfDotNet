//using System;
//using System.Xml.Serialization;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using System.Runtime.InteropServices.ComTypes;
//using System.Threading;

//namespace TestXmlSerializer
//{
//    class Program
//    {
//        //static void Main(string[] args)
//        //{
//        //    //声明一个猫咪对象
//        //    var c = new Cat { Color = "White", Speed = 10, Saying = "White or black,  so long as the cat can catch mice,  it is a good cat" };

//        //    //序列化这个对象
//        //    XmlSerializer serializer = new XmlSerializer(typeof(Cat));

//        //    //将对象序列化输出到控制台
//        //    //serializer.Serialize(Console.Out, c);

//        //    try
//        //    {
//        //        FileStream fs = new FileStream(@"E:\TestXml.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
//        //        XmlSerializer xs = new XmlSerializer(typeof(Cat));
//        //        xs.Serialize(fs, c);
//        //        xs.Serialize(Console.Out, c);
//        //        fs.Seek(0, SeekOrigin.Begin);
//        //        Cat c2 = (Cat)xs.Deserialize(fs);
//        //        fs.Close();

//        //        FileStream fs1 = new FileStream(@"E:\TestXml.xml", FileMode.Open);
//        //        Cat c1 = (Cat)xs.Deserialize(fs1);
//        //        System.Console.Out.WriteLine(c1.Age);
//        //        fs1.Close();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine(ex.Message);
//        //    }



//        //    XmlSerializer serializer2 = new XmlSerializer(typeof(Cat));
//        //    Console.Read();
//        //}

//        static void Main(string[] args)
//        {
//            string test = "abcdefgh #D  EOT";
//            string refer = "#D;EOD";
//            Console.Out.WriteLine(test.Contains(refer));

//            TestDelegate t = new TestDelegate();
//            t.d1 += t.Out1;
//            t.d1 += t.Out2;
//            t.RunningDelegate1();


//            t.d2 += t.Out2;
//            t.d2 += t.Out1;
//            t.RunningDelegate2();

//            t.d3 += TestReturnInt1;
//            t.d3 += TestReturnInt3;
//            t.d3 += TestReturnInt2;
//            t.RunningDelegate3();

//            TestThread testThread = new TestThread();
//            testThread.TestThreadFunction();
//        }

//        public static int TestReturnInt1()
//        {
//            Console.Out.WriteLine("int 1");
//            return 1;
//        }
//        public static int TestReturnInt2()
//        {
//            Console.Out.WriteLine("int 2");
//            return 2;
//        }
//        public static int TestReturnInt3()
//        {
//            Console.Out.WriteLine("int 3");
//            return 3;
//        }
//    }

//    [XmlRoot("cat")]
//    public class Cat
//    {
//        //定义Color属性的序列化为cat节点的属性
//        [XmlAttribute("color")]
//        public string Color { get; set; }

//        //要求不序列化Speed属性
//        [XmlIgnore]
//        public int Speed { get; set; }

//        //设置Saying属性序列化为Xml子元素
//        [XmlElement("saying")]
//        public string Saying { get; set; }

//        [XmlAttribute("age")]
//        public int Age { get; set; }
//    }

//    public class TestThread
//    {
//        public TestThread() { }

//        public void TestThreadFunction()
//        {
//            Thread t = new Thread(SomeAction){Name = "thread1"};
//            Thread t2 = new Thread(SomeAction2){Name = "thread2"};

//            t.Start();
//            t2.Start();
//            for (int index = 0; index < 10; index++)
//            {
//                Console.Out.WriteLine(" this is in TestThreadFunction  " +Thread.CurrentThread.Name);
//                if (index == 4)
//                {
//                    t.Join();
//                }
//                Thread.Sleep(100);
//            }

//        }
//        public void SomeAction()
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                Console.Out.WriteLine("this is in SomeAction  "+Thread.CurrentThread.Name); 
//                Thread.Sleep(100);
//            }
//        }

//        public void SomeAction2()
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                Console.Out.WriteLine("this is in SomeAction2  " +Thread.CurrentThread.Name);
//                Thread.Sleep(100);
//            }
//        }
//    }

//    public class TestDelegate
//    {
//        public delegate void delegate1();

//        public delegate void delegate2();

//        public delegate int Delegate3();

//        public delegate1 d1;
//        public delegate2 d2;
//        public Delegate3 d3;

//        public void RunningDelegate1()
//        {
//            d1.Invoke();
//        }

//        public void RunningDelegate2()
//        {
//            d2.Invoke();
//        }

//        public void RunningDelegate3()
//        {
//            d3.Invoke();
//        }
//        public void Out1()
//        {
//            Console.Out.WriteLine("Out function1");
//        }

//        public void Out2()
//        {
//            Console.Out.WriteLine("Out function2");
//        }

//        public void Out3()
//        {
//            Console.Out.WriteLine("Out Function3");
//        }
//    }

//}

