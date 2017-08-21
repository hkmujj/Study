using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace Engine.TAX2.SS7C.Adapter.Detail
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateTrainStateProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public UpdateTrainStateProvider(IEventAggregator eventAggregator, SS7CViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            //var d = ViewModel.TrainStateViewModel.Model;
        }
    }
}