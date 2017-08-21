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
    public class CarEmergBrakeRelayState1UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarEmergBrakeRelayState1UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.Brake.BrakePage1.Value.EmergBrakeRelayState1).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.PreUnkownIndex));
            ;

            foreach (var it in d)
            {
                it.State = GetElectricRelayState(args, it);
            }
        }

        private ElectricRelayState GetElectricRelayState(CommunicationDataChangedArgs args, CarItem<ElectricRelayState, CarEmergBrakeRelayConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.PreActivedIndex))
            {
                return ElectricRelayState.Actived;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.PreUnactiveIndex))
            {
                return ElectricRelayState.Unactive;
            }
            return ElectricRelayState.Unkown;
        }
    }
}