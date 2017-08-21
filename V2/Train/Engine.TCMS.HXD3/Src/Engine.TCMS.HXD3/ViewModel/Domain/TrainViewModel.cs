using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Model.TCMS.Domain;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class TrainViewModel : NotificationObject
    {
        private ObservableCollection<Car> m_Cars;
        private RunDirection m_RunDirection;

        public RunDirection RunDirection
        {
            set
            {
                if (value == m_RunDirection)
                {
                    return;
                }

                m_RunDirection = value;
                RaisePropertyChanged(() => RunDirection);
            }
            get { return m_RunDirection; }
        }

        public ObservableCollection<Car> Cars
        {
            set
            {
                if (Equals(value, m_Cars))
                {
                    return;
                }

                m_Cars = value;
                RaisePropertyChanged(() => Cars);
            }
            get { return m_Cars; }
        }

        public TrainViewModel()
        {
            Cars = new ObservableCollection<Car>
            {
                new Car() {EndPointVisible = true},
                new Car() {EndPointVisible = true}
            };
        }
    }
}