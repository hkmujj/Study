using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.TrainSetting.Detail
{
    [Export]
    public class CountMileageModel : NotificationObject
    {
        private double m_PreMileage;
        private double m_CurrentMileage;
        private int m_SelectedIndex;

        public CountMileageModel()
        {
            SelectedIndex = 0;
        }

        public double PreMileage
        {
            set
            {
                if (value.Equals(m_PreMileage))
                {
                    return;
                }

                m_PreMileage = value;
                RaisePropertyChanged(() => PreMileage);
            }
            get { return m_PreMileage; }
        }

        public double CurrentMileage
        {
            get { return m_CurrentMileage; }
            set
            {
                if (value.Equals(m_CurrentMileage))
                {
                    return;
                }

                m_CurrentMileage = value;
                RaisePropertyChanged(() => CurrentMileage);
            }
        }

        public CountMileageType SelectedModel
        {
            get { return (CountMileageType) SelectedIndex; }
            set { SelectedIndex = (int) value; }
        }

        public int SelectedIndex
        {
            get { return m_SelectedIndex; }
            set
            {
                if (value == m_SelectedIndex)
                {
                    return;
                }

                m_SelectedIndex = value;
                RaisePropertyChanged(() => SelectedIndex);
                RaisePropertyChanged(() => SelectedModel);
            }
        }
    }
}