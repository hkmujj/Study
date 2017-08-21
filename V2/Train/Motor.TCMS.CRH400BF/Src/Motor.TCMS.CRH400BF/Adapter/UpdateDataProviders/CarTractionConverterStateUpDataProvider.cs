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
    public class CarTractionConverterStateUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarTractionConverterStateUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }
        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d =
                TrainModel.CarCollection.Select(s => s.EquipmentCutOffPage.Value.TractionConverterState)
                    .Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnKnow));
            ;

            foreach (var it in d)
            {

                it.State = GetTractionConverterState(args, it);
            }
        }
        private TractionConverterState GetTractionConverterState(CommunicationDataChangedArgs args,
            CarItem<TractionConverterState, CarTractionConverterConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Fault))
            {
                return TractionConverterState.Fault;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.NotRun))
            {
                return TractionConverterState.NotRun;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CutOff))
            {
                return TractionConverterState.CutOff ;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnKnow ))
            {
                return TractionConverterState.Unknow;
            }

            return TractionConverterState.Run;
        }

    }
}
