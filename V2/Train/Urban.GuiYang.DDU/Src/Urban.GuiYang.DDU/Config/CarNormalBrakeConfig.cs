using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car常用制动配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarNormalBrakeConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("前制动施加")]
        public string PreApplyIndex { get; private set; }

        [ExcelField("前制动缓解")]
        public string PrelaseIndex { get; private set; }

        [ExcelField("前制动故障")]
        public string PreFaultIndex { get; private set; }

        [ExcelField("前制动隔离")]
        public string PreCutOffIndex { get; private set; }

        [ExcelField("前制动未知")]
        public string PreUnkownIndex { get; private set; }

        [ExcelField("后制动施加")]
        public string BeApplyIndex { get; private set; }

        [ExcelField("后制动缓解")]
        public string BeRelaseIndex { get; private set; }

        [ExcelField("后制动故障")]
        public string BeFaultIndex { get; private set; }

        [ExcelField("后制动隔离")]
        public string BeCutOffIndex { get; private set; }

        [ExcelField("后制动未知")]
        public string BeUnkownIndex { get; private set; }
        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }





    
}