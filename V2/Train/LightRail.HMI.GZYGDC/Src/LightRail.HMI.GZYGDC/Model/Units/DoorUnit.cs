using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using LightRail.HMI.GZYGDC.Extension;
using LightRail.HMI.GZYGDC.Interfaces;
using LightRail.HMI.GZYGDC.Service;

namespace LightRail.HMI.GZYGDC.Model.Units
{
    /// <summary>
    ///     门单元
    /// </summary>
    [ExcelLocation("广州有轨电车车辆屏界面接口表.xls", "DoorUnit")]
    public class DoorUnit : NotificationObject, ISetValueProvider, IUnit
    {
        /// <summary>
        /// 构造
        /// </summary>
        public DoorUnit()
        {
            if (m_DoorPriorityService == null)
            {
                m_DoorPriorityService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<DoorPriorityService>();
                if (m_DoorPriorityService != null)
                {
                    State = m_DoorPriorityService.GetLowPriority();
                }
            }

        }
        private DoorState m_State;

        /// <summary>
        ///     门状态
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
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }

        /// <summary>
        /// 门
        /// </summary>
        [ExcelField("门")]
        public int Location { get; set; }
        /// <summary>
        ///  紧急解锁
        /// </summary>
        [ExcelField("门紧急解锁")]
        public string EmergencyLock { get; set; }

        /// <summary>
        /// 门切除
        /// </summary>
        [ExcelField("门切除")]
        public string Cut { get; set; }

        /// <summary>
        /// 门严重故障
        /// </summary>
        [ExcelField("门严重故障")]
        public string SeriousFault { get; set; }


        /// <summary>
        /// 门次要故障
        /// </summary>
        [ExcelField("门次要故障")]
        public string MinorFault { get; set; }


        /// <summary>
        /// 门障碍
        /// </summary>
        [ExcelField("门障碍")]
        public string Hinder { get; set; }

        /// <summary>
        /// 门开
        /// </summary>
        [ExcelField("门开")]
        public string Opened { get; set; }

        /// <summary>
        /// 门关好
        /// </summary>
        [ExcelField("门关")]
        public string Closed { get; set; }

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
            args.UpdateIfContain(EmergencyLock, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(DoorState.EmergencyLock, b,this);
            });
            args.UpdateIfContain(Cut, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(DoorState.Cut, b,this);
            });
            args.UpdateIfContain(SeriousFault, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(DoorState.SeriousFault, b,this);
            });
            args.UpdateIfContain(MinorFault, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(DoorState.MinorFault, b,this);
            });
            args.UpdateIfContain(Hinder, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(DoorState.Hinder, b,this);
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
    ///     门状态
    /// </summary>
    public enum DoorState
    {
        /// <summary>
        /// 紧急解锁
        /// </summary>
        [Description("紧急解锁")]
        EmergencyLock,

        /// <summary>
        /// 门切除
        /// </summary>
        [Description("门切除")]
        Cut,

        /// <summary>
        /// 门严重故障
        /// </summary>
        [Description("门严重故障")]
        SeriousFault,


        /// <summary>
        /// 门次要故障
        /// </summary>
        [Description("门次要故障")]
        MinorFault,

        /// <summary>
        /// 门障碍
        /// </summary>
        [Description("门障碍")]
        Hinder,

        /// <summary>
        /// 门开
        /// </summary>
        [Description("门开")]
        Opened,

        /// <summary>
        ///　门关
        /// </summary>
        [Description("门关")]
        Closed
    }

    /// <summary>
    /// 疏散门状态
    /// </summary>
    public enum EvacuateDoor
    {
        /// <summary>
        /// 疏散门打开/未锁上
        /// </summary>
        Open,
        /// <summary>
        /// 疏散门锁上
        /// </summary>
        Lock
    }

    /// <summary>
    /// 间隔门状态
    /// </summary>
    public enum IntervalDoor
    {
        /// <summary>
        /// 间隔门打开/未锁上
        /// </summary>
        Open,
        /// <summary>
        /// 间隔门锁上
        /// </summary>
        Lock,
    }
}
