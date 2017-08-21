using System.IO;
using CommonUtil.Util;

namespace Subway.ATC.Casco.Config
{
    public class ConfigManager
    {
        public static readonly ConfigManager Instance = new ConfigManager();

        private ConfigManager()
        {

        }

        public CascoATCConfig CascoATCConfig { private set; get; }

        public DiagConfig DiagConfig { private set; get; }

        public DifferenceConfig DifferenceConfig { private set; get; }

        public void Initalize(string rootConfigDirectory, string projectConfigDirectory)
        {
            DiagConfig =
                DataSerialization.DeserializeFromXmlFile<DiagConfig>(Path.Combine(projectConfigDirectory,
                    "DiagConfig.xml"));

            CascoATCConfig =
                DataSerialization.DeserializeFromXmlFile<CascoATCConfig>(Path.Combine(rootConfigDirectory,
                    CascoATCConfig.FileName));

            DifferenceConfig = new DifferenceConfig();
            DifferenceConfig.Initalize(projectConfigDirectory, CascoATCConfig.UsedEnv);
        }
    }
}