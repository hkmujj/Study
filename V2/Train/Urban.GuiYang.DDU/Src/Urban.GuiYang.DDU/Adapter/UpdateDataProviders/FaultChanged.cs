using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    internal class FaultChanged : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public FaultChanged(IEventAggregator eventAggregator, DomainViewModel viewModel) : base(eventAggregator, viewModel)
        {

        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            foreach (var arg in args.ChangedBools.NewValue)
            {
                ViewModel.FaultViewModel.Model.FaultManager.InfoChanged(arg.Key, arg.Value);
            }

        }
    }
}