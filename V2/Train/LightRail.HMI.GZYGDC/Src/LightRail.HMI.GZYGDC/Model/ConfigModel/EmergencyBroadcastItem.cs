using System.Diagnostics;
using Excel.Interface;
using LightRail.HMI.GZYGDC.Interface;

namespace LightRail.HMI.GZYGDC.Model.ConfigModel
{
    [ExcelLocation("紧急广播表.xls", "Sheet1")]
    [DebuggerDisplay("编号={Index}, 名称={Name}")]
    public class EmergencyBroadcastItem : ISetValueProvider
    {
        [ExcelField("编号", true)]
        public int Index { set; get; }

        [ExcelField("标题")]
        public string Name { set; get; }


        [ExcelField("详细内容")]
        public string Detail { set; get; }


        //是否选中
        public bool IsChecked { set; get; }


        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}