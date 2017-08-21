using System;
using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.Controller;
using LightRail.HMI.GZYGDC.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.ViewModel
{
    [Export]
    public class RunningViewModel : NotificationObject
    {
        [ImportingConstructor]
        public RunningViewModel(RunningController controller, RunningModel model)
        {
            Controller = controller;
            //Controller.ViewModel = Lazy<RunningViewModel>(this);

            Controller.ViewModel = new Lazy<RunningViewModel>(() => this);

            Model = model;

            Controller.Initalize();
        }


        public RunningController Controller { private set; get; }

        public RunningModel Model { private set; get; }
    }
}