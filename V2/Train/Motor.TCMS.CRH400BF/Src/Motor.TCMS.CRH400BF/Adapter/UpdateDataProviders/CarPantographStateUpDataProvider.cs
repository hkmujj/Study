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
    public class CarPantographStateUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarPantographStateUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d =
                TrainModel.CarCollection.Select(s => s.EquipmentCutOffPage.Value.PantographState)
                    .Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnKnow));
            ;

            foreach (var it in d)
            {

                it.State = GetPantographState(args, it);
            }
        }

        private PantographState GetPantographState(CommunicationDataChangedArgs args,
            CarItem<PantographState, CarPantographConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Up))
            {
                return PantographState.Uped;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Down))
            {
                return PantographState.Downed;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnKnow))
            {
                return PantographState.UnKnow;
            }
            return PantographState.CutOff;
        }
    }
}
