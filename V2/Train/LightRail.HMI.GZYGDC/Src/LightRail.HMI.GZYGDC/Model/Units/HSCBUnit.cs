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
    /// HSCB高速断路器单元
    /// </summary>
    [ExcelLocation("广州有轨电车车辆屏界面接口表.xls", "HSCBUnit")]
    public class HSCBUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private HSCBState m_State;

        /// <summary>
        /// 
        /// </summary>
        public HSCBUnit()
        {
            m_HSCBPriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<HSCBPriorityService>();
            if (m_HSCBPriorityService != null)
            {
                State = m_HSCBPriorityService.GetLowPriority();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public HSCBState State
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

        private readonly HSCBPriorityService m_HSCBPriorityService;
        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }


        /// <summary>
        /// 高速断路器严重故障
        /// </summary>
        [ExcelField("高速断路器严重故障")]
        public string SeriousFault { get; set; }


        /// <summary>
        /// 高速断路器闭合
        /// </summary>
        [ExcelField("高速断路器闭合")]
        public string Close { get; set; }


        /// <summary>
        /// 高速断路器断开
        /// </summary>
        [ExcelField("高速断路器断开")]
        public string Discon { get; set; }


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
            args.UpdateIfContain(SeriousFault, b =>
            {
                State = m_HSCBPriorityService.GetPriorityValue(HSCBState.SeriousFault, b, this);
            });
            args.UpdateIfContain(Close, b =>
            {
                State = m_HSCBPriorityService.GetPriorityValue(HSCBState.Close, b, this);
            });
            args.UpdateIfContain(Discon, b =>
            {
                State = m_HSCBPriorityService.GetPriorityValue(HSCBState.Discon, b, this);
            });


        }
    }

    /// <summary>
    /// 牵引状态
    /// </summary>
    public enum HSCBState
    {
        /// <summary>
        /// 高速断路器严重故障
        /// </summary>
        [Description("高速断路器严重故障")]
        SeriousFault,
        /// <summary>
        /// 高速断路器闭合
        /// </summary>
        [Description("高速断路器闭合")]
        Close,
        /// <summary>
        /// 高速断路器断开
        /// </summary>
        [Description("高速断路器断开")]
        Discon,
    }
}