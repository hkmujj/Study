using System.Collections.Generic;
using System.ComponentModel;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.ShenZhenLine9.Extention;
using Subway.ShenZhenLine9.Interfaces;
using Subway.ShenZhenLine9.Service;

namespace Subway.ShenZhenLine9.Models.Units
{
    /// <summary>
    ///     门单元
    /// </summary>
    [ExcelLocation("深圳地铁7号线子系统界面接口表.xls", "DoorUnit")]
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
        public int Door { get; set; }
        /// <summary>
        ///     紧急解锁
        /// </summary>
        [ExcelField("紧急解锁")]
        public string EmergencyLock { get; set; }

        /// <summary>
        ///     门切除
        /// </summary>
        [ExcelField("门切除")]
        public string Cut { get; set; }

        /// <summary>
        ///     门故障
        /// </summary>
        [ExcelField("门故障")]
        public string Fault { get; set; }

        /// <summary>
        ///     门检测
        /// </summary>
        [ExcelField("门检测")]
        public string Check { get; set; }

        /// <summary>
        ///     门闪烁
        /// </summary>
        [ExcelField("门闪烁")]
        public string Flicker { get; set; }

        /// <summary>
        ///     门关好
        /// </summary>
        [ExcelField("门关好")]
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
                State = m_DoorPriorityService.GetPriorityValue(this,DoorState.EmergencyLock, b);
            });
            args.UpdateIfContain(Cut, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(this, DoorState.Cut, b);
            });
            args.UpdateIfContain(Fault, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(this, DoorState.Fault, b);
            });
            args.UpdateIfContain(Check, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(this, DoorState.Check, b);
            });
            args.UpdateIfContain(Flicker, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(this, DoorState.Flicker, b);
            });
            args.UpdateIfContain(Closed, b =>
            {
                State = m_DoorPriorityService.GetPriorityValue(this, DoorState.Closed, b);
            });
        }
    }

    /// <summary>
    ///     门状态
    /// </summary>
    public enum DoorState
    {
        None,
        /// <summary>
        ///     紧急解锁
        /// </summary>
        [Description("紧急解锁")]
        EmergencyLock,

        /// <summary>
        ///     门切除
        /// </summary>
        [Description("门切除")]
        Cut,

        /// <summary>
        ///     门故障
        /// </summary>
        [Description("门故障")]
        Fault,

        /// <summary>
        ///     门检测到障碍物
        /// </summary>
        [Description("门检测到障碍物")]
        Check,

        /// <summary>
        ///     门开好
        /// </summary>
        [Description("门开好/门开关过程中,无故障")]
        Flicker,

        /// <summary>
        ///     门关好
        /// </summary>
        [Description("门关好,无故障")]
        Closed
    }
}
