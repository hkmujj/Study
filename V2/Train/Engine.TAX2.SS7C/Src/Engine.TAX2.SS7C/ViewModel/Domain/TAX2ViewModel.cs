using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller.Domain.TAX2;
using Engine.TAX2.SS7C.Model.Domain.TrainState.TAX;
using Engine.TAX2.SS7C.ViewModel.Domain.TAX2Info;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain
{
    [Export]
    public class TAX2ViewModel : NotificationObject
    {
        [ImportingConstructor]
        public TAX2ViewModel(CheckPantographViewModel checkPantographViewModel, CheckSonaViewModel checkSonaViewModel, CheckSoundViewModel checkSoundViewModel, CheckTMISViewModel checkTMISViewModel, CheckTrackViewModel checkTrackViewModel, CheckTrainControlViewModel checkTrainControlViewModel, CheckWalkViewModel checkWalkViewModel, CheckDMISViewModel checkDMISViewModel, TAXModel model, TAXController controller)
        {
            CheckPantographViewModel = checkPantographViewModel;
            CheckSonaViewModel = checkSonaViewModel;
            CheckSoundViewModel = checkSoundViewModel;
            CheckTMISViewModel = checkTMISViewModel;
            CheckTrackViewModel = checkTrackViewModel;
            CheckTrainControlViewModel = checkTrainControlViewModel;
            CheckWalkViewModel = checkWalkViewModel;
            CheckDMISViewModel = checkDMISViewModel;
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public TAXModel Model { get; private set; }

        public TAXController Controller { get; private set; }

        public CheckPantographViewModel CheckPantographViewModel { get; private set; }

        public CheckSonaViewModel CheckSonaViewModel { get; private set; }

        public CheckSoundViewModel CheckSoundViewModel { get; private set; }

        public CheckTMISViewModel CheckTMISViewModel { get; private set; }

        public CheckTrackViewModel CheckTrackViewModel { get; private set; }

        public CheckTrainControlViewModel CheckTrainControlViewModel { get; private set; }

        public CheckWalkViewModel CheckWalkViewModel { get; private set; }

        public CheckDMISViewModel CheckDMISViewModel { private set; get; }

  
    }
}