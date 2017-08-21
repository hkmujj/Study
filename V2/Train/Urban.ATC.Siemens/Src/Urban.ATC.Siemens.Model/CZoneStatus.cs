using Microsoft.Practices.Prism.ViewModel;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Domain.Interface
{
    public class CZoneStatus : NotificationObject, ICZoneStatus
    {
        private OBCUModel m_ObcuModel;
        private DriveingBrakeType m_DriveingBrakeType;
        private TrainInteGrity m_TrainInteGrityC3;
        private TrainInteGrity m_TrainInteGrityC4;
        private MaximumMode m_MaximumModel;
        private bool m_ModelFlicker;

        public MaximumMode MaximumMode
        {
            get { return m_MaximumModel; }

            set
            {
                if (value == m_MaximumModel)
                {
                    return;
                }
                m_MaximumModel = value;
                RaisePropertyChanged(() => MaximumMode);
            }
        }

        public bool ModelFlicker
        {
            get { return m_ModelFlicker; }
            set
            {
                if (value == m_ModelFlicker)
                {
                    return;
                }
                m_ModelFlicker = value;
                RaisePropertyChanged(() => ModelFlicker);
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

        public DriveingBrakeType DriveingBrakeType
        {
            get { return m_DriveingBrakeType; }

            set
            {
                if (value == m_DriveingBrakeType)
                {
                    return;
                }
                m_DriveingBrakeType = value;
                RaisePropertyChanged(() => DriveingBrakeType);
            }
        }

        public OBCUModel ObcuModel
        {
            get { return m_ObcuModel; }
            set
            {
                if (value == m_ObcuModel)
                {
                    return;
                }
                m_ObcuModel = value;
                RaisePropertyChanged(() => ObcuModel);
            }
        }
    }
}