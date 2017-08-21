using Excel.Interface;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    [ExcelLocation("紧急广播.xls","Sheet1")]
    public class BoradcastItem:ISetValueProvider
    {
        [ExcelField("逻辑号")]
        public int LogicName { get; set; }
        [ExcelField("编号")]
        public int Number { get; set; }
        [ExcelField("内容")]
        public string Content { get; set; }
        [ExcelField("类型")]
        public int Type { get; set; }
        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
