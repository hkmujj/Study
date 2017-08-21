using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class TotalWalkingDistanceViewModel : NotificationObject
    {
        private double m_Distance;

        public double Distance
        {
            get { return m_Distance; }
            set
            {
                if (value.Equals(m_Distance))
                {
                    return;
                }
                m_Distance = value;
                RaisePropertyChanged(() => Distance);
            }
        }
    }
}