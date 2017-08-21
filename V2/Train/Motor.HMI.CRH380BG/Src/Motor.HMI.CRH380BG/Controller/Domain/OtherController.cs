using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Model.Interface;
using Motor.HMI.CRH380BG.ViewModel.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace Motor.HMI.CRH380BG.Controller.Domain
{
    [Export]
    [Export(typeof(IResetSupport))]
    public class OtherController : ControllerBase<OtherViewModel>, IResetSupport
    {
        private DispatcherTimer m_UpdateCurrentTimeTimer;

        [ImportingConstructor]
        public OtherController()
        {
        }

        public override void Initalize()
        {
            m_UpdateCurrentTimeTimer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 500), DispatcherPriority.Normal,
                OnUpdateCurrentTime, Application.Current.Dispatcher);
            Reset();
        }

        private void OnUpdateCurrentTime(object sender, EventArgs eventArgs)
        {
            ViewModel.Model.SimTime = DateTime.Now;
        }

        public void Reset()
        {
            ViewModel.Model.AdjustSpan = TimeSpan.Zero;
        }
    }
}
