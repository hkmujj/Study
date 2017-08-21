using System.Diagnostics;
using Excel.Interface;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    [DebuggerDisplay("LogicKey={LogicKey}, Contrnt={Content}")]
    public class TestUnitBase : ISetValueProvider
    {
        [ExcelField("内容")]
        public string Content { get; set; }

        [ExcelField("编号", true)]
        public string LogicKey { get; set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }

    [ExcelLocation("HXD3.TCMS试验信息内容.xls", "显示灯")]
    public class DisplayLightTestUnit : TestUnitBase
    {

    }

    [ExcelLocation("HXD3.TCMS试验信息内容.xls", "辅助电源")]
    public class AssistPowerTestUnit : TestUnitBase
    {

    }

    [ExcelLocation("HXD3.TCMS试验信息内容.xls", "零级位")]
    public class ZeroLevelTestUnit : TestUnitBase
    {

    }

    [ExcelLocation("HXD3.TCMS试验信息内容.xls", "起动")]
    public class StartTestUnit : TestUnitBase
    {

    }

    [ExcelLocation("HXD3.TCMS试验信息内容.xls", "主司控器")]
    public class MasterDriverControlTestUnit : TestUnitBase
    {

    }

}