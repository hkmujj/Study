using System.Diagnostics;
using Excel.Interface;
using LightRail.HMI.GZYGDC.Interface;

namespace LightRail.HMI.GZYGDC.Model.ConfigModel
{
    [ExcelLocation("站点名称.xls", "Sheet1")]
    [DebuggerDisplay("编号={Index}, 名称={Name}")]
    public class StationItem : ISetValueProvider, IStation
    {
        [ExcelField("编号", true)]
        public int Index { set; get; }

        [ExcelField("名称")]
        public string Name { set; get; }

        

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}