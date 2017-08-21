using System;
using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.Controller;
using LightRail.HMI.GZYGDC.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.ViewModel
{
    [Export]
    public class EmergencyBroadcastViewModel : NotificationObject
    {
        [ImportingConstructor]
        public EmergencyBroadcastViewModel(EmergencyBroadcastController controller, EmergencyBroadcastModel model)
        {
            Controller = controller;
            //Controller.ViewModel = Lazy<EmergencyBroadcastViewModel>(this);

            Controller.ViewModel = new Lazy<EmergencyBroadcastViewModel>(() => this);

            Model = model;

            Controller.Initalize();

            Instance = this;
        }

        public static EmergencyBroadcastViewModel Instance { private set; get; }

        public EmergencyBroadcastController Controller { private set; get; }

        public EmergencyBroadcastModel Model { private set; get; }
    }
}