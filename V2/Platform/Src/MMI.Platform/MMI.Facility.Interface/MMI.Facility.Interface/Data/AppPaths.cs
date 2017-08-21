using System.IO;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class AppPaths
    {
        /// <summary>
        /// 
        /// </summary>
        public string LogicConfigFile { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ObjectConfigFile { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ViewConfigFile { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ProjectName { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ProjectWorkDirectory { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ConfigDirectory { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ResourceDirectory { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string LogDirectory { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseDirectory"></param>
        /// <param name="appConfig"></param>
        public AppPaths(string baseDirectory, IAppConfig appConfig)
        {
            ProjectName = appConfig.AppName;
            ProjectWorkDirectory = Path.Combine(baseDirectory, ProjectName);

            ConfigDirectory = Path.Combine(ProjectWorkDirectory, "Config");
            ResourceDirectory = Path.Combine(ProjectWorkDirectory, "Resources");
            LogDirectory = Path.Combine(ProjectWorkDirectory, "Log");

            if (appConfig.AppFileConfig != null)
            {
                LogicConfigFile = Path.Combine(ConfigDirectory, appConfig.AppFileConfig.LogicConfigFile);
                ViewConfigFile = Path.Combine(ConfigDirectory, appConfig.AppFileConfig.ViewConfigFile);
                ObjectConfigFile = Path.Combine(ConfigDirectory, appConfig.AppFileConfig.ObjectConfigFile);
            }
        }
    }
}
