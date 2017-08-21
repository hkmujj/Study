using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.BrakeSys
{
    [Export]
    public class BrakeSysModel : NotificationObject
    {
        private double m_BrakePipePressue;
        private double m_BrakeCylinderPressure;
        private double m_BalancePressure;
        private double m_TotalAirPressure;
        private double m_FlowRate;
        private UseState m_SigleTrain;
        private AirBrakePressure m_BrakePressure;
        private UseState m_ATP;
        private EleAirBrakeState m_EleAirBrake;
        private UseState m_AirSupply;
        private TrainType m_TrainType;
        private double m_CMM;

        public TrainType TrainType
        {
            set
            {
                if (value == m_TrainType)
                {
                    return;
                }

                m_TrainType = value;
                RaisePropertyChanged(() => TrainType);
            }
            get { return m_TrainType; }
        }

        /// <summary>
        /// 补风
        /// </summary>
        public UseState AirSupply
        {
            set
            {
                if (value == m_AirSupply)
                {
                    return;
                }

                m_AirSupply = value;
                RaisePropertyChanged(() => AirSupply);
            }
            get { return m_AirSupply; }
        }

        public EleAirBrakeState EleAirBrake
        {
            get { return m_EleAirBrake; }
            set
            {
                if (value == m_EleAirBrake)
                {
                    return;
                }

                m_EleAirBrake = value;
                RaisePropertyChanged(() => EleAirBrake);
            }
        }

        public UseState ATP
        {
            get { return m_ATP; }
            set
            {
                if (value == m_ATP)
                {
                    return;
                }

                m_ATP = value;
                RaisePropertyChanged(() => ATP);
            }
        }

        public AirBrakePressure BrakePressure
        {
            get { return m_BrakePressure; }
            set
            {
                if (value == m_BrakePressure)
                {
                    return;
                }

                m_BrakePressure = value;
                RaisePropertyChanged(() => BrakePressure);
            }
        }

        public UseState SigleTrain
        {
            get { return m_SigleTrain; }
            set
            {
                if (value == m_SigleTrain)
                {
                    return;
                }

                m_SigleTrain = value;
                RaisePropertyChanged(() => SigleTrain);
            }
        }

        public double CMM
        {
            get { return m_CMM; }
            set
            {
                if (value.Equals(m_CMM))
                {
                    return;
                }

                m_CMM = value;
                RaisePropertyChanged(() => CMM);
            }
        }

        public double FlowRate
        {
            get { return m_FlowRate; }
            set
            {
                if (value.Equals(m_FlowRate))
                {
                    return;
                }

                m_FlowRate = value;
                RaisePropertyChanged(() => FlowRate);
            }
        }

        public double TotalAirPressure
        {
            get { return m_TotalAirPressure; }
            set
            {
                if (value.Equals(m_TotalAirPressure))
                {
                    return;
                }

                m_TotalAirPressure = value;
                RaisePropertyChanged(() => TotalAirPressure);
            }
        }

        public double BalancePressure
        {
            get { return m_BalancePressure; }
            set
            {
                if (value.Equals(m_BalancePressure))
                {
                    return;
                }

                m_BalancePressure = value;
                RaisePropertyChanged(() => BalancePressure);
            }
        }

        public double BrakeCylinderPressure
        {
            get { return m_BrakeCylinderPressure; }
            set
            {
                if (value.Equals(m_BrakeCylinderPressure))
                {
                    return;
                }

                m_BrakeCylinderPressure = value;
                RaisePropertyChanged(() => BrakeCylinderPressure);
            }
        }

        public double BrakePipePressue
        {
            get { return m_BrakePipePressue; }
            set
            {
                if (value.Equals(m_BrakePipePressue))
                {
                    return;
                }

                m_BrakePipePressue = value;
                RaisePropertyChanged(() => BrakePipePressue);
            }
        }
    }
}