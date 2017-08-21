using System.Diagnostics;
using Excel.Interface;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [ExcelLocation("HXD3.TCMS开关状态内容.xls", "Sheet1")]
    [DebuggerDisplay("Name={Name}")]
    public class SwitchStateConfig : NameIndex
    {
        [ExcelField("Index红")]
        public string IndexRed { protected set; get; }
    }
}