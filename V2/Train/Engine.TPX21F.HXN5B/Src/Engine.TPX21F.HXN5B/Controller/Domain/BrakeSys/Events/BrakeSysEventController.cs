using System;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Event;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Engine.TPX21F.HXN5B.ViewModel.Domain.BrakeSys;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TPX21F.HXN5B.Controller.Domain.BrakeSys.Events
{
    [Export]
    public class BrakeSysEventController : ControllerBase<BrakeSysEventViewModel>
    {
        public const int EmergerBrakeSecondCount = 60;

        private readonly IEventAggregator m_EventAggregator;

        [ImportingConstructor]
        public BrakeSysEventController(IEventAggregator eventAggregator)
        {
            m_EventAggregator = eventAggregator;
        }

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            GlobalTimer.Instance.Tick1S += TimerOnTick1S;
        }

        private void TimerOnTick1S(object sender, EventArgs eventArgs)
        {
            if (ViewModel.Model.EmergerBrakeFlag)
            {
                ViewModel.Model.CurrentEmergerBrakeCount = Math.Max(0, ViewModel.Model.CurrentEmergerBrakeCount - 1);
                if (ViewModel.Model.CurrentEmergerBrakeCount == 0)
                {
                    m_EventAggregator.GetEvent<EmergerBrakeCountDownEvent>().Publish(new EmergerBrakeCountDownEvent.Args(CountDownState.Stopped));
                }
            }
        }

        public void StartEmergerCountDown()
        {
            if (!ViewModel.Model.EmergerBrakeFlag)
            {
                ViewModel.Model.CurrentEmergerBrakeCount = EmergerBrakeSecondCount;
                ViewModel.Model.EmergerBrakeFlag = true;
                m_EventAggregator.GetEvent<EmergerBrakeCountDownEvent>().Publish(new EmergerBrakeCountDownEvent.Args(CountDownState.Started));
            }
        }

        public void StopEmergerCountDown()
        {
            ViewModel.Model.EmergerBrakeFlag = false;
        }
    }
}