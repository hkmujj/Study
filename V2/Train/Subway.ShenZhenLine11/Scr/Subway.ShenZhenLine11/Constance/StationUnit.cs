using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.ShenZhenLine11.Constance
{
    [ExcelLocation("站点名称.xls", "Sheet1")]
    public class StationUnit : NotificationObject, ISetValueProvider
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        [ExcelField("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 站点编号
        /// </summary>
        [ExcelField("编号", true)]
        public int Index { get; set; }
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}