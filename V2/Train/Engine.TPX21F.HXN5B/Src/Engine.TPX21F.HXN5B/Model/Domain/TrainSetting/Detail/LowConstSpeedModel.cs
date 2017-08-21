using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.TrainSetting.Detail
{
    [Export]
    public class LowConstSpeedModel : NotificationObject
    {
        private float m_CurrentLowSpeed;

        public float CurrentLowSpeed
        {
            get { return m_CurrentLowSpeed; }
            set
            {
                if (value.Equals(m_CurrentLowSpeed))
                {
                    return;
                }

                m_CurrentLowSpeed = value;
                RaisePropertyChanged(() => CurrentLowSpeed);
            }
        }
    }
}