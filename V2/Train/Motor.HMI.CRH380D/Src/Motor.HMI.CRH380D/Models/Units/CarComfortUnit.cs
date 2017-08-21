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
    ///     空调状态单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "CarComfortUnit")]
    public class CarComfortUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public CarComfortUnit()
        {
            if (m_CarComfortPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_CarComfortPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<CarComfortPriorityService>();
                if (m_CarComfortPriorityService != null)
                {
                    State = m_CarComfortPriorityService.GetLowPriority();
                }
            }

        }
        private CarComfortState m_State;

        /// <summary>
        ///     空调状态状态
        /// </summary>
        public CarComfortState State
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
        ///  空调故障
        /// </summary>
        [ExcelField("空调故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 空调运行
        /// </summary>
        [ExcelField("空调运行")]
        public string TurnOn { get; set; }

        /// <summary>
        /// 空调停止
        /// </summary>
        [ExcelField("空调停止")]
        public string TurnOff { get; set; }

        /// <summary>
        /// 空调本地模式
        /// </summary>
        [ExcelField("空调本地模式")]
        public string Cut { get; set; }

        /// <summary>
        /// HVAC开关
        /// </summary>
        [ExcelField("HVAC开关")]
        public string TurnOnAndOff{ get; set; }

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


        private readonly CarComfortPriorityService m_CarComfortPriorityService;


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
            args.UpdateIfContain(Fault, b =>
            {
                State = m_CarComfortPriorityService.GetPriorityValue(CarComfortState.Fault, b, this);
            });
            args.UpdateIfContain(TurnOn, b =>
            {
                State = m_CarComfortPriorityService.GetPriorityValue(CarComfortState.TurnOn, b, this);
            });
            args.UpdateIfContain(TurnOff, b =>
            {
                State = m_CarComfortPriorityService.GetPriorityValue(CarComfortState.TurnOff, b, this);
            });
            args.UpdateIfContain(Cut, b =>
            {
                State = m_CarComfortPriorityService.GetPriorityValue(CarComfortState.Cut, b, this);
            });
        }
    }

    /// <summary>
    ///     空调状态状态
    /// </summary>
    public enum CarComfortState
    {
        /// <summary>
        ///　空调停止
        /// </summary>
        [Description("空调停止")]
        TurnOff,
        /// <summary>
        /// 空调故障
        /// </summary>
        [Description("空调故障")]
        Fault,
        /// <summary>
        /// 空调运行
        /// </summary>
        [Description("空调运行")]
        TurnOn,
        /// <summary>
        ///　空调本地模式
        /// </summary>
        [Description("空调本地模式")]
        Cut,
    }
}
