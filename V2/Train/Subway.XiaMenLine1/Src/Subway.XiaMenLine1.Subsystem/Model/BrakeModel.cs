using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Subway.XiaMenLine1.Interface.Enum;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    public class BrakeModel : NotificationObject
    {
        public ObservableCollection<BrakeUnit> BrakeItemCollection { set; get; }

        public IEnumerable<BrakeUnit> Car1BrakeItems { get { return BrakeItemCollection.Where(w => w.CarIndex == 1).OrderBy(o => o.BrakeLocation); } }
        public IEnumerable<BrakeUnit> Car2BrakeItems { get { return BrakeItemCollection.Where(w => w.CarIndex == 2).OrderBy(o => o.BrakeLocation); } }
        public IEnumerable<BrakeUnit> Car3BrakeItems { get { return BrakeItemCollection.Where(w => w.CarIndex == 3).OrderBy(o => o.BrakeLocation); } }
        public IEnumerable<BrakeUnit> Car4BrakeItems { get { return BrakeItemCollection.Where(w => w.CarIndex == 4).OrderBy(o => o.BrakeLocation); } }
        public IEnumerable<BrakeUnit> Car5BrakeItems { get { return BrakeItemCollection.Where(w => w.CarIndex == 5).OrderBy(o => o.BrakeLocation); } }
        public IEnumerable<BrakeUnit> Car6BrakeItems { get { return BrakeItemCollection.Where(w => w.CarIndex == 6).OrderBy(o => o.BrakeLocation); } }


        private BrakeStatus m_Car6BrakeParking;
        private BrakeStatus m_Car5BrakeParking;
        private BrakeStatus m_Car4BrakeParking;
        private BrakeStatus m_Car3BrakeParking;
        private BrakeStatus m_Car2BrakeParking;
        private BrakeStatus m_Car1BrakeParking;
        private BrakeStatus m_Car6BrakeNormal3;
        private BrakeStatus m_Car5BrakeNormal3;
        private BrakeStatus m_Car4BrakeNormal3;
        private BrakeStatus m_Car3BrakeNormal3;
        private BrakeStatus m_Car2BrakeNormal3;
        private BrakeStatus m_Car1BrakeNormal3;
        private BrakeStatus m_Car6BrakeNormal2;
        private BrakeStatus m_Car5BrakeNormal2;
        private BrakeStatus m_Car4BrakeNormal2;
        private BrakeStatus m_Car3BrakeNormal2;
        private BrakeStatus m_Car2BrakeNormal2;
        private BrakeStatus m_Car1BrakeNormal2;
        private BrakeStatus m_Car6BrakeNormal1;
        private BrakeStatus m_Car5BrakeNormal1;
        private BrakeStatus m_Car4BrakeNormal1;
        private BrakeStatus m_Car3BrakeNormal1;
        private BrakeStatus m_Car2BrakeNormal1;
        private BrakeStatus m_Car1BrakeNormal1;

        public BrakeModel()
        {
            Car1BrakeNormal1 = BrakeStatus.NormalBrakeRelieve;
            Car1BrakeNormal2 = BrakeStatus.NormalBrakeRelieve;
            Car1BrakeNormal3 = BrakeStatus.NormalBrakeRelieve;
            Car1BrakeParking = BrakeStatus.ParkBrakeRelieve;
            Car2BrakeNormal1 = BrakeStatus.NormalBrakeRelieve;
            Car2BrakeNormal2 = BrakeStatus.NormalBrakeRelieve;
            Car2BrakeNormal3 = BrakeStatus.NormalBrakeRelieve;
            Car2BrakeParking = BrakeStatus.ParkBrakeRelieve;
            Car3BrakeNormal1 = BrakeStatus.NormalBrakeRelieve;
            Car3BrakeNormal2 = BrakeStatus.NormalBrakeRelieve;
            Car3BrakeNormal3 = BrakeStatus.NormalBrakeRelieve;
            Car3BrakeParking = BrakeStatus.ParkBrakeRelieve;
            Car4BrakeNormal1 = BrakeStatus.NormalBrakeRelieve;
            Car4BrakeNormal2 = BrakeStatus.NormalBrakeRelieve;
            Car4BrakeNormal3 = BrakeStatus.NormalBrakeRelieve;
            Car4BrakeParking = BrakeStatus.ParkBrakeRelieve;
            Car5BrakeNormal1 = BrakeStatus.NormalBrakeRelieve;
            Car5BrakeNormal2 = BrakeStatus.NormalBrakeRelieve;
            Car5BrakeNormal3 = BrakeStatus.NormalBrakeRelieve;
            Car5BrakeParking = BrakeStatus.ParkBrakeRelieve;
            Car6BrakeNormal1 = BrakeStatus.NormalBrakeRelieve;
            Car6BrakeNormal2 = BrakeStatus.NormalBrakeRelieve;
            Car6BrakeNormal3 = BrakeStatus.NormalBrakeRelieve;
            Car6BrakeParking = BrakeStatus.ParkBrakeRelieve;

        }

        public BrakeStatus Car1BrakeNormal1
        {
            get { return m_Car1BrakeNormal1; }
            set
            {
                if (value == m_Car1BrakeNormal1)
                {
                    return;
                }
                m_Car1BrakeNormal1 = value;
                RaisePropertyChanged(() => Car1BrakeNormal1);
            }
        }

        public BrakeStatus Car2BrakeNormal1
        {
            get { return m_Car2BrakeNormal1; }
            set
            {
                if (value == m_Car2BrakeNormal1)
                {
                    return;
                }
                m_Car2BrakeNormal1 = value;
                RaisePropertyChanged(() => Car2BrakeNormal1);
            }
        }

        public BrakeStatus Car3BrakeNormal1
        {
            get { return m_Car3BrakeNormal1; }
            set
            {
                if (value == m_Car3BrakeNormal1)
                {
                    return;
                }
                m_Car3BrakeNormal1 = value;
                RaisePropertyChanged(() => Car3BrakeNormal1);
            }
        }

        public BrakeStatus Car4BrakeNormal1
        {
            get { return m_Car4BrakeNormal1; }
            set
            {
                if (value == m_Car4BrakeNormal1)
                {
                    return;
                }
                m_Car4BrakeNormal1 = value;
                RaisePropertyChanged(() => Car4BrakeNormal1);
            }
        }

        public BrakeStatus Car5BrakeNormal1
        {
            get { return m_Car5BrakeNormal1; }
            set
            {
                if (value == m_Car5BrakeNormal1)
                {
                    return;
                }
                m_Car5BrakeNormal1 = value;
                RaisePropertyChanged(() => Car5BrakeNormal1);
            }
        }

        public BrakeStatus Car6BrakeNormal1
        {
            get { return m_Car6BrakeNormal1; }
            set
            {
                if (value == m_Car6BrakeNormal1)
                {
                    return;
                }
                m_Car6BrakeNormal1 = value;
                RaisePropertyChanged(() => Car6BrakeNormal1);
            }
        }

        public BrakeStatus Car1BrakeNormal2
        {
            get { return m_Car1BrakeNormal2; }
            set
            {
                if (value == m_Car1BrakeNormal2)
                {
                    return;
                }
                m_Car1BrakeNormal2 = value;
                RaisePropertyChanged(() => Car1BrakeNormal2);
            }
        }

        public BrakeStatus Car2BrakeNormal2
        {
            get { return m_Car2BrakeNormal2; }
            set
            {
                if (value == m_Car2BrakeNormal2)
                {
                    return;
                }
                m_Car2BrakeNormal2 = value;
                RaisePropertyChanged(() => Car2BrakeNormal2);
            }
        }

        public BrakeStatus Car3BrakeNormal2
        {
            get { return m_Car3BrakeNormal2; }
            set
            {
                if (value == m_Car3BrakeNormal2)
                {
                    return;
                }
                m_Car3BrakeNormal2 = value;
                RaisePropertyChanged(() => Car3BrakeNormal2);
            }
        }

        public BrakeStatus Car4BrakeNormal2
        {
            get { return m_Car4BrakeNormal2; }
            set
            {
                if (value == m_Car4BrakeNormal2)
                {
                    return;
                }
                m_Car4BrakeNormal2 = value;
                RaisePropertyChanged(() => Car4BrakeNormal2);
            }
        }

        public BrakeStatus Car5BrakeNormal2
        {
            get { return m_Car5BrakeNormal2; }
            set
            {
                if (value == m_Car5BrakeNormal2)
                {
                    return;
                }
                m_Car5BrakeNormal2 = value;
                RaisePropertyChanged(() => Car5BrakeNormal2);
            }
        }

        public BrakeStatus Car6BrakeNormal2
        {
            get { return m_Car6BrakeNormal2; }
            set
            {
                if (value == m_Car6BrakeNormal2)
                {
                    return;
                }
                m_Car6BrakeNormal2 = value;
                RaisePropertyChanged(() => Car6BrakeNormal2);
            }
        }

        public BrakeStatus Car1BrakeNormal3
        {
            get { return m_Car1BrakeNormal3; }
            set
            {
                if (value == m_Car1BrakeNormal3)
                {
                    return;
                }
                m_Car1BrakeNormal3 = value;
                RaisePropertyChanged(() => Car1BrakeNormal3);
            }
        }

        public BrakeStatus Car2BrakeNormal3
        {
            get { return m_Car2BrakeNormal3; }
            set
            {
                if (value == m_Car2BrakeNormal3)
                {
                    return;
                }
                m_Car2BrakeNormal3 = value;
                RaisePropertyChanged(() => Car2BrakeNormal3);
            }
        }

        public BrakeStatus Car3BrakeNormal3
        {
            get { return m_Car3BrakeNormal3; }
            set
            {
                if (value == m_Car3BrakeNormal3)
                {
                    return;
                }
                m_Car3BrakeNormal3 = value;
                RaisePropertyChanged(() => Car3BrakeNormal3);
            }
        }

        public BrakeStatus Car4BrakeNormal3
        {
            get { return m_Car4BrakeNormal3; }
            set
            {
                if (value == m_Car4BrakeNormal3)
                {
                    return;
                }
                m_Car4BrakeNormal3 = value;
                RaisePropertyChanged(() => Car4BrakeNormal3);
            }
        }

        public BrakeStatus Car5BrakeNormal3
        {
            get { return m_Car5BrakeNormal3; }
            set
            {
                if (value == m_Car5BrakeNormal3)
                {
                    return;
                }
                m_Car5BrakeNormal3 = value;
                RaisePropertyChanged(() => Car5BrakeNormal3);
            }
        }

        public BrakeStatus Car6BrakeNormal3
        {
            get { return m_Car6BrakeNormal3; }
            set
            {
                if (value == m_Car6BrakeNormal3)
                {
                    return;
                }
                m_Car6BrakeNormal3 = value;
                RaisePropertyChanged(() => Car6BrakeNormal3);
            }
        }

        public BrakeStatus Car1BrakeParking
        {
            get { return m_Car1BrakeParking; }
            set
            {
                if (value == m_Car1BrakeParking)
                {
                    return;
                }
                m_Car1BrakeParking = value;
                RaisePropertyChanged(() => Car1BrakeParking);
            }
        }

        public BrakeStatus Car2BrakeParking
        {
            get { return m_Car2BrakeParking; }
            set
            {
                if (value == m_Car2BrakeParking)
                {
                    return;
                }
                m_Car2BrakeParking = value;
                RaisePropertyChanged(() => Car2BrakeParking);
            }
        }

        public BrakeStatus Car3BrakeParking
        {
            get { return m_Car3BrakeParking; }
            set
            {
                if (value == m_Car3BrakeParking)
                {
                    return;
                }
                m_Car3BrakeParking = value;
                RaisePropertyChanged(() => Car3BrakeParking);
            }
        }

        public BrakeStatus Car4BrakeParking
        {
            get { return m_Car4BrakeParking; }
            set
            {
                if (value == m_Car4BrakeParking)
                {
                    return;
                }
                m_Car4BrakeParking = value;
                RaisePropertyChanged(() => Car4BrakeParking);
            }
        }

        public BrakeStatus Car5BrakeParking
        {
            get { return m_Car5BrakeParking; }
            set
            {
                if (value == m_Car5BrakeParking)
                {
                    return;
                }
                m_Car5BrakeParking = value;
                RaisePropertyChanged(() => Car5BrakeParking);
            }
        }

        public BrakeStatus Car6BrakeParking
        {
            get { return m_Car6BrakeParking; }
            set
            {
                if (value == m_Car6BrakeParking)
                {
                    return;
                }
                m_Car6BrakeParking = value;
                RaisePropertyChanged(() => Car6BrakeParking);
            }
        }
    }
}