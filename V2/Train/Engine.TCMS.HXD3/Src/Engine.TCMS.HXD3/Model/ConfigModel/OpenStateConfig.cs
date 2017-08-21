using System.Diagnostics;
using Excel.Interface;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [DebuggerDisplay("Name={Name}")]
    [ExcelLocation("HXD3.TCMS¿ª·Å×´Ì¬ÄÚÈÝ.xls", "Sheet1")]
    public class OpenStateConfig : ISetValueProvider
    {
        [DebuggerStepThrough]
        public OpenStateConfig(string name, string inIndex, string outIndex)
        {
            Name = name;
            InIndex = inIndex;
            OutIndex = outIndex;
        }

        public OpenStateConfig()
        {

        }

        [ExcelField("Name")]
        public string Name { private set; get; }

        [ExcelField("InputIndex")]
        public string InIndex { private set; get; }

        [ExcelField("OutputIndex")]
        public string OutIndex { private set; get; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}