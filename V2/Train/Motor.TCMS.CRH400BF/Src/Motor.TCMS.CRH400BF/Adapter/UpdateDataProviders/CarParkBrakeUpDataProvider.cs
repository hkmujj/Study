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
    public class CarParkBrakeUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarParkBrakeUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = TrainModel.CarCollection.Select(s => s.BrakePage.Value.ParkBrake).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnKnow));
            ;

            foreach (var it in d)
            {
                it.State.ParkBrakeEffectState = GetParkBrakeEffectState(args, it);
                it.State.ParkBrakeState = GetParkBrakeState(args, it);
            }
        }

        private ParkBrakeEffectState GetParkBrakeEffectState(CommunicationDataChangedArgs args, CarItem<ValidateParkBrakeItem, CarParkBrakeConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnEffective))
            {
                return ParkBrakeEffectState.UnEffective;
            }

            return ParkBrakeEffectState.Effective;
        }

        private ParkBrakeState GetParkBrakeState(CommunicationDataChangedArgs args, CarItem<ValidateParkBrakeItem, CarParkBrakeConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Remission))
            {
                return ParkBrakeState.Remission;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Isolation))
            {
                return ParkBrakeState.Isolation;
            }
            return ParkBrakeState.Run;
        }
    }
}
