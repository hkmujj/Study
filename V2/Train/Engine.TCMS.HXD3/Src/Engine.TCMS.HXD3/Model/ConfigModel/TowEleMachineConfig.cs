using System.Diagnostics;
using Excel.Interface;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [ExcelLocation("HXD3.TCMS牵引电机画面内容.xls", "Sheet1")]
    [DebuggerDisplay("Name={Name}")]
    public class TowEleMachineConfig : ISetValueProvider
    {
        [ExcelField("Name", true)]
        public string Name { set; get; }

        [ExcelField("位1")]
        public string IndexName1 { set; get; }

        [ExcelField("位2")]
        public string IndexName2 { set; get; }

        [ExcelField("位3")]
        public string IndexName3 { set; get; }

        [ExcelField("位4")]
        public string IndexName4 { set; get; }

        [ExcelField("位5")]
        public string IndexName5 { set; get; }

        [ExcelField("位6")]
        public string IndexName6 { set; get; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}