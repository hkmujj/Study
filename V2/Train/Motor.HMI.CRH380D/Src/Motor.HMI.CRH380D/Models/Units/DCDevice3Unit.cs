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
    ///     DCDevice3单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "DCDevice3Unit")]
    public class DCDevice3Unit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public DCDevice3Unit()
        {
            if (m_DCDevice3PriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_DCDevice3PriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<DCDevice3PriorityService>();
                if (m_DCDevice3PriorityService != null)
                {
                    State = m_DCDevice3PriorityService.GetLowPriority();
                }
            }

        }
        private DCDevice3State m_State;

        /// <summary>
        ///     DCDevice3状态
        /// </summary>
        public DCDevice3State State
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
        ///  设备3故障
        /// </summary>
        [ExcelField("设备3故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 设备3正常
        /// </summary>
        [ExcelField("设备3正常")]
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

        private readonly DCDevice3PriorityService m_DCDevice3PriorityService;


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
                State = m_DCDevice3PriorityService.GetPriorityValue(DCDevice3State.Normal, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_DCDevice3PriorityService.GetPriorityValue(DCDevice3State.Fault, b, this);
            });
        }
    }

    /// <summary>
    ///     DCDevice3状态
    /// </summary>
    public enum DCDevice3State
    {
        /// <summary>
        /// 设备3正常
        /// </summary>
        [Description("设备3正常")]
        Normal,

        /// <summary>
        ///　设备3故障
        /// </summary>
        [Description("设备3故障")]
        Fault,
    }
}
