using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class TractionModel : ModelBase
    {
        private float m_SideInverterVoltage;
        private float m_SubInverterVoltage;
        private float m_TractionInverterForce1;
        private float m_TractionInverterForce2;

        private ObservableCollection<LCMUnit> m_LCMUnits;
        private ObservableCollection<ACMUnit> m_ACMUnits;
        private ObservableCollection<MCMUnit> m_MCMUnits;

        [ImportingConstructor]
        public TractionModel(TractionController tractionController)
        {
            TrainModel = new TrainModel();

            LCMUnits = new ObservableCollection<LCMUnit>(GlobalParam.Instance.LCMUnits.OrderBy(o => o.Num));
            ACMUnits = new ObservableCollection<ACMUnit>(GlobalParam.Instance.ACMUnits.OrderBy(o => o.Num));
            MCMUnits = new ObservableCollection<MCMUnit>(GlobalParam.Instance.MCMUnits.OrderBy(o => o.Num));

            TractionController = tractionController;
            TractionController.ViewModel = this;
            TractionController.Initalize();
        }

        /// <summary>
        /// 控制
        /// </summary>
        public TractionController TractionController { get; set; }
        
        /// <summary>
        /// 车辆集合
        /// </summary>
        public TrainModel TrainModel { get; private set; }

        /// <summary>
        /// 网侧变流器电压
        /// </summary>
        public float SideInverterVoltage
        {
            get { return m_SideInverterVoltage; }
            set {
                if (value == m_SideInverterVoltage)
                {
                    return;
                }
                m_SideInverterVoltage = value;
                RaisePropertyChanged(()=>SideInverterVoltage);
            }
        }

        /// <summary>
        /// 辅助变流器电压
        /// </summary>
        public float SubInverterVoltage
        {
            get { return m_SubInverterVoltage; }
            set
            {
                if (value == m_SubInverterVoltage)
                {
                    return;
                }
                m_SubInverterVoltage = value;
                RaisePropertyChanged(() => SubInverterVoltage);
            }
        }

        /// <summary>
        /// 牵引变流器1力
        /// </summary>
        public float TractionInverterForce1
        {
            get { return m_TractionInverterForce1; }
            set
            {
                if (value == m_TractionInverterForce1)
                {
                    return;
                }
                m_TractionInverterForce1 = value;
                RaisePropertyChanged(() => TractionInverterForce1);
            }
        }

        /// <summary>
        /// 牵引变流器2力
        /// </summary>
        public float TractionInverterForce2
        {
            get { return m_TractionInverterForce2; }
            set
            {
                if (value == m_TractionInverterForce2)
                {
                    return;
                }
                m_TractionInverterForce2 = value;
                RaisePropertyChanged(() => TractionInverterForce2);
            }
        }

        /// <summary>
        /// LCM单元
        /// </summary>
        public ObservableCollection<LCMUnit> LCMUnits
        {
            get { return m_LCMUnits; }
            private set
            {
                if (Equals(value, m_LCMUnits))
                {
                    return;
                }
                m_LCMUnits = value;
                RaisePropertyChanged(() => LCMUnits);
                RaisePropertyChanged(() => LCM1);
                RaisePropertyChanged(() => LCM2);
            }
        }
        /// <summary>
        /// LCM
        /// </summary>
        public LCMUnit LCM1 { get { return LCMUnits.Where(w => w.Car == 0 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// LCM2
        /// </summary>
        public LCMUnit LCM2 { get { return LCMUnits.Where(w => w.Car == 0 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// ACM单元
        /// </summary>
        public ObservableCollection<ACMUnit> ACMUnits
        {
            get { return m_ACMUnits; }
            private set
            {
                if (Equals(value, m_ACMUnits))
                {
                    return;
                }
                m_ACMUnits = value;
                RaisePropertyChanged(() => ACMUnits);
                RaisePropertyChanged(() => ACM);
            }
        }

        /// <summary>
        /// ACM1
        /// </summary>
        public ACMUnit ACM { get { return ACMUnits.Where(w => w.Car == 0 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        

        /// <summary>
        /// MCM单元
        /// </summary>
        public ObservableCollection<MCMUnit> MCMUnits
        {
            get { return m_MCMUnits; }
            private set
            {
                if (Equals(value, m_MCMUnits))
                {
                    return;
                }
                m_MCMUnits = value;
                RaisePropertyChanged(() => MCMUnits);
                RaisePropertyChanged(() => MCM1);
                RaisePropertyChanged(() => MCM2);
            }
        }

        /// <summary>
        /// MCM1
        /// </summary>
        public MCMUnit MCM1 { get { return MCMUnits.Where(w => w.Car == 0 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// MCM2
        /// </summary>
        public MCMUnit MCM2 { get { return MCMUnits.Where(w => w.Car == 0 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }
    }
}