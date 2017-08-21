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
    ///     外门单元
    /// </summary>
    [ExcelLocation("界面接口表.xls", "DoorUnit")]
    public class DoorUnit : NotificationObject, ISetValueProvider, IUnit
    {
        private bool m_IsChecked;

        /// <summary>
        /// 构造
        /// </summary>
        public DoorUnit()
        {
            if (m_DoorPriorityService == null)
            {
                if (GlobalParam.Instance.InitParam != null)
                    m_DoorPriorityService = GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<DoorPriorityService>();
                if (m_DoorPriorityService != null)
                {
                    State = m_DoorPriorityService.GetLowPriority();
                }
            }

        }
        private DoorState m_State;

        /// <summary>
        ///     外门状态
        /// </summary>
        public DoorState State
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
        /// 外门
        /// </summary>
        [ExcelField("外门")]
        public int Location { get; set; }
        /// <summary>
        ///  外门切除
        /// </summary>
        [ExcelField("外门切除")]
        public string Cut { get; set; }

        /// <summary>
        /// 外门打开并切除
        /// </summary>
        [ExcelField("外门打开并切除")]
        public string OpenAndCut { get; set; }

        /// <summary>
        /// 外门故障
        /// </summary>
        [ExcelField("外门故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 外门释放
        /// </summary>
        [ExcelField("外门释放")]
        public string Release { get; set; }

        /// <summary>
        /// 外门开
        /// </summary>
        [ExcelField("外门开")]
        public string Opened { get; set; }

        /// <summary>
        /// 外门关好
        /// </summary>
        [ExcelField("外门关")]
        public string Closed { get; set; }

        /// <summary>
        /// 外门切入切除
        /// </summary>
        [ExcelField("外门切入切除")]
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

        private readonly DoorPriorityService m_DoorPriorityService;


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
                State = m_DoorPriorityService.GetPriorityValue(DoorState.Cut, b,this);
            });
            args.UpdateIfContain(OpenAndCut, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(DoorState.OpenAndCut, b, this);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(DoorState.Fault, b, this);
            });
            args.UpdateIfContain(Release, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(DoorState.Release, b, this);
            });
            args.UpdateIfContain(Opened, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(DoorState.Opened, b, this);
            });
            args.UpdateIfContain(Closed, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(DoorState.Closed, b,this);
            });
        }
    }

    /// <summary>
    ///     外门状态
    /// </summary>
    public enum DoorState
    {
        /// <summary>
        ///　外门关
        /// </summary>
        [Description("外门关")]
        Closed,

        /// <summary>
        /// 紧急解锁
        /// </summary>
        [Description("外门切除")]
        Cut,

        /// <summary>
        /// 外门切除
        /// </summary>
        [Description("外门打开并切除")]
        OpenAndCut,

        /// <summary>
        /// 外门故障
        /// </summary>
        [Description("外门故障")]
        Fault,

        /// <summary>
        /// 外门故障
        /// </summary>
        [Description("外门释放")]
        Release,

        /// <summary>
        /// 外门开
        /// </summary>
        [Description("外门开")]
        Opened
    }
}
