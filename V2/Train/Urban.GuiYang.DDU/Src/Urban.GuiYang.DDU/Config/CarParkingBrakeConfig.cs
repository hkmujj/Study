using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car停放制动配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarParkingBrakeConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("施加")]
        public string ApplyIndex { get; private set; }

        [ExcelField("缓解")]
        public string RelaseIndex { get; private set; }

        [ExcelField("故障")]
        public string FaultIndex { get; private set; }

        [ExcelField("隔离")]
        public string CutOffIndex { get; private set; }

        [ExcelField("未知")]
        public string UnkownIndex { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }

}