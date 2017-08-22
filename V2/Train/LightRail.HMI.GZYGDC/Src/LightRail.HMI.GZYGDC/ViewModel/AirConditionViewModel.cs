using System;
using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.Controller;
using LightRail.HMI.GZYGDC.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.ViewModel
{
    [Export]
    public class AirConditionViewModel : NotificationObject
    {
        [ImportingConstructor]
        public AirConditionViewModel(AirConditionController controller, AirConditionModel model)
        {
            Controller = controller;

            Controller.ViewModel = new Lazy<AirConditionViewModel>(() => this);

            Model = model;

            Controller.Initalize();

            Instance = this;
        }

        public static AirConditionViewModel Instance { private set; get; }

        public AirConditionController Controller { private set; get; }

        public AirConditionModel Model { private set; get; }
    }
}