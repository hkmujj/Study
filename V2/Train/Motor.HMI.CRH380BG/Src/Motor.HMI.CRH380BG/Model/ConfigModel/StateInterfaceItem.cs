using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Motor.HMI.CRH380BG.Model.ConfigModel
{
    [ExcelLocation("Motor.HMI.CRH380BG按键状态集.xls", "Sheet1")]
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

        [ExcelField("第9个按键内容")]
        public string B9Content { private set; get; }

        [ExcelField("第9个按键处理")]
        public string B9ActionClassName { private set; get; }

        [ExcelField("第10个按键内容")]
        public string B10Content { private set; get; }

        [ExcelField("第10个按键处理")]
        public string B10ActionClassName { private set; get; }

        [ExcelField("开关按键处理")]
        public string BSwitchActionClassName { private set; get; }

        [ExcelField("亮度按键处理")]
        public string BLightActionClassName { private set; get; }

        [ExcelField("日夜切换按键处理")]
        public string BDayandnightSwitchActionClassName { private set; get; }

        [ExcelField("语言选择按键处理")]
        public string BLanguageSelectActionClassName { private set; get; }

        [ExcelField("信息盒按键处理")]
        public string BInfoBoxActionClassName { private set; get; }

        [ExcelField("故障信息按键处理")]
        public string BFaultInfoActionClassName { private set; get; }

        [ExcelField("列车行驶期间校正按键处理")]
        public string BTrainRunningCheckActionClassName { private set; get; }

        [ExcelField("列车停止校正处理")]
        public string BTrainStopCheckActionClassName { private set; get; }

        [ExcelField("显示器切换按键处理")]
        public string BSwitchDisplayActionClassName { private set; get; }

        [ExcelField("返回按键处理")]
        public string BReturnActionClassName { private set; get; }

        [ExcelField("向上按键处理")]
        public string BUpActionClassName { private set; get; }

        [ExcelField("向下按键处理")]
        public string BDownActionClassName { private set; get; }

        [ExcelField("向左按键处理")]
        public string BLeftActionClassName { private set; get; }

        [ExcelField("向右按键处理")]
        public string BRightActionClassName { private set; get; }

        [ExcelField("确认按键处理")]
        public string BOKActionClassName { private set; get; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}