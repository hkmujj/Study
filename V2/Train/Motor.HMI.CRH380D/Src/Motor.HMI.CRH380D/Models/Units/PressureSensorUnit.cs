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
    ///     压力传感器单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "PressureSensorUnit")]
    public class PressureSensorUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public PressureSensorUnit()
        {
            if (m_PressureSensorPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_PressureSensorPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<PressureSensorPriorityService>();
                if (m_PressureSensorPriorityService != null)
                {
                    State = m_PressureSensorPriorityService.GetLowPriority();
                }
            }

        }
        private PressureSensorState m_State;

        /// <summary>
        ///     压力传感器状态
        /// </summary>
        public PressureSensorState State
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
        ///  压力传感器故障
        /// </summary>
        [ExcelField("压力传感器故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 压力传感器切除
        /// </summary>
        [ExcelField("压力传感器切除")]
        public string Cut { get; set; }

        /// <summary>
        /// 压力传感器正常
        /// </summary>
        [ExcelField("压力传感器正常")]
        public string TurnOn { get; set; }


        /// <summary>
        /// 压力传感器切入切除
        /// </summary>
        [ExcelField("压力传感器切入切除")]
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

        private readonly PressureSensorPriorityService m_PressureSensorPriorityService;


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
                State = m_PressureSensorPriorityService.GetPriorityValue(PressureSensorState.Cut, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_PressureSensorPriorityService.GetPriorityValue(PressureSensorState.Fault, b, this);
            });
            args.UpdateIfContain(TurnOn, b =>
            {
                State = m_PressureSensorPriorityService.GetPriorityValue(PressureSensorState.TurnOn, b, this);
            });
        }
    }

    /// <summary>
    ///     压力传感器状态
    /// </summary>
    public enum PressureSensorState
    {
        /// <summary>
        /// 压力传感器正常
        /// </summary>
        [Description("压力传感器正常")]
        TurnOn,
        
        /// <summary>
        ///　压力传感器故障
        /// </summary>
        [Description("压力传感器故障")]
        Fault,

        /// <summary>
        /// 压力传感器切除
        /// </summary>
        [Description("压力传感器切除")]
        Cut,
    }
}
