using System.IO;
using Yunda.COM.MMICommunication.Config.Model;
using Yunda.COM.MMICommunication.Utils;

namespace Yunda.COM.MMICommunication
{
    internal class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        private GlobalParam()
        {
            
        }

        public void Initalize(string configPath)
        {
            MainConfig = DataSerialization.DeserializeFromXmlFile<MainConfig>(Path.Combine(configPath,
                             MainConfig.File)) ?? new MainConfig();
        }

        public MainConfig MainConfig { get; private set; }
    }
}