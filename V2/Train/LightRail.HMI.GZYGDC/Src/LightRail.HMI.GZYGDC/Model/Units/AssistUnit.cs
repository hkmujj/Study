using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using LightRail.HMI.GZYGDC.Extension;
using LightRail.HMI.GZYGDC.Interfaces;
using LightRail.HMI.GZYGDC.Service;

namespace LightRail.HMI.GZYGDC.Model.Units
{
    /// <summary>
    /// 
    /// </summary>
    [ExcelLocation("广州有轨电车车辆屏界面接口表.xls", "AssistUnit")]
    public class AssistUnit : NotificationObject, IUnit, ISetValueProvider
    {
        private AssistState m_State;

        /// <summary>
        /// 
        /// </summary>
        public AssistUnit()
        {
            m_AssistPriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<AssistPriorityService>();
            if (m_AssistPriorityService != null)
            {
                State = m_AssistPriorityService.GetLowPriority();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public AssistState State
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

        private readonly AssistPriorityService m_AssistPriorityService;
        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [ExcelField("位置")]
        public int Location { get; set; }


        /// <summary>
        /// 辅助电源严重故障
        /// </summary>
        [ExcelField("辅助电源严重故障")]
        public string SeriousFault { get; set; }


        /// <summary>
        /// 辅助电源次要故障
        /// </summary>
        [ExcelField("辅助电源次要故障")]
        public string MinorFault { get; set; }


        /// <summary>
        /// 辅助电源运行
        /// </summary>
        [ExcelField("辅助电源运行")]
        public string Run { get; set; }
 
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {

            args.UpdateIfContain(SeriousFault, b =>
            {
                State = m_AssistPriorityService.GetPriorityValue(AssistState.SeriousFault, b, this);
            });
            args.UpdateIfContain(MinorFault, b =>
            {
                State = m_AssistPriorityService.GetPriorityValue(AssistState.MinorFault, b, this);
            });
            args.UpdateIfContain(Run, b =>
            {
                State = m_AssistPriorityService.GetPriorityValue(AssistState.Run, b, this);
            });
        }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum AssistState
    {
        /// <summary>
        /// 充电机故障
        /// </summary>
        [Description("辅助电源严重故障")]
        SeriousFault,

        /// <summary>
        /// 充电机运行
        /// </summary>
        [Description("辅助电源次要故障")]
        MinorFault,

        /// <summary>
        /// 辅助电源运行
        /// </summary>
        Run,
    }
}