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
    public class CarParkingBrakeUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarParkingBrakeUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.MainViewPage.ParkingBrake).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnkownIndex));

            foreach (var it in doors)
            {
                it.State = GetParkingBrake(args, it);
            }
        }

        private ParkingBrakeState GetParkingBrake(CommunicationDataChangedArgs args, CarItem<ParkingBrakeState, CarParkingBrakeConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.ApplyIndex))
            {
                return ParkingBrakeState.Apply;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CutOffIndex))
            {
                return ParkingBrakeState.CutOff;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.FaultIndex))
            {
                return ParkingBrakeState.Fault;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.RelaseIndex))
            {
                return ParkingBrakeState.Relase;
            }
            return ParkingBrakeState.Unkown;
        }
    }
}