using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380BG.View.Contents.BrakeStatus;

namespace Motor.HMI.CRH380BG.Model.Domain.Brake
{
    [Export]
    public class BrakeModel : NotificationObject
    {
        

        //总风管
        private float m_TotalWindPipePressure;
        private float m_TotalWindPipe1Pressure;
        private float m_TotalWindPipe8Pressure;

        //列车管
        private float m_TrainPipePressure;

        //制动有效D
        private Efficence m_IsBrakeD1Efficent;
        private Efficence m_IsBrakeD2Efficent;
        private Efficence m_IsBrakeD3Efficent;
        private Efficence m_IsBrakeD4Efficent;
        private Efficence m_IsBrakeD5Efficent;
        private Efficence m_IsBrakeD6Efficent;
        private Efficence m_IsBrakeD7Efficent;
        private Efficence m_IsBrakeD8Efficent;

        //制动施加D
        private EfficentState m_IsBrakeD1Apply;
        private EfficentState m_IsBrakeD2Apply;
        private EfficentState m_IsBrakeD3Apply;
        private EfficentState m_IsBrakeD4Apply;
        private EfficentState m_IsBrakeD5Apply;
        private EfficentState m_IsBrakeD6Apply;
        private EfficentState m_IsBrakeD7Apply;
        private EfficentState m_IsBrakeD8Apply;

        //制动有效E
        private Efficence m_IsBrakeE1Efficent;
        private Efficence m_IsBrakeE2Efficent;
        private Efficence m_IsBrakeE3Efficent;
        private Efficence m_IsBrakeE4Efficent;
        private Efficence m_IsBrakeE5Efficent;
        private Efficence m_IsBrakeE6Efficent;
        private Efficence m_IsBrakeE7Efficent;
        private Efficence m_IsBrakeE8Efficent;

        //制动施加E
        private EfficentState m_IsBrakeE1Apply;
        private EfficentState m_IsBrakeE2Apply;
        private EfficentState m_IsBrakeE3Apply;
        private EfficentState m_IsBrakeE4Apply;
        private EfficentState m_IsBrakeE5Apply;
        private EfficentState m_IsBrakeE6Apply;
        private EfficentState m_IsBrakeE7Apply;
        private EfficentState m_IsBrakeE8Apply;

        [ImportingConstructor]

        public BrakeModel(BrakeEfficentModel brakeEfficentModel, ParkingBrakeModel parkingBrakeModel, BrakeFunctionStatusModel brakeFunctionStatusModel)
        {
            BrakeEfficentModel = brakeEfficentModel;
            ParkingBrakeModel = parkingBrakeModel;
            BrakeFunctionStatusModel = brakeFunctionStatusModel;
        }

        public BrakeFunctionStatusModel BrakeFunctionStatusModel { get; private set; }

        public BrakeEfficentModel BrakeEfficentModel { get; private set; }

        public float TotalWindPipePressure
        {
            get { return m_TotalWindPipePressure; }
            set
            {
                if (value.Equals(m_TotalWindPipePressure))
                {
                    return;
                }
                m_TotalWindPipePressure = value;
                RaisePropertyChanged(() => TotalWindPipePressure);
            }
        }

        public float TotalWindPipe1Pressure
        {
            get { return m_TotalWindPipe1Pressure; }
            set
            {
                if (value.Equals(m_TotalWindPipe1Pressure))
                {
                    return;
                }
                m_TotalWindPipe1Pressure = value;
                RaisePropertyChanged(() => TotalWindPipe1Pressure);
            }
        }

        public float TotalWindPipe8Pressure
        {
            get { return m_TotalWindPipe8Pressure; }
            set
            {
                if (value.Equals(m_TotalWindPipe8Pressure))
                {
                    return;
                }
                m_TotalWindPipe8Pressure = value;
                RaisePropertyChanged(() => TotalWindPipe8Pressure);
            }
        }

        public float TrainPipePressure
        {
            get { return m_TrainPipePressure; }
            set
            {
                if (value.Equals(m_TrainPipePressure))
                {
                    return;
                }
                m_TrainPipePressure = value;
                RaisePropertyChanged(() => TrainPipePressure);
            }
        }

        public Efficence IsBrakeD1Efficent
        {
            get { return m_IsBrakeD1Efficent; }
            set
            {
                if (value == m_IsBrakeD1Efficent)
                {
                    return;
                }
                m_IsBrakeD1Efficent = value;
                RaisePropertyChanged(() => IsBrakeD1Efficent);
            }
        }

        public Efficence IsBrakeD2Efficent
        {
            get { return m_IsBrakeD2Efficent; }
            set
            {
                if (value == m_IsBrakeD2Efficent)
                {
                    return;
                }
                m_IsBrakeD2Efficent = value;
                RaisePropertyChanged(() => IsBrakeD2Efficent);
            }
        }

        public Efficence IsBrakeD3Efficent
        {
            get { return m_IsBrakeD3Efficent; }
            set
            {
                if (value == m_IsBrakeD3Efficent)
                {
                    return;
                }
                m_IsBrakeD3Efficent = value;
                RaisePropertyChanged(() => IsBrakeD3Efficent);
            }
        }
        public Efficence IsBrakeD4Efficent
        {
            get { return m_IsBrakeD4Efficent; }
            set
            {
                if (value == m_IsBrakeD4Efficent)
                {
                    return;
                }
                m_IsBrakeD4Efficent = value;
                RaisePropertyChanged(() => IsBrakeD4Efficent);
            }
        }

        public Efficence IsBrakeD5Efficent
        {
            get { return m_IsBrakeD5Efficent; }
            set
            {
                if (value == m_IsBrakeD5Efficent)
                {
                    return;
                }
                m_IsBrakeD5Efficent = value;
                RaisePropertyChanged(() => IsBrakeD5Efficent);
            }
        }

        public Efficence IsBrakeD6Efficent
        {
            get { return m_IsBrakeD6Efficent; }
            set
            {
                if (value == m_IsBrakeD6Efficent)
                {
                    return;
                }
                m_IsBrakeD6Efficent = value;
                RaisePropertyChanged(() => IsBrakeD6Efficent);
            }
        }

        public Efficence IsBrakeD7Efficent
        {
            get { return m_IsBrakeD7Efficent; }
            set
            {
                if (value == m_IsBrakeD7Efficent)
                {
                    return;
                }
                m_IsBrakeD7Efficent = value;
                RaisePropertyChanged(() => IsBrakeD7Efficent);
            }
        }

        public Efficence IsBrakeD8Efficent
        {
            get { return m_IsBrakeD8Efficent; }
            set
            {
                if (value == m_IsBrakeD8Efficent)
                {
                    return;
                }
                m_IsBrakeD8Efficent = value;
                RaisePropertyChanged(() => IsBrakeD8Efficent);
            }
        }

        public EfficentState IsBrakeD1Apply
        {
            get { return m_IsBrakeD1Apply; }
            set
            {
                if (value == m_IsBrakeD1Apply)
                {
                    return;
                }
                m_IsBrakeD1Apply = value;
                RaisePropertyChanged(() => IsBrakeD1Apply);
            }
        }

        public EfficentState IsBrakeD2Apply
        {
            get { return m_IsBrakeD2Apply; }
            set
            {
                if (value == m_IsBrakeD2Apply)
                {
                    return;
                }
                m_IsBrakeD2Apply = value;
                RaisePropertyChanged(() => IsBrakeD2Apply);
            }
        }
               
        public EfficentState IsBrakeD3Apply
        {
            get { return m_IsBrakeD3Apply; }
            set
            {
                if (value == m_IsBrakeD3Apply)
                {
                    return;
                }
                m_IsBrakeD3Apply = value;
                RaisePropertyChanged(() => IsBrakeD3Apply);
            }
        }
               
        public EfficentState IsBrakeD4Apply
        {
            get { return m_IsBrakeD4Apply; }
            set
            {
                if (value == m_IsBrakeD4Apply)
                {
                    return;
                }
                m_IsBrakeD4Apply = value;
                RaisePropertyChanged(() => IsBrakeD4Apply);
            }
        }
               
        public EfficentState IsBrakeD5Apply
        {
            get { return m_IsBrakeD5Apply; }
            set
            {
                if (value == m_IsBrakeD5Apply)
                {
                    return;
                }
                m_IsBrakeD5Apply = value;
                RaisePropertyChanged(() => IsBrakeD5Apply);
            }
        }
               
        public EfficentState IsBrakeD6Apply
        {
            get { return m_IsBrakeD6Apply; }
            set
            {
                if (value == m_IsBrakeD6Apply)
                {
                    return;
                }
                m_IsBrakeD6Apply = value;
                RaisePropertyChanged(() => IsBrakeD6Apply);
            }
        }
               
        public EfficentState IsBrakeD7Apply
        {
            get { return m_IsBrakeD7Apply; }
            set
            {
                if (value == m_IsBrakeD7Apply)
                {
                    return;
                }
                m_IsBrakeD7Apply = value;
                RaisePropertyChanged(() => IsBrakeD7Apply);
            }
        }
               
        public EfficentState IsBrakeD8Apply
        {
            get { return m_IsBrakeD8Apply; }
            set
            {
                if (value == m_IsBrakeD8Apply)
                {
                    return;
                }
                m_IsBrakeD8Apply = value;
                RaisePropertyChanged(() => IsBrakeD8Apply);
            }
        }

        public Efficence IsBrakeE1Efficent
        {
            get { return m_IsBrakeE1Efficent; }
            set
            {
                if (value == m_IsBrakeE1Efficent)
                {
                    return;
                }
                m_IsBrakeE1Efficent = value;
                RaisePropertyChanged(() => IsBrakeE1Efficent);
            }
        }

        public Efficence IsBrakeE2Efficent
        {
            get { return m_IsBrakeE2Efficent; }
            set
            {
                if (value == m_IsBrakeE2Efficent)
                {
                    return;
                }
                m_IsBrakeE2Efficent = value;
                RaisePropertyChanged(() => IsBrakeE2Efficent);
            }
        }

        public Efficence IsBrakeE3Efficent
        {
            get { return m_IsBrakeE3Efficent; }
            set
            {
                if (value == m_IsBrakeE3Efficent)
                {
                    return;
                }
                m_IsBrakeE3Efficent = value;
                RaisePropertyChanged(() => IsBrakeE3Efficent);
            }
        }
        public Efficence IsBrakeE4Efficent
        {
            get { return m_IsBrakeE4Efficent; }
            set
            {
                if (value == m_IsBrakeE4Efficent)
                {
                    return;
                }
                m_IsBrakeE4Efficent = value;
                RaisePropertyChanged(() => IsBrakeE4Efficent);
            }
        }

        public Efficence IsBrakeE5Efficent
        {
            get { return m_IsBrakeE5Efficent; }
            set
            {
                if (value == m_IsBrakeE5Efficent)
                {
                    return;
                }
                m_IsBrakeE5Efficent = value;
                RaisePropertyChanged(() => IsBrakeE5Efficent);
            }
        }

        public Efficence IsBrakeE6Efficent
        {
            get { return m_IsBrakeE6Efficent; }
            set
            {
                if (value == m_IsBrakeE6Efficent)
                {
                    return;
                }
                m_IsBrakeE6Efficent = value;
                RaisePropertyChanged(() => IsBrakeE6Efficent);
            }
        }

        public Efficence IsBrakeE7Efficent
        {
            get { return m_IsBrakeE7Efficent; }
            set
            {
                if (value == m_IsBrakeE7Efficent)
                {
                    return;
                }
                m_IsBrakeE7Efficent = value;
                RaisePropertyChanged(() => IsBrakeE7Efficent);
            }
        }

        public Efficence IsBrakeE8Efficent
        {
            get { return m_IsBrakeE8Efficent; }
            set
            {
                if (value == m_IsBrakeE8Efficent)
                {
                    return;
                }
                m_IsBrakeE8Efficent = value;
                RaisePropertyChanged(() => IsBrakeE8Efficent);
            }
        }

        public EfficentState IsBrakeE1Apply
        {
            get { return m_IsBrakeE1Apply; }
            set
            {
                if (value == m_IsBrakeE1Apply)
                {
                    return;
                }
                m_IsBrakeE1Apply = value;
                RaisePropertyChanged(() => IsBrakeE1Apply);
            }
        }

        public EfficentState IsBrakeE2Apply
        {
            get { return m_IsBrakeE2Apply; }
            set
            {
                if (value == m_IsBrakeE2Apply)
                {
                    return;
                }
                m_IsBrakeE2Apply = value;
                RaisePropertyChanged(() => IsBrakeE2Apply);
            }
        }

        public EfficentState IsBrakeE3Apply
        {
            get { return m_IsBrakeE3Apply; }
            set
            {
                if (value == m_IsBrakeE3Apply)
                {
                    return;
                }
                m_IsBrakeE3Apply = value;
                RaisePropertyChanged(() => IsBrakeE3Apply);
            }
        }

        public EfficentState IsBrakeE4Apply
        {
            get { return m_IsBrakeE4Apply; }
            set
            {
                if (value == m_IsBrakeE4Apply)
                {
                    return;
                }
                m_IsBrakeE4Apply = value;
                RaisePropertyChanged(() => IsBrakeE4Apply);
            }
        }

        public EfficentState IsBrakeE5Apply
        {
            get { return m_IsBrakeE5Apply; }
            set
            {
                if (value == m_IsBrakeE5Apply)
                {
                    return;
                }
                m_IsBrakeE5Apply = value;
                RaisePropertyChanged(() => IsBrakeE5Apply);
            }
        }

        public EfficentState IsBrakeE6Apply
        {
            get { return m_IsBrakeE6Apply; }
            set
            {
                if (value == m_IsBrakeE6Apply)
                {
                    return;
                }
                m_IsBrakeE6Apply = value;
                RaisePropertyChanged(() => IsBrakeE6Apply);
            }
        }

        public EfficentState IsBrakeE7Apply
        {
            get { return m_IsBrakeE7Apply; }
            set
            {
                if (value == m_IsBrakeE7Apply)
                {
                    return;
                }
                m_IsBrakeE7Apply = value;
                RaisePropertyChanged(() => IsBrakeE7Apply);
            }
        }

        public EfficentState IsBrakeE8Apply
        {
            get { return m_IsBrakeE8Apply; }
            set
            {
                if (value == m_IsBrakeE8Apply)
                {
                    return;
                }
                m_IsBrakeE8Apply = value;
                RaisePropertyChanged(() => IsBrakeE8Apply);
            }
        }

    
        public ParkingBrakeModel ParkingBrakeModel { get; private set; }
    }

   
}