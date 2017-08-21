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
    ///     LCM单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "MCMUnit")]
    public class MCMUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public MCMUnit()
        {
            if (m_MCMPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_MCMPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<MCMPriorityService>();
                if (m_MCMPriorityService != null)
                {
                    State = m_MCMPriorityService.GetLowPriority();
                }
            }

        }
        private MCMState m_State;

        /// <summary>
        ///     MCM状态
        /// </summary>
        public MCMState State
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
        ///  MCM故障
        /// </summary>
        [ExcelField("MCM故障")]
        public string Fault { get; set; }

        /// <summary>
        /// MCM切除
        /// </summary>
        [ExcelField("MCM切除")]
        public string Cut { get; set; }

        /// <summary>
        /// MCM关
        /// </summary>
        [ExcelField("MCM运行")]
        public string TurnOn { get; set; }

        /// <summary>
        /// MCM未运行
        /// </summary>
        [ExcelField("MCM未运行")]
        public string TurnOff { get; set; }

        /// <summary>
        /// MCM切入切除
        /// </summary>
        [ExcelField("MCM切入切除")]
        public string CutInAndCutOFF { get; set; }

        /// <summary>
        /// 选择状态
        /// </summary>
        public bool IsChecked
        {
            get { return m_IsChecked; }
            set
            {
                if (value == m_IsChecked)
                {
                    return;
                }
                m_IsChecked = value;
                RaisePropertyChanged(() => IsChecked);
            }
        }

        private readonly MCMPriorityService m_MCMPriorityService;


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
            args.UpdateIfContain(Cut, b =>
            {
                State = m_MCMPriorityService.GetPriorityValue(MCMState.Cut, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_MCMPriorityService.GetPriorityValue(MCMState.Fault, b, this);
            });
            args.UpdateIfContain(TurnOn, b =>
            {
                State = m_MCMPriorityService.GetPriorityValue(MCMState.TurnOn, b, this);
            });
            args.UpdateIfContain(TurnOff, b =>
            {
                State = m_MCMPriorityService.GetPriorityValue(MCMState.TurnOff, b, this);
            });
        }
    }

    /// <summary>
    ///     MCM状态
    /// </summary>
    public enum MCMState
    {
        /// <summary>
        /// MCM未运行
        /// </summary>
        [Description("MCM未运行")]
        TurnOff,

        /// <summary>
        ///　MCM故障
        /// </summary>
        [Description("MCM故障")]
        Fault,

        /// <summary>
        /// MCM切除
        /// </summary>
        [Description("MCM切除")]
        Cut,
        
        /// <summary>
        /// MCM运行
        /// </summary>
        [Description("MCM运行")]
        TurnOn,
        
    }
}
