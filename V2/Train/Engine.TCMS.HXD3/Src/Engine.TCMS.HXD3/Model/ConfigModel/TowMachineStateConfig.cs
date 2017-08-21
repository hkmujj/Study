using System.Diagnostics;
using Excel.Interface;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [ExcelLocation("HXD3.TCMS风机状态内容.xls", "Sheet1")]
    [DebuggerDisplay("Name={Name}")]
    public class WindMachineStateConfig : NameIndex
    {
        public WindMachineStateConfig(int row)
        {
            Row = row;
        }

        public WindMachineStateConfig()
        {
        }

        [ExcelField("所在行")]
        public int Row { private set; get; }
    }
}