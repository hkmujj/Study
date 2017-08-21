using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller.Domain.TAX2;
using Engine.TAX2.SS7C.Model.Domain.TrainState.TAX;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain.TAX2Info
{
    [Export]
    public class CheckTrackViewModel : NotificationObject
    {
        [ImportingConstructor]
        public CheckTrackViewModel(CheckTrackModel model, CheckTrackContrller contrller)
        {
            Model = model;
            Contrller = contrller;
            contrller.ViewModel = this;
            contrller.Initalize();
        }

        public CheckTrackModel Model { get; private set; }

        public CheckTrackContrller Contrller { get; private set; }
    }
}