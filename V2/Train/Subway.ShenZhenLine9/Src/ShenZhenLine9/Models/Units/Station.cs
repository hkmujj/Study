using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Models.Units
{
    /// <summary>
    /// 站点
    /// </summary>
    [ExcelLocation("站点名称.xls", "Sheet1")]
    public class Station : NotificationObject, ISetValueProvider, IStation
    {
        private bool m_IsChecked;

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }

        /// <summary>
        /// 编号
        /// </summary>
        [ExcelField("编号")]
        public int Index { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [ExcelField("名称")]
        public string Name { get; set; }

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
    }
}