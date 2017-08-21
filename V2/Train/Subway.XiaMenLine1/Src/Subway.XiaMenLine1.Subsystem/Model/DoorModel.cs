using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    public class DoorModel : NotificationObject
    {
        public ObservableCollection<DoorUnit> DoorUnitCollection { set; get; }
        public DoorUnit CABOneDoorOne { get { return DoorUnitCollection.First(w => w.Car == 0 && w.DoorIndex == 1); } }
        public DoorUnit CABOneDoorTwo { get { return DoorUnitCollection.First(w => w.Car == 0 && w.DoorIndex == 2); } }
        public DoorUnit CABTwoDoorOne { get { return DoorUnitCollection.First(w => w.Car == 7 && w.DoorIndex == 1); } }
        public DoorUnit CABTwoDoorTwo { get { return DoorUnitCollection.First(w => w.Car == 7 && w.DoorIndex == 2); } }
        public IEnumerable<DoorUnit> Car1PartUpDoorUnits { get { return DoorUnitCollection.Where(w => w.Car == 1 && w.DoorIndex % 2 != 0); } }
        public IEnumerable<DoorUnit> Car2PartUpDoorUnits { get { return DoorUnitCollection.Where(w => w.Car == 2 && w.DoorIndex % 2 != 0); } }
        public IEnumerable<DoorUnit> Car3PartUpDoorUnits { get { return DoorUnitCollection.Where(w => w.Car == 3 && w.DoorIndex % 2 != 0); } }
        public IEnumerable<DoorUnit> Car4PartUpDoorUnits { get { return DoorUnitCollection.Where(w => w.Car == 4 && w.DoorIndex % 2 == 0).OrderByDescending(o => o.DoorIndex); } }
        public IEnumerable<DoorUnit> Car5PartUpDoorUnits { get { return DoorUnitCollection.Where(w => w.Car == 5 && w.DoorIndex % 2 == 0).OrderByDescending(o => o.DoorIndex); } }
        public IEnumerable<DoorUnit> Car6PartUpDoorUnits { get { return DoorUnitCollection.Where(w => w.Car == 6 && w.DoorIndex % 2 == 0).OrderByDescending(o => o.DoorIndex); } }

        public IEnumerable<DoorUnit> Car1PartDownDoorUnits { get { return DoorUnitCollection.Where(w => w.Car == 1 && w.DoorIndex % 2 == 0); } }
        public IEnumerable<DoorUnit> Car2PartDownDoorUnits { get { return DoorUnitCollection.Where(w => w.Car == 2 && w.DoorIndex % 2 == 0); } }
        public IEnumerable<DoorUnit> Car3PartDownDoorUnits { get { return DoorUnitCollection.Where(w => w.Car == 3 && w.DoorIndex % 2 == 0); } }
        public IEnumerable<DoorUnit> Car4PartDownDoorUnits { get { return DoorUnitCollection.Where(w => w.Car == 4 && w.DoorIndex % 2 != 0).OrderByDescending(o => o.DoorIndex); } }
        public IEnumerable<DoorUnit> Car5PartDownDoorUnits { get { return DoorUnitCollection.Where(w => w.Car == 5 && w.DoorIndex % 2 != 0).OrderByDescending(o => o.DoorIndex); } }
        public IEnumerable<DoorUnit> Car6PartDownDoorUnits { get { return DoorUnitCollection.Where(w => w.Car == 6 && w.DoorIndex % 2 != 0).OrderByDescending(o => o.DoorIndex); } }

    }
}