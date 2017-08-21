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

    [Export(typeof (IUpdateDataProvider))]
    public class CarMainBreakerStateUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarMainBreakerStateUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }
        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d =
                TrainModel.CarCollection.Select(s => s.EquipmentCutOffPage.Value.MainBreakerState)
                    .Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnKnow));
            ;

            foreach (var it in d)
            {

                it.State = GetMainBrakerState(args, it);
            }
        }
        private MainBreakerState GetMainBrakerState(CommunicationDataChangedArgs args,
            CarItem<MainBreakerState, CarMainBreakerConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Open ))
            {
                return MainBreakerState.Open ;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Close ))
            {
                return MainBreakerState.Close;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnKnow))
            {
                return MainBreakerState.UnKnow;
            }
            return MainBreakerState.CutOff;
        }

    }

}
