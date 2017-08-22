using System;
using System.ComponentModel.Composition;
using DevExpress.Xpf.Editors.Helpers;
using LightRail.HMI.GZYGDC.Controller;
using LightRail.HMI.GZYGDC.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.ViewModel
{
    [Export]
    public class DomainViewModel : NotificationObject
    {
        [ImportingConstructor]
        public DomainViewModel(DomainController controller, DomainModel model, HardwareBtnViewModel hardwareBtnViewModel, 
            TitleViewModel titleViewModel, RunningViewModel runningViewModel, EmergencyBroadcastViewModel emergencyBroadcastViewModel,
            EventInfoViewModel eventInfoViewModel, AirConditionViewModel airConditionViewModel)
        {
            Controller = controller;
            //Controller.ViewModel = Lazy<DomainViewModel>(this);

            Controller.ViewModel = new Lazy<DomainViewModel>(() => this);

            Model = model;

            HardwareBtnViewModel = hardwareBtnViewModel;

            TitleViewModel = titleViewModel;
            RunningViewModel = runningViewModel;
            EmergencyBroadcastViewModel = emergencyBroadcastViewModel;
            EventInfoViewModel = eventInfoViewModel;
            AirConditionViewModel = airConditionViewModel;

            Controller.Initalize();
        }


        public DomainController Controller { private set; get; }

        public DomainModel Model { private set; get; }

        public HardwareBtnViewModel HardwareBtnViewModel { private set; get; }


        public TitleViewModel TitleViewModel { private set; get; }

        public RunningViewModel RunningViewModel { private set; get; }

        public EmergencyBroadcastViewModel EmergencyBroadcastViewModel { private set; get; }

        public EventInfoViewModel EventInfoViewModel { private set; get; }


        public AirConditionViewModel AirConditionViewModel { private set; get; }

    }
}