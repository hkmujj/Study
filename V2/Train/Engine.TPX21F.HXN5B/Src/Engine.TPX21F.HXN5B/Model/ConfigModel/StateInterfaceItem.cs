using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Engine.TPX21F.HXN5B.Model.ConfigModel
{
    [ExcelLocation("Engine.TPX21F.HXN5B按键状态集.xls", "Sheet1")]
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

        [ExcelField("选择的按键名")]
        public string SelectedBtnNames { private set; get; }

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

        [ExcelField("温度按键处理")]
        public string BTemperatureActionClassName { private set; get; }

        [ExcelField("温度旁边按键处理")]
        public string BUnlockActionClassName { private set; get; }

        [ExcelField("亮度变暗按键处理")]
        public string BLightDownActionClassName { private set; get; }

        [ExcelField("亮度变亮按键处理")]
        public string BLightUpActionClassName { private set; get; }

        [ExcelField("对比度按键处理")]
        public string BContrastActionClassName { private set; get; }

        [ExcelField("声音变小按键处理")]
        public string BSoundDownActionClassName { private set; get; }

        [ExcelField("声音变大按键处理")]
        public string BSoundUpActionClassName { private set; get; }

        [ExcelField("上下文按键处理")]
        public string BContextActionClassName { private set; get; }

        [ExcelField("叹号按键处理")]
        public string BExclamationActionClassName { private set; get; }

        [ExcelField("问号按键处理")]
        public string BQuestionMarkActionClassName { private set; get; }

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