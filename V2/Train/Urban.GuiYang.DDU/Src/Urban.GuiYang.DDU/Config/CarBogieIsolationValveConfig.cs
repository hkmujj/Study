using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car转向架隔离阀配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarBogieIsolationValveConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("前转向架隔离")]
        public string PreIsolationIndex { get; private set; }

        [ExcelField("前转向架未隔离")]
        public string PreUnisolationIndex { get; private set; }

        [ExcelField("前转向架未知")]
        public string PreUnkownIndex { get; private set; }

        [ExcelField("后转向架隔离")]
        public string BeIsolationIndex { get; private set; }

        [ExcelField("后转向架未隔离")]
        public string BeUnisolationIndex { get; private set; }

        [ExcelField("后转向架未知")]
        public string BeUnkownIndex { get; private set; }



        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }

    
}
