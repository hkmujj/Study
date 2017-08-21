using Microsoft.Practices.Prism.ViewModel;
using System.Drawing;

namespace Urban.ATC.Siemens.Model.ViewModel
{
    public class RegionCViewModel : NotificationObject
    {
        private Image m_DrivingImage;
        private string m_MaxumumModel;
        private Image m_TrainIntegrity;
        private Image m_TrainBrakeState;
        private Image m_TrainOBCUStatus;

        public Image DrivingImage
        {
            get { return m_DrivingImage; }
            set
            {
                if (Equals(value, m_DrivingImage))
                {
                    return;
                }
                m_DrivingImage = value;
                RaisePropertyChanged(() => DrivingImage);
            }
        }

        public string MaxumumModel
        {
            get { return m_MaxumumModel; }
            set
            {
                if (value == m_MaxumumModel)
                {
                    return;
                }
                m_MaxumumModel = value;
                RaisePropertyChanged(() => MaxumumModel);
            }
        }

        public Image TrainIntegrity
        {
            get { return m_TrainIntegrity; }
            set
            {
                if (Equals(value, m_TrainIntegrity))
                {
                    return;
                }
                m_TrainIntegrity = value;
                RaisePropertyChanged(() => TrainIntegrity);
            }
        }

        public Image TrainBrakeState
        {
            get { return m_TrainBrakeState; }
            set
            {
                if (Equals(value, m_TrainBrakeState))
                {
                    return;
                }
                m_TrainBrakeState = value;
                RaisePropertyChanged(() => TrainBrakeState);
            }
        }

        public Image TrainOBCUStatus
        {
            get { return m_TrainOBCUStatus; }
            set
            {
                if (Equals(value, m_TrainOBCUStatus))
                {
                    return;
                }
                m_TrainOBCUStatus = value;
                RaisePropertyChanged(() => TrainOBCUStatus);
            }
        }
    }
}