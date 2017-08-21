using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Motor.TCMS.CRH400BF.Config;
using Motor.TCMS.CRH400BF.Extension;
using Motor.TCMS.CRH400BF.Model.Constant;
using Motor.TCMS.CRH400BF.Model.Train;
using Motor.TCMS.CRH400BF.Model.Train.CartPart;

namespace Motor.TCMS.CRH400BF.Adapter.UpdateDataProviders
{

    [Export(typeof(IUpdateDataProvider))]
    public class CarAirBrakeUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirBrakeUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = TrainModel.CarCollection.Select(s => s.BrakePage.Value.AirBrake).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnKnow));
            foreach (var it in d)
            {
                it.State.AirBrakeEffectState = GetAirBrakeEffectState(args, it);
                it.State.AirBrakeState = GetAirBrakeState(args, it);
            }
        }

        private AirBrakeEffectState GetAirBrakeEffectState(CommunicationDataChangedArgs args, CarItem<ValidateAirBrakeItem, CarAirBrakeConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnEffective))
            {
                return AirBrakeEffectState.UnEffective;
            }

            return AirBrakeEffectState.Effective;
        }

        private AirBrakeState GetAirBrakeState(CommunicationDataChangedArgs args, CarItem<ValidateAirBrakeItem, CarAirBrakeConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Remission))
            {
                return AirBrakeState.Remission;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Isolation))
            {
                return AirBrakeState.Isolation;
            }
            return AirBrakeState.Run;
        }
    }
}
