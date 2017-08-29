using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class BreakModel : ModelBase
    {
        private ObservableCollection<BCUUnit> m_BCUUnits;
        private ObservableCollection<ParkBreakUnit> m_ParkBreakUnits;
        private ObservableCollection<EmergencyBreakUnit> m_EmergencyBreakUnits;
        private float m_BreakValue0;
        private float m_BreakValue7;
        private float m_BreakValue6;
        private float m_BreakValue5;
        private float m_BreakValue4;
        private float m_BreakValue3;
        private float m_BreakValue2;
        private float m_BreakValue1;

        [ImportingConstructor]
        public BreakModel(BreakController breakController)
        {
            BCUUnits = new ObservableCollection<BCUUnit>(GlobalParam.Instance.BCUUnits.OrderBy(o => o.Num));
            ParkBreakUnits = new ObservableCollection<ParkBreakUnit>(GlobalParam.Instance.ParkBreakUnits.OrderBy(o => o.Num));
            EmergencyBreakUnits = new ObservableCollection<EmergencyBreakUnit>(GlobalParam.Instance.EmergencyBreakUnits.OrderBy(o => o.Num));

            BreakController = breakController;
            BreakController.ViewModel = this;
            BreakController.Initalize();
        }

        public BreakController BreakController { get; private set; }

        /// <summary>
        /// BCU单元
        /// </summary>
        public ObservableCollection<BCUUnit> BCUUnits
        {
            get { return m_BCUUnits; }
            private set
            {
                if (Equals(value, m_BCUUnits))
                {
                    return;
                }
                m_BCUUnits = value;
                RaisePropertyChanged(() => BCUUnits);
                RaisePropertyChanged(() => BCU0);
                RaisePropertyChanged(() => BCU7);
                RaisePropertyChanged(() => BCU6);
                RaisePropertyChanged(() => BCU5);
                RaisePropertyChanged(() => BCU4);
                RaisePropertyChanged(() => BCU3);
                RaisePropertyChanged(() => BCU2);
                RaisePropertyChanged(() => BCU1);
            }
        }

        /// <summary>
        /// BCU0
        /// </summary>
        public BCUUnit BCU0 { get { return BCUUnits.Where(w => w.Car == 0 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// BCU7
        /// </summary>
        public BCUUnit BCU7 { get { return BCUUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// BCU6
        /// </summary>
        public BCUUnit BCU6 { get { return BCUUnits.Where(w => w.Car == 6 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// BCU5
        /// </summary>
        public BCUUnit BCU5 { get { return BCUUnits.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// BCU4
        /// </summary>
        public BCUUnit BCU4 { get { return BCUUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// BCU3
        /// </summary>
        public BCUUnit BCU3 { get { return BCUUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// BCU2
        /// </summary>
        public BCUUnit BCU2 { get { return BCUUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// BCU1
        /// </summary>
        public BCUUnit BCU1 { get { return BCUUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 停放制动单元
        /// </summary>
        public ObservableCollection<ParkBreakUnit> ParkBreakUnits
        {
            get { return m_ParkBreakUnits; }
            private set
            {
                if (Equals(value, m_ParkBreakUnits))
                {
                    return;
                }
                m_ParkBreakUnits = value;
                RaisePropertyChanged(() => ParkBreakUnits);
                RaisePropertyChanged(() => ParkBreak7);
                RaisePropertyChanged(() => ParkBreak5);
                RaisePropertyChanged(() => ParkBreak2);
            }
        }

        /// <summary>
        /// ParkBreak7
        /// </summary>
        public ParkBreakUnit ParkBreak7 { get { return ParkBreakUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// ParkBreak5
        /// </summary>
        public ParkBreakUnit ParkBreak5 { get { return ParkBreakUnits.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// ParkBreak2
        /// </summary>
        public ParkBreakUnit ParkBreak2 { get { return ParkBreakUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 紧急制动安全制动单元
        /// </summary>
        public ObservableCollection<EmergencyBreakUnit> EmergencyBreakUnits
        {
            get { return m_EmergencyBreakUnits; }
            private set
            {
                if (Equals(value, m_EmergencyBreakUnits))
                {
                    return;
                }
                m_EmergencyBreakUnits = value;
                RaisePropertyChanged(() => EmergencyBreakUnits);
                RaisePropertyChanged(() => EmergencyBreak0);
                RaisePropertyChanged(() => EmergencyBreak7);
                RaisePropertyChanged(() => EmergencyBreak6);
                RaisePropertyChanged(() => EmergencyBreak5);
                RaisePropertyChanged(() => EmergencyBreak4);
                RaisePropertyChanged(() => EmergencyBreak3);
                RaisePropertyChanged(() => EmergencyBreak2);
                RaisePropertyChanged(() => EmergencyBreak1);
            }
        }

        /// <summary>
        /// EmergencyBreak0
        /// </summary>
        public EmergencyBreakUnit EmergencyBreak0 { get { return EmergencyBreakUnits.Where(w => w.Car == 0 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// EmergencyBreak7
        /// </summary>
        public EmergencyBreakUnit EmergencyBreak7 { get { return EmergencyBreakUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// EmergencyBreak6
        /// </summary>
        public EmergencyBreakUnit EmergencyBreak6 { get { return EmergencyBreakUnits.Where(w => w.Car == 6 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// EmergencyBreak5
        /// </summary>
        public EmergencyBreakUnit EmergencyBreak5 { get { return EmergencyBreakUnits.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// EmergencyBreak4
        /// </summary>
        public EmergencyBreakUnit EmergencyBreak4 { get { return EmergencyBreakUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// EmergencyBreak3
        /// </summary>
        public EmergencyBreakUnit EmergencyBreak3 { get { return EmergencyBreakUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// EmergencyBreak2
        /// </summary>
        public EmergencyBreakUnit EmergencyBreak2 { get { return EmergencyBreakUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        /// <summary>
        /// EmergencyBreak1
        /// </summary>
        public EmergencyBreakUnit EmergencyBreak1 { get { return EmergencyBreakUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }


        /// <summary>
        /// 车0制动力
        /// </summary>
        public float BreakValue0
        {
            get { return m_BreakValue0; }
            set
            {
                if (value == m_BreakValue0)
                {
                    return;
                }
                m_BreakValue0 = value;
                RaisePropertyChanged(()=>BreakValue0);
            }
        }

        /// <summary>
        /// 车7制动力
        /// </summary>
        public float BreakValue7
        {
            get { return m_BreakValue7; }
            set
            {
                if (value == m_BreakValue7)
                {
                    return;
                }
                m_BreakValue7 = value;
                RaisePropertyChanged(() => BreakValue7);
            }
        }

        /// <summary>
        /// 车6制动力
        /// </summary>
        public float BreakValue6
        {
            get { return m_BreakValue6; }
            set
            {
                if (value == m_BreakValue6)
                {
                    return;
                }
                m_BreakValue6 = value;
                RaisePropertyChanged(() => BreakValue6);
            }
        }

        /// <summary>
        /// 车5制动力
        /// </summary>
        public float BreakValue5
        {
            get { return m_BreakValue5; }
            set
            {
                if (value == m_BreakValue5)
                {
                    return;
                }
                m_BreakValue5 = value;
                RaisePropertyChanged(() => BreakValue5);
            }
        }

        /// <summary>
        /// 车4制动力
        /// </summary>
        public float BreakValue4
        {
            get { return m_BreakValue4; }
            set
            {
                if (value == m_BreakValue4)
                {
                    return;
                }
                m_BreakValue4 = value;
                RaisePropertyChanged(() => BreakValue4);
            }
        }

        /// <summary>
        /// 车3制动力
        /// </summary>
        public float BreakValue3 
        {
            get { return m_BreakValue3; }
            set
            {
                if (value == m_BreakValue3)
                {
                    return;
                }
                m_BreakValue3 = value;
                RaisePropertyChanged(() => BreakValue3 );
            }
        }

        /// <summary>
        /// 车2制动力
        /// </summary>
        public float BreakValue2 
        {
            get { return m_BreakValue2; }
            set
            {
                if (value == m_BreakValue2)
                {
                    return;
                }
                m_BreakValue2 = value;
                RaisePropertyChanged(() => BreakValue2 );
            }
        }

        /// <summary>
        /// 车1制动力
        /// </summary>
        public float BreakValue1
        {
            get { return m_BreakValue1; }
            set
            {
                if (value == m_BreakValue1)
                {
                    return;
                }
                m_BreakValue1 = value;
                RaisePropertyChanged(() => BreakValue1);
            }
        }
    }
}