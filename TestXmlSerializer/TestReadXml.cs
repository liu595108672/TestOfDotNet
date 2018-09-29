using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace TestXmlSerializer
{
    class TestReadXml
    {
        static void Main(string[] args)
        {
            try
            {
                //AnnotatedECG f = new AnnotatedECG();
                //Code code = new Code();
                //code.code = "123";
                //code.codeSystem = "4567";
                //ID id = new ID();
                //id.root = "id-root";
                //f.code = code;
                //f.id = id;

                TestReadFile(@".\test.xml");

                string original =
                    "<data class=\"monitor\" crc=\"9B46\" msgID=\"0004\">0014FFDF0000000580000016000C000001800073000001C202424EE002424EE08000000002FAF080</data>";
                TestHandleString(original);
                //FileStream file = new FileStream(@"E:\2015031005.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                //XmlSerializer serializer = new XmlSerializer(typeof(AnnotatedECG));
                //AnnotatedECG annotatedECG = new AnnotatedECG();
                //annotatedECG = (AnnotatedECG)serializer.Deserialize(file);

                ////FileStream file2 = new FileStream(@"E:\newTest.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                ////serializer.Serialize(file2, f);

                //serializer.Serialize(Console.Out, annotatedECG);
            }
            catch (Exception e)
            {
                Console.Out.Write(e.Message);
            }

            Console.In.Read();
        }

        static void TestReadFile(string filePath)
        {
            try
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(filePath, Encoding.ASCII);
                List<string> sList = new List<string>();
                string s1 = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(s1) && !s1.Equals("*"))
                {
                    sList.Add(s1);
                    s1 = streamReader.ReadLine();
                }
                string s2 = streamReader.ReadToEnd();
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception);
                throw;
            }

        }

        static void TestHandleString(string original)
        {
            Regex regex = new Regex(@"<data\s+class=""\S+""\s+crc=""\S+""\s+msgID=""\d+"">");
            Regex regex2 = new Regex(@"</data>");
            var tmp = regex.Matches(original);
            foreach (Match VARIABLE in tmp)
            {
                   
            }
            var strTmp = regex.Replace(original, "****");
             strTmp = regex2.Replace(strTmp, "****");
        }
    }


    [XmlRoot("AnnotatedECG")]
    public class AnnotatedECG
    {
        [XmlElement("id")]
        public ID id { set; get; }

        [XmlElement("code")]
        public Code code { set; get; }

        [XmlElement("digits")]
        public string digits { set; get; }
    }

    public class Code
    {
        [XmlAttribute("code")]
        public string code { set; get; }

        [XmlAttribute("codeSystem")]
        public string codeSystem { set; get; }
    }

    public class ID
    {
        [XmlAttribute("root")]
        public string root { set; get; }
    }
}
