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
    public class CarPackingBrakeIsolationValveStateUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarPackingBrakeIsolationValveStateUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.Brake.BrakePage1.Value.PackingBrakeIsolationValveState).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnkownIndex));
            ;

            foreach (var it in d)
            {
                it.State = GetPackingBrakeIsolationValveState(args, it);
            }
        }

        private IsolationValveState GetPackingBrakeIsolationValveState(CommunicationDataChangedArgs args, CarItem<IsolationValveState, CarPackingBrakeIsolationValveConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.IsolationIndex))
            {
                return IsolationValveState.Isolation;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnisolationIndex))
            {
                return IsolationValveState.Unisolation;
            }
            return IsolationValveState.Unkown;
        }
    }
}