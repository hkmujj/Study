using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Engine.TPX21F.HXN5B.Model.ConfigModel
{
    [ExcelLocation("Engine.TPX21F.HXN5B机车设置-软开关.xls", "Sheet1")]
    [DebuggerDisplay("Title={Title}")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class SoftSwitchItemConfig : ISetValueProvider
    {
        [DebuggerStepThrough]
        public SoftSwitchItemConfig(string title, string upContent, string downContent)
        {
            Title = title;
            UpContent = upContent;
            DownContent = downContent;
        }

        public SoftSwitchItemConfig()
        {
            
        }

        [ExcelField("标题")]
        public string Title { private set; get; }

        [ExcelField("上部内容")]
        public string UpContent { private set; get; }

        [ExcelField("下部内容")]
        public string DownContent { private set; get; }

        [ExcelField("上部的逻辑索引")]
        public string LogicIndexUp { private set; get; }

        [ExcelField("下部的逻辑索引")]
        public string LogicIndexDown { private set; get; }

        [ExcelField("发送的逻辑位")]
        public string OutLogicIndex { private set; get; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
            
        }
    }
}