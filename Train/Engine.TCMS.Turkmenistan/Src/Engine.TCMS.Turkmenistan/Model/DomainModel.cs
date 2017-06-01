using System.ComponentModel.Composition;
using System.Windows;
using Engine.TCMS.Turkmenistan.Constant;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Extension;
using Engine.TCMS.Turkmenistan.Model.BtnStragy;
using Engine.TCMS.Turkmenistan.Resources.Keys;
using Engine.TCMS.Turkmenistan.View.Layout;
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
            m_NavigatorToState = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToState>();
            m_NavigatorToView = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>();
            IsVisibility = Visibility.Hidden;
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<DataServiceDataChangedEvent>()
                .Subscribe(DataChanged,ThreadOption.UIThread);
        }

        private void DataChanged(DataServiceDataChangedEvent.Args obj)
        {
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InB亮屏, b => IsVisibility = b ? Visibility.Visible : Visibility.Hidden);
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InBF1按键按下, b =>
            {
                if (b)
                {
                    CurrentStateInterface.BtnF1.ClickCommand.Execute();
                }
            });
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InBF2按键按下, b =>
            {
                if (b)
                {
                    CurrentStateInterface.BtnF2.ClickCommand.Execute();
                }
            });
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InBF3按键按下, b =>
            {
                if (b)
                {
                    CurrentStateInterface.BtnF3.ClickCommand.Execute();
                }
            });
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InBF4按键按下, b =>
            {
                if (b)
                {
                    CurrentStateInterface.BtnF4.ClickCommand.Execute();
                }
            });
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InBF5按键按下, b =>
            {
                if (b)
                {
                    var command = CurrentStateInterface.BtnF5.ClickCommand;
                    command.Execute();
                }
            });
        }

        private readonly NavigatorToState m_NavigatorToState;
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
        public AxleBitModel AxleBitModel { get; private set; }  
        [Import]
        public MainModel MainModel { get; private set; }
        [Import]
        public FaultCutModel FaultCutModel { get; private set; }
        [Import]
        public RunParamModel RunParamModel { get; private set; }
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
                    m_NavigatorToView.Publish(new NavigatorToView.Args(typeof(ShellContentStyle1Layout).FullName));
                    m_NavigatorToState.Publish(new NavigatorToState.Args(StateKeys.Root_本车信息));
                    m_NavigatorToView.Publish(new NavigatorToView.Args(ViewNames.CurrentAxleView));
                    m_NavigatorToView.Publish(new NavigatorToView.Args(ViewNames.CurrentProgressbarView));
                    m_NavigatorToView.Publish(new NavigatorToView.Args(ViewNames.CurrentRunparamView));
                }
                else
                {
                    m_NavigatorToView.Publish(new NavigatorToView.Args(typeof(BlackView).FullName));
                }
                RaisePropertyChanged(() => IsVisibility);
            }
        }
    }
}