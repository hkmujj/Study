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
    ///     高速断路器单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "QuickBreakUnit")]
    public class QuickBreakUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public QuickBreakUnit()
        {
            if (m_QuickBreakPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_QuickBreakPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<QuickBreakPriorityService>();
                if (m_QuickBreakPriorityService != null)
                {
                    State = m_QuickBreakPriorityService.GetLowPriority();
                }
            }

        }
        private QuickBreakState m_State;

        /// <summary>
        ///     高速断路器状态
        /// </summary>
        public QuickBreakState State
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
        ///  高速断路器故障
        /// </summary>
        [ExcelField("高速断路器故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 高速断路器开
        /// </summary>
        [ExcelField("高速断路器开")]
        public string Open { get; set; }

        /// <summary>
        /// 高速断路器关
        /// </summary>
        [ExcelField("高速断路器关")]
        public string Closed { get; set; }

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

        private readonly QuickBreakPriorityService m_QuickBreakPriorityService;


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
            args.UpdateIfContain(Closed, b =>
            {
                State = m_QuickBreakPriorityService.GetPriorityValue(QuickBreakState.Closed, b, this);
            });
            args.UpdateIfContain(Open, b =>
            {
                State = m_QuickBreakPriorityService.GetPriorityValue(QuickBreakState.Open, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_QuickBreakPriorityService.GetPriorityValue(QuickBreakState.Fault, b, this);
            });
        }
    }

    /// <summary>
    ///     高速断路器状态
    /// </summary>
    public enum QuickBreakState
    {
        /// <summary>
        /// 高速断路器开
        /// </summary>
        [Description("高速断路器开")]
        Open,
        /// <summary>
        /// 高速断路器关
        /// </summary>
        [Description("高速断路器关")]
        Closed,
        /// <summary>
        ///　高速断路器故障
        /// </summary>
        [Description("高速断路器故障")]
        Fault,
    }
}
