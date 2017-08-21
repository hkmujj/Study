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
    public class CarAirCompreeStateUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirCompreeStateUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.Brake.BrakePage1.Value.AirCompreeState).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnkownIndex));
            ;

            foreach (var it in d)
            {
                it.State = GetAirCompreeState(args, it);
            }
        }

        private AirCompreeState GetAirCompreeState(CommunicationDataChangedArgs args, CarItem<AirCompreeState, CarAirCompreeConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.FaultIndex))
            {
                return AirCompreeState.Fault;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.RunningIndex))
            {
                return AirCompreeState.Running;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.StopedIndex))
            {
                return AirCompreeState.Stoped;
            }
            return AirCompreeState.Unkown;
        }
    }
}