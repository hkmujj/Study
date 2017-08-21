using System;
using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.TCMS.CRH400BF.Model;
using Motor.TCMS.CRH400BF.Model.Train;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Controller
{
    [Export]
    public class OtherController : ControllerBase<TrainViewModel>
    {
        protected OtherModel Model { get { return ViewModel.Model.OtherModel; } }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            GlobalTimer.Instance.Tick500MS += OnUpdateCurrentTime;
        }

        private void OnUpdateCurrentTime(object sender, EventArgs e)
        {
            Model.NowInBF = DateTime.Now;
        }
    }
}
