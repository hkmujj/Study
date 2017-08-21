using System.Diagnostics;
using Excel.Interface;

namespace Motor.HMI.CRH380D.Models.ConfigModel
{
    [ExcelLocation("提示文本.xls", "HandleTest")]
    [DebuggerDisplay("编号={Index}, 内容={Info}")]
    public class HandleTestInfo : ISetValueProvider
    {
        [ExcelField("编号", true)]
        public int Index { set; get; }

        [ExcelField("内容")]
        public string Info { set; get; }
        

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}