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
    public class CarBcu1StateUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarBcu1StateUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d =
                TrainModel.CarCollection.Select(s => s.BrakeInfoPage.Value.Bcu1State)
                    .Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Unknow));
            ;

            foreach (var it in d)
            {

                it.State = GetBcu1State(args, it);
            }
        }

        private BrakeInfoCommonState GetBcu1State(CommunicationDataChangedArgs args, CarItem<BrakeInfoCommonState, CarBcu1StateConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.NotRun))
            {
                return BrakeInfoCommonState.NotRun;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnEffect))
            {
                return BrakeInfoCommonState.UnEffect;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Unknow))
            {
                return BrakeInfoCommonState.Unkown;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Unusual))
            {
                return BrakeInfoCommonState.Unusual;
            }

            return BrakeInfoCommonState.Run;
        }
    }
}
