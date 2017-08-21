using Excel.Interface;

namespace General.CIR.Config
{
    [ExcelLocation("机车类型表.xls", "机型代码")]
    public class EngineType : ISetValueProvider
    {
        [ExcelField("机车类型编码", true)]
        public int Number
        {
            get;
            set;
        }

        [ExcelField("机车类型符号", false)]
        public string StrNumber
        {
            get;
            set;
        }

        [ExcelField("机车类型符号", false)]
        public string Name
        {
            get;
            set;
        }

        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}
