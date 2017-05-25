using System.ComponentModel.Composition;
using System.Windows;
using Engine.TCMS.Turkmenistan.Constant;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Model.BtnStragy;
using Engine.TCMS.Turkmenistan.Resources.Keys;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Model
{
    [Export]
    public class DomainModel : NotificationObject
    {
        public DomainModel()
        {
            _mNavigatorToState = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToState>();
            m_NavigatorToView = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>();
            IsVisibility = Visibility.Hidden;
        }

        private readonly NavigatorToState _mNavigatorToState;
        private readonly NavigatorToView m_NavigatorToView;
        private IStateInterface m_CurrentStateInterface;
        private Visibility m_IsVisibility;

        public IStateInterface CurrentStateInterface
        {
            private set
            {
                if (Equals(value, m_CurrentStateInterface))
                {
                    return;
                }

                m_CurrentStateInterface = value;
                m_CurrentStateInterface.UpdateState();
                RaisePropertyChanged(() => CurrentStateInterface);
            }
            get { return m_CurrentStateInterface; }
        }

        public void UpdateCurrentState(IStateInterface current)
        {
            CurrentStateInterface = current;
        }
        [Import]
        public MainModel MainModel { get; private set; }
        /// <summary>
        /// 黑屏
        /// </summary>
        public Visibility IsVisibility
        {
            get { return m_IsVisibility; }
            set
            {
                if (value == m_IsVisibility)
                    return;
                m_IsVisibility = value;
                if (m_IsVisibility == Visibility.Visible)
                {
                    _mNavigatorToState.Publish(new NavigatorToState.Args(StateKeys.Root_本车信息));
                    m_NavigatorToView.Publish(new NavigatorToView.Args(ViewNames.CurrentAxleView));
                    m_NavigatorToView.Publish(new NavigatorToView.Args(ViewNames.CurrentProgressbarView));
                    m_NavigatorToView.Publish(new NavigatorToView.Args(ViewNames.CurrentRunparamView));
                }
                RaisePropertyChanged(() => IsVisibility);
            }
        }
    }
}