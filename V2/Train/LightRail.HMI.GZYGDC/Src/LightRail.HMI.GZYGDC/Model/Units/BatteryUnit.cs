using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommonUtil.Util;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using LightRail.HMI.GZYGDC.Extension;
using LightRail.HMI.GZYGDC.Interfaces;
using LightRail.HMI.GZYGDC.Service;

namespace LightRail.HMI.GZYGDC.Model.Units
{
    /// <summary>
    /// 电池单元
    /// </summary>
    [ExcelLocation("广州有轨电车车辆屏界面接口表.xls", "BatteryUnit")]
    public class BatteryUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private BatteryState m_State;

        /// <summary>
        /// 
        /// </summary>
        public BatteryUnit()
        {
            m_BatteryPriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<BatteryPriorityService>();
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


        /// <summary>
        /// 电池未充电
        /// </summary>
        [ExcelField("电池未充电")]
        public string unCharged { get; set; }


        /// <summary>
        /// 电池充电中
        /// </summary>
        [ExcelField("电池充电中")]
        public string Charging { get; set; }


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
            args.UpdateIfContain(unCharged, b =>
            {
                State = m_BatteryPriorityService.GetPriorityValue(BatteryState.unCharged, b, this);
            });
            args.UpdateIfContain(Charging, b =>
            {
                State = m_BatteryPriorityService.GetPriorityValue(BatteryState.Charging, b, this);
            });


        }
    }

    /// <summary>
    /// 电池状态
    /// </summary>
    public enum BatteryState
    {
        /// <summary>
        /// 电池故障
        /// </summary>
        [Description("电池故障")]
        Fault,
        /// <summary>
        /// 电池正常
        /// </summary>
        [Description("电池正常")]
        Normal,
        /// <summary>
        /// 电池未充电
        /// </summary>
        [Description("电池未充电")]
        unCharged,
        /// <summary>
        /// 电池充电中
        /// </summary>
        [Description("电池充电中")]
        Charging,
    }
}