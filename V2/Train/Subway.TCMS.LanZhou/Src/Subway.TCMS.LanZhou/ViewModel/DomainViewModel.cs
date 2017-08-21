using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Controller;
using Subway.TCMS.LanZhou.Model;

namespace Subway.TCMS.LanZhou.ViewModel
{
    [Export]
    public class DomainViewModel : NotificationObject
    {
        [ImportingConstructor]
        public DomainViewModel(DomainController controller, DomainModel model, HardwareBtnViewModel hardwareBtnViewModel, OtherViewModel otherViewModel, TrainViewModel trainViewModel, PISViewModel pisViewModel, FalutViewModel falutViewModel, ISendInterface sendInterface, AireConditionViewModel aireConditionViewModel)
        {
            Controller = controller;
            Model = model;
            //HardwareBtnViewModel = hardwareBtnViewModel;
            OtherViewModel = otherViewModel;
            TrainViewModel = trainViewModel;
            PISViewModel = pisViewModel;
            FalutViewModel = falutViewModel;
            SendInterface = sendInterface;
            AireConditionViewModel = aireConditionViewModel;
            aireConditionViewModel.Initalize(this);
            controller.ViewModel = this;
        }

        public DomainController Controller { private set; get; }

        public DomainModel Model { private set; get; }

        public ISendInterface SendInterface { get; private set; }

       // public HardwareBtnViewModel HardwareBtnViewModel { private set; get; }

        public OtherViewModel OtherViewModel { get; private set; }

        public TrainViewModel TrainViewModel { get; private set; }

        public PISViewModel PISViewModel { get; private set; }

        public FalutViewModel FalutViewModel { get; private set; }

        public AireConditionViewModel AireConditionViewModel { get; private set; }
    }
}