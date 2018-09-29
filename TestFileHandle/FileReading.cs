using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestFileHandle
{
    public class FileReading
    {
        public void ReadFileAndSerilaize()
        {
            var streamReader = new StreamReader(@".\MonitorConfig.json", Encoding.Default);
            string strConfig = streamReader.ReadToEnd();
            var Config = JsonConvert.DeserializeObject<ForwardMonitorConfigModel>(strConfig);
        }
    }
}
