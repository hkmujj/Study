using System;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car控制模式.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarControlModelConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("空调控制模式")]
        public AirConditionItemType ItemType { get; private set; }

        [ExcelField("集控")]
        public string FocusControlIndex { get; private set; }

        [ExcelField("本控")]
        public string SelfControlIndex { get; private set; }

        [ExcelField("未知")]
        public string UnkownIndex { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case nameof(ItemType):
                    ItemType = (AirConditionItemType) Enum.Parse(typeof (AirConditionItemType), value);
                    break;
            }
        }
    }


}
