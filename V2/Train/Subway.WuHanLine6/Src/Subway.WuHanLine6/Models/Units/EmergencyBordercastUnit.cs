using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.WuHanLine6.Models.Units
{
    /// <summary>
    ///     紧急广播单元
    /// </summary>
    [ExcelLocation("紧急广播.xls", "Sheet1")]
    public class EmergencyBordercastUnit : NotificationObject, ISetValueProvider
    {
        private bool m_IsChecked;

        /// <summary>
        ///     显示内容
        /// </summary>
        [ExcelField("显示内容")]
        public string Content { get; set; }

        /// <summary>
        ///     发送逻辑号
        /// </summary>
        [ExcelField("发送逻辑号")]
        public string SendValueLogic { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked
        {
            get { return m_IsChecked; }
            set
            {
                if (value == m_IsChecked)
                {
                    return;
                }
                m_IsChecked = value;
                RaisePropertyChanged(() => IsChecked);
            }
        }

        /// <summary>
        ///     广播编号
        /// </summary>
        [ExcelField("广播编号")]
        public int Number { get; set; }

        /// <summary />
        /// <param name="propertyOrFieldName" />
        /// <param name="value" />
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}