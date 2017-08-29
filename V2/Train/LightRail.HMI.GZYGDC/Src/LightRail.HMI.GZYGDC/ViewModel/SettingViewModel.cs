using System;
using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.Controller;
using LightRail.HMI.GZYGDC.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.ViewModel
{
    [Export]
    public class SettingViewModel : NotificationObject
    {
        [ImportingConstructor]
        public SettingViewModel(SettingController controller, SettingModel model)
        {
            Controller = controller;

            Controller.ViewModel = new Lazy<SettingViewModel>(() => this);

            Model = model;

            Controller.Initalize();

            Instance = this;
        }

        public static SettingViewModel Instance { private set; get; }

        public SettingController Controller { private set; get; }

        public SettingModel Model { private set; get; }
    }
}