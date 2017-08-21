using ES.Facility.PublicModule.IO;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.Net;

namespace MMI.Facility.DataType.Config
{
    public class SystemProjectConfig : ISystemProjectConfig
    {
        [IniField(Section = "工程配置信息", KeyName = "工程名称", DefaultValue = "MMI")]
        public string ProjectName { set; get; }

        [IniField(Section = "工程配置信息", KeyName = "工程配置文件", DefaultValue = "MMI")]
        public string ProjectConfigFile { set; get; }

        [IniField(Section = "工程配置信息", KeyName = "启用的工程数量", DefaultValue = "0")]
        public int StartProjectCount { set; get; }

        [IniField(Section = "工程配置信息", KeyName = "工程1", DefaultValue = "")]
        public string Project1 { set; get; }

        [IniField(Section = "工程配置信息", KeyName = "工程2", DefaultValue = "")]
        public string Project2 { set; get; }

        [IniField(Section = "工程配置信息", KeyName = "工程3", DefaultValue = "")]
        public string Project3 { set; get; }

        [IniField(Section = "工程配置信息", KeyName = "工程4", DefaultValue = "")]
        public string Project4 { set; get; }

        [IniField(Section = "工程配置信息", KeyName = "工程识别信息", DefaultValue = "未识别")]
        public string ProjectIdentify { set; get; }

        [IniField(Section = "工程配置信息", KeyName = "网络选择", DefaultValue = "0")]
        public NetType NetType { set; get; }

        public string[] Projects { get; set; }

        public void Ready()
        {
            Projects = new string[]
                       {
                           Project1,
                           Project2,
                           Project3,
                           Project4,
                       };
        }
    }
}