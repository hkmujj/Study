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
    ///     停放制动单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "ParkBreakUnit")]
    public class ParkBreakUnit : NotificationObject, ISetValueProvider, IUnit
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ParkBreakUnit()
        {
            if (m_ParkBreakPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_ParkBreakPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<ParkBreakPriorityService>();
                if (m_ParkBreakPriorityService != null)
                {
                    State = m_ParkBreakPriorityService.GetLowPriority();
                }
            }

        }
        private ParkBreakState m_State;

        /// <summary>
        ///     停放制动状态
        /// </summary>
        public ParkBreakState State
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
        ///  停放制动施加
        /// </summary>
        [ExcelField("停放制动施加")]
        public string Force { get; set; }

        /// <summary>
        /// 停放制动缓解
        /// </summary>
        [ExcelField("停放制动缓解")]
        public string Remit { get; set; }

        /// <summary>
        /// 停放制动故障
        /// </summary>
        [ExcelField("停放制动故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 停放制动隔离
        /// </summary>
        [ExcelField("停放制动隔离")]
        public string Cut { get; set; }


        private readonly ParkBreakPriorityService m_ParkBreakPriorityService;


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
            args.UpdateIfContain(Force, b =>
            {
                State = m_ParkBreakPriorityService.GetPriorityValue(ParkBreakState.Force, b, this);
            });
            args.UpdateIfContain(Remit, b =>
            {
                State = m_ParkBreakPriorityService.GetPriorityValue(ParkBreakState.Remit, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_ParkBreakPriorityService.GetPriorityValue(ParkBreakState.Fault, b, this);
            });
            args.UpdateIfContain(Cut, b =>
            {
                State = m_ParkBreakPriorityService.GetPriorityValue(ParkBreakState.Cut, b, this);
            });
        }
    }

    /// <summary>
    ///     停放制动状态
    /// </summary>
    public enum ParkBreakState
    {
        /// <summary>
        ///  停放制动施加
        /// </summary>
        [Description("停放制动施加")]
        Force,
        /// <summary>
        /// 停放制动缓解
        /// </summary>
        [Description("停放制动缓解")]
        Remit,
        /// <summary>
        /// 停放制动故障
        /// </summary>
        [Description("停放制动故障")]
        Fault,
        /// <summary>
        /// 停放制动隔离
        /// </summary>
        [Description("停放制动隔离")]
        Cut,
    }
}
