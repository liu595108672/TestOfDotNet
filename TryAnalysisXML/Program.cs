
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace TryAnalysisXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XNamespace ns = "urn:hl7-org:v3";
            string fileName = @"E:\2015031012.xml";
            XmlNameTable nameTable = XmlReader.Create(fileName).NameTable;
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);

            XElement root = XElement.Load(fileName);



        }

      
    }

    /// <summary>
    /// 病人的身份信息，简单直白
    /// </summary>
    public class PatientInfo
    {

        public string ID { set; get; }
        public string Code { set; get; }
        public string BirthTime { set; get; }
        //若FamilyName 和 GivenName 被合并记录，将记录值赋给FamilyName  GivenName不进行赋值
        public string FamilyName { set; get; }
        public string GivenName { set; get; }
        public string Gender { set; get; }
        //种族代码，详细对照参阅HL7标准手册
        public string raceCode{ set; get; }
    }

    /// <summary>
    /// 此类型为单一导联通道的抽象，不包含起止时间等内容
    /// 起止时间，记录间隔等数据在确定导联的类中进行记录
    /// </summary>
    public class Lead
    {
        //由代码系统为每种通道提供统一代码，可以作为导联通道判断依据
        public string Code { set; get; }
        //基线值
        public Origin Origin { set; get; }
        //分辨率
        public Scale Scale { set; get; }
      
        //数据 记录数据为数值类型，可能为负值
        public int[] Digits { set; get; }

    }

    /// <summary>
    /// X导联抽象出来的类
    /// *****************************************考虑是否要在构造函数中直接声明十二个Leads？*****************
    /// </summary>
    public  class XLeadsElectrocarDiogram
    {
        //记录序列的绝对时间、增量(间隔)由 code[@code='TIME_ABSOLUTE" 来进行定位
        public string AbsoluteTime { set; get; }
        public Increment Increment { set; get; }
        public List<Lead> Leads { set; get; }//考虑这里！！！是否在构造函数中对Leads直接进行声明
        public int LeadsNum { set; get; }
    }

    public class Increment
    {
        public string value { set; get; }
        public string unit { set; get; }
    }

    public class Origin
    {
        public string unit { set; get; }
        public string value { set; get; }
    }

    public class Scale
    {
        public string unit { set; get; }
        public string value { set; get; }
    }
}
