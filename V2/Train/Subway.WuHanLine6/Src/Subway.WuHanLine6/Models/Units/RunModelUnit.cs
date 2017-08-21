using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Models.Units
{
    /// <summary>
    /// RunModel
    /// </summary>
    [ExcelLocation("运行模式文本.xls", "Sheet1")]
    public class RunModelUnit : NotificationObject, ISetValueProvider
    {
        /// <summary>
        /// 文本
        /// </summary>
        [ExcelField("文本")]
        public string Text { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        [ExcelField("颜色")]
        public RunModelColr Color { get; set; }

        /// <summary>
        /// 逻辑号
        /// </summary>
        [ExcelField("逻辑号")]
        public string LogicName { get; set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Color":
                    Color = (RunModelColr)int.Parse(value);
                    break;
            }
        }
    }
}