using System;
using System.Reflection;

namespace Engine.LCDM.HXD3.Config
{
    public class ViewModelConfig
    {
        public string Name { get; set; }
        public string BaseName { get; set; }
        public Type PropertieType { get; set; }
        public Type BaseType { get; set; }
        public PropertyInfo Info { get; set; }
        public object Value { get; set; }
        public object BaseValue { get; set; }

        public void SetValue()
        {
            //Info.SetValue(BaseValue, Value);
        }

    }
}