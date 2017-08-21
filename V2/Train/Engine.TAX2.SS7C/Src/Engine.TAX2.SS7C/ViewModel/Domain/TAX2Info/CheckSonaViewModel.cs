using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller.Domain.TAX2;
using Engine.TAX2.SS7C.Model.Domain.TrainState.TAX;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain.TAX2Info
{
    [Export]
    public class CheckSonaViewModel : NotificationObject
    {
        [ImportingConstructor]
        public CheckSonaViewModel(CheckSonaModel model, CheckSonaContrller contrller)
        {
            Model = model;
            Contrller = contrller;
            contrller.ViewModel = this;
            contrller.Initalize();
        }

        public CheckSonaModel Model { get; private set; }

        public CheckSonaContrller Contrller { get; private set; }
    }
}