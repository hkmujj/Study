using Microsoft.Practices.Prism.ViewModel;
using System.Drawing;

namespace Urban.ATC.Siemens.Model.ViewModel
{
    public class RegionMViewModel : NotificationObject
    {
        private string m_ActualModel;
        private string m_ActualLevel;
        private string m_DoorModel;
        private Image m_ReverseOperation;
        private Image m_StoppingAccuracy;
        private Image m_DoorRelease;
        private Image m_Departure;
        private Image m_Emergency;
        private Image m_Error;
        private Image m_Special;
        private Image m_ButtonReactivation;

        public string ActualModel
        {
            get { return m_ActualModel; }
            set
            {
                if (value == m_ActualModel)
                {
                    return;
                }
                m_ActualModel = value;
                RaisePropertyChanged(() => ActualModel);
            }
        }

        public string ActualLevel
        {
            get { return m_ActualLevel; }
            set
            {
                if (value == m_ActualLevel)
                {
                    return;
                }
                m_ActualLevel = value;
                RaisePropertyChanged(() => ActualLevel);
            }
        }

        public string DoorModel
        {
            get { return m_DoorModel; }
            set
            {
                if (value == m_DoorModel)
                {
                    return;
                }
                m_DoorModel = value;
                RaisePropertyChanged(() => DoorModel);
            }
        }

        public Image ReverseOperation
        {
            get { return m_ReverseOperation; }
            set
            {
                if (Equals(value, m_ReverseOperation))
                {
                    return;
                }
                m_ReverseOperation = value;
                RaisePropertyChanged(() => ReverseOperation);
            }
        }

        public Image StoppingAccuracy
        {
            get { return m_StoppingAccuracy; }
            set
            {
                if (Equals(value, m_StoppingAccuracy))
                {
                    return;
                }
                m_StoppingAccuracy = value;
                RaisePropertyChanged(() => StoppingAccuracy);
            }
        }

        public Image DoorRelease
        {
            get { return m_DoorRelease; }
            set
            {
                if (Equals(value, m_DoorRelease))
                {
                    return;
                }
                m_DoorRelease = value;
                RaisePropertyChanged(() => DoorRelease);
            }
        }

        public Image Departure
        {
            get { return m_Departure; }
            set
            {
                if (Equals(value, m_Departure))
                {
                    return;
                }
                m_Departure = value;
                RaisePropertyChanged(() => Departure);
            }
        }

        public Image Emergency
        {
            get { return m_Emergency; }
            set
            {
                if (Equals(value, m_Emergency))
                {
                    return;
                }
                m_Emergency = value;
                RaisePropertyChanged(() => Emergency);
            }
        }

        public Image Error
        {
            get { return m_Error; }
            set
            {
                if (Equals(value, m_Error))
                {
                    return;
                }
                m_Error = value;
                RaisePropertyChanged(() => Error);
            }
        }

        public Image Special
        {
            get { return m_Special; }
            set
            {
                if (Equals(value, m_Special))
                {
                    return;
                }
                m_Special = value;
                RaisePropertyChanged(() => Special);
            }
        }

        public Image ButtonReactivation
        {
            get { return m_ButtonReactivation; }
            set
            {
                if (Equals(value, m_ButtonReactivation))
                {
                    return;
                }
                m_ButtonReactivation = value;
                RaisePropertyChanged(() => ButtonReactivation);
            }
        }
    }
}