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
    ///     DCDevice2单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "DCDevice2Unit")]
    public class DCDevice2Unit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public DCDevice2Unit()
        {
            if (m_DCDevice2PriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_DCDevice2PriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<DCDevice2PriorityService>();
                if (m_DCDevice2PriorityService != null)
                {
                    State = m_DCDevice2PriorityService.GetLowPriority();
                }
            }

        }
        private DCDevice2State m_State;

        /// <summary>
        ///     DCDevice2状态
        /// </summary>
        public DCDevice2State State
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
        ///  设备2故障
        /// </summary>
        [ExcelField("设备2故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 设备2正常
        /// </summary>
        [ExcelField("设备2正常")]
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

        private readonly DCDevice2PriorityService m_DCDevice2PriorityService;


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
                State = m_DCDevice2PriorityService.GetPriorityValue(DCDevice2State.Normal, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_DCDevice2PriorityService.GetPriorityValue(DCDevice2State.Fault, b, this);
            });
        }
    }

    /// <summary>
    ///     DCDevice2状态
    /// </summary>
    public enum DCDevice2State
    {
        /// <summary>
        /// 设备2正常
        /// </summary>
        [Description("设备2正常")]
        Normal,

        /// <summary>
        ///　设备2故障
        /// </summary>
        [Description("设备2故障")]
        Fault,
    }
}
