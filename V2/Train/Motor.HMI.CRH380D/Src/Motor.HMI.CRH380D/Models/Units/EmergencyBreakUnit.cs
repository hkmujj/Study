using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Interfaces;
using Motor.HMI.CRH380D.Service;

namespace Motor.HMI.CRH380D.Models.Units
{
    /// <summary>
    ///     紧急制动安全制动单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "EmergencyBreakUnit")]
    public class EmergencyBreakUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public EmergencyBreakUnit()
        {
            if (m_EmergencyBreakPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_EmergencyBreakPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<EmergencyBreakPriorityService>();
                if (m_EmergencyBreakPriorityService != null)
                {
                    State = m_EmergencyBreakPriorityService.GetLowPriority();
                }
            }

        }
        private EmergencyBreakState m_State;

        /// <summary>
        ///     紧急制动安全制动状态
        /// </summary>
        public EmergencyBreakState State
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

        /// <summary>
        /// 编号
        /// </summary>
        [ExcelField("编号")]
        public int Num { get; set; }

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
        ///  安全制动施加
        /// </summary>
        [ExcelField("安全制动施加")]
        public string SafeBreakForce { get; set; }

        /// <summary>
        /// 紧急制动施加
        /// </summary>
        [ExcelField("紧急制动施加")]
        public string EmergencyBreakForce { get; set; }

        /// <summary>
        /// 紧急制动安全制动缓解
        /// </summary>
        [ExcelField("紧急制动安全制动缓解")]
        public string Remit { get; set; }

        /// <summary>
        /// 紧急制动安全制动故障
        /// </summary>
        [ExcelField("紧急制动安全制动故障")]
        public string Fault { get; set; }


        private readonly EmergencyBreakPriorityService m_EmergencyBreakPriorityService;


        /// <summary />
        /// <param name="propertyOrFieldName" />
        /// <param name="value" />
        public void SetValue(string propertyOrFieldName, string value)
        {
            
        }

        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        public void DataChanged(IDictionary<int, bool> args)
        {
            args.UpdateIfContain(SafeBreakForce, b =>
            {
                State = m_EmergencyBreakPriorityService.GetPriorityValue(EmergencyBreakState.SafeBreakForce, b, this);
            });
            args.UpdateIfContain(EmergencyBreakForce, b =>
            {
                State = m_EmergencyBreakPriorityService.GetPriorityValue(EmergencyBreakState.EmergencyBreakForce, b, this);
            });
            args.UpdateIfContain(Remit, b =>
            {
                State = m_EmergencyBreakPriorityService.GetPriorityValue(EmergencyBreakState.Remit, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_EmergencyBreakPriorityService.GetPriorityValue(EmergencyBreakState.Fault, b, this);
            });
        }
    }

    /// <summary>
    ///     紧急制动安全制动状态
    /// </summary>
    public enum EmergencyBreakState
    {
        /// <summary>
        /// 紧急制动施加
        /// </summary>
        [Description("紧急制动施加")]
        EmergencyBreakForce,
        /// <summary>
        /// 安全制动施加
        /// </summary>
        [Description("安全制动施加")]
        SafeBreakForce,
        /// <summary>
        ///　紧急制动安全制动缓解
        /// </summary>
        [Description("紧急制动安全制动缓解")]
        Remit,
        /// <summary>
        ///　紧急制动安全制动故障
        /// </summary>
        [Description("紧急制动安全制动故障")]
        Fault,
    }
}
