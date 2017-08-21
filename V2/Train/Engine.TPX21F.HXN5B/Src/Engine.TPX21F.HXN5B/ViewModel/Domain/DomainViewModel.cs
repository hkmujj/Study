using System;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Controller.Domain;
using Engine.TPX21F.HXN5B.Model.Domain;
using Engine.TPX21F.HXN5B.ViewModel.Domain.Fault;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.ViewModel.Domain
{
    [Export]
    public class DomainViewModel : NotificationObject
    {
        [ImportingConstructor]
        public DomainViewModel(DomainModel model, DomainController controller,
            FaultManagerViewModel faultManagerViewModel, MonitorViewModel monitorViewModel,
            SoftSwitchViewModel softSwitchViewModel, BrakeSysViewModel brakeSysViewModel, TestViewModel testViewModel, TrainSettingViewModel trainSettingViewModel, MaintenanceViewModel maintenanceViewModel, Lazy<HXN5BViewModel> parent)
        {
            Model = model;
            Controller = controller;
            FaultManagerViewModel = faultManagerViewModel;
            MonitorViewModel = monitorViewModel;
            SoftSwitchViewModel = softSwitchViewModel;
            BrakeSysViewModel = brakeSysViewModel;
            TestViewModel = testViewModel;
            TrainSettingViewModel = trainSettingViewModel;
            MaintenanceViewModel = maintenanceViewModel;
            Parent = parent;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public Lazy<HXN5BViewModel> Parent { get; private set; }

        public DomainModel Model { private set; get; }

        public DomainController Controller { private set; get; }

        public FaultManagerViewModel FaultManagerViewModel { private set; get; }

        public MonitorViewModel MonitorViewModel { private set; get; }

        public SoftSwitchViewModel SoftSwitchViewModel { private set; get; }

        public BrakeSysViewModel BrakeSysViewModel { private set; get; }

        public TestViewModel TestViewModel { private set; get; }

        public TrainSettingViewModel TrainSettingViewModel { get; private set; }

        public MaintenanceViewModel MaintenanceViewModel { get; private set; }
    }
}