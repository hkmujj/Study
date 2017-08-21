using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Event;
using Engine.TCMS.HXD3.Model.Interface;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class TestRunViewModel : NotificationObject
    {
        private float m_RecordSecond;
        private float m_WalkDistance;
        private float m_Accelerated;
        private readonly IEventAggregator m_EventAggregator;
        private bool m_IsTesting;

        public DelegateCommand<string> StartupTestRunCommand { private set; get; }

        [ImportingConstructor]
        public TestRunViewModel(IEventAggregator eventAggregator)
        {
            m_EventAggregator = eventAggregator;
            StartupTestRunCommand = new DelegateCommand<string>(OnStartupTestRun);
        }


        private void OnStartupTestRun(string obj)
        {
            m_EventAggregator.GetEvent<StartupTestRunEvent>().Publish(new StartupTestRunEvent.EventArgs(obj == "true"));
        }

        public bool IsTesting
        {
            get { return m_IsTesting; }
            set
            {
                if (value == m_IsTesting)
                {
                    return;
                }

                m_IsTesting = value;
                RaisePropertyChanged(() => IsTesting);
            }
        }

        public float Accelerated
        {
            set
            {
                if (value.Equals(m_Accelerated))
                {
                    return;
                }

                m_Accelerated = value;
                RaisePropertyChanged(() => Accelerated);
            }
            get { return m_Accelerated; }
        }

        public float WalkDistance
        {
            set
            {
                if (value.Equals(m_WalkDistance))
                {
                    return;
                }

                m_WalkDistance = value;
                RaisePropertyChanged(() => WalkDistance);
            }
            get { return m_WalkDistance; }
        }

        public float RecordSecond
        {
            set
            {
                if (value.Equals(m_RecordSecond))
                {
                    return;
                }

                m_RecordSecond = value;
                RaisePropertyChanged(() => RecordSecond);
            }
            get { return m_RecordSecond; }
        }
    }
}