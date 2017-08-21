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
    [ExcelLocation("界面接口表.xls", "LCMUnit")]
    public class LCMUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public LCMUnit()
        {
            if (m_LCMPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_LCMPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<LCMPriorityService>();
                if (m_LCMPriorityService != null)
                {
                    State = m_LCMPriorityService.GetLowPriority();
                }
            }

        }
        private LCMState m_State;

        /// <summary>
        ///     LCM状态
        /// </summary>
        public LCMState State
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
        ///  LCM故障
        /// </summary>
        [ExcelField("LCM故障")]
        public string Fault { get; set; }

        /// <summary>
        /// LCM切除
        /// </summary>
        [ExcelField("LCM切除")]
        public string Cut { get; set; }

        /// <summary>
        /// LCM关
        /// </summary>
        [ExcelField("LCM运行")]
        public string TurnOn { get; set; }

        /// <summary>
        /// LCM未运行
        /// </summary>
        [ExcelField("LCM未运行")]
        public string TurnOff { get; set; }

        /// <summary>
        /// LCM切入切除
        /// </summary>
        [ExcelField("LCM切入切除")]
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

        private readonly LCMPriorityService m_LCMPriorityService;


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
                State = m_LCMPriorityService.GetPriorityValue(LCMState.Cut, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_LCMPriorityService.GetPriorityValue(LCMState.Fault, b, this);
            });
            args.UpdateIfContain(TurnOn, b =>
            {
                State = m_LCMPriorityService.GetPriorityValue(LCMState.TurnOn, b, this);
            });
            args.UpdateIfContain(TurnOff, b =>
            {
                State = m_LCMPriorityService.GetPriorityValue(LCMState.TurnOff, b, this);
            });
        }
    }

    /// <summary>
    ///     LCM状态
    /// </summary>
    public enum LCMState
    {
        /// <summary>
        /// LCM未运行
        /// </summary>
        [Description("LCM未运行")]
        TurnOff,

        /// <summary>
        ///　LCM故障
        /// </summary>
        [Description("LCM故障")]
        Fault,

        /// <summary>
        /// LCM切除
        /// </summary>
        [Description("LCM切除")]
        Cut,
        
        /// <summary>
        /// LCM运行
        /// </summary>
        [Description("LCM运行")]
        TurnOn,
        
    }
}
