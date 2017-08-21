using System;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Event;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Engine.TPX21F.HXN5B.ViewModel.Domain;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TPX21F.HXN5B.Controller.Domain
{
    [Export]
    public class DomainController : ControllerBase<DomainViewModel>
    {
        private IEventAggregator m_EventAggregator;
        public const int VigilantSecondCount = 10;

        private CountDownState m_CountDownState;

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            m_CountDownState = CountDownState.Stopped;
            m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            GlobalTimer.Instance.Tick1S += TimerOnTick1S;
        }

        private void TimerOnTick1S(object sender, EventArgs eventArgs)
        {
            if (ViewModel.Model.VigilantCountDownFlag)
            {
                ViewModel.Model.CurrentVigilantCountDown = Math.Max(ViewModel.Model.CurrentVigilantCountDown - 1, 0);
                if (ViewModel.Model.CurrentVigilantCountDown == 0 && m_CountDownState != CountDownState.Stopped)
                {
                    m_CountDownState = CountDownState.Stopped;
                    m_EventAggregator.GetEvent<VigilantCountDownEvent>()
                        .Publish(new VigilantCountDownEvent.Args(CountDownState.Stopped));
                }
            }
        }

        public void StartVigilantCountDown()
        {
            if (!ViewModel.Model.VigilantCountDownFlag)
            {
                ViewModel.Model.CurrentVigilantCountDown = VigilantSecondCount;
                ViewModel.Model.VigilantCountDownFlag = true;
                m_CountDownState = CountDownState.Started;
                m_EventAggregator.GetEvent<VigilantCountDownEvent>().Publish(new VigilantCountDownEvent.Args(CountDownState.Started));
            }
        }

        public void StopVigilantCountDown()
        {
            ViewModel.Model.VigilantCountDownFlag = false;
            m_CountDownState = CountDownState.Stopped;
        }
    }
}