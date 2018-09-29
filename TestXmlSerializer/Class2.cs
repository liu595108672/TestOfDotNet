//using System.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Xml.Linq;
//using System.Xml.XPath;
//using System.Xml;
//using TryAnalysisXML;

//namespace TestXmlSerializer
//{
//    class Class2
//    {
//        static void Main(string[] args)
//        {
//            testTryAnalysisXML(@"E:\2015031012.xml");

//            int[] arr = new int[] { 0 };
//            XNamespace ns = "urn:hl7-org:v3";
//            string filePath = @"E:\2015031012.xml";
//            XElement root = XElement.Load(filePath);
//            XmlNameTable nameTable = XmlReader.Create(filePath).NameTable;
//            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);
//            namespaceManager.AddNamespace("ns", "urn:hl7-org:v3");

            
//            XElement patientInfo = root.XPathSelectElement("//ns:subjectDemographicPerson", namespaceManager);
//            XElement dataSequenceSet  = root.XPathSelectElement("ns:component/ns:series//ns:sequenceSet",namespaceManager);
//            string leadCode = "MDC_ECG_LEAD_I";
//            XElement tmp = dataSequenceSet.XPathSelectElement(@"ns:component/ns:sequence/ns:code[@code='" + leadCode + "']", namespaceManager);
//            XElement digits = dataSequenceSet.XPathSelectElement("//ns:digits", namespaceManager);
//            string strDigits = digits.Value;
//            string [] strDigitsArr = strDigits.Split(' ');
//            int[] intDigitsArr = new int[strDigitsArr.Length];
//            int k = 0;
//            foreach(var i in strDigitsArr)
//            {
//                if (String.IsNullOrWhiteSpace(i))
//                {
//                    break;
//                }
//                else
//                {
//                    intDigitsArr[k++] = int.Parse(i);
//                }
//            }




//            XElement tmp2 = dataSequenceSet.XPathSelectElement(@"ns:component/ns:sequence/ns:code[@code='" + leadCode + "']", namespaceManager);
//            XElement digits2 = dataSequenceSet.XPathSelectElement("//ns:digits", namespaceManager);
//            XElement xelOriginOfDigits2 = digits2.XPathSelectElement("//ns:origin", namespaceManager);
//            xelOriginOfDigits2.Attribute("value");



//            var testNodes = patientInfo.XPathEvaluate("//@partType", namespaceManager);
//            testAppConfigurationManager();


//        }

//        /// <summary>
//        /// 测试  XMLAnalysis 工具类
//        /// </summary>
//        /// <param name="filePath"></param>
//        static void testTryAnalysisXML(string filePath)
//        {
//            XMLAnalysisTool tool = new XMLAnalysisTool(filePath,12);
//            Lead l = tool.GetLeadByLeadCode("MDC_ECG_LEAD_I");
//            List<Lead> ls = tool.GetLeadsDataFromXMLAll();
//            XLeadsElectrocarDiogram xECG = tool.AnalysisXLeadsECGStandard();
//            XLeadsElectrocarDiogram xECG2 = tool.AnalysisXLeadsECGAll();

//            tool.FilePath = @"E:\2015031014.xml";
//            XLeadsElectrocarDiogram xECG3 = tool.AnalysisXLeadsECGAll();


//        }
//        static void testAppConfigurationManager()
//        {

//            try
//            {
//                var appSettings = ConfigurationManager.AppSettings;

//                if (appSettings.Count == 0)
//                {
//                    Console.WriteLine("AppSettings is empty.");
//                }
//                else
//                {
//                    Console.WriteLine("this is my test:"+appSettings["LED1"]);

//                    foreach (var key in appSettings.AllKeys)
//                    {
//                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
//                    }
//                }
//            }
//            catch (ConfigurationErrorsException)
//            {
//                Console.WriteLine("Error reading app settings");
//            }
//        }
//    }
//}
