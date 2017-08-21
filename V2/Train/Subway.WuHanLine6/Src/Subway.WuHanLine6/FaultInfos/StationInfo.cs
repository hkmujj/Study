using System;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.WuHanLine6.FaultInfos
{
    /// <summary>
    /// 站点名称
    /// </summary>
    [ExcelLocation("车站名称.xls", "Sheet1")]
    public class StationInfo : NotificationObject, ISetValueProvider
    {
        private bool m_IsChecked;

        /// <summary>
        /// 车站名称
        /// </summary>
        [ExcelField("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 车站编号
        /// </summary>
        [ExcelField("编号")]
        public int Index { get; set; }

        /// <summary>
        /// IsChecked改变事件
        /// </summary>
        public event Action<StationInfo> IsCheckedChanged;
        /// <summary>
        /// 
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
                OnIsCheckedChanged(this);
                RaisePropertyChanged(() => IsChecked);
            }
        }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }

        private void OnIsCheckedChanged(StationInfo obj)
        {
            if (IsCheckedChanged != null) IsCheckedChanged.Invoke(obj);
        }
    }
}