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
    ///     ACM单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "ACMUnit")]
    public class ACMUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public ACMUnit()
        {
            if (m_ACMPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_ACMPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<ACMPriorityService>();
                if (m_ACMPriorityService != null)
                {
                    State = m_ACMPriorityService.GetLowPriority();
                }
            }

        }
        private ACMState m_State;

        /// <summary>
        ///     ACM状态
        /// </summary>
        public ACMState State
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
        ///  ACM故障
        /// </summary>
        [ExcelField("ACM故障")]
        public string Fault { get; set; }

        /// <summary>
        /// ACM切除
        /// </summary>
        [ExcelField("ACM切除")]
        public string Cut { get; set; }

        /// <summary>
        /// ACM关
        /// </summary>
        [ExcelField("ACM运行")]
        public string TurnOn { get; set; }

        /// <summary>
        /// ACM未运行
        /// </summary>
        [ExcelField("ACM未运行")]
        public string TurnOff { get; set; }

        /// <summary>
        /// ACM切入切除
        /// </summary>
        [ExcelField("ACM切入切除")]
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

        private readonly ACMPriorityService m_ACMPriorityService;


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
                State = m_ACMPriorityService.GetPriorityValue(ACMState.Cut, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_ACMPriorityService.GetPriorityValue(ACMState.Fault, b, this);
            });
            args.UpdateIfContain(TurnOn, b =>
            {
                State = m_ACMPriorityService.GetPriorityValue(ACMState.TurnOn, b, this);
            });
            args.UpdateIfContain(TurnOff, b =>
            {
                State = m_ACMPriorityService.GetPriorityValue(ACMState.TurnOff, b, this);
            });
        }
    }

    /// <summary>
    ///     ACM状态
    /// </summary>
    public enum ACMState
    {
        /// <summary>
        /// ACM未运行
        /// </summary>
        [Description("ACM未运行")]
        TurnOff,

        /// <summary>
        ///　ACM故障
        /// </summary>
        [Description("ACM故障")]
        Fault,

        /// <summary>
        /// ACM切除
        /// </summary>
        [Description("ACM切除")]
        Cut,
        
        /// <summary>
        /// ACM运行
        /// </summary>
        [Description("ACM运行")]
        TurnOn,
        
    }
}
