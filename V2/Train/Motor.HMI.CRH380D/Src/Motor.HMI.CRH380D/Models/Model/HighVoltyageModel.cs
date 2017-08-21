using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class HighVoltyageModel : ModelBase
    {
        private ObservableCollection<PantographUnit> m_PantographUnits;
        private ObservableCollection<LCBUnit> m_LCBUnits;
        private ObservableCollection<QuickBreakUnit> m_QuickBreakUnits;
        private ObservableCollection<GroundingUnit> m_GroundingUnits;
        private float m_Voltage1;
        private float m_Voltage2;
        private float m_Electricity1; 
        private float m_Electricity2;

        [ImportingConstructor]
        public HighVoltyageModel(HighVoltyageController highVoltyageController)
        {
            PantographUnits = new ObservableCollection<PantographUnit>(GlobalParam.Instance.PantographUnits.OrderBy(o => o.Num));
            LCBUnits = new ObservableCollection<LCBUnit>(GlobalParam.Instance.LCBUnits.OrderBy(o => o.Num));
            QuickBreakUnits = new ObservableCollection<QuickBreakUnit>(GlobalParam.Instance.QuickBreakUnits.OrderBy(o => o.Num));
            GroundingUnits = new ObservableCollection<GroundingUnit>(GlobalParam.Instance.GroundingUnits.OrderBy(o => o.Num));

            HighVoltyageController = highVoltyageController;
            HighVoltyageController.ViewModel = new Lazy<HighVoltyageModel>(()=>this);
            HighVoltyageController.Initalize();
        }

        public HighVoltyageController HighVoltyageController { get; private set; }

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
                RaisePropertyChanged(() => Pantograph1);
                RaisePropertyChanged(() => Pantograph2);
            }
        }

        /// <summary>
        /// 受电弓1状态
        /// </summary>
        public PantographUnit Pantograph1 { get { return PantographUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 受电弓2状态
        /// </summary>
        public PantographUnit Pantograph2 { get { return PantographUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
      
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
                RaisePropertyChanged(() => LCB1);
                RaisePropertyChanged(() => LCB2);
            }
        }

        /// <summary>
        /// LCB1状态
        /// </summary>
        public LCBUnit LCB1 { get { return LCBUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// LCB2状态
        /// </summary>
        public LCBUnit LCB2 { get { return LCBUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

      
        /// <summary>
        /// 所有高速断路器状态
        /// </summary>
        public ObservableCollection<QuickBreakUnit> QuickBreakUnits
        {
            get { return m_QuickBreakUnits; }
            set
            {
                if (Equals(value, m_QuickBreakUnits))
                {
                    return;
                }
                m_QuickBreakUnits = value;
                RaisePropertyChanged(() => QuickBreakUnits);
                RaisePropertyChanged(() => QuickBreak1);
                RaisePropertyChanged(() => QuickBreak2);
            }
        }

        /// <summary>
        /// 高速断路器1状态
        /// </summary>
        public QuickBreakUnit QuickBreak1 { get { return m_QuickBreakUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 高速断路器2状态
        /// </summary>
        public QuickBreakUnit QuickBreak2 { get { return m_QuickBreakUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }


        /// <summary>
        /// 所有接地开关状态
        /// </summary>
        public ObservableCollection<GroundingUnit> GroundingUnits
        {
            get { return m_GroundingUnits; }
            set
            {
                if (Equals(value, m_GroundingUnits))
                {
                    return;
                }
                m_GroundingUnits = value;
                RaisePropertyChanged(() => GroundingUnits);
                RaisePropertyChanged(() => Pantograph1);
                RaisePropertyChanged(() => Pantograph2);
            }
        }

        /// <summary>
        /// 接地开关1状态
        /// </summary>
        public GroundingUnit Grounding1 { get { return m_GroundingUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 接地开关2状态
        /// </summary>
        public GroundingUnit Grounding2 { get { return m_GroundingUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }


        /// <summary>
        /// 7车电压
        /// </summary>
        public float Voltage1
        {
            get { return m_Voltage1; }
            set
            {
                if (value == m_Voltage1)
                {
                    return;
                }
                m_Voltage1 = value;
                RaisePropertyChanged(() => Voltage1);
            }
        }

        /// <summary>
        /// 2车电压
        /// </summary>
        public float Voltage2
        {
            get { return m_Voltage2; }
            set
            {
                if (value == m_Voltage2)
                {
                    return;
                }
                m_Voltage2 = value;
                RaisePropertyChanged(() => Voltage2);
            }
        }
        /// <summary>
        /// 7车电流
        /// </summary>
        public float Electricity1
        {
            get { return m_Electricity1; }
            set
            {
                if (value == m_Electricity1)
                {
                    return;
                }
                m_Electricity1 = value;
                RaisePropertyChanged(() => Electricity1);
            }
        }

        /// <summary>
        /// 2车电流
        /// </summary>
        public float Electricity2
        {
            get { return m_Electricity2; }
            set
            {
                if (value == m_Electricity2)
                {
                    return;
                }
                m_Electricity2 = value;
                RaisePropertyChanged(() => Electricity2);
            }
        }
    }
}