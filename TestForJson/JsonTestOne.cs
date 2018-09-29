using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace TestForJson
{
    public class JsonTestOne
    {
        public string path = @".\Data.json";

        public void TestParseJsonList()
        {
            var reader = new StreamReader(path);
            var text = reader.ReadToEnd();

            var piccolist = JsonConvert.DeserializeObject(text);

            var listOfPiccoItems = JsonConvert.DeserializeObject<PiccoList>(text);

        }
    }
}
