using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car列车连挂开关.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarCoulplingStateConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("正常位")]
        public string NormalIndex { get; private set; }

        [ExcelField("CTE位")]
        public string CTEIndex { get; private set; }

        [ExcelField("未知")]
        public string UnkownIndex { get; private set; }

       
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }

    
}
