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
    public class CarEmergBrakeRelayState2UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarEmergBrakeRelayState2UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.Brake.BrakePage1.Value.EmergBrakeRelayState2).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.BeUnkownIndex));
            ;

            foreach (var it in d)
            {
                it.State = GetElectricRelayState(args, it);
            }
        }

        private ElectricRelayState GetElectricRelayState(CommunicationDataChangedArgs args, CarItem<ElectricRelayState, CarEmergBrakeRelayConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.BeActivedIndex))
            {
                return ElectricRelayState.Actived;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.BeUnactiveIndex))
            {
                return ElectricRelayState.Unactive;
            }
            return ElectricRelayState.Unkown;
        }
    }
}