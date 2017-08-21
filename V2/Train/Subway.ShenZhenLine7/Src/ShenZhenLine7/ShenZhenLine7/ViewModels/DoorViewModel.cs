using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Subway.ShenZhenLine7.Envets;
using Subway.ShenZhenLine7.Extention;
using Subway.ShenZhenLine7.Models;
using Subway.ShenZhenLine7.Models.Units;
using Subway.ShenZhenLine7.Resource.Keys;

namespace Subway.ShenZhenLine7.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class DoorViewModel : ViewModelBase
    {
        private ObservableCollection<DoorUnit> m_DoorUnits;
        private IntervalDoor m_IntervalDoor2;
        private IntervalDoor m_IntervalDoor1;
        private EvacuateDoor m_EvacuateDoor2;
        private EvacuateDoor m_EvacuateDoor1;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DoorViewModel()
        {
            DoorUnits = new ObservableCollection<DoorUnit>(GlobalParam.Instance.DoorUnits);
            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<BoolDataChangedEvent>()
                .Subscribe(DoorStateChanged, ThreadOption.UIThread);
        }

        private void DoorStateChanged(DataChangedEventArgs<bool> args)
        {
            args.Data.UpdateIfContain(InBoolKeys.司机室1间隔门锁上, b => IntervalDoor1 = b ? IntervalDoor.Lock : IntervalDoor.Open);
            args.Data.UpdateIfContain(InBoolKeys.司机室2间隔门锁上, b => IntervalDoor2 = b ? IntervalDoor.Lock : IntervalDoor.Open);
            args.Data.UpdateIfContain(InBoolKeys.司机室1疏散门锁上, b => EvacuateDoor1 = b ? EvacuateDoor.Lock : EvacuateDoor.Open);
            args.Data.UpdateIfContain(InBoolKeys.司机室2疏散门锁上, b => EvacuateDoor2 = b ? EvacuateDoor.Lock : EvacuateDoor.Open);
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car1Top { get { return DoorUnits.Where(w => w.Car == 1 && w.Location % 2 == 0).OrderBy(o => o.Location); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car1Buttom { get { return DoorUnits.Where(w => w.Car == 1 && w.Location % 2 != 0).OrderBy(o => o.Location); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car2Top { get { return DoorUnits.Where(w => w.Car == 2 && w.Location % 2 == 0).OrderBy(o => o.Location); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car2Buttom { get { return DoorUnits.Where(w => w.Car == 2 && w.Location % 2 != 0).OrderBy(o => o.Location); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car3Top { get { return DoorUnits.Where(w => w.Car == 3 && w.Location % 2 == 0).OrderBy(o => o.Location); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car3Buttom { get { return DoorUnits.Where(w => w.Car == 3 && w.Location % 2 != 0).OrderBy(o => o.Location); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car4Top { get { return DoorUnits.Where(w => w.Car == 4 && w.Location % 2 != 0).OrderByDescending(o => o.Location); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car4Buttom { get { return DoorUnits.Where(w => w.Car == 4 && w.Location % 2 == 0).OrderByDescending(o => o.Location); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car5Top { get { return DoorUnits.Where(w => w.Car == 5 && w.Location % 2 != 0).OrderByDescending(o => o.Location); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car5Buttom { get { return DoorUnits.Where(w => w.Car == 5 && w.Location % 2 == 0).OrderByDescending(o => o.Location); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car6Top { get { return DoorUnits.Where(w => w.Car == 6 && w.Location % 2 != 0).OrderByDescending(o => o.Location); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DoorUnit> Car6Buttom { get { return DoorUnits.Where(w => w.Car == 6 && w.Location % 2 == 0).OrderByDescending(o => o.Location); } }
        /// <summary>
        /// 司机室1门1状态
        /// </summary>
        public DoorUnit Cab1Door1 { get { return DoorUnits.FirstOrDefault(w => w.Car == 0 && w.Location == 1); } }
        /// <summary>
        /// 司机室1门1状态
        /// </summary>
        public DoorUnit Cab1Door2 { get { return DoorUnits.FirstOrDefault(w => w.Car == 0 && w.Location == 2); } }
        /// <summary>
        /// 司机室1门1状态
        /// </summary>
        public DoorUnit Cab2Door1 { get { return DoorUnits.FirstOrDefault(w => w.Car == 7 && w.Location == 1); } }
        /// <summary>
        /// 司机室1门1状态
        /// </summary>
        public DoorUnit Cab2Door2 { get { return DoorUnits.FirstOrDefault(w => w.Car == 7 && w.Location == 2); } }

        /// <summary>
        /// 疏散门1
        /// </summary>
        public EvacuateDoor EvacuateDoor1
        {
            get { return m_EvacuateDoor1; }
            set
            {
                if (value == m_EvacuateDoor1)
                {
                    return;
                }
                m_EvacuateDoor1 = value;
                RaisePropertyChanged(() => EvacuateDoor1);
            }
        }

        /// <summary>
        /// 疏散门2
        /// </summary>
        public EvacuateDoor EvacuateDoor2
        {
            get { return m_EvacuateDoor2; }
            set
            {
                if (value == m_EvacuateDoor2)
                {
                    return;
                }
                m_EvacuateDoor2 = value;
                RaisePropertyChanged(() => EvacuateDoor2);
            }
        }

        /// <summary>
        /// 间隔门1
        /// </summary>
        public IntervalDoor IntervalDoor1
        {
            get { return m_IntervalDoor1; }
            set
            {
                if (value == m_IntervalDoor1)
                {
                    return;
                }
                m_IntervalDoor1 = value;
                RaisePropertyChanged(() => IntervalDoor1);
            }
        }

        /// <summary>
        /// 间隔门2
        /// </summary>
        public IntervalDoor IntervalDoor2
        {
            get { return m_IntervalDoor2; }
            set
            {
                if (value == m_IntervalDoor2)
                {
                    return;
                }
                m_IntervalDoor2 = value;
                RaisePropertyChanged(() => IntervalDoor2);
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