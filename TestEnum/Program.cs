using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WxUtilities.Extensions;

namespace TestEnum
{
    class Program
    {

        static void Main(string[] args)
        {
            byte bTest = 0x46;
            var strTest2 = "abc";
            var strTest = Encoding.ASCII.GetBytes(strTest2);
            var strtest3 = Encoding.ASCII.GetString(new byte[] { bTest });
           

            t(35);
            var bArray = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };
            var int16T = BitConverter.ToInt16(bArray, 1);
            var fValue = BitConverter.ToSingle(bArray, 1);

            //List<int> lt = new List<int>();
            //test2();




            List<ParameterFloat> listTest = new List<ParameterFloat>();
            ParameterFloat tmp1 = new ParameterFloat();
            ParameterFloat tmp2 = new ParameterFloat();
            ParameterFloat tmp3 = new ParameterFloat();

            tmp1.intOrder = 1;
            tmp2.intOrder = 2;
            tmp3.intOrder = 3;
            tmp1.strName = "test1";
            tmp2.strName = "test2";
            tmp3.strName = "test3";

            listTest.Add(tmp1);
            listTest.Add(tmp2);
            listTest.Add(tmp3);

            listTest.First(x => x.intOrder == 1).strName = "TEST1";

            foreach (var item in listTest)
            {
                Console.WriteLine(item.intOrder + "  " + item.strName);
            }



        }

        static void test1()
        {
            List<byte> bList = new List<byte>();
            bList.Add((byte)GroupId.ControllerState);
            Console.Out.WriteLine(GroupId.ControllerState);
            Enum.GetValues(typeof(GroupId));
            bList.Add(Convert.ToByte(GroupId.ControllerState));
            Console.Out.WriteLine(GroupId.Test);
            byte b = Convert.ToByte(GroupId.Test);

            byte b2 = 30;
            byte[] bytes = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            byte[] bytes2 = new byte[5];
            Array.Copy(bytes, 2, bytes2, 0, 5);
            Console.Out.WriteLine(bytes);
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);

            int intTest = queue.Dequeue();

            float f = 123.456F;
            Console.Out.WriteLine(f.ToString());
            Console.Out.WriteLine(0x29 > 0x11);
        }
        static void test2()
        {
            List<byte> bList = new List<byte>();
            requestForControlSettings(bList);
            byte[] buffer =
            {
                0x02,71,0x20,0x20,50,54,0x2E,0x03,0x0D,
                0x02,71,55,50,0x2E,50,54,0x03,0x0D,
                0x02,71,0x20,52,51,0x2E,54,0x03,0x0D,
                0x02,71,0x20,0x20,50,54,0x2E,0x03,0x0D,
            };
            string translated = Encoding.ASCII.GetString(buffer);
            int a = 4 + 256;
            byte[] b = BitConverter.GetBytes(a);
            var b2 = b.Reverse().ToArray();
            short s = 0x7B8A;
            byte[] bs = BitConverter.GetBytes(s);
            var bs2 = bs.Reverse().ToArray();

            var length = 1024 + 5;
            var bodysize = new byte[2];

            bodysize[0] = (byte)length;
            bodysize[1] = (byte)(length >> 8);


            short s2 = Convert.ToInt16(bs2);
        }
        static void requestForControlSettings(List<byte> originList)
        {

            ParameterId[] parameterIds =
            {
                ParameterId.ProtocolType, ParameterId.InterliVentController, ParameterId.Mode,
                ParameterId.FCmv, ParameterId.FSimv,ParameterId.TidalVolume,
                ParameterId.InspTime,ParameterId.PauseTime, ParameterId.FlowPattern,
                ParameterId.PressureTrigger,ParameterId.PEEPAndCPAP,ParameterId.PressuerSupport,
                ParameterId.Oxygen,ParameterId.MMVAndASV, ParameterId.InspTimeNew,
                ParameterId.PControl,ParameterId.Trigger, ParameterId.IE,
                ParameterId.PeakFlow,ParameterId.PawOrPaux, ParameterId.ETS,
                ParameterId.Ramp,ParameterId.BodyWt, ParameterId.MinVol
            };
            foreach (var tmp in parameterIds)
            {
                RequestModel requestModel = new RequestModel(tmp);
                requestModel.InsertIntoByteList(originList);
            }

        }


        static void t(byte abc)
        {
            var tt = abc;
            var ttt = abc.ToString();
            var tttt = BitConverter.ToString(new byte[] { abc });
        }



        public class RequestModel
        {
            byte[] request = { 0x02, 0x00, 0x03, 0x0D };
            public RequestModel(ParameterId pid)
            {
                request[1] = (byte)pid;
            }

            public RequestModel(byte pid)
            {
                request[1] = pid;
            }

            public byte[] GetRequest()
            {
                return request;
            }

            /// <summary>
            /// 该函数向传入的list中添加内容，最终使原List是所有命令的集合
            /// </summary>
            /// <param name="bList"></param>
            public void InsertIntoByteList(List<byte> bList)
            {
                bList.Add(request[0]);
                bList.Add(request[1]);
                bList.Add(request[2]);
                bList.Add(request[3]);
            }
        }


        public class RequestHeader
        {
            public byte Start
            {
                set => Start = 0x02;
                get => Start;
            }
        }

        public class RequestBody
        {
            public byte ParameterIdentifier{ get; set; }
        }

        public class RequestEnd
        {
            public byte EndOfText
            {
                get => EndOfText;
                set => EndOfText = 0x03;
            }

            public byte EndOfRequest
            {
                get => EndOfRequest;
                set => EndOfRequest = 0x0D;
            }
        }



    }
    enum GroupId :byte
    {
        Identifications = 0x40,
        SwVersions = 0x41,
        DateAndTime = 0x42,
        MonitoredParameters = 0x50,
        SpecialMonitoredParameters = 0x51,
        ControllerState = 0x52,
        Test = 30,

    }

    public enum ParameterId : byte
    {
        /// <summary>
        /// due to the protocol file using decimal to identify the parameterId
        /// here using the same code system (decimal)
        /// </summary>
        #region Control Settings

        ProtocolType = 30,
        InterliVentController = 31,
        Mode = 40,
        FCmv = 41,
        FSimv = 42,
        TidalVolume = 43,
        InspTime = 44,
        PauseTime = 45,
        FlowPattern = 46,
        PressureTrigger = 47,
        PEEPAndCPAP = 48,
        PressuerSupport = 49,
        Oxygen = 50,
        MMVAndASV = 51,
        InspTimeNew = 59,
        PControl = 87,
        Trigger = 104,
        IE = 105,
        PeakFlow = 106,
        PawOrPaux = 107,
        ETS = 108,
        Ramp = 109,
        BodyWt = 110,
        MinVol = 111,
        #endregion

        #region Monitored Parameters

        BreathingTime = 29,
        PetCO2 = 35,
        SpO2 = 36,
        Pulse = 37,
        HLI = 38,
        VariablityIndex = 39,
        PetCo2kPa = 58,
        InspVolume = 60,
        ExpVolume = 61,
        VexpAndMin = 62,
        fTotal = 63,
        fSpont = 64,
        IERatio = 65,
        PMax = 66,
        pMean = 67,
        PEEPAndCPAPMonitored = 68,
        PPlateau = 69,
        tExpPat = 70,
        OxygenMonitoered = 71,
        RInsp = 72,
        RExp = 73,
        Compliance = 74,
        InspFlow = 75,
        VTInspMandatory = 76,
        VTInspSpont = 77,
        VTExpMandatory = 78,
        VTExpSpont = 79,
        AutoPEEP = 103,
        PMin = 112,
        InspTimeMonitored = 114,
        VtLeak = 114,
        P01 = 115,
        ExpFlow = 116,
        RCexp = 117,
        RCinsp = 118,
        WOB = 119,
        PTP = 121,
        PInsp = 122,
        #endregion
    }





    class ParameterFloat
    {
        public int intOrder { set; get; }
        public string strName { set; get; }
    }
}
