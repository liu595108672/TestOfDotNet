using System.Threading;
using System.IO.Ports;

namespace TestSerialPort
{
    class TestSerialPortTry
    {
        SerialPort com = new SerialPort
        {
            PortName = "COM1",
            BaudRate = 9600,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One,
            RtsEnable = true,
            ReadTimeout = 1000
        };

        SerialPort port;
        public void TestSerialPort()
        {
            //TODO 测一下引用的形式的话，是否会被析构
            TestReferance(com);
            com.PortName = "COM2";
            TestReferance2();


            try
            {
                port.Open();
            }
            catch(System.TypeInitializationException e)
            {

            }
            catch (System.NullReferenceException e)
            {

            }
            catch (System.Exception e)
            {
                string mes = e.Message;
            }
            //com.Open();
            //while (true)
            //{
            //    var bLength = com.BytesToRead;
            //    byte[] byy=new byte[bLength];
            //    com.Read(byy, 0, bLength);

            //    Thread.Sleep(1000);
            //}
        }

        public void TestReferance(SerialPort serialPort) 
        {
            port = serialPort;
        }

        public void TestReferance2()
        {
            try
            {
                port.Open();
            }
            catch (System.Exception e)
            {

            }
        }
    }
}
