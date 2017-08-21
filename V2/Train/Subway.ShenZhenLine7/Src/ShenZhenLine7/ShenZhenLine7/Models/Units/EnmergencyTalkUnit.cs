using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.ShenZhenLine7.Extention;
using Subway.ShenZhenLine7.Interfaces;
using Subway.ShenZhenLine7.Service;

namespace Subway.ShenZhenLine7.Models.Units
{
    /// <summary>
    /// 紧急对讲单元
    /// </summary>
    [ExcelLocation("深圳地铁7号线子系统界面接口表.xls", "EnmergencyTalkUnit")]
    public class EnmergencyTalkUnit : NotificationObject, IUnit, ISetValueProvider
    {
        private EmergencyTalkState m_State;

        /// <summary>
        /// 
        /// </summary>
        public EnmergencyTalkUnit()
        {
            m_EmergencyTalkPriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<EmergencyTalkPriorityService>();
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
        /// 故障
        /// </summary>
        [ExcelField("故障")]
        public string Fault { get; set; }
        /// <summary>
        /// 激活
        /// </summary>
        [ExcelField("激活")]
        public string Active { get; set; }
        /// <summary>
        /// 对讲
        /// </summary>
        [ExcelField("对讲")]
        public string Talk { get; set; }
        /// <summary>
        /// 未激活
        /// </summary>
        [ExcelField("未激活")]
        public string UnActive { get; set; }
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(Fault, b =>
             {
                 State = m_EmergencyTalkPriorityService.GetPriorityValue(EmergencyTalkState.Fault, b,this);
             });
            args.UpdateIfContain(Active, b =>
            {
                State = m_EmergencyTalkPriorityService.GetPriorityValue(EmergencyTalkState.Active, b,this);
            });
            args.UpdateIfContain(Talk, b =>
            {
                State = m_EmergencyTalkPriorityService.GetPriorityValue(EmergencyTalkState.Talk, b,this);
            });
            args.UpdateIfContain(UnActive, b =>
            {
                State = m_EmergencyTalkPriorityService.GetPriorityValue(EmergencyTalkState.UnActive, b,this);
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
        /// 故障
        /// </summary>
        [Description("乘客紧急通讯单元故障")]
        Fault,
        /// <summary>
        /// 激活
        /// </summary>
        [Description("乘客紧急通讯单元激活，乘客要求紧急对讲")]
        Active,
        /// <summary>
        /// 对讲
        /// </summary>
        [Description("乘客紧急通讯单元激活，司机已打开通讯通道")]
        Talk,
        /// <summary>
        /// 未激活
        /// </summary>
        [Description("乘客紧急通讯单元正常，未激活")]
        UnActive,
    }
}