using System.Collections.Generic;
using System.Linq;
using Excel.Interface;
using Urban.Phillippine.View.Interface;

namespace Urban.Phillippine.View.Config
{
    public class IndexConfigure
    {
        public static IndexConfigure Instance = new IndexConfigure();
        public static IDictionary<string, int> InBoolIndex { get; private set; }
        public static IDictionary<string, int> OutBoolIndex { get; private set; }
        public static IDictionary<string, int> IntFloatIndex { get; private set; }
        public static IDictionary<string, int> OutFloatIndex { get; private set; }

        public void Load(string path)
        {
            InBoolIndex = ExcelParser.Parser<InBoolKeyConfig>(path).ToDictionary(d => d.Name, d => d.Key);
            OutBoolIndex = ExcelParser.Parser<OutBoolKeyConfig>(path).ToDictionary(d => d.Name, d => d.Key);
            IntFloatIndex = ExcelParser.Parser<InFloatKeyConfig>(path).ToDictionary(d => d.Name, d => d.Key);
            OutFloatIndex = ExcelParser.Parser<OutFloatKeyConfig>(path).ToDictionary(d => d.Name, d => d.Key);
        }
    }

    [ExcelLocation("PhilippineLogic.xls", "InBool")]
    public class InBoolKeyConfig : IKeysFileConfig, ISetValueProvider
    {
        [ExcelField("Name")]
        public string Name { get; set; }

        [ExcelField("Index")]
        public int Key { get; set; }

        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Name":
                    Name = value;
                    break;

                case "Key":
                    Key = int.Parse(value);
                    break;
            }
        }
    }

    [ExcelLocation("PhilippineLogic.xls", "OutBool")]
    public class OutBoolKeyConfig : IKeysFileConfig, ISetValueProvider
    {
        [ExcelField("Name")]
        public string Name { get; set; }

        [ExcelField("Index")]
        public int Key { get; set; }

        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Name":
                    Name = value;
                    break;

                case "Key":
                    Key = int.Parse(value);
                    break;
            }
        }
    }

    [ExcelLocation("PhilippineLogic.xls", "InFloat")]
    public class InFloatKeyConfig : IKeysFileConfig, ISetValueProvider
    {
        [ExcelField("Name")]
        public string Name { get; set; }

        [ExcelField("Index")]
        public int Key { get; set; }

        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Name":
                    Name = value;
                    break;

                case "Key":
                    Key = int.Parse(value);
                    break;
            }
        }
    }

    [ExcelLocation("PhilippineLogic.xls", "OutFloat")]
    public class OutFloatKeyConfig : IKeysFileConfig, ISetValueProvider
    {
        [ExcelField("Name")]
        public string Name { get; set; }

        [ExcelField("Index")]
        public int Key { get; set; }

        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Name":
                    Name = value;
                    break;

                case "Key":
                    Key = int.Parse(value);
                    break;
            }
        }
    }
}