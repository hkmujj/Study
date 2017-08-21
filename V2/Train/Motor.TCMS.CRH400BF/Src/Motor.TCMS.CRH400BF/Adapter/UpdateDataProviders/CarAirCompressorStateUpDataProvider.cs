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
    public class CarAirCompressorStateUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirCompressorStateUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d =
                TrainModel.CarCollection.Select(s => s.EquipmentCutOffPage.Value.AirCompressorState)
                    .Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Unknow));
            ;

            foreach (var it in d)
            {

                it.State = GetAirCompressorState(args, it);
            }
        }

        private AirCompressorState GetAirCompressorState(CommunicationDataChangedArgs args,
            CarItem<AirCompressorState, CarAirCompressorConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CutOff ))
            {
                return AirCompressorState.CutOff;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Fault ))
            {
                return AirCompressorState.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Unknow ))
            {
                return AirCompressorState.Unknow;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.NotRun))
            {
                return AirCompressorState.NotRun;
            }

            return AirCompressorState.Run;
        }
    }
}
