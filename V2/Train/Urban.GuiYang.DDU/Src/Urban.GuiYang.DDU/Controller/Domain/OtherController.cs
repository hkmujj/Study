using System;
using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Urban.GuiYang.DDU.Model;
using Urban.GuiYang.DDU.Model.Train;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Controller.Domain
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
            Model.NowInDDU = DateTime.Now;
        }
    }
}