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
    public class CarEleBrakeUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarEleBrakeUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = TrainModel.CarCollection.Select(s => s.BrakePage.Value.EleBrake).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnKnow));
            ;

            foreach (var it in d)
            {
                it.State.EleBrakeEffectState = GetEleBrakeEffectState(args, it);
                it.State.EleBrakeState = GetEleBrakeState(args, it);
            }
        }

        private EleBrakeEffectState GetEleBrakeEffectState(CommunicationDataChangedArgs args, CarItem<ValidateEleBrakeItem, CarEleBrakeConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnEffective))
            {
                return EleBrakeEffectState.UnEffective ;
            }

            return EleBrakeEffectState.Effective;
        }

        private EleBrakeState GetEleBrakeState(CommunicationDataChangedArgs args, CarItem<ValidateEleBrakeItem, CarEleBrakeConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.None))
            {
                return EleBrakeState.None;
            }

            return EleBrakeState.Run;
        }
    }
}
