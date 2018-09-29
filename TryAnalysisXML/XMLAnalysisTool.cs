using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System;
using System.Configuration;

namespace TryAnalysisXML
{
    /// <summary>
    /// 本类提供对XML文件进行解析的方法   
    /// 因为HL7标准当中有许多可选项，所以在搭建本类的时候力求灵活  （要提供必要的重载）
    /// 由于不同机器的原始数据格式不一样，所以针对不同机器需要进行不同的转换，故在这里不做不同机器的应对（应对环节交由对机器源数据转换）
    /// </summary>
    public class XMLAnalysisTool
    {

        public int LeadsNum { set; get; }
        public string FilePath { set; get; }
        public XElement Root { set; get; }
        //有没有简单的写法，给 Ns 设定default值？
        public XNamespace Ns { set; get; }
        private XLeadsElectrocarDiogram xLeadsElectrocarDiogram { set; get; }
        private XmlNamespaceManager nsManager {  get; }
        private XmlNameTable nameTablle { get; }


        //************************************** V 开放函数区
        //TODO 如果有后续版本进行开发的话,考虑添加初始化等函数 用来对已经声明的tool进行资源释放等
        public XMLAnalysisTool()
        {
            LeadsNum = 12;
            Ns = @"urn:hl7-org:v3";
        }
        public XMLAnalysisTool(string filePath)
        {
            FilePath = filePath;
            Ns = "urn:hl7-org:v3";
            nameTablle = XmlReader.Create(FilePath).NameTable;
            nsManager.AddNamespace("ns", "urn:hl7-org:v3");

            Root = XElement.Load(FilePath);
            LeadsNum = 12;
        }
        public XMLAnalysisTool(string filePath, int leadsNum)
        {
            FilePath = filePath;
            Ns = "urn:hl7-org:v3";
            nameTablle = XmlReader.Create(FilePath).NameTable;
            nsManager = new XmlNamespaceManager(nameTablle);
            nsManager.AddNamespace("ns", "urn:hl7-org:v3");

            Root = XElement.Load(FilePath);
            LeadsNum = leadsNum;
        }
        public XMLAnalysisTool(string filePath, int leadsNum, string xmlns)
        {
            FilePath = filePath;
            Ns = xmlns;
            nameTablle = XmlReader.Create(FilePath).NameTable;
            nsManager = new XmlNamespaceManager(nameTablle);
            nsManager.AddNamespace("ns", xmlns);

            Root = XElement.Load(FilePath);
            LeadsNum = leadsNum;
        }
        
        /// <summary>
        /// //TODO 从XML中获取病人信息
        /// </summary>
        /// <returns></returns>
        public PatientInfo GetPatientInfoFromXML()
        {
            return null;
        }

        /// <summary>
        /// 根据LeadsNum返回标准导联  
        /// 注：LeadsNum 默认值为12
        /// </summary>
        /// <param name="leadCode"></param>
        /// <returns></returns>
        public List<Lead> GetLeadsDataFromXMLStandard()
        {
            LeadsNum = (LeadsNum == 0 ? 12 : LeadsNum);
            List<Lead> leadListToReturn = new List<Lead>();
            List<string> leadCodeList = GetECGLeadsCode();
            for (int i = 0; i < LeadsNum; i++)
            {
                Lead tmp = GetLeadByLeadCode(leadCodeList[i]);
                leadListToReturn.Add(tmp);
            }
            return leadListToReturn;
        }

        /// <summary>
        /// 忽略LeadsNum的限制，返回XML文件当中所有的导联数据,并将导联数修改为XML文件当中的导联数量
        /// </summary>
        /// <returns></returns>
        public List<Lead> GetLeadsDataFromXMLAll()
        {

            List<Lead> leadListToReturn = new List<Lead>();
            XElement sequenceSet = GetDataSequenceSet();
            XElement tmp = sequenceSet.XPathSelectElement("ns:component", nsManager);
            do  //由于 sequenceSet 的第一个子节点是 绝对时间，所以使用do while方式将该节点现行过滤掉
            {
                tmp = (XElement)tmp.NextNode;
                leadListToReturn.Add(GetLeadByLeadXElement(tmp));

            } while (null != tmp.NextNode);


            return leadListToReturn;
        }

        /// <summary>
        /// 通过导联代号获取对应导联数据（返回值为导联对象）
        /// 注：当XML文件当中没有对应的记录时，仍然会返回一个对象，但是该对象中digits 为长度为1的数组
        /// </summary>
        /// <param name="leadCode"></param>
        /// <returns></returns>
        public Lead GetLeadByLeadCode(string leadCode)
        {
            Lead leadToReturn = new Lead();
            try
            {
                //寻找这个lead对应的节点
                XElement sequenceSet = GetDataSequenceSet();
                XElement tmp = sequenceSet.XPathSelectElement(@"ns:component/ns:sequence/ns:code[@code='" + leadCode + "']", nsManager);
                //判断获取是否为空   为空则说明xml文件中没有对应的记录
                if (null == tmp)
                {
                    leadToReturn.Code = leadCode;
                    leadToReturn.Digits = new int[1];
                    leadToReturn.Scale = new Scale();
                    leadToReturn.Origin = new Origin();
                }
                else
                {
                    XElement theLead = tmp.Parent;

                    leadToReturn.Code = GetLeadCodeByXElement(theLead);
                    leadToReturn.Digits = GetDigitsByXElement(theLead);
                    leadToReturn.Scale = GetScaleByXElement(theLead);
                    leadToReturn.Origin = GetOriginByXElement(theLead);
                }
            }
            catch (Exception e)
            {
                //输出error信息 或者写到log日志里面
                Console.WriteLine(e.Message);

                leadToReturn.Digits = new int[] { 0 };
                leadToReturn.Code = "发生错误：" + e.Message;
                leadToReturn.Scale = null;
                leadToReturn.Origin = null;
            }
            return leadToReturn;
        }

        /// <summary>
        /// 用来获取导联数据组  
        /// 注意,这里只获取导联数据组,不获取拓展序列
        /// </summary>
        /// <returns></returns>
        public XElement GetDataSequenceSet()
        {
            return Root.XPathSelectElement("ns:component/ns:series//ns:sequenceSet", nsManager);
        }

        /// <summary>
        /// 获取 标准 X导联的心电图数据(包含时间等内容)
        /// 由于多少导联是根据XML文件来确定的，外界人为确定导联数是不合理的！所以这里不传入导联数（LeadsNum）
        /// </summary>
        /// <returns></returns>
        public XLeadsElectrocarDiogram AnalysisXLeadsECGStandard()
        {
            xLeadsElectrocarDiogram = new XLeadsElectrocarDiogram();
            xLeadsElectrocarDiogram.AbsoluteTime = GetECGAbsoluteTime();
            xLeadsElectrocarDiogram.Increment = GetECGIncrement();
            xLeadsElectrocarDiogram.Leads = GetLeadsDataFromXMLStandard();
            return xLeadsElectrocarDiogram;
        }

        /// <summary>
        /// 获取 XML文件中所有导联心电图数据（包含时间等内容）
        /// </summary>
        /// <returns></returns>
        public XLeadsElectrocarDiogram AnalysisXLeadsECGAll()
        {
            xLeadsElectrocarDiogram = new XLeadsElectrocarDiogram();
            xLeadsElectrocarDiogram.AbsoluteTime = GetECGAbsoluteTime();
            xLeadsElectrocarDiogram.Increment = GetECGIncrement();
            xLeadsElectrocarDiogram.Leads = GetLeadsDataFromXMLAll();
            return xLeadsElectrocarDiogram;
        }




        //**************************************** V 私有工具函数区域
        /// <summary>
        /// 根据XElement获取导联名称
        /// </summary>
        /// <param name="theLead"></param>
        /// <returns></returns>
        private string GetLeadCodeByXElement(XElement theLead)
        {
            var leadCodeElemnt = theLead.XPathSelectElement(".//ns:code", nsManager);
            string leadCode = leadCodeElemnt.Attribute("code").Value;
            return leadCode;
        }

        /// <summary>
        /// 根据XElement节点获取Lead对象
        /// </summary>
        /// <param name="theLead"></param>
        /// <returns></returns>
        private Lead GetLeadByLeadXElement(XElement theLead)
        {
            return GetLeadByLeadCode(GetLeadCodeByXElement(theLead));
        }
        /// <summary>
        /// 获取基线值(开始时间相对于记录的延迟时间)
        /// </summary>
        /// <param name="theLead"></param>
        /// <returns></returns>
        private Origin GetOriginByXElement(XElement theLead)
        {
            Origin originToReturn = new Origin();
            XElement tmp = theLead.XPathSelectElement(".//ns:origin", nsManager);
            originToReturn.value = tmp.Attribute("value").Value;
            originToReturn.unit = tmp.Attribute("unit").Value;
            return originToReturn;
        }

        /// <summary>
        /// 获取分辨率(倍率)
        /// </summary>
        /// <param name="theLead"></param>
        /// <returns></returns>
        private Scale GetScaleByXElement(XElement theLead)
        {
            Scale scaleToReturn = new Scale();
            XElement tmp = theLead.XPathSelectElement(".//ns:scale", nsManager);
            scaleToReturn.value = tmp.Attribute("value").Value;
            scaleToReturn.unit = tmp.Attribute("unit").Value;
            return scaleToReturn;
        }

        /// <summary>
        /// 用来将导联数据从  一个string   最终转成   一个int数组
        /// 输入参数为该导联节点
        /// </summary>
        /// <param name="theLead"></param>
        /// <returns></returns>
        private int[] GetDigitsByXElement(XElement theLead)
        {
            string strDigits = theLead.XPathSelectElement(".//ns:digits", nsManager).Value;
            string[] strDigitsArr = strDigits.Trim().Split(' ');
            int[] intDigitsArr = new int[strDigitsArr.Length];
            int k = 0;
            foreach (var i in strDigitsArr)
            {
                if (String.IsNullOrWhiteSpace(i))
                {
                    break;
                }
                else
                {
                    intDigitsArr[k++] = int.Parse(i);
                }
            }
            return intDigitsArr;
        }

        /// <summary>
        /// 获取开始时间（head）
        /// </summary>
        /// <returns></returns>
        private string GetECGAbsoluteTime()
        {
            XElement timeInfo = GetDataSequenceSet().XPathSelectElement(".//ns:code[@code='TIME_ABSOLUTE']", nsManager);
            XElement tmpParent = timeInfo.Parent;
            return tmpParent.XPathSelectElement(".//ns:head", nsManager).Attribute("value").Value;
        }

        /// <summary>
        /// 获取频率（时间间隔）
        /// </summary>
        /// <returns></returns>
        private Increment GetECGIncrement()
        {
            Increment incrementToReturn = new Increment();
            XElement timeInfo = GetDataSequenceSet().XPathSelectElement(".//ns:code[@code='TIME_ABSOLUTE']", nsManager).Parent;
            XElement tmpIncrement = timeInfo.XPathSelectElement(".//ns:increment", nsManager);
            incrementToReturn.unit = tmpIncrement.Attribute("unit").Value;
            incrementToReturn.value = tmpIncrement.Attribute("value").Value;

            return incrementToReturn;
        }

        /// <summary>
        /// 从配置文件当中读取所有被配置的导联代号
        /// </summary>
        /// <returns></returns>
        private List<string> GetECGLeadsCode()
        {
            List<string> listToReturn = new List<string>();
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count == 0)
                {
                    throw new Exception("配置文件中未配置导联名称");
                }
                //TODO  这里只是将配置文件中所有的leadCode都读进来，正式的时候应当作区分
                foreach (var key in appSettings.AllKeys)
                {
                    listToReturn.Add(appSettings[key]);
                }

            }
            catch (Exception e)
            {
                //TODO 这里对异常进行处理  做日志记录的话，可以考虑使用 log4
                Console.Out.WriteLine(e.Message);
            }

            return listToReturn;
        }

    }
}
