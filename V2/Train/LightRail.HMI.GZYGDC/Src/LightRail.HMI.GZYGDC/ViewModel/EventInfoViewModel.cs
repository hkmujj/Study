using System;
using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.Controller;
using LightRail.HMI.GZYGDC.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.ViewModel
{
    [Export]
    public class EventInfoViewModel : NotificationObject
    {
        [ImportingConstructor]
        public EventInfoViewModel(EventInfoController controller, EventInfoModel model)
        {
            Controller = controller;
            //Controller.ViewModel = Lazy<EventInfoViewModel>(this);

            Controller.ViewModel = new Lazy<EventInfoViewModel>(() => this);

            Model = model;

            Controller.Initalize();
        }


        public EventInfoController Controller { private set; get; }

        public EventInfoModel Model { private set; get; }
    }
}