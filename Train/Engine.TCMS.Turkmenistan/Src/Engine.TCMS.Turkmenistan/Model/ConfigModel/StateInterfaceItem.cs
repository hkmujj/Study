using System.Diagnostics;
using Excel.Interface;

namespace Engine.TCMS.Turkmenistan.Model.ConfigModel
{
    [ExcelLocation("Engine.TCMS.Turkmenistan按键状态集.xls", "Sheet1")]
    [DebuggerDisplay("Key={Key}, Title={Title}")]
    public class StateInterfaceItem : ISetValueProvider
    {
        [ExcelField("Key", true)]
        public string Key { private set; get; }

        [ExcelField("标题")]
        public string Title { private set; get; }

        [ExcelField("内容控件名")]
        public string ContentViewName { private set; get; }

        [ExcelField("第1个按键中文内容")]
        public string B1ChContent { private set; get; }

        [ExcelField("第1个按键处理")]
        public string B1ActionClassName { private set; get; }

        [ExcelField("第2个按键中文内容")]
        public string B2ChContent { private set; get; }

        [ExcelField("第2个按键处理")]
        public string B2ActionClassName { private set; get; }

        [ExcelField("第3个按键中文内容")]
        public string B3ChContent { private set; get; }

        [ExcelField("第3个按键处理")]
        public string B3ActionClassName { private set; get; }

        [ExcelField("第4个按键中文内容")]
        public string B4ChContent { private set; get; }

        [ExcelField("第4个按键处理")]
        public string B4ActionClassName { private set; get; }

        [ExcelField("第5个按键中文内容")]
        public string B5ChContent { private set; get; }

        [ExcelField("第5个按键处理")]
        public string B5ActionClassName { private set; get; }
        [ExcelField("第1个按键土库曼斯坦内容")]
        public string B1TmContent { private set; get; }
        [ExcelField("第2个按键土库曼斯坦内容")]
        public string B2TmContent { private set; get; }
        [ExcelField("第3个按键土库曼斯坦内容")]
        public string B3TmContent { private set; get; }
        [ExcelField("第4个按键土库曼斯坦内容")]
        public string B4TmContent { private set; get; }
        [ExcelField("第5个按键土库曼斯坦内容")]
        public string B5TmContent { private set; get; }
        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}