using System;

namespace MMI.Facility.DataType.Attributes
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ConfigureDescriptionAttribute : Attribute
    {
        public ConfigureDescriptionAttribute(string description, string fileName)
        {
            FileName = fileName;
            Description = description;
        }

        public string Description { private set; get; }

        public string FileName { private set; get; }

    }
}