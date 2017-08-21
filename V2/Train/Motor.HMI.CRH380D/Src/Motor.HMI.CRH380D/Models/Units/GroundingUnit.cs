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
    [ExcelLocation("界面接口表.xls", "GroundingUnit")]
    public class GroundingUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public GroundingUnit()
        {
            if (m_GroundingPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_GroundingPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<GroundingPriorityService>();
                if (m_GroundingPriorityService != null)
                {
                    State = m_GroundingPriorityService.GetLowPriority();
                }
            }

        }
        private GroundingState m_State;

        /// <summary>
        ///     高速断路器状态
        /// </summary>
        public GroundingState State
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
        ///  接地开关故障
        /// </summary>
        [ExcelField("接地开关故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 接地开关开
        /// </summary>
        [ExcelField("接地开关开")]
        public string Open { get; set; }

        /// <summary>
        /// 接地开关关
        /// </summary>
        [ExcelField("接地开关关")]
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

        private readonly GroundingPriorityService m_GroundingPriorityService;


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
                State = m_GroundingPriorityService.GetPriorityValue(GroundingState.Closed, b, this);
            });
            args.UpdateIfContain(Open, b =>
            {
                State = m_GroundingPriorityService.GetPriorityValue(GroundingState.Open, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_GroundingPriorityService.GetPriorityValue(GroundingState.Fault, b, this);
            });
        }
    }

    /// <summary>
    ///     高速断路器状态
    /// </summary>
    public enum GroundingState
    {
        /// <summary>
        /// 接地开关开
        /// </summary>
        [Description("接地开关开")]
        Open,

        /// <summary>
        /// 接地开关关
        /// </summary>
        [Description("接地开关关")]
        Closed,

        /// <summary>
        ///　接地开关故障
        /// </summary>
        [Description("接地开关故障")]
        Fault,
    }
}
