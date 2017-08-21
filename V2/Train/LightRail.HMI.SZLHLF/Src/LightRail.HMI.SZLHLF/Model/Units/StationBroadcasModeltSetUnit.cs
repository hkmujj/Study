using System.Diagnostics;
using Excel.Interface;
using LightRail.HMI.SZLHLF.Extension;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.SZLHLF.Model.Units
{
    [DebuggerDisplay("Name={Name}")]
    [ExcelLocation("龙华车辆屏界面接口表.xls", "StationBroadcasModeltSet")]
    public class StationBroadcasModeltSetUnit : NotificationObject, ISetValueProvider
    {
        private bool _isChecked;

        /// <summary>
        /// 编号
        /// </summary>
        [ExcelField("编号")]
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [ExcelField("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 发送逻辑接口号
        /// </summary>
        [ExcelField("发送逻辑号")]
        public string SendLogicName { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (value == _isChecked) return;
                _isChecked = value;
                SendLogicName.SendBoolData(value);
                RaisePropertyChanged(() => IsChecked);
            }
        }

        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}