using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Models.State;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class ActivateModel : ModelBase
    {
        private ObservableCollection<PantographUnit> m_PantographUnits;
        private ObservableCollection<LCBUnit> m_LCBUnits;
        private InverterState m_InverterState00;
        private InverterState m_InverterState06;
        private InverterState m_InverterState03;
        private InverterState m_InverterState01;
        private BatteryChargerState m_BatteryChargerState05;
        private BatteryChargerState m_BatteryChargerState04;

        [ImportingConstructor]
        public ActivateModel()
        {
            PantographUnits = new ObservableCollection<PantographUnit>(GlobalParam.Instance.PantographUnits.OrderBy(o => o.Num));
            LCBUnits = new ObservableCollection<LCBUnit>(GlobalParam.Instance.LCBUnits.OrderBy(o => o.Num));
        }

        /// <summary>
        /// 所有受电弓状态
        /// </summary>
        public ObservableCollection<PantographUnit> PantographUnits
        {
            get { return m_PantographUnits; }
            set
            {
                if (Equals(value, m_PantographUnits))
                {
                    return;
                }
                m_PantographUnits = value;
                RaisePropertyChanged(() => PantographUnits);
                RaisePropertyChanged(() => PantographState07);
                RaisePropertyChanged(() => PantographState02);
            }
        }

        /// <summary>
        /// 7号车受电弓
        /// </summary>
        public PantographUnit PantographState07 { get { return PantographUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2号车受电弓
        /// </summary>
        public PantographUnit PantographState02 { get { return PantographUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 所有LCB状态
        /// </summary>
        public ObservableCollection<LCBUnit> LCBUnits
        {
            get { return m_LCBUnits; }
            set
            {
                if (Equals(value, m_LCBUnits))
                {
                    return;
                }
                m_LCBUnits = value;
                RaisePropertyChanged(() => LCBUnits);
                RaisePropertyChanged(() => LCBState07);
                RaisePropertyChanged(() => LCBState02);
            }
        }

        /// <summary>
        /// 7号车LCB
        /// </summary>
        public LCBUnit LCBState07 { get { return LCBUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2号车LCB
        /// </summary>
        public LCBUnit LCBState02 { get { return LCBUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// 0号车逆变器
        /// </summary>
        public InverterState InverterState00
        {
            get { return m_InverterState00; }
            set
            {
                if (value == m_InverterState00)
                {
                    return;
                }
                m_InverterState00 = value;
                RaisePropertyChanged(() => InverterState00);
            }
        }

        /// <summary>
        /// 6号车逆变器
        /// </summary>
        public InverterState InverterState06
        {
            get { return m_InverterState06; }
            set
            {
                if (value == m_InverterState06)
                {
                    return;
                }
                m_InverterState06 = value;
                RaisePropertyChanged(() => InverterState06);
            }
        }

        /// <summary>
        /// 3号车逆变器
        /// </summary>
        public InverterState InverterState03
        {
            get { return m_InverterState03; }
            set
            {
                if (value == m_InverterState03)
                {
                    return;
                }
                m_InverterState03 = value;
                RaisePropertyChanged(() => InverterState03);
            }
        }

        /// <summary>
        /// 1号车逆变器
        /// </summary>
        public InverterState InverterState01
        {
            get { return m_InverterState01; }
            set
            {
                if (value == m_InverterState01)
                {
                    return;
                }
                m_InverterState01 = value;
                RaisePropertyChanged(() => InverterState01);
            }
        }

        /// <summary>
        /// 5号车充电机
        /// </summary>
        public BatteryChargerState BatteryChargerState05
        {
            get { return m_BatteryChargerState05; }
            set
            {
                if (value == m_BatteryChargerState05)
                {
                    return;
                }
                m_BatteryChargerState05 = value;
                RaisePropertyChanged(() => BatteryChargerState05);
            }
        }

        /// <summary>
        /// 4号车充电机
        /// </summary>
        public BatteryChargerState BatteryChargerState04
        {
            get { return m_BatteryChargerState04; }
            set
            {
                if (value == m_BatteryChargerState04)
                {
                    return;
                }
                m_BatteryChargerState04 = value;
                RaisePropertyChanged(() => BatteryChargerState04);
            }
        }
    }
}