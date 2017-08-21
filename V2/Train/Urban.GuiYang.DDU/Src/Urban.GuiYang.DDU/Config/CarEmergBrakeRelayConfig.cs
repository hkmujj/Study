using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car紧急制动继电器配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarEmergBrakeRelayConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("前继电器激活")]
        public string PreActivedIndex { get; private set; }

        [ExcelField("前继电器未激活")]
        public string PreUnactiveIndex { get; private set; }

        [ExcelField("前继电器未知")]
        public string PreUnkownIndex { get; private set; }

        [ExcelField("后继电器激活")]
        public string BeActivedIndex { get; private set; }

        [ExcelField("后继电器未激活")]
        public string BeUnactiveIndex { get; private set; }

        [ExcelField("后继电器未知")]
        public string BeUnkownIndex { get; private set; }



        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }

    
}
