using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller.Domain;
using Engine.TAX2.SS7C.Model.Domain.TrainState;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain
{
    [Export]
    public class TrainStateViewModel : NotificationObject
    {
        [ImportingConstructor]
        public TrainStateViewModel(TrainStateModel model, TAX2ViewModel tax2ViewModel, TrainStateController controller)
        {
            Model = model;
            TAX2ViewModel = tax2ViewModel;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public TrainStateModel Model { get; private set; }

        public TrainStateController Controller { get; private set; }

        public TAX2ViewModel TAX2ViewModel { get; private set; }
    }
}