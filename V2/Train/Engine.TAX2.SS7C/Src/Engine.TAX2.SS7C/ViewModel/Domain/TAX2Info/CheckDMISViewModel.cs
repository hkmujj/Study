using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller.Domain.TAX2;
using Engine.TAX2.SS7C.Model.Domain.TrainState.TAX;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain.TAX2Info
{
    [Export]
    public class CheckDMISViewModel : NotificationObject
    {
        [ImportingConstructor]
        public CheckDMISViewModel(CheckDMISModel model, CheckDMISContrller contrller)
        {
            Model = model;
            Contrller = contrller;
            contrller.ViewModel = this;
            contrller.Initalize();
        }

        public CheckDMISModel Model { get; private set; }

        public CheckDMISContrller Contrller { get; private set; }
    }
}