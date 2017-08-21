using Excel.Interface;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [ExcelLocation("HXD3.TCMS软件版本信息.xls", "Sheet1")]
    public class SoftWareVersionUnit : ISetValueProvider
    {
        [ExcelField("软件")]
        public string SoftWare { get; set; }
        [ExcelField("主版本")]
        public string MasterVersion { get; set; }
        [ExcelField("次版本")]
        public string SlaveVersion { get; set; }
        [ExcelField("行")]
        public int Row { get; set; }
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}