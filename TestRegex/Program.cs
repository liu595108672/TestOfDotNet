using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;

namespace TestRegex
{
    class Program
    {
        static void Main(string[] args)
        {

            string HamiltonG5 = "170P 1891P!---P\"-- - P#---P$---P%---P&---P'---P(---P)---P*---P+---P,---P----P.---P/---P0---P1---P2---P3---P4---P5---P6---P7---P8---P9---P:---P;---P<0P=---P>43P?---P@---PA---PB---PC---PD---PE---PF---PG---PH---PI---PJ---PK---PL---PM---PN---PO---PP---PQ---PR---PS---PU---PV---PW---PX---PY---PZ---P[---P\\---P]---66\r"
                + "172P 1892P!---P\"---P#---P$---P%---P&---P'---P(---P)---P*---P+---P,---P----P.---P/---P0---P1---P2---P3---P4---P5---P6---P7---P8---P9---P:---P;---P<0P=---P>43P?---P@---PA---PB---PC---PD---PE---PF---PG---PH---PI---PJ---PK---PL---PM---PN---PO---PP---PQ---PR---PS---PU---PV---PW---PX---PY---PZ---P[---P\\---P]---8C\r"
                + "172P 1892P!---P\"---P#---P$---P%---P&---P'---P(---P)---P*---P+---P,---P----P.---P/---P0---P1---P2---P3---P4---P5---P6---P7---P8---P9---P:---P;---P<0P=---P>43P?---P@---PA---PB---PC---PD---PE---PF---PG---PH---PI---PJ---PK---PL---PM---PN---PO---PP---PQ---PR---PS---PU---PV---PW---PX---PY---PZ---P[---P\\---P]---8D\r"
                + "P\"---P#---P$---P%---P&---P'---P(---P)---P*---P+---P,---P----P.---P/---P0---P1---P2---P3---P4---P5---P6---P7---P8---P9---P:---P;---P<0P=---P>43P?---P@---PA---PB---PC---PD---PE---PF---PG---PH---PI---PJ---PK---PL---PM---PN---PO---PP---PQ---PR---PS---PU---PV---PW---PX---PY---PZ---P[---P\\---P]---8E\r"
                + "172P 1892P!---P\"---P#---P$---P%---P&---P'---P(---P)---P*---P+---P,---P----P.---P/---P0---P1---P2---P3---P4---P5---P6---P7---P8---P9---P:---P;---P<0P=---P>43P?---P@---PA---PB---PC---PD---PE---PF---PG---PH---PI---PJ---PK---PL---PM---PN---PO---PP---PQ---PR---PS---PU---PV---PW---PX---PY---PZ---P[---P\\---P]---8F\r"
                + "172P 1892P!---P\"---P#---P$---P%---P&---P'---P(---P)---P*---P+---P,---P----P.---P/---P0---P1---P2---P3---P4---P5---P6---P7---P8---P9---P:---P;---P<0P=---P>43P?---P@---PA---PB---PC---PD---PE---PF---PG---PH---PI---PJ---PK---PL---PM---PN---PO---PP---PQ---PR---PS---PU---PV---PW---PX---PY---PZ---P[---P\\---P]---8A\r";

            List<string> listToReturn = new List<string>();

            // 这样做虽然看起来很傻，但是这样做保证了如果中间有数据丢失的话仍然可以正常工作（比如一个数据包中包含了三条消息，但是中间的开始符号丢失了）
            while (Regex.Match(HamiltonG5, @"\x02.+?\x03.{2}\r").Success)
            {
                var abortLength = 0;
                var abordPre = Regex.Match(HamiltonG5, @".*?(?=\x02.+\x03.{2}\r)").Value;
                abortLength += abordPre.Length;

                var tmpMatch = Regex.Match(HamiltonG5, @"\x02.+?\x03.{2}\r");
                listToReturn.Add(tmpMatch.Value);
                abortLength += tmpMatch.Value.Length;
                //对 HamiltonG5 进行处理,过大之后进行重置
                HamiltonG5 = HamiltonG5.Substring(abortLength);
            }


            //***********************************************************************
            string Hamilton = "o9999.\rj9999.\r";
            var t = Regex.Match(Hamilton, @"(\x02.+?\x03\x0D)");

            var messageCollection = Regex.Matches(Hamilton, @"(\x02.+?\x03\x0D)");
            foreach (Match tmp in messageCollection)
            {
                var tHamilton= Regex.Match(tmp.Value, @"(?<=\x02).{1}").Value;
                var  tHamilton2= Regex.Match(tmp.Value, @"(?<=\x02.{1}).+?(?=\x03\x0D)").Value;
            }


            string Edwards = "";
            string itemPartten = "[a-zA-Z].+(?=([a-zA-Z]|\x03))";
            var matches2 = Regex.Matches(Edwards, itemPartten);
            foreach (Match match in matches2)
            {

            }



            //*************************************************************************
            string aa = "K2215A06B9C00000D00000E00000F00000G00000H00000I00035J00035K00000L3.891M0.388N00000O0.388P00084Q00000R00000S00000T00000U00000V00000W00000X00000Y00000Z00000a00001b00001c00014d00001e00012f0g0h0i0j0k0l0m0n0o0p0q0r00000s000001D\r\n";
            string tmpData = aa.Substring(5, aa.Length - 9);
            var tmpResult = Regex.Matches(tmpData, @"[A-Za-z]");
            foreach (Match item in tmpResult)
            {
                string s = Regex.Match(tmpData, @"(?<=" + item.Value + @").{5}").Value;
                float fff = 0.0f;
                bool flag = float.TryParse(s, out fff);
            }
            var tmpResult2 = tmpResult.Cast<string>();
            







            //************************************************************************
            string a = "001e";
            var originalData =" 0.0|    17.3|  -327.7|    0013|    52.2| -3276.8| -3276.8|    56.5|    27.9|    97.4|    10.6|00000000| -3276.8|     0.0| -3276.8|  -327.7|  -327.7|    8000| -3276.8| -3276.8| -3276.8|  -327.7|  -327.7|   100.0|     9.9|00000000| -3276.8|     0.0|     0.0|    17.3|  -327.7|    0013|    52.2| -3276.8| -3276.8|    56.5|    27.9|    97.4|    10.6|00000000| -3276.8|     0.0|        |        |        |        |        |\n\r" +
                "03/26/2015 15:43:49|       3|       0|       3|       3|       2|      27|     0.0|    17.2|  -327.7|    0013|    52.9| -3276.8| -3276.8|    56.5|    27.9|    97.4|    10.7|00000000| -3276.8|     0.0| -3276.8|  -327.7|  -327.7|    8000| -3276.8| -3276.8| -3276.8|  -327.7|  -327.7|   100.0|    10.0|00000000| -3276.8|     0.0|     0.0|    17.2|  -327.7|    0013|    52.9| -3276.8| -3276.8|    56.5|    27.9|    97.4|    10.7|00000000| -3276.8|     0.0|        |        |        |        |        |\n\r" +
                "03/26/2015 15:43:50|       3|       0|       3|       3|       2|      27|     0.0|    17.3|  -327.7|    0013|    51.9| -3276.8| -3276.8|    56.4|    27.7|    97.4|    10.6|00000000| -3276.8|     0.0| -3276.8|  -327.7|  -327.7|    8000| -3276.8| -3276.8| -3276.8|  -327.7|  -327.7|   100.0|    10.0|00000000| -3276.8|     0.0|     0.0|    17.3|  -327.7|    0013|    51.9| -3276.8| -3276.8|    56.4|    27.7|    97.4|    10.6|00000000| -3276.8|     0.0|        |        |        |        |        |\n\r" +
                "03/26/2015 15:43:51|       3|       0|       3|       3|       2|      27|     0.0|    17.2|  -327.7|    0013|    49.4| -3276.8| -3276.8|    56.8|    27.4|    97.4|    10.6|00000000| -3276.8|     0.0| -3276.8|  -327.7|  -327.7|    8000| -3276.8| -3276.8| -3276.8|  -327.7|  -327.7|   100.0|    10.0|00000000| -3276.8|     0.0|     0.0|    17.2|  -327.7|    0013|    49.4| -3276.8| -3276.8|    56.8|    27.4|    97.4|    10.6|00000000| -3276.8|     0.0|        |        |        |        |        |\n\r" +
                "03/26/2015 15:43:52|       3|       ";

            var tmpAbound = Regex.Match(originalData, @".*\n\r").Value;//首先把不能够形成完整Message的数据获取到

            var tmpUseable = originalData.Substring(tmpAbound.Length);
            var tmpContent = Regex.Match(tmpUseable, @"(\d{2}/\d{2}/\d{4} \d{2}:\d{2}:\d{2}.*\n\r)+").Value;



            //*******************************************************************
            while (true)
            {
                var strMessage = @"result{type:RACE;error:0;PT:43.1 s;INR:3.42;APTT:42.0;FIB:0.70;TT:19.7;APTT_error:0;FIB_error:16;TT_error:0;0;0;code:756403707;lot:;date:07;PID:615331;index:028;C1:-0.1;C2:-0.1;qclock:0;target:1;name:;Sex:;BirthDate:;operatorID:;APTT_RATIO:infSN:013500H0700072;version:V2.1.0.07}";
                int startIndex = strMessage.IndexOf("{");
                int endIndex = strMessage.IndexOf("}");
                var tmpMessage = strMessage.Substring(startIndex, endIndex - startIndex); //这边不用正则表达式是因为：消息当中可能包含数字、字母、特殊符号、空格等内容

                var strItemList = tmpMessage.Split(';');

                string itemNamePatten = @"\w+(?=:(\w*\W*\w*)+)";
                string itemValuePatten = @"(?<=\w+:)(\w*\W*\w*)+";
                foreach (var item in strItemList)
                {
                    string itemName = Regex.Match(item, itemNamePatten).Value;
                    string strItemValue = Regex.Match(item, itemValuePatten).Value;


                    DateTime tmpDate = DateTime.Now;

                    if ((itemName.ToLower() == "date" || itemName.ToLower() == "datetime"))
                    {
                        if (DateTime.TryParse(strItemValue, out tmpDate))
                        {
                            //Everything is all right! We get the correct datetime
                        }
                        else
                        {
                            tmpDate = DateTime.Now;
                        }
                    }

                }

            }







            RegexTestOne tOne = new RegexTestOne();
            tOne.TestMatches();
            Console.Read();
            RegexTest regexTest = new RegexTest();

            //string regex = @"(\-|\+){0,1}\d+(\.\d+){0,1}";
            //string stringTest = "0d";
            //string result = regexTest.GetingRegexString(stringTest, regex);
            //string result2 = stringTest.Substring(result.Length);
            //var list = stringTest.Split('^');

            string regex = @"TestDateTime";
            string IDParttern = @"\w*ID\w?";
            string NameParttern = @"\w*Name\w?";
            string stringTest = @"MethodID=0MethodName=i-STATTestTypeID=0TestType=Test ResultPanelID=8PanelName=CG4+TestDateTime=10/23/2017 18:00:51SerialNumber=373356PatientID=0309OriginalPatientID=0309PatientName=";
            string itemTmp = Regex.Match(stringTest, @"\d+\/\d+/\d+\s\d+:\d+:\d+").Value;
            string result = regexTest.GetingRegexString(stringTest, regex);
            DateTime dateTime = DateTime.Parse(itemTmp);
            string result1 = regexTest.GetingRegexString(stringTest, IDParttern);
            string result2 = regexTest.GetingRegexString(stringTest, NameParttern);
            string result3 = stringTest.Substring(result.Length);
            var list = stringTest.Split('^');

            //string[] commonlyUsedStrings = new[]
            //{
            //    @"^[0-9]*$",
            //    @"^\d*$",
            //    @"^(0|[1-9][0-9]*)$",
            //    @"^(\-)?[1-9]\d*(.\d+)?",
            //    @"^[a-zA-Z](\d|[._]|[a-zA-Z]){4,19}$", // 5-20个字符  以英文字母开头，允许出现 . _ 以及数字
            //    @"^[\u4e00-\u9fa5]{0,}$", //汉字
            //    @"^\w+([a-zA-Z._-])*@[a-zA-Z0-9]+[.][a-zA-Z0-9]{2,14}$",// e-mail验证
            //    @"^\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}$", //e-mail另一种验证
            //    @"^[1-9]\d{0,2}(,\d{3})*$", //使用,间隔的金额验证
            //    @"^\d{17}[\d|x]|\d{15}$",//身份证验证
            //    @"^[0-9-()（）]{7,18}$",//电话号码验证
            //    @"^((https|http|ftp|rtsp|mms)?:\/\/)[^\s]+",// url验证

            //};

            //string s0 = regexTest.GetingRegexString("1234567890123456", @"^\d*$");
            //string s1 = regexTest.GetingRegexString("012345", @"^(0|[1-9][0-9]*)$");
            //string s2 = regexTest.GetingRegexString("0", @"^(0|[1-9][0-9]*)$");
            //string s3 = regexTest.GetingRegexString("012345", @"^((0|[1-9])[0-9]*)$");
            //string s4 = regexTest.GetingRegexString("12345", @"^(\-)?[1-9]\d*(.\d+)?");
            //string s6 = regexTest.GetingRegexString("()))", @"[\*\)\(\.\\)]");


            //string s5 = Console.ReadLine();
            //while (s5!="*")
            //{
            //    //s5 = regexTest.GetingRegexString(s5, @"^(\-)?\d*(\.\d+)?$");
            //    s5 = regexTest.GetingRegexString(s5, @"^[a-zA-Z](\d|[._]|[a-zA-Z]){4,19}");
            //    Console.Out.WriteLine(s5);
            //    s5 = Console.ReadLine();
            //}


            TestPattern test = new TestPattern("FirstCharX", @"^x\w*");
            test.TestPatterns.Add("LastCharY", @"\w*$Y");  //TODO 这里需要添加待测试的正则表达式模式
            test.TestPatterns.Add("FirstCharOfWordX", @"\bX\w*");
            test.TestPatterns.Add("LastCharOfWordY", @"\w*Y\b");

            result = regexTest.GetingRegexString("XabcdefY", test.TestPatterns["FirstCharX"]);
            result = regexTest.GetingRegexString("XabcdefY", test.TestPatterns["FirstCharOfWordX"]);
            result = regexTest.GetingRegexString("XabcdefY", test.TestPatterns["LastCharY"]);
            result = regexTest.GetingRegexString("XabcdefY", test.TestPatterns["LastCharOfWordY"]);
            result = regexTest.GetingRegexString("abcd Aba bbA", @"\b\w");
            result = regexTest.GetingRegexString("abcd Aba abA", @"A\b\w*\b");
            

            //Console.ReadLine();
        }
    }


    class RegexTest
    {
        public string GetingRegexString(string origin, string regex)
        {
            Regex r = new Regex(regex);
            return r.Match(origin).Value;
        }
    }

    enum TestCase
    {

    }
    class TestPattern
    {
        public Dictionary<String, String> TestPatterns { set; get; }

        public TestPattern( )
        {
            TestPatterns = new Dictionary<string, string>();
        }
        public TestPattern(string key,string value)
        {
            TestPatterns = new Dictionary<string, string>();
            //TestPatterns[key] = value;
            TestPatterns.Add(key, value);
        }
    }
    public class testPublicClass1
    {

    }
    public class testPublicClass2
    {

    }
}
