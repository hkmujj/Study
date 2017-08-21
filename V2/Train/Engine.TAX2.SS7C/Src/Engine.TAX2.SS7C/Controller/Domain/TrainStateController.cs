using System;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using Engine.TAX2.SS7C.Events;
using Engine.TAX2.SS7C.Model;
using Engine.TAX2.SS7C.ViewModel.Domain;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TAX2.SS7C.Controller.Domain
{
    [Export]
    public class TrainStateController : ControllerBase<TrainStateViewModel>
    {
        [ImportingConstructor]
        public TrainStateController(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ResetRunningTimeEvent = EventAggregator.GetEvent<ResetRunningTimeEvent>();
        }

        public IEventAggregator EventAggregator { get; private set; }

        public ResetRunningTimeEvent ResetRunningTimeEvent { get; private set; }

        public static readonly TimeSpan m_TimeSpan1s = TimeSpan.FromSeconds(1);

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ResetRunningTimeEvent.Subscribe(OnResetRunningTime);
            GlobalTimer.Instance.Time1S += UpdateRunningTime;
        }

        private void UpdateRunningTime()
        {
            UpdateRunningTime(false);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void UpdateRunningTime(bool isReset)
        {
            if (isReset)
            {
                ViewModel.Model.RunningTime = TimeSpan.Zero;
            }
            else
            {
                ViewModel.Model.RunningTime+= m_TimeSpan1s;
            }
        }

        private void OnResetRunningTime(ResetRunningTimeEvent.Args args)
        {
            UpdateRunningTime(true);
        }
    }
}