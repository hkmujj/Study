using System;
using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.Controller;
using LightRail.HMI.GZYGDC.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.ViewModel
{
    [Export]
    public class BroadcastControlViewModel : NotificationObject
    {
        [ImportingConstructor]
        public BroadcastControlViewModel(BroadcastControlController controller, BroadcastControlModel model)
        {
            Controller = controller;

            Controller.ViewModel = new Lazy<BroadcastControlViewModel>(() => this);

            Model = model;

            Controller.Initalize();

            Instance = this;
        }

        public static BroadcastControlViewModel Instance { private set; get; }

        public BroadcastControlController Controller { private set; get; }

        public BroadcastControlModel Model { private set; get; }
    }
}