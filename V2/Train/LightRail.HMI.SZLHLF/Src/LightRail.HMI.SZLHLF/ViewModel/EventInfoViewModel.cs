using System;
using LightRail.HMI.SZLHLF.Controller;
using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Model.EventModel;

namespace LightRail.HMI.SZLHLF.ViewModel
{
    [Export]
    public class EventInfoViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public EventInfoViewModel(EventInfoController controller, EventInfoModel model)
        {
            Controller = controller;

            Controller.ViewModel = new Lazy<EventInfoViewModel>(() => this);

            Model = model;

            Controller.Initalize();
        }


        public EventInfoController Controller { private set; get; }

        public EventInfoModel Model { private set; get; }
    }
}
