using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Text.RegularExpressions;
using NLog;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TestForJson
{
    public class TestDynamicObject
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private string _NsNamespaceInusing = "ns:";
        private const string testConst = "abcd";

        public void JsonToDynamic(string strJson)
        {
            
            var tmp = JsonConvert.DeserializeObject<dynamic>(strJson);
            JObject tmp1 = tmp.Required.Leads;
            Console.WriteLine(tmp1.GetType());


            var tmpIncrement = (JObject)tmp.Required.Leads.TimeAbsolute.Increment;
            string tmpValue= GetItemValue(tmpIncrement["Value"].ToString(), tmpIncrement["privatePrefix"].ToString());
            string tmpUnit = GetItemValue(tmpIncrement["Unit"].ToString(), tmpIncrement["privatePrefix"].ToString());






            var additionRoot = tmp.AdditionInfo.ECGAbout;
            //暂时不考虑单位的问题，而且其实单位应该也不会是问题，大部分情况都是统一的单位
            GetItemValue(additionRoot.Heart_rate.Value.ToString(), additionRoot.Heart_rate.privatePrefix.ToString());


            foreach (JProperty item in tmp1.Children())
            {
                if (item.Name.Contains("LEAD"))
                {
                    foreach (JProperty item2 in item.Value)
                    {
                        if (item2.Name == "Origin")
                        {
                            var valuePath = item2.Value["Value"];
                        }

                        if (item2.Name == "Digits")
                        {
                            var digitsPath = item2.Parent["privatePrefix"].ToString();
                        }
                    }
                }
            }




            var tmpJobj = (JObject)tmp.Required.Leads;
            int count1=0, count2 = 0;
            foreach (var item in tmpJobj)
            {
               
                if (item.Key.Contains("LEAD"))
                {
                    foreach (JProperty item2 in item.Value)
                    {
                        if (item2.Name.Contains("Origin"))
                        {
                            string originUnit = item2.Value["Unit"].ToString();
                        }

                        if (item2.Name.Contains("Digits"))
                        {
                            string digitPath = item2.Value.ToString();
                        }
                    }
                }   
            }

            
            var testSelector = GetTargetSelector(tmp.Required.Patient.BirthDay.Value, tmp.Required.Patient.privatePrefix.Value);
            GetNodeFromXml("test.xml", testSelector);
        }

        private TargetSelector GetTargetSelector(string strPath, string prefix = "")
        {
            TargetSelector selectorToReturn = new TargetSelector();
            string tmpNodePath = string.Empty;
            string tmpAttributeName = string.Empty;
            //Due to it is not used so frequently, this way to combian string will not use much menmory or calculate resource
            try
            {
                tmpNodePath = Regex.Replace(prefix + strPath, @"//(?=\w+)", @"//ns:" );
                tmpNodePath = Regex.Replace(tmpNodePath, @"(?<!/)/(?=\w+)", @"/ns:");

                tmpNodePath = Regex.Replace(tmpNodePath, @"@\w+$", "");
                tmpAttributeName = Regex.Match(strPath, @"(?<=@)\w+$").Value;

                selectorToReturn.NodePath = tmpNodePath ;
                selectorToReturn.Attribute = tmpAttributeName;
            }
            catch (ArgumentException e)
            {
                logger.Error("ArgumentExcetpion occurs while combining XPath path.");
                logger.Error("Exception message is: {0}", e.Message);
                logger.Error("Three para coming into function are: strPath:{0} , prefix:{1} ", strPath, prefix, );
                logger.Error("The middle products are: tmpNodePath:{0} , tmpAttribute:{1}", tmpNodePath, tmpAttributeName);
                logger.Error("Throw this exception to parent...");
                throw e;
            }
            catch (Exception e)
            {
                logger.Error("Unexpected exception occurs while combining XPath path.");
                logger.Error("Exception message is: {0}", e.Message);
                logger.Error("The middle products are: tmpNodePath:{0} , tmpAttribute:{1}", tmpNodePath, tmpAttributeName);
                logger.Error("Throw this exception to parent...");
                throw e;
            }
            return selectorToReturn;
        }

        private void GetNodeFromXml(string filePath,TargetSelector selector)
        {
            XmlNameTable nameTable = XmlReader.Create(filePath).NameTable;
            XmlNamespaceManager nsManager = new XmlNamespaceManager(nameTable);
            nsManager.AddNamespace("ns", "urn:hl7-org:v3");
            XElement xElement = XElement.Load(filePath);

            var tmp = xElement.XPathSelectElement(selector.NodePath,nsManager)?.Attribute(selector.Attribute)?.Value;
            

        }

        protected string GetItemValue(string strPath, string prefix = "")
        {
            var strToReturn = string.Empty;
            var targetSelector = GetTargetSelector(strPath, prefix);

            if (string.IsNullOrEmpty(strToReturn))
            {
                logger.Warn("The result is null or empty by using nodePath:{0} ,attribute:{1}", targetSelector.NodePath, targetSelector.Attribute);
            }
            return strToReturn;
        }
    }


    public class TargetSelector
    {
        public string NodePath { set; get; }
        public string Attribute { set; get; }
    }

}
