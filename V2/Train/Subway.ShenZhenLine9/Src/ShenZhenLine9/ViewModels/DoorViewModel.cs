using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
using Subway.ShenZhenLine9.Extention;
using Subway.ShenZhenLine9.Models;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class DoorViewModel : ViewModelBase
    {
        private ObservableCollection<DoorUnit> m_DoorUnits;
        private bool m_EvacuateDoor2Opne;
        private bool m_EvacuateDoor1Opne;
        private bool m_IntervalDoor2Opne;
        private bool m_IntervalDoor1Opne;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DoorViewModel()
        {
            DoorUnits = new ObservableCollection<DoorUnit>(GlobalParam.Instance.DoorUnits);
            DoorUnits.ForEach(f => f.State = DoorState.Flicker);
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car1Top { get { return DoorUnits.Where(w => w.Car == 1 && w.Door % 2 == 0).OrderBy(o => o.Door); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car1Buttom { get { return DoorUnits.Where(w => w.Car == 1 && w.Door % 2 != 0).OrderBy(o => o.Door); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car2Top { get { return DoorUnits.Where(w => w.Car == 2 && w.Door % 2 == 0).OrderBy(o => o.Door); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car2Buttom { get { return DoorUnits.Where(w => w.Car == 2 && w.Door % 2 != 0).OrderBy(o => o.Door); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car3Top { get { return DoorUnits.Where(w => w.Car == 3 && w.Door % 2 == 0).OrderBy(o => o.Door); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car3Buttom { get { return DoorUnits.Where(w => w.Car == 3 && w.Door % 2 != 0).OrderBy(o => o.Door); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car4Top { get { return DoorUnits.Where(w => w.Car == 4 && w.Door % 2 != 0).OrderByDescending(o => o.Door); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car4Buttom { get { return DoorUnits.Where(w => w.Car == 4 && w.Door % 2 == 0).OrderByDescending(o => o.Door); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car5Top { get { return DoorUnits.Where(w => w.Car == 5 && w.Door % 2 != 0).OrderByDescending(o => o.Door); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car5Buttom { get { return DoorUnits.Where(w => w.Car == 5 && w.Door % 2 == 0).OrderByDescending(o => o.Door); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car6Top { get { return DoorUnits.Where(w => w.Car == 6 && w.Door % 2 != 0).OrderByDescending(o => o.Door); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car6Buttom { get { return DoorUnits.Where(w => w.Car == 6 && w.Door % 2 == 0).OrderByDescending(o => o.Door); } }
        /// <summary>
        /// 司机室1门1状态
        /// </summary>
        public DoorUnit Cab1Door1 { get { return DoorUnits.FirstOrDefault(w => w.Car == 0 && w.Door == 1); } }
        /// <summary>
        /// 司机室1门1状态
        /// </summary>
        public DoorUnit Cab1Door2 { get { return DoorUnits.FirstOrDefault(w => w.Car == 0 && w.Door == 2); } }
        /// <summary>
        /// 司机室1门1状态
        /// </summary>
        public DoorUnit Cab2Door1 { get { return DoorUnits.FirstOrDefault(w => w.Car == 7 && w.Door == 1); } }
        /// <summary>
        /// 司机室1门1状态
        /// </summary>
        public DoorUnit Cab2Door2 { get { return DoorUnits.FirstOrDefault(w => w.Car == 7 && w.Door == 2); } }

        /// <summary>
        /// 间隔门打开/未锁闭
        /// </summary>
        public bool IntervalDoor1Opne
        {
            get { return m_IntervalDoor1Opne; }
            set
            {
                if (value == m_IntervalDoor1Opne)
                    return;

                m_IntervalDoor1Opne = value;
                RaisePropertyChanged(() => IntervalDoor1Opne);
            }
        }

        /// <summary>
        /// 间隔门打开/未锁闭
        /// </summary>
        public bool IntervalDoor2Opne
        {
            get { return m_IntervalDoor2Opne; }
            set
            {
                if (value == m_IntervalDoor2Opne)
                    return;

                m_IntervalDoor2Opne = value;
                RaisePropertyChanged(() => IntervalDoor2Opne);
            }
        }

        /// <summary>
        /// 间隔门打开/未锁闭
        /// </summary>
        public bool EvacuateDoor1Opne
        {
            get { return m_EvacuateDoor1Opne; }
            set
            {
                if (value == m_EvacuateDoor1Opne)
                    return;

                m_EvacuateDoor1Opne = value;
                RaisePropertyChanged(() => EvacuateDoor1Opne);
            }
        }

        /// <summary>
        /// 间隔门打开/未锁闭
        /// </summary>
        public bool EvacuateDoor2Opne
        {
            get { return m_EvacuateDoor2Opne; }
            set
            {
                if (value == m_EvacuateDoor2Opne)
                    return;

                m_EvacuateDoor2Opne = value;
                RaisePropertyChanged(() => EvacuateDoor2Opne);
            }
        }

        /// <summary>
        /// 所有门单元
        /// </summary>
        public ObservableCollection<DoorUnit> DoorUnits
        {
            get { return m_DoorUnits; }
            private set
            {
                if (Equals(value, m_DoorUnits))
                {
                    return;
                }
                m_DoorUnits = value;
                RaisePropertyChanged(() => DoorUnits);
                RaisePropertyChanged(() => Car1Top);
                RaisePropertyChanged(() => Car1Buttom);
                RaisePropertyChanged(() => Car2Top);
                RaisePropertyChanged(() => Car2Buttom);
                RaisePropertyChanged(() => Car3Top);
                RaisePropertyChanged(() => Car3Buttom);
                RaisePropertyChanged(() => Car4Top);
                RaisePropertyChanged(() => Car4Buttom);
                RaisePropertyChanged(() => Car5Top);
                RaisePropertyChanged(() => Car5Buttom);
                RaisePropertyChanged(() => Car6Top);
                RaisePropertyChanged(() => Car6Buttom);
                RaisePropertyChanged(() => Cab1Door1);
                RaisePropertyChanged(() => Cab1Door2);
                RaisePropertyChanged(() => Cab2Door1);
                RaisePropertyChanged(() => Cab2Door2);
            }
        }

      
    }
}