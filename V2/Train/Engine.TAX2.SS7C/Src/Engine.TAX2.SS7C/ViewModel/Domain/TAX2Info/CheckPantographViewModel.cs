using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller.Domain.TAX2;
using Engine.TAX2.SS7C.Model.Domain.TrainState.TAX;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain.TAX2Info
{
    [Export]
    public class CheckPantographViewModel : NotificationObject
    {
        [ImportingConstructor]
        public CheckPantographViewModel(CheckPantographModel model, CheckPantographContrller contrller)
        {
            Model = model;
            Contrller = contrller;
            contrller.ViewModel = this;
            contrller.Initalize();
        }

        public CheckPantographModel Model { get; private set; }

        public CheckPantographContrller Contrller { get; private set; }
    }
}