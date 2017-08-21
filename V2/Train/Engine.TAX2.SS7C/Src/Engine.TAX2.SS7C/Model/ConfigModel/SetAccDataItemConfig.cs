using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Engine.TAX2.SS7C.Model.ConfigModel
{
    [ExcelLocation("Engine.TAX2.SS7C机车状态-二级显示-设置累计数据.xls", "Sheet1")]
    [DebuggerDisplay("Content={Content}")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class SetAccDataItemConfig : ISetValueProvider
    {
        [ExcelField("显示内容")]
        public string Content { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}