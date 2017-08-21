using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Model.Train.CarPart;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Adapter.UpdateDataProviders
{
    [Export(typeof (IUpdateDataProvider))]
    public class CarTractionInvertorStateUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarTractionInvertorStateUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.MainViewPage.TractionInvertorState).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.ApplyIndex));
            ;

            foreach (var it in d)
            {
                it.State = GetTractionInvertorState(args, it);
            }
        }

        private TractionInvertorState GetTractionInvertorState(CommunicationDataChangedArgs args, CarItem<TractionInvertorState, CarTowInvertorConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.ApplyIndex))
            {
                return TractionInvertorState.TowApply;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.EleBrakeIndex))
            {
                return TractionInvertorState.EleBrakeApply;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.FaultIndex))
            {
                return TractionInvertorState.Fault;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.NormalIndex))
            {
                return TractionInvertorState.Normal;
            }
            
            return TractionInvertorState.Unkown;
        }
    }
}