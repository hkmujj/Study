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
    ///     DCDevice1单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "DCDevice1Unit")]
    public class DCDevice1Unit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public DCDevice1Unit()
        {
            if (m_DCDevice1PriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_DCDevice1PriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<DCDevice1PriorityService>();
                if (m_DCDevice1PriorityService != null)
                {
                    State = m_DCDevice1PriorityService.GetLowPriority();
                }
            }

        }
        private DCDevice1State m_State;

        /// <summary>
        ///     DCDevice1状态
        /// </summary>
        public DCDevice1State State
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
        ///  设备1故障
        /// </summary>
        [ExcelField("设备1故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 设备1正常
        /// </summary>
        [ExcelField("设备1正常")]
        public string Normal { get; set; }


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

        private readonly DCDevice1PriorityService m_DCDevice1PriorityService;


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
            args.UpdateIfContain(Normal, b =>
            {
                State = m_DCDevice1PriorityService.GetPriorityValue(DCDevice1State.Normal, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_DCDevice1PriorityService.GetPriorityValue(DCDevice1State.Fault, b, this);
            });
        }
    }

    /// <summary>
    ///     DCDevice1状态
    /// </summary>
    public enum DCDevice1State
    {
        /// <summary>
        /// DCDevice1正常
        /// </summary>
        [Description("设备1正常")]
        Normal,

        /// <summary>
        ///　DCDevice1故障
        /// </summary>
        [Description("设备1故障")]
        Fault,
    }
}
