using System;

namespace MMITool.Addin.MMIConfiguration.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewMappedConfigTypeAttribute : Attribute
    {
        public ViewMappedConfigTypeAttribute(Type viewType, Type configType, bool isSeniorItem)
        {
            IsSeniorItem = isSeniorItem;
            ConfigType = configType;
            ViewType = viewType;
        }

        public Type ViewType { private set; get; }

        public Type ConfigType { private set; get; }

        public bool IsSeniorItem { private set; get; }
    }
}