using Motor.HMI.CRH380BG.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using Motor.HMI.CRH380BG.Resources.Keys;
using Motor.HMI.CRH380BG.Model;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    [Export]
    class ConfirmTrainConfigActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {
            stateViewModel.Model.ViewModel.DomainViewModel.Domain.Model.MainData.TrainConfigVisible = false;
            stateViewModel.Model.ViewModel.DomainViewModel.Domain.Model.MainData.ConfirmConfigVisible = true;
            CreateTimer1();
            CreateTimer2();
        }

        public void CreateTimer1()
        {
            var time = new DispatcherTimer();
            time.Interval = new TimeSpan(0, 0, 3);
            time.Tick += (sender, args) =>
            {
                time.Stop();
                ViewManager.Value.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_开始);
                ViewManager.Value.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_开始);
                ViewModel.Value.Domain.Model.MainData.ConfirmConfigVisible = false;
            };
            time.Start();
        }

        public void CreateTimer2()
        {
            var time = new DispatcherTimer();
            time.Interval = new TimeSpan(0, 0, 8);
            time.Tick += (sender, args) =>
            {
                time.Stop();
                ViewManager.Value.MainViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root);
                ViewManager.Value.ReserveViewModel.StateViewModel.Controller.NavigateTo(StateKeys.Root_制动状态);
            };
            time.Start();
        }
    }
}
