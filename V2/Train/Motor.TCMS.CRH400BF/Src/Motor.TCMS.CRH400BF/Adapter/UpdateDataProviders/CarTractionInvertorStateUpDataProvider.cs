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
    public class CarTractionInvertorStateUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarTractionInvertorStateUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }
        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d =
                TrainModel.CarCollection.Select(s => s.EquipmentCutOffPage.Value.TractionInvertorState)
                    .Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnKnow));
            ;

            foreach (var it in d)
            {

                it.State = GetTractionInvertorState(args, it);
            }
        }
        private TractionInvertorState GetTractionInvertorState(CommunicationDataChangedArgs args,
            CarItem<TractionInvertorState, CarTractionInvertorConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Fault))
            {
                return TractionInvertorState.Fault;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.NotRun))
            {
                return TractionInvertorState.NotRun;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CutOff))
            {
                return TractionInvertorState.CutOff;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnKnow))
            {
                return TractionInvertorState.Unknow;
            }

            return TractionInvertorState.Run;
        }

    }
}
