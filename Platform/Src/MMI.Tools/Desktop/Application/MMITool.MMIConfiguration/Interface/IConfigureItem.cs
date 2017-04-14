using System;
using MMI.Facility.DataType.Attributes;

namespace MMITool.Addin.MMIConfiguration.Interface
{
    public interface IConfigureItem
    {
        /// <summary>
        /// 显示名
        /// </summary>
        string ViewName { get; }

        Type Type { get; }

        ConfigureDescriptionAttribute ConfigureDescription { get; }
    }
}