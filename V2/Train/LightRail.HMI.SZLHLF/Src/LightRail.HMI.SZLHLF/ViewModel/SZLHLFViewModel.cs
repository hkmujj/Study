using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Controller;
using LightRail.HMI.SZLHLF.Model;

namespace LightRail.HMI.SZLHLF.ViewModel
{
    [Export]
    public class SZLHLFViewModel : ModelBase
    {
        [ImportingConstructor]
        public SZLHLFViewModel(SZLHLFController controller, SZLHLFModel model,
            TrainInfoViewModel trainInfoViewModel, OtherViewModel otherViewModel, SecondLevelViewModel secondLevelViewModel, EventInfoViewModel eventInfoViewModel)
        {
            Controller = controller;
            Model = model;
            controller.ViewModel = this;
            TrainInfoViewModel = trainInfoViewModel;
            OtherViewModel = otherViewModel;
            SecondLevelViewModel = secondLevelViewModel;
            EventInfoViewModel = eventInfoViewModel;

            controller.Initalize();
            DoMainViewModel = this;

            OtherViewModel.Model.HMIBlack = true;
        }

        static public SZLHLFViewModel DoMainViewModel { private set; get; }

        public SZLHLFController Controller { private set; get; }

        public SZLHLFModel Model { private set; get; }
        
        public TrainInfoViewModel TrainInfoViewModel { private set; get; }

        public OtherViewModel OtherViewModel { private set; get; }


        public SecondLevelViewModel SecondLevelViewModel { private set; get; }

        public EventInfoViewModel EventInfoViewModel { private set; get; }


    }
}