//using System.Linq;
//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.XPath;
//using System;

//namespace TestXmlSerializer
//{
//    class Class1
//    {
//        static void Main(string[] args)
//        {
//            try
//            {
//                string fileName = @"E:\2015031005.xml";
//                XmlNameTable nameTable = XmlReader.Create(fileName).NameTable;
//                XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);
//                namespaceManager.AddNamespace("ns", "urn:hl7-org:v3");

//                XElement root = XElement.Load(fileName);
//                var nodes = root.XPathSelectElements(
//                    "digits",
//                    namespaceManager);
//                XNamespace ns1 = "urn:hl7-org:v3";
//                var node = root.XPathSelectElement("digits", namespaceManager);
//            }
//            catch (Exception e)
//            {
//                System.Console.Out.WriteLine(e.Message);

//            }
//        }
//        public void test(string[] args)
//        {
//            XNamespace ns = "urn:hl7-org:v3";
//            string fileName = @"E:\2015031005.xml";
//            XElement root = XElement.Load(fileName);
//            XmlNameTable nameTable = XmlReader.Create(fileName).NameTable;
//            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);
//            namespaceManager.AddNamespace("ns", "urn:hl7-org:v3");
//            var nodes = root.XPathSelectElements(
//                "ns:component/ns:series/ns:component/ns:sequenceSet/ns:component/ns:sequence/ns:code[@code='MDC_ECG_LEAD_I']", 
//                namespaceManager);
//            XNamespace ns1 = "urn:hl7-org:v3";
//            var node = nodes.FirstOrDefault();
//            string s = node.Parent.Element(ns1 + "value").Element(ns1 + "digits").Value;
       

//        }
//    }
//}
