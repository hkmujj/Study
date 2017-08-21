using System;
using System.Threading;
using System.Windows;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class RegionCViewModel : ViewModelBase
    {
        private Timer m_Timer;

        public RegionCViewModel()
        {
            DriveingBrake = DriveingBrakeType.Initial;
            MaximumMode = MaximumMode.Initial;
            TrainInteGrityC3 = TrainInteGrity.Initial;
            TrainInteGrityC4 = TrainInteGrity.Initial;
            HeadOBCU = OBCUModel.None;
            m_Timer = new Timer(state =>
            {
                Visibility = Visibile
                    ? DateTime.Now.Second % 2 == 0 ? Visibility.Visible : Visibility.Hidden
                    : Visibility.Visible;
            }, null, 1000, 1000);
        }

        private DriveingBrakeType m_DriveingBrake;
        private MaximumMode m_MaximumMode;
        private TrainInteGrity m_TrainInteGrityC3;
        private TrainInteGrity m_TrainInteGrityC4;
        private OBCUModel m_OBCU;

        private bool m_Visibile;
        private Visibility m_Visibility;
        private OBCUModel m_TailOBCU;

        public Visibility Visibility
        {
            get { return m_Visibility; }
            set
            {
                if (value == m_Visibility)
                {
                    return;
                }
                m_Visibility = value;
                RaisePropertyChanged(() => Visibility);
            }
        }

        public bool Visibile
        {
            get { return m_Visibile; }
            set
            {
                if (value == m_Visibile)
                {
                    return;
                }
                m_Visibile = value;
                RaisePropertyChanged(() => Visibile);
            }
        }

        public DriveingBrakeType DriveingBrake
        {
            get { return m_DriveingBrake; }
            set
            {
                if (value == m_DriveingBrake)
                {
                    return;
                }
                m_DriveingBrake = value;
                RaisePropertyChanged(() => DriveingBrake);
            }
        }

        public MaximumMode MaximumMode
        {
            get { return m_MaximumMode; }
            set
            {
                if (value == m_MaximumMode)
                {
                    return;
                }
                m_MaximumMode = value;
                RaisePropertyChanged(() => MaximumMode);
            }
        }

        public TrainInteGrity TrainInteGrityC3
        {
            get { return m_TrainInteGrityC3; }
            set
            {
                if (value == m_TrainInteGrityC3)
                {
                    return;
                }
                m_TrainInteGrityC3 = value;
                RaisePropertyChanged(() => TrainInteGrityC3);
            }
        }

        public TrainInteGrity TrainInteGrityC4
        {
            get { return m_TrainInteGrityC4; }
            set
            {
                if (value == m_TrainInteGrityC4)
                {
                    return;
                }
                m_TrainInteGrityC4 = value;
                RaisePropertyChanged(() => TrainInteGrityC4);
            }
        }

        public OBCUModel HeadOBCU
        {
            get { return m_OBCU; }
            set
            {
                if (value == m_OBCU)
                {
                    return;
                }
                m_OBCU = value;
                RaisePropertyChanged(() => HeadOBCU);
            }
        }

        public OBCUModel TailOBCU
        {
            set
            {
                if (value == m_TailOBCU)
                {
                    return;
                }
                m_TailOBCU = value;
                RaisePropertyChanged(() => TailOBCU);
            }
            get { return m_TailOBCU; }
        }
    }
}