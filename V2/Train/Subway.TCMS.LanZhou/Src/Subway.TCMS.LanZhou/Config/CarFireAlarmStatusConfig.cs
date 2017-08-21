
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou烟火警报.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarFireAlarmStatusConfig : ISetValueProvider
    {
        [ExcelField("Index")]
        public int Index { get; private set; }

        [ExcelField("正常状态")]
        public string NormalStatus { get; private set; }

        [ExcelField("故障状态")]
        public string FualtStatus { get; private set; }

        [ExcelField("污染状态")]
        public string PollutedStatus { get; private set; }

        [ExcelField("无效状态")]
        public string InvalidStatus { get; private set; }

        [ExcelField("火警状态")]
        public string FireStatus { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
