using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{

    [ExcelLocation("Urban.GuiYang.DDU列车Car高速断路器配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarHightSwitchConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("前断路器断开")]
        public string PreOffIndex { get; private set; }

        [ExcelField("前断路器闭合")]
        public string PreOnIndex { get; private set; }

        [ExcelField("前断路器未知")]
        public string PreUnkownIndex { get; private set; }

        [ExcelField("后断路器断开")]
        public string BeOffIndex { get; private set; }

        [ExcelField("后断路器闭合")]
        public string BeOnIndex { get; private set; }

        [ExcelField("后断路器未知")]
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