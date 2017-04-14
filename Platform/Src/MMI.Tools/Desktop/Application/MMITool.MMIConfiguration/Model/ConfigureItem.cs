using System;
using MMI.Facility.DataType.Attributes;
using MMITool.Addin.MMIConfiguration.Interface;

namespace MMITool.Addin.MMIConfiguration.Model
{
    class ConfigureItem : IConfigureItem
    {
        public ConfigureItem(Type type, ConfigureDescriptionAttribute configureDescription, string viewName)
        {
            ViewName = viewName;
            ConfigureDescription = configureDescription;
            Type = type;
        }

        /// <summary>
        /// 显示名
        /// </summary>
        public string ViewName { private set; get; }

        public Type Type { get; private set; }

        public ConfigureDescriptionAttribute ConfigureDescription { get; private set; }
    }
}