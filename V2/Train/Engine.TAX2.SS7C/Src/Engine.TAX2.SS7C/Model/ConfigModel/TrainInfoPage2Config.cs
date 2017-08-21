using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Engine.TAX2.SS7C.Model.ConfigModel
{
    [ExcelLocation("Engine.TAX2.SS7C机车状态二内容.xls", "Sheet1")]
    [DebuggerDisplay("ShowingContent={ShowingContent}")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class TrainInfoPage2Config : ISetValueProvider
    {
        public TrainInfoPage2Config()
        {
            
        }

        public TrainInfoPage2Config(string showingContent)
        {
            ShowingContent = showingContent;
        }

        [ExcelField("显示内容")]
        public string ShowingContent { private set; get; }

        [ExcelField("索引名称")]
        public string IndexName { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
            
        }
    }
}