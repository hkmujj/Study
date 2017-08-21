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
    /// 牵引单元
    /// </summary>
    [ExcelLocation("广州有轨电车车辆屏界面接口表.xls", "TractionUnit")]
    public class TractionUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private TractionState m_State;

        /// <summary>
        /// 
        /// </summary>
        public TractionUnit()
        {
            m_TractionPriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<TractionPriorityService>();
            if (m_TractionPriorityService != null)
            {
                State = m_TractionPriorityService.GetLowPriority();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public TractionState State
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

        private readonly TractionPriorityService m_TractionPriorityService;
        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }


        /// <summary>
        /// 牵引严重故障
        /// </summary>
        [ExcelField("牵引严重故障")]
        public string SeriousFault { get; set; }


        /// <summary>
        /// 牵引次要故障
        /// </summary>
        [ExcelField("牵引次要故障")]
        public string MinorFault { get; set; }

        /// <summary>
        /// 牵引正在自检
        /// </summary>
        [ExcelField("牵引正在自检")]
        public string SelfChecking { get; set; }

        /// <summary>
        /// 牵引运行
        /// </summary>
        [ExcelField("牵引运行")]
        public string Run { get; set; }

        /// <summary>
        /// 牵引停止
        /// </summary>
        [ExcelField("牵引停止")]
        public string Stop { get; set; }


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
                State = m_TractionPriorityService.GetPriorityValue(TractionState.SeriousFault, b, this);
            });
            args.UpdateIfContain(MinorFault, b =>
            {
                State = m_TractionPriorityService.GetPriorityValue(TractionState.MinorFault, b, this);
            });
            args.UpdateIfContain(SelfChecking, b =>
            {
                State = m_TractionPriorityService.GetPriorityValue(TractionState.SelfChecking, b, this);
            });
            args.UpdateIfContain(Run, b =>
            {
                State = m_TractionPriorityService.GetPriorityValue(TractionState.Run, b, this);
            });
            args.UpdateIfContain(Stop, b =>
            {
                State = m_TractionPriorityService.GetPriorityValue(TractionState.Stop, b, this);
            });
           

        }
    }

    /// <summary>
    /// 牵引状态
    /// </summary>
    public enum TractionState
    {
        /// <summary>
        /// 牵引严重故障
        /// </summary>
        [Description("牵引严重故障")]
        SeriousFault,
        /// <summary>
        /// 牵引次要故障
        /// </summary>
        [Description("牵引次要故障")]
        MinorFault,
        /// <summary>
        /// 牵引正在自检
        /// </summary>
        [Description("牵引正在自检")]
        SelfChecking,
        /// <summary>
        /// 牵引运行
        /// </summary>
        [Description("牵引运行")]
        Run,
        /// <summary>
        /// 牵引停止
        /// </summary>
        [Description("牵引停止")]
        Stop,
       
    }
}