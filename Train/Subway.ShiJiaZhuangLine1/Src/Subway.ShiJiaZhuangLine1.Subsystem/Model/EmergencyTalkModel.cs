using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class EmergencyTalkModel : NotificationObject
    {
        public ObservableCollection<EmergencyTalkUnit> EmergencyTalkUnitCollection { set; get; }


        public IEnumerable<EmergencyTalkUnit> Car1PartDownTalkUnits
        {
            get { return EmergencyTalkUnitCollection.Where(w => w.Car == 1 && w.Location%2 == 0); }
        }

        public IEnumerable<EmergencyTalkUnit> Car2PartDownTalkUnits
        {
            get { return EmergencyTalkUnitCollection.Where(w => w.Car == 2 && w.Location%2 == 0); }
        }

        public IEnumerable<EmergencyTalkUnit> Car3PartDownTalkUnits
        {
            get { return EmergencyTalkUnitCollection.Where(w => w.Car == 3 && w.Location%2 == 0); }
        }

        public IEnumerable<EmergencyTalkUnit> Car4PartDownTalkUnits
        {
            get
            {
                return
                    EmergencyTalkUnitCollection.Where(w => w.Car == 4 && w.Location%2 != 0)
                        .OrderByDescending(o => o.Location);
            }
        }

        public IEnumerable<EmergencyTalkUnit> Car5PartDownTalkUnits
        {
            get
            {
                return
                    EmergencyTalkUnitCollection.Where(w => w.Car == 5 && w.Location%2 != 0)
                        .OrderByDescending(o => o.Location);
            }
        }

        public IEnumerable<EmergencyTalkUnit> Car6PartDownTalkUnits
        {
            get
            {
                return
                    EmergencyTalkUnitCollection.Where(w => w.Car == 6 && w.Location%2 != 0)
                        .OrderByDescending(o => o.Location);
            }
        }

        public IEnumerable<EmergencyTalkUnit> Car1PartUpTalkUnits
        {
            get { return EmergencyTalkUnitCollection.Where(w => w.Car == 1 && w.Location%2 != 0); }
        }

        public IEnumerable<EmergencyTalkUnit> Car2PartUpTalkUnits
        {
            get { return EmergencyTalkUnitCollection.Where(w => w.Car == 2 && w.Location%2 != 0); }
        }

        public IEnumerable<EmergencyTalkUnit> Car3PartUpTalkUnits
        {
            get { return EmergencyTalkUnitCollection.Where(w => w.Car == 3 && w.Location%2 != 0); }
        }

        public IEnumerable<EmergencyTalkUnit> Car4PartUpTalkUnits
        {
            get
            {
                return
                    EmergencyTalkUnitCollection.Where(w => w.Car == 4 && w.Location%2 == 0)
                        .OrderByDescending(o => o.Location);
            }
        }

        public IEnumerable<EmergencyTalkUnit> Car5PartUpTalkUnits
        {
            get
            {
                return
                    EmergencyTalkUnitCollection.Where(w => w.Car == 5 && w.Location%2 == 0)
                        .OrderByDescending(o => o.Location);
            }
        }

        public IEnumerable<EmergencyTalkUnit> Car6PartUpTalkUnits
        {
            get
            {
                return
                    EmergencyTalkUnitCollection.Where(w => w.Car == 6 && w.Location%2 == 0)
                        .OrderByDescending(o => o.Location);
            }
        }
    }
}