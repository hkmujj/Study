using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car牵引逆变器配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarTowInvertorConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("正常")]
        public string NormalIndex { get; private set; }

        [ExcelField("索引施加")]
        public string ApplyIndex { get; private set; }

        [ExcelField("电制动施加")]
        public string EleBrakeIndex { get; private set; }

        [ExcelField("故障")]
        public string FaultIndex { get; private set; }

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