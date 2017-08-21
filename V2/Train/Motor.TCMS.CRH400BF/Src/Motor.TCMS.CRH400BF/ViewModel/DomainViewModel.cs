using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Motor.TCMS.CRH400BF.Controller;
using Motor.TCMS.CRH400BF.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.TCMS.CRH400BF.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DomainViewModel : NotificationObject
    {
        [ImportingConstructor]
        public DomainViewModel(HardwareBtnViewModel hardwareBtnViewModel
            , StateViewModel mainStateViewModel,
             DomainModel model, DomainController controller, TrainViewModel trainViewModel)
        {
            HardwareBtnViewModel = hardwareBtnViewModel;
            StateViewModel = mainStateViewModel;
            Model = model;
            Controller = controller;

            controller.ViewModel = this;
            mainStateViewModel.DomainViewModel = this;
            hardwareBtnViewModel.DomainViewModel = this;
            TrainViewModel = trainViewModel;
        }

        public ViewLocation ViewLocation { get; set; }

        public IRegionManager RegionManager { get; set; }

        public DomainController Controller { get; private set; }

        public DomainModel Model { get; private set; }

        public StateViewModel StateViewModel { get; private set; }

        public HardwareBtnViewModel HardwareBtnViewModel { private set; get; }

        public TrainViewModel TrainViewModel { private set; get; }
        public void Initalize()
        {
            Controller.Initalize();
            StateViewModel.Controller.Initalize();
            HardwareBtnViewModel.Initalize(ViewLocation);
            TrainViewModel.Controller.Initalize();
        }
    }
}