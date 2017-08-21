using System.ComponentModel.Composition;
using System.Diagnostics;
using Engine.TPX21F.HXN5B.Controller.Domain.TrainSetting;
using Engine.TPX21F.HXN5B.Model.Domain.TrainSetting;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.ViewModel.Domain
{
    [Export]
    public class TrainSettingViewModel :NotificationObject
    {
        [ImportingConstructor]
        [DebuggerStepThrough]
        public TrainSettingViewModel(TrainSettingModel model, TrainSettingController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public TrainSettingController Controller { get; private set; }

        public TrainSettingModel Model { get; private set; }
    }
}