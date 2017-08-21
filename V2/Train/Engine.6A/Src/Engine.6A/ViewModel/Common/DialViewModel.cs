using System.ComponentModel.Composition;
using System.Windows.Input;
using Engine._6A.Config;
using Engine._6A.Constance;
using Engine._6A.Enums;
using Engine._6A.Interface.ViewModel;
using Engine._6A.Views.Axle6;
using Engine._6A.Views.Axle8;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Engine._6A.ViewModel.Common
{
    [Export(typeof(IDialViewModel))]
    public class DialViewModel : ViewModelBase, IDialViewModel
    {
        private double m_SecAngle;
        private double m_MiuAngle;
        private double m_HourAngle;

        public DialViewModel()
        {
            ChangedView = new DelegateCommand(ChangedCurrentMethod);
        }
        private void ChangedCurrentMethod()
        {
            RegionManager.RequestNavigate(RegionNames.MainContentRegion, Axle6ControlName.MainView);
            RegionManager.RequestNavigate(RegionNames.ContentRegion, Axle6ControlName.MainContent);
            if (GlobalParam.Engine6AConfig.Type == Engine6AType.Alex6)
            {
                ServiceLocator.Current.GetInstance<AxleButtonView>().IsCurrent = true;
            }
            else
            {
                ServiceLocator.Current.GetInstance<Axle8ButtonView>().IsCurrent = true;
            }


        }
        public ICommand ChangedView { get; private set; }

        public double HourAngle
        {
            get { return m_HourAngle; }
            set
            {
                if (value.Equals(m_HourAngle))
                {
                    return;
                }
                m_HourAngle = value;
                RaisePropertyChanged(() => HourAngle);
            }
        }

        public double MiuAngle
        {
            get { return m_MiuAngle; }
            set
            {
                if (value.Equals(m_MiuAngle))
                {
                    return;
                }
                m_MiuAngle = value;
                RaisePropertyChanged(() => MiuAngle);
            }
        }

        public double SecAngle
        {
            get { return m_SecAngle; }
            set
            {
                if (value.Equals(m_SecAngle))
                {
                    return;
                }
                m_SecAngle = value;
                RaisePropertyChanged(() => SecAngle);
            }
        }
    }
}