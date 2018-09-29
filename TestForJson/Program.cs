using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
namespace TestForJson
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("abcdefghijklmnop");
            sb.Remove(0, 4);




            string fileName = "TestJosnToDynamic.json";
            var fileContent = File.ReadAllText(fileName);

            string fileName2 = "test2.xml";
            var root = XElement.Load(fileName2);

            var xml2Json = JsonConvert.SerializeObject(root);

            var json2Obj = JsonConvert.DeserializeObject<dynamic>(xml2Json);

            TestDynamicObject testDynamic = new TestDynamicObject();
            testDynamic.JsonToDynamic(fileContent);



            EdwardIFMoutConfigHelper configHelper = new EdwardIFMoutConfigHelper();
            configHelper.Initialize();

            var t = JsonConvert.SerializeObject(configHelper);
            Console.WriteLine(t);

            JsonTestOne jsonTestOne = new JsonTestOne();
            jsonTestOne.TestParseJsonList();

        }
     


      
    }

    public class EdwardIFMoutConfigHelper
    {
        public List<ProtocalItem> normalItems = new List<ProtocalItem>();
        public List<ProtocalItem> addtionItems = new List<ProtocalItem>();

        public void Initialize()
        {
            AddNormalParameter("A", "SaO2: Arterial Oxygen Saturation", "%");
            AddNormalParameter("a", "SNR: Signal-to-Noise Ratio", "dB");
            AddNormalParameter("B", "BT: Blood Temperature", "degrees C");
            AddNormalParameter("b", "SVV: Stroke Volume Variation", "%");
            AddNormalParameter("C", "CO: Continuous Cardiac Output", "L/min");
            AddNormalParameter("c", "CI: Continuous Cardiac Output Index", "L/min/m^2");
            AddNormalParameter("D", "DO2: Oxygen Delivery", "mL O2/min");
            AddNormalParameter("E", "RVEF: Right Ventricular Ejection Fraction", "%");
            AddNormalParameter("e", "RVEF STAT: Right Ventricular Ejection Fraction STAT token", "%");

            #region Fault
            AddNormalParameter("F", "CO/TD Bolus Fault token and Fault number", "");
            AddNormalParameter("f", "Oximetry Fault token and Fault number", "");
            AddNormalParameter("L", "", "");
            AddNormalParameter("l", "", "");
            AddNormalParameter("M", "", "");
            AddNormalParameter("m", "", "");
            AddNormalParameter("N", "", "");
            #endregion 

            AddNormalParameter("H", "HRavg: Average Heart Rate", "bpm");
            AddNormalParameter("I", "ICO: Bolus Cardiac Output", "L/Min");
            AddNormalParameter("i", "ICI: Bolus Cardiac output Index", "L/min/m^2");
            AddNormalParameter("J", "EDV: End Diastolic Volume", "mL");
            AddNormalParameter("j", "EDVI: End Diastolic Volume Inex", "mL/m^2");
            AddNormalParameter("K", "ESV: End Systolic Volume", "mL");
            AddNormalParameter("k", "ESVI: End Systolic Volume Index", "mL/,^2");
            AddNormalParameter("O", "VO2: Oxygen Consumption", "mL O2/min");
            AddNormalParameter("P", "MAP: Mean Arterial Pressure", "mmHg");
            AddNormalParameter("p", "CVP: Central Venous Pressure", "mmHg");
            AddNormalParameter("Q", "SQI: Oximetry Signal Quality Indicator", "");
            AddNormalParameter("R", "SVR: Systemic Vascular Resistance", "dn-s/cm^5");
            AddNormalParameter("r", "SVRI: Systemic Vascular Resistance Index", "dn-s-m^2/cm^5");
            AddNormalParameter("S", "SV: Stroke Volume", "mL/beat");
            AddNormalParameter("s", "SVI: Stroke Volume Index", "mL/beat/m^2");
            AddNormalParameter("T", "CO: Cardiac Output", "L/min");
            AddNormalParameter("t", "CI: Cardiac Output index", "L/min/m^2");
            AddNormalParameter("U", "EDV: End Diastolic volume", "mL/m^2");
            AddNormalParameter("V", "SvO2: Mixed Venous Oxygen Saturation", "%");
            AddNormalParameter("v", "ScvO2: Central Venous Oxygen Saturation", "%");
            AddNormalParameter("W", "SV: Stroke Volume STAT", "mL/beat");
            AddNormalParameter("w", "SVI: Stroke Volume Index STAT", "mL/beat/m^2");
            AddNormalParameter("X", "O2EI: Oxygen Extraction Index", "%");


            AddNormalParameter("Y", "Addtional Parameters", "");
            AddNormalParameter("y", "Addtional Faults/Alerts/Alarms", "");
            #region Addition Parameter
            //TODO 暂时不对这部分做实现，额外参数只有EV1000这种型号的机器才有并且，对应的参数很多，增加起来还很麻烦
            AddAddtionalParameter("00", "", "");
            #endregion

        }



        public void AddNormalParameter(string identity, string description, string unit)
        {
            normalItems.Add(new ProtocalItem(identity, description, unit));
        }

        public void AddAddtionalParameter(string identity,string description,string unit)
        {
            addtionItems.Add(new ProtocalItem(identity, description, unit));
        }
    }


    /// <summary>
    /// 该类型给出  Identity description Unit  解析方式*
    ///     
    ///     ！！考虑如何在本类型中给出解析方式
    ///     目前只对数值进行解析
    /// </summary>
    public class ProtocalItem
    {
        public ProtocalItem(string identity, string description, string unit)
        {
            Identity = identity;
            Description = description;
            Unit = unit;
        }

        public string Description { set; get; }

        public string Identity { set; get; }

        public string Unit { set; get; }
    }

}
