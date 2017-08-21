using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Model.Domain;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain
{
    [Export]
    public class TrainInfoViewModel :NotificationObject
    {
        [ImportingConstructor]
        public TrainInfoViewModel(TrainInfoModel model)
        {
            Model = model;
        }

        public TrainInfoModel Model { private set; get; }

      
    }
}
