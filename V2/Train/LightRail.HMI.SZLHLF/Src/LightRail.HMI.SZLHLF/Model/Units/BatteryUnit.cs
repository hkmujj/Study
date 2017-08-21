using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using LightRail.HMI.SZLHLF.Extension;
using LightRail.HMI.SZLHLF.Interfaces;
using LightRail.HMI.SZLHLF.Service;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.SZLHLF.Model.Units
{
    /// <summary>
    /// 电池单元
    /// </summary>
    [ExcelLocation("龙华车辆屏界面接口表.xls", "BatteryUnit")]
    public class BatteryUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private BatteryState m_State;

        /// <summary>
        /// 
        /// </summary>
        public BatteryUnit()
        {
            if (GlobalParam.Instance.InitParam != null)
                m_BatteryPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<BatteryPriorityService>();
            if (m_BatteryPriorityService != null)
            {
                State = m_BatteryPriorityService.GetLowPriority();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public BatteryState State
        {
            get { return m_State; }
            set
            {
                if (value == m_State)
                {
                    return;
                }
                m_State = value;
                RaisePropertyChanged(() => State);
            }
        }

        private readonly BatteryPriorityService m_BatteryPriorityService;
        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }


        /// <summary>
        /// 电池故障
        /// </summary>
        [ExcelField("电池故障")]
        public string Fault { get; set; }


        /// <summary>
        /// 电池正常
        /// </summary>
        [ExcelField("电池正常")]
        public string Normal { get; set; }
        

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }

        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(Fault, b =>
            {
                State = m_BatteryPriorityService.GetPriorityValue(BatteryState.Fault, b, this);
            });
            args.UpdateIfContain(Normal, b =>
            {
                State = m_BatteryPriorityService.GetPriorityValue(BatteryState.Normal, b, this);
            });

        }
    }

    /// <summary>
    /// 电池状态
    /// </summary>
    public enum BatteryState
    {
        /// <summary>
        /// 电池正常
        /// </summary>
        [Description("电池正常")]
        Normal,
        /// <summary>
        /// 电池故障
        /// </summary>
        [Description("电池故障")]
        Fault,
    }
}