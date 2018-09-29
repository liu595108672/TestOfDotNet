using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TestRegex
{
    public class RegexTestOne
    {
        public void TestMatches()
        {
            string message = @"1||11|0|5|CK-MB^cTnI^Myo|ng/ml^ng/ml^ng/ml|4.440^0.382^54.744|2.50^0.01^30.00|80.00^100.00^600.00|5.00^1.00^70.00|2|0|2018-01-09 09:06:34|||";
            string mappingPartten = @"(\w|-|\^|\.|/)+?(?=\|)";
            //string mappingPartten = @"\w*|";

            var matches = Regex.Matches(message, mappingPartten);
            foreach (Match tmpMatch in matches)
            {
                Console.WriteLine(tmpMatch.Value);
            }

            var listTmp = message.Split('|');

            CheckandParseMessage(message);

        }

        private List<ItemModelForLarge> CheckandParseMessage(string message)
        {
            List<string> listTmp = new List<string>();
            List<string> listItemValueTmp = new List<string>();
            List<string> listItemNameTmp = new List<string>();
            List<string> listItemUnitTmp = new List<string>();
            List<ItemModelForLarge> listToReturn = new List<ItemModelForLarge>();
            ItemModelForLarge itemTmp = new ItemModelForLarge();
            listTmp = message.Split('|').ToList();

            if (true)
            {
                listToReturn.Add(new ItemModelForLarge("No", listTmp[2], ""));

                listToReturn.Add(new ItemModelForLarge("ID", listTmp[3], ""));

                listItemValueTmp = listTmp[4].Split('^').ToList();
                foreach (var item in listItemValueTmp)
                {
                    listToReturn.Add(new ItemModelForLarge("ItemNo", item, "")); //todo : 待验证。去重时候是根据什么来进行的  如果是根据ItenName的话，那这个方式不能将所有的ItemNo添加进来
                }

                listItemNameTmp = listTmp[5].Split('^').ToList();
                listItemUnitTmp = listTmp[6].Split('^').ToList();
                listItemValueTmp = listTmp[7].Split('^').ToList();
                for (int i = 0; i < listItemNameTmp.Count; i++)
                {
                    listToReturn.Add(new ItemModelForLarge(listItemNameTmp[i], listItemValueTmp[i], listItemUnitTmp[i]));
                }
                listItemValueTmp = listTmp[8].Split('^').ToList();
                for (int i = 0; i < listItemNameTmp.Count; i++)
                {
                    listToReturn.Add(new ItemModelForLarge(listItemNameTmp[i] + "_Min", listItemValueTmp[i], listItemUnitTmp[i]));
                }
                listItemValueTmp = listTmp[9].Split('^').ToList();
                for (int i = 0; i < listItemNameTmp.Count; i++)
                {
                    listToReturn.Add(new ItemModelForLarge(listItemNameTmp[i] + "_Max", listItemValueTmp[i], listItemUnitTmp[i]));
                }
                listItemValueTmp = listTmp[10].Split('^').ToList();
                for (int i = 0; i < listItemNameTmp.Count; i++)
                {
                    listToReturn.Add(new ItemModelForLarge(listItemNameTmp[i] + "_Ref", listItemValueTmp[i], listItemUnitTmp[i]));
                }

                listToReturn.Add(new ItemModelForLarge("Sample", listTmp[11], ""));
                listToReturn.Add(new ItemModelForLarge("Mode", listTmp[12], ""));
                listToReturn.Add(new ItemModelForLarge("DateTime", listTmp[13], 0.0f, ""));
            }
            return listToReturn;

        }
    }
    public class ItemModelForLarge
    {
        public string itemName { set; get; }
        public string itemValueStr { set; get; }
        public float itemValueF { set; get; }
        public string unit { set; get; }

        public ItemModelForLarge()
        {

        }
        public ItemModelForLarge(string itemName, string itemValueStr, float itemValueF, string unit)
        {
            this.itemName = itemName;
            this.itemValueStr = itemValueStr;
            this.itemValueF = itemValueF;
            this.unit = unit;
        }

        public ItemModelForLarge(string itemName, string itemValueStr, string unit)
        {
            float tmp = 0.0f;
            this.itemName = itemName;
            this.itemValueStr = itemValueStr;
            float.TryParse(itemValueStr, out tmp);
            this.itemValueF = tmp;
            this.unit = unit;
        }
    }
}
