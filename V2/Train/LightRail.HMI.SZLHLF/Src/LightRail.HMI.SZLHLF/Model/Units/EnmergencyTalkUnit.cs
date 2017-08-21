using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using LightRail.HMI.SZLHLF.Service;
using LightRail.HMI.SZLHLF.Extension;
using LightRail.HMI.SZLHLF.Interfaces;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.SZLHLF.Model.Units
{
    /// <summary>
    /// 紧急对讲单元
    /// </summary>
    [ExcelLocation("龙华车辆屏界面接口表.xls", "EnmergencyTalkUnit")]
    public class EnmergencyTalkUnit : NotificationObject, IUnit, ISetValueProvider
    {
        private EmergencyTalkState m_State;

        /// <summary>
        /// 
        /// </summary>
        public EnmergencyTalkUnit()
        {
            if (GlobalParam.Instance.InitParam != null)
                m_EmergencyTalkPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<EmergencyTalkPriorityService>();
            if (m_EmergencyTalkPriorityService != null)
            {
                State = m_EmergencyTalkPriorityService.GetLowPriority();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public EmergencyTalkState State
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

        private readonly EmergencyTalkPriorityService m_EmergencyTalkPriorityService;
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
        /// 严重故障
        /// </summary>
        [ExcelField("严重故障")]
        public string SeriousFault { get; set; }
        /// <summary>
        /// 通话中
        /// </summary>
        [ExcelField("通话中")]
        public string Calling { get; set; }
        /// <summary>
        /// 通话请求
        /// </summary>
        [ExcelField("通话请求")]
        public string CallRequest { get; set; }
        /// <summary>
        /// 通话结束
        /// </summary>
        [ExcelField("通话结束")]
        public string CallEnd { get; set; }
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(SeriousFault, b =>
             {
                 State = m_EmergencyTalkPriorityService.GetPriorityValue(EmergencyTalkState.SeriousFault, b,this);
             });
            args.UpdateIfContain(Calling, b =>
            {
                State = m_EmergencyTalkPriorityService.GetPriorityValue(EmergencyTalkState.Calling, b,this);
            });
            args.UpdateIfContain(CallRequest, b =>
            {
                State = m_EmergencyTalkPriorityService.GetPriorityValue(EmergencyTalkState.CallRequest, b,this);
            });
            args.UpdateIfContain(CallEnd, b =>
            {
                State = m_EmergencyTalkPriorityService.GetPriorityValue(EmergencyTalkState.CallEnd, b,this);
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
    public enum EmergencyTalkState
    {
        /// <summary>
        /// 通话结束
        /// </summary>
        [Description("通话结束")]
        CallEnd,
        /// <summary>
        /// 严重故障
        /// </summary>
        [Description("严重故障")]
        SeriousFault,
        /// <summary>
        /// 通话中
        /// </summary>
        [Description("通话中")]
        Calling,
        /// <summary>
        /// 通话请求
        /// </summary>
        [Description("通话请求")]
        CallRequest,
    }
}