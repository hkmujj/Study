using Excel.Interface;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Engine.Angola.TCMS.Model.ConfigModel
{
    [ExcelLocation("Engine.Angola.TCMS按键状态集.xls", "Sheet1")]
    [DebuggerDisplay("Key={Key}, Title={Title}")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class StateInterfaceItem : ISetValueProvider
    {
        [ExcelField("Key", true)]
        public string Key { private set; get; }

        [ExcelField("标题")]
        public string Title { private set; get; }

        [ExcelField("内容控件名")]
        public string ContentViewName { private set; get; }

        //[ExcelField("第1个按键内容")]
        public string F1Content { private set; get; }

        [ExcelField("第1个按键处理")]
        public string F1ActionClassName { private set; get; }

        //[ExcelField("第2个按键内容")]
        public string F2Content { private set; get; }

        [ExcelField("第2个按键处理")]
        public string F2ActionClassName { private set; get; }

        //[ExcelField("第3个按键内容")]
        public string F3Content { private set; get; }

        [ExcelField("第3个按键处理")]
        public string F3ActionClassName { private set; get; }

        //[ExcelField("第4个按键内容")]
        public string F4Content { private set; get; }

        [ExcelField("第4个按键处理")]
        public string F4ActionClassName { private set; get; }

        //[ExcelField("第5个按键内容")]
        public string F5Content { private set; get; }

        [ExcelField("第5个按键处理")]
        public string F5ActionClassName { private set; get; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}
