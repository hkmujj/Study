using Excel.Interface;
namespace Other.ContactLine.JW4G.Model
{
    [ExcelLocation("Other.ContactLine.JW4G故障接口.xls", "Sheet1")]
    public class FalutInUnit:ISetValueProvider
    {
        [ExcelField("逻辑号")]
        public int Number { get; set; }

        [ExcelField("内容")]
        public string ContentAlgorithmBase { get; set; }
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
