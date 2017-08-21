using System.Diagnostics;
using Excel.Interface;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [ExcelLocation("HXD3.TCMS���״̬����.xls", "Sheet1")]
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

        [ExcelField("������")]
        public int Row { private set; get; }
    }
}