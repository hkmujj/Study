using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller;
using Engine.TAX2.SS7C.Model;
using Engine.TAX2.SS7C.ViewModel.Domain;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel
{
    [Export]
    public class SS7CViewModel : NotificationObject
    {
        [ImportingConstructor]
        public SS7CViewModel(SS7CController controller, SS7CModel model, HardwareBtnViewModel hardwareBtnViewModel, TrainInfoViewModel trainInfoViewModel, OtherViewModel otherViewModel, TrainStateViewModel trainStateViewModel, SecondLevelViewModel secondLevelViewModel)
        {
            Controller = controller;
            Model = model;
            controller.ViewModel = this;
            HardwareBtnViewModel = hardwareBtnViewModel;
            TrainInfoViewModel = trainInfoViewModel;
            OtherViewModel = otherViewModel;
            TrainStateViewModel = trainStateViewModel;
            SecondLevelViewModel = secondLevelViewModel;

            controller.Initalize();
        }


        public SS7CController Controller { private set; get; }

        public SS7CModel Model { private set; get; }

        public HardwareBtnViewModel HardwareBtnViewModel { private set; get; }

        public TrainInfoViewModel TrainInfoViewModel { private set; get; }

        public OtherViewModel OtherViewModel { get; private set; }

        public TrainStateViewModel TrainStateViewModel { get; private set; }

        public SecondLevelViewModel SecondLevelViewModel { get; private set; }

      
    }
}