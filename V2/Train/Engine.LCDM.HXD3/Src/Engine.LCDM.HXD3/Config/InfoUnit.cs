using Excel.Interface;

namespace Engine.LCDM.HXD3.Config
{
    [ExcelLocation("信息表.xls", "Sheet1")]
    public class InfoUnit : ISetValueProvider
    {

        [ExcelField("编号")]
        public int Number { get; set; }
        [ExcelField("内容1")]
        public string ContentOne { get; set; }
        [ExcelField("内容2")]
        public string ContentTwo { get; set; }
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}