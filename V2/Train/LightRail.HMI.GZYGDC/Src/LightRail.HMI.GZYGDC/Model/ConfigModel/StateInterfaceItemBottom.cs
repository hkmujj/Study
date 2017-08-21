using System.Diagnostics;
using Excel.Interface;

namespace LightRail.HMI.GZYGDC.Model.ConfigModel
{
    [ExcelLocation("LightRail.HMI.GZYGDC底部按键状态集.xls", "Sheet1")]
    [DebuggerDisplay("Key={Key}, Title={Title}")]
    public class StateInterfaceItemBottom : ISetValueProvider
    {
        [ExcelField("Key", true)]
        public string Key { private set; get; }

        [ExcelField("标题")]
        public string Title { private set; get; }

        [ExcelField("内容控件名")]
        public string ContentViewName { private set; get; }

        [ExcelField("内容控件标题")]
        public string ContentViewTitle { private set; get; }

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

        [ExcelField("第9个按键内容")]
        public string B9Content { private set; get; }

        [ExcelField("第9个按键处理")]
        public string B9ActionClassName { private set; get; }

        [ExcelField("第10个按键内容")]
        public string B10Content { private set; get; }

        [ExcelField("第10个按键处理")]
        public string B10ActionClassName { private set; get; }


        [ExcelField("第11个按键内容")]
        public string B11Content { private set; get; }

        [ExcelField("第11个按键处理")]
        public string B11ActionClassName { private set; get; }

        [ExcelField("第12个按键内容")]
        public string B12Content { private set; get; }

        [ExcelField("第12个按键处理")]
        public string B12ActionClassName { private set; get; }

        [ExcelField("第13个按键内容")]
        public string B13Content { private set; get; }

        [ExcelField("第13个按键处理")]
        public string B13ActionClassName { private set; get; }

        [ExcelField("第14个按键内容")]
        public string B14Content { private set; get; }

        [ExcelField("第14个按键处理")]
        public string B14ActionClassName { private set; get; }

        [ExcelField("第15个按键内容")]
        public string B15Content { private set; get; }

        [ExcelField("第15个按键处理")]
        public string B15ActionClassName { private set; get; }

        [ExcelField("第16个按键内容")]
        public string B16Content { private set; get; }

        [ExcelField("第16个按键处理")]
        public string B16ActionClassName { private set; get; }

        [ExcelField("第17个按键内容")]
        public string B17Content { private set; get; }

        [ExcelField("第17个按键处理")]
        public string B17ActionClassName { private set; get; }

        [ExcelField("第18个按键内容")]
        public string B18Content { private set; get; }

        [ExcelField("第18个按键处理")]
        public string B18ActionClassName { private set; get; }

        [ExcelField("第19个按键内容")]
        public string B19Content { private set; get; }

        [ExcelField("第19个按键处理")]
        public string B19ActionClassName { private set; get; }

        [ExcelField("第20个按键内容")]
        public string B20Content { private set; get; }

        [ExcelField("第20个按键处理")]
        public string B20ActionClassName { private set; get; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}