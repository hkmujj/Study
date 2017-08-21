using System;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car车厢内外温度.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarTemperatureConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("空调控制模式")]
        public AirConditionItemType ItemType { get; private set; }

        [ExcelField("内温度数值")]
        public string InsideTemIndex { get; private set; }

        [ExcelField("外温度数值")]
        public string OutTemIndex { get; private set; }

        [ExcelField("温度未知")]
        public string UnkownIndex { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case nameof(ItemType):
                    ItemType = (AirConditionItemType)Enum.Parse(typeof(AirConditionItemType), value);
                    break;
            }
        }
    }

    
}
