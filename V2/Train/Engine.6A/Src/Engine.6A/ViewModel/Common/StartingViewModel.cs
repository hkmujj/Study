using System.ComponentModel.Composition;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Engine._6A.Constance;
using Engine._6A.Event;
using Engine._6A.EventArgs;
using Engine._6A.Interface.ViewModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

namespace Engine._6A.ViewModel.Common
{
    [Export(typeof(IStartingViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class StartingViewModel : ViewModelBase, IStartingViewModel
    {
        private Visibility m_Visibility;
        private Visibility m_AVisibility;
        private string m_LoadText;
        private readonly Timer m_DoubleTimer;
        private double m_CurrentProgress;

        public double CurrentProgress
        {
            get { return m_CurrentProgress; }
            private set
            {
                if (m_CurrentProgress.Equals(value))
                {
                    return;
                }
                m_CurrentProgress = value;
                LoadText = string.Format("LOADING...{0}%", value);
                if (value > 100)
                {
                    var events = EventAggregator.GetEvent<NavigateEvent>();
                    events.Publish(new NavigateEventArgs() { Region = RegionNames.MainRegion, Name = CoontrolNameBase.MainContentView });
                    events.Publish(new NavigateEventArgs() { Region = RegionNames.MainContentRegion, Name = CoontrolNameBase.CurrentDialView });
                    m_DoubleTimer.Change(int.MaxValue, int.MaxValue);
                }
                TVisibility = m_CurrentProgress >= 50 ? Visibility.Hidden : (m_CurrentProgress >= 2 ? Visibility.Visible : Visibility.Hidden);
                AVisibility = m_CurrentProgress >= 50 ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public StartingViewModel()
        {
            ViewDispaly = new DelegateCommand(ViewDispalyMethod);
            if (EventAggregator == null)
            {
                //EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            }

            TVisibility = Visibility.Hidden;
            AVisibility = Visibility.Hidden;
            CurrentProgress = 0;

            m_DoubleTimer = new Timer(state =>
            {
                CurrentProgress++;
            }, null, int.MaxValue, int.MaxValue);

            if (EventAggregator != null)
            {
                EventAggregator.GetEvent<NavigateEvent>().Subscribe(args =>
                {
                    RegionManager.RequestNavigate(args.Region, args.Name);
                }, ThreadOption.UIThread);
            }
        }

        private void ViewDispalyMethod()
        {
            m_DoubleTimer.Change(0, 50);
        }



        public Visibility TVisibility
        {
            get { return m_Visibility; }
            set
            {
                if (value == m_Visibility)
                {
                    return;
                }
                m_Visibility = value;
                RaisePropertyChanged(() => TVisibility);
            }
        }

        public Visibility AVisibility
        {
            get { return m_AVisibility; }
            set
            {
                if (value == m_AVisibility)
                {
                    return;
                }
                m_AVisibility = value;
                RaisePropertyChanged(() => AVisibility);
            }
        }

        public string LoadText
        {
            get { return m_LoadText; }
            set
            {
                if (value == m_LoadText)
                {
                    return;
                }
                m_LoadText = value;
                RaisePropertyChanged(() => LoadText);
            }
        }

        public ICommand ViewDispaly { get; private set; }

        public override void Clear()
        {
            base.Clear();
            CurrentProgress = 0;
        }
    }
}