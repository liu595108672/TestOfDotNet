using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using NLog;
namespace TestForAsaki
{
    public class AsakiConfig
    {
        public ConfigModel Config { set; get; }
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AsakiConfig()
        {
            string path = @".\config.json";
            GetConfig(path);
        }

        public AsakiConfig(string path)
        {
            GetConfig(path);
        }

        public void GetConfig(string filePath)
        {
            if (File.Exists(filePath))
            {
                var sr = new StreamReader(filePath);
                var text = sr.ReadToEnd();

                try
                {
                    Config = JsonConvert.DeserializeObject<ConfigModel>(text);
                    logger.Debug("Reading config, remote point:  {0}:{1}", Config.IP, Config.TelnetPort);
                    if (Config.UserName!=null)
                    {
                        logger.Debug("Using UserName: {0}", Config.UserName);
                    }
                    else
                    {
                        logger.Debug("Not using UserName!");
                    }

                    if (Config.Password!=null)
                    {
                        logger.Debug("Using password {0}",Config.Password);
                    }
                    else
                    {
                        logger.Debug("Not using password!");
                    }
                }
                catch (Exception e)
                {
                    logger.Error("Unexpected exception occurs while deserialize config! Exception message is : {0}",e.Message);
                }
            }
        }
    }

    public class ConfigModel
    {
        public string IP { set; get; }
        public int TelnetPort { set; get; }
        public int TcpServerPort { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }

    }
}
