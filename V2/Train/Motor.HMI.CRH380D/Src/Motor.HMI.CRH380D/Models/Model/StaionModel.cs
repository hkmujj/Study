using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class StationModel : ModelBase
    {
        private ObservableCollection<StationUnit> m_StationUnits;
        private bool m_PassengerTalk0;
        private bool m_PassengerTalk7;
        private bool m_PassengerTalk6;
        private bool m_PassengerTalk5;
        private bool m_PassengerTalk4;
        private bool m_PassengerTalk3;
        private bool m_PassengerTalk2;
        private bool m_PassengerTalk1;

        [ImportingConstructor]
        public StationModel(StationController stationController)
        {
            StationUnits = new ObservableCollection<StationUnit>(GlobalParam.Instance.StationUnits.OrderBy(o => o.Num));

            TrainModel = new TrainModel();

            StationController = stationController;
            StationController.ViewModel = this;
            StationController.Initalize();
        }


       public StationController StationController { get; private set; }

       /// <summary>
       /// 车辆集合
       /// </summary>
       public TrainModel TrainModel { get; private set; }

        /// <summary>
        /// 所有门单元
        /// </summary>
        public ObservableCollection<StationUnit> StationUnits
        {
            get { return m_StationUnits; }
            private set
            {
                if (Equals(value, m_StationUnits))
                {
                    return;
                }
                m_StationUnits = value;
                RaisePropertyChanged(() => StationUnits);
                RaisePropertyChanged(() => Car0Station1);
                RaisePropertyChanged(() => Car0Station3);
                RaisePropertyChanged(() => Car1Station1);
                RaisePropertyChanged(() => Car1Station3);
                RaisePropertyChanged(() => Car2Station1);
                RaisePropertyChanged(() => Car2Station2);
                RaisePropertyChanged(() => Car2Station3);
                RaisePropertyChanged(() => Car2Station4);
                RaisePropertyChanged(() => Car3Station1);
                RaisePropertyChanged(() => Car3Station2);
                RaisePropertyChanged(() => Car3Station3);
                RaisePropertyChanged(() => Car3Station4);
                RaisePropertyChanged(() => Car4Station1);
                RaisePropertyChanged(() => Car4Station3);
                RaisePropertyChanged(() => Car5Station1);
                RaisePropertyChanged(() => Car5Station2);
                RaisePropertyChanged(() => Car5Station3);
                RaisePropertyChanged(() => Car5Station4);
                RaisePropertyChanged(() => Car6Station1);
                RaisePropertyChanged(() => Car6Station2);
                RaisePropertyChanged(() => Car6Station3);
                RaisePropertyChanged(() => Car6Station4);
                RaisePropertyChanged(() => Car7Station1);
                RaisePropertyChanged(() => Car7Station2);
                RaisePropertyChanged(() => Car7Station3);
                RaisePropertyChanged(() => Car7Station4);
            }
        }
        /// <summary>
        /// 0车1门
        /// </summary>
        public StationUnit Car0Station1 { get {return StationUnits.Where(w => w.Car == 0 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 0车3门
        /// </summary>
        public StationUnit Car0Station3 { get { return StationUnits.Where(w => w.Car == 0 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 1车1门
        /// </summary>
        public StationUnit Car1Station1 { get { return StationUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 1车3门
        /// </summary>
        public StationUnit Car1Station3 { get { return StationUnits.Where(w => w.Car == 1 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2车1门
        /// </summary>
        public StationUnit Car2Station1 { get { return StationUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2车2门
        /// </summary>
        public StationUnit Car2Station2 { get { return StationUnits.Where(w => w.Car == 2 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2车3门
        /// </summary>
        public StationUnit Car2Station3 { get { return StationUnits.Where(w => w.Car == 2 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2车4门
        /// </summary>
        public StationUnit Car2Station4 { get { return StationUnits.Where(w => w.Car == 2 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车1门
        /// </summary>
        public StationUnit Car3Station1 { get { return StationUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车2门
        /// </summary>
        public StationUnit Car3Station2 { get { return StationUnits.Where(w => w.Car == 3 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车3门
        /// </summary>
        public StationUnit Car3Station3 { get { return StationUnits.Where(w => w.Car == 3 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车4门
        /// </summary>
        public StationUnit Car3Station4 { get { return StationUnits.Where(w => w.Car == 3 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4车1门
        /// </summary>
        public StationUnit Car4Station1 { get { return StationUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// 4车3门
        /// </summary>
        public StationUnit Car4Station3 { get { return StationUnits.Where(w => w.Car == 4 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// 5车1门
        /// </summary>
        public StationUnit Car5Station1 { get { return StationUnits.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 5车2门
        /// </summary>
        public StationUnit Car5Station2 { get { return StationUnits.Where(w => w.Car == 5 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 5车3门
        /// </summary>
        public StationUnit Car5Station3 { get { return StationUnits.Where(w => w.Car == 5 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 5车4门
        /// </summary>
        public StationUnit Car5Station4 { get { return StationUnits.Where(w => w.Car == 5 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 6车1门
        /// </summary>
        public StationUnit Car6Station1 { get { return StationUnits.Where(w => w.Car == 6 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 6车2门
        /// </summary>
        public StationUnit Car6Station2 { get { return StationUnits.Where(w => w.Car == 6 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 6车3门
        /// </summary>
        public StationUnit Car6Station3 { get { return StationUnits.Where(w => w.Car == 6 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 6车4门
        /// </summary>
        public StationUnit Car6Station4 { get { return StationUnits.Where(w => w.Car == 6 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 7车1门
        /// </summary>
        public StationUnit Car7Station1 { get { return StationUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 7车2门
        /// </summary>
        public StationUnit Car7Station2 { get { return StationUnits.Where(w => w.Car == 7 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 7车3门
        /// </summary>
        public StationUnit Car7Station3 { get { return StationUnits.Where(w => w.Car == 7 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 7车4门
        /// </summary>
        public StationUnit Car7Station4 { get { return StationUnits.Where(w => w.Car == 7 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车0乘客紧急对讲
        /// </summary>
        public bool PassengerTalk0
        {
            get { return m_PassengerTalk0; }
            set {
                if (value == m_PassengerTalk0)
                {
                    return;
                }
                m_PassengerTalk0 = value;
                RaisePropertyChanged(() => PassengerTalk0);
            }
        }

        /// <summary>
        /// 车7乘客紧急对讲
        /// </summary>
        public bool PassengerTalk7
        {
            get { return m_PassengerTalk7; }
            set
            {
                if (value == m_PassengerTalk7)
                {
                    return;
                }
                m_PassengerTalk7 = value;
                RaisePropertyChanged(() => PassengerTalk7);
            }
        }

        /// <summary>
        /// 车6乘客紧急对讲
        /// </summary>
        public bool PassengerTalk6
        {
            get { return m_PassengerTalk6; }
            set
            {
                if (value == m_PassengerTalk6)
                {
                    return;
                }
                m_PassengerTalk6 = value;
                RaisePropertyChanged(() => PassengerTalk6);
            }
        }

        /// <summary>
        /// 车5乘客紧急对讲
        /// </summary>
        public bool PassengerTalk5
        {
            get { return m_PassengerTalk5; }
            set
            {
                if (value == m_PassengerTalk5)
                {
                    return;
                }
                m_PassengerTalk5 = value;
                RaisePropertyChanged(() => PassengerTalk5);
            }
        }

        /// <summary>
        /// 车4乘客紧急对讲
        /// </summary>
        public bool PassengerTalk4
        {
            get { return m_PassengerTalk4; }
            set
            {
                if (value == m_PassengerTalk4)
                {
                    return;
                }
                m_PassengerTalk4 = value;
                RaisePropertyChanged(() => PassengerTalk4);
            }
        }

        /// <summary>
        /// 车3乘客紧急对讲
        /// </summary>
        public bool PassengerTalk3
        {
            get { return m_PassengerTalk3; }
            set
            {
                if (value == m_PassengerTalk3)
                {
                    return;
                }
                m_PassengerTalk3 = value;
                RaisePropertyChanged(() => PassengerTalk3);
            }
        }

        /// <summary>
        /// 车2乘客紧急对讲
        /// </summary>
        public bool PassengerTalk2
        {
            get { return m_PassengerTalk2; }
            set
            {
                if (value == m_PassengerTalk2)
                {
                    return;
                }
                m_PassengerTalk2 = value;
                RaisePropertyChanged(() => PassengerTalk2);
            }
        }

        /// <summary>
        /// 车1乘客紧急对讲
        /// </summary>
        public bool PassengerTalk1
        {
            get { return m_PassengerTalk1; }
            set
            {
                if (value == m_PassengerTalk1)
                {
                    return;
                }
                m_PassengerTalk1 = value;
                RaisePropertyChanged(() => PassengerTalk1);
            }
        }
        
    }
}