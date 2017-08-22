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
    public class CarBatteryChargerStateUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarBatteryChargerStateUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.AssistPage.Value.BatteryChargerState).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnkownIndex));
            ;

            foreach (var it in d)
            {
                it.State.BatteryChargerState = GetBatteryChargerState(args, it);
                it.State.GeneralState = GetBool(args, it);
                it.State.Value = DataService.ReadService.GetInFloatOf(it.ItemConfig.ValueIndex);
            }
        }

        private bool GetBool(CommunicationDataChangedArgs args, CarItem<ValidateFloatStateItem, CarBatteryChargerStateConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnkownIndex))
            {
                return true;
            }
            return false;
        }

        private BatteryChargerState GetBatteryChargerState(CommunicationDataChangedArgs args, CarItem<ValidateFloatStateItem, CarBatteryChargerStateConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.FaultIndex))
            {
                return BatteryChargerState.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.WorkingIndex))
            {
                return BatteryChargerState.Working;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.NormalIndex))
            {
                return BatteryChargerState.Normal;
            }
            return BatteryChargerState.Unknow;
        }
    }
}