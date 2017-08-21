using System.Diagnostics;
using Excel.Interface;

namespace Tram.CBTC.Casco.Model.ConfigModel
{
    [ExcelLocation("Subway.CBTC.QuanLuTong按键状态集.xls", "Sheet1")]
    [DebuggerDisplay("Key={Key}, Title={Title}")]
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

        [ExcelField("警惕按键内容")]
        public string BAlertContent { private set; get; }

        [ExcelField("警惕按键处理")]
        public string BAlertActionClassName { private set; get; }

        [ExcelField("解锁按键内容")]
        public string BUnlockContent { private set; get; }

        [ExcelField("解锁按键处理")]
        public string BUnlockActionClassName { private set; get; }

        [ExcelField("缓解按键内容")]
        public string BRelaseContent { private set; get; }

        [ExcelField("缓解按键处理")]
        public string BRelaseActionClassName { private set; get; }

        [ExcelField("查询按键内容")]
        public string BQueryContent { private set; get; }

        [ExcelField("查询按键处理")]
        public string BQueryActionClassName { private set; get; }

        [ExcelField("向左按键内容")]
        public string BLeftContent { private set; get; }

        [ExcelField("向左按键处理")]
        public string BLeftActionClassName { private set; get; }

        [ExcelField("向右按键内容")]
        public string BRightContent { private set; get; }

        [ExcelField("向右按键处理")]
        public string BRightActionClassName { private set; get; }

        [ExcelField("向上按键内容")]
        public string BUpContent { private set; get; }

        [ExcelField("向上按键处理")]
        public string BUpActionClassName { private set; get; }

        [ExcelField("向下按键内容")]
        public string BDownContent { private set; get; }

        [ExcelField("向下按键处理")]
        public string BDownActionClassName { private set; get; }

        [ExcelField("转存按键内容")]
        public string BSaveAsContent { private set; get; }

        [ExcelField("转存按键处理")]
        public string BSaveAsActionClassName { private set; get; }

        [ExcelField("设定按键内容")]
        public string BSettingContent { private set; get; }

        [ExcelField("设定按键处理")]
        public string BSettingActionClassName { private set; get; }

        [ExcelField("确认按键内容")]
        public string BOKContent { private set; get; }

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