using System;
using MMI.Facility.DataType.Attributes;
using MMITool.Addin.MMIConfiguration.Attributes;

namespace MMITool.Addin.MMIConfiguration.Model
{
    /// <summary>
    /// 配置类型的映射信息
    /// </summary>
    public class ConfigTypeMapInfo
    {
        public ConfigTypeMapInfo(Type configType, ConfigureDescriptionAttribute configureDescription,
            ViewMappedConfigTypeAttribute viewMappedConfigType)
        {
            ViewMappedConfigType = viewMappedConfigType;
            ConfigureDescription = configureDescription;
            ConfigType = configType;
        }

        public Type ConfigType { private set; get; }

        public ConfigureDescriptionAttribute ConfigureDescription { private set; get; }

        public ViewMappedConfigTypeAttribute ViewMappedConfigType { private set; get; }

    }
}