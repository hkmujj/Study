namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppFileConfig
    {
        /// <summary>
        /// [IniField(Section = "绘图配置信息", KeyName = "视图配置文件", DefaultValue = "views.mmi")]
        /// </summary>
        string ViewConfigFile { get; }

        /// <summary>
        /// [IniField(Section = "绘图配置信息", KeyName = "对象配置文件", DefaultValue = "objects.mmi")]
        /// </summary>
        string ObjectConfigFile { get; }

        /// <summary>
        /// [IniField(Section = "绘图配置信息", KeyName = "逻辑配置文件", DefaultValue = "logic.mmi")]
        /// </summary>
        string LogicConfigFile { get; }
    }
}