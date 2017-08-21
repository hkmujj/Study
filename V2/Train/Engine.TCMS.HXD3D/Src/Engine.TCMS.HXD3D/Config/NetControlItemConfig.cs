using Excel.Interface;

namespace Engine.TCMS.HXD3D.Config
{
    [ExcelLocation("过程数据-网络控制内容配置.xls", "Sheet1")]
    public class NetControlItemConfig : ISetValueProvider
    {
        [ExcelField("Name")]
        public string Name { get; private set; }

        [ExcelField("RedIndex")]
        public string RedIndex { get; private set; }

        [ExcelField("YellowIndex")]
        public string YellowIndex { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}