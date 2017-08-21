using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Model.ConfigModel
{
    [ExcelLocation("Subway.TCMS.LanZhou按键状态集.xls", "Sheet1")]
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

        [ExcelField("第1个按键内容")]
        public string B1Content { private set; get; }

        [ExcelField("第1个按键处理")]
        public string B1ActionClassName { private set; get; }

        [ExcelField("第2个按键内容")]
        public string B2Content { private set; get; }

        [ExcelField("第2个按键处理")]
        public string B2ActionClassName { private set; get; }

        [ExcelField("第3个按键内容")]
        public string B3Content { private set; get; }

        [ExcelField("第3个按键处理")]
        public string B3ActionClassName { private set; get; }

        [ExcelField("第4个按键内容")]
        public string B4Content { private set; get; }

        [ExcelField("第4个按键处理")]
        public string B4ActionClassName { private set; get; }

        [ExcelField("第5个按键内容")]
        public string B5Content { private set; get; }

        [ExcelField("第5个按键处理")]
        public string B5ActionClassName { private set; get; }

        [ExcelField("第6个按键内容")]
        public string B6Content { private set; get; }

        [ExcelField("第6个按键处理")]
        public string B6ActionClassName { private set; get; }

        [ExcelField("第7个按键内容")]
        public string B7Content { private set; get; }

        [ExcelField("第7个按键处理")]
        public string B7ActionClassName { private set; get; }

        [ExcelField("第8个按键内容")]
        public string B8Content { private set; get; }

        [ExcelField("第8个按键处理")]
        public string B8ActionClassName { private set; get; }


        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}