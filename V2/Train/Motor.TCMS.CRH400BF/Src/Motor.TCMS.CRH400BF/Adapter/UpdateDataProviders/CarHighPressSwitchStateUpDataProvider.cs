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
    public class CarHighPressSwitchStateUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarHighPressSwitchStateUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }
        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d =
                TrainModel.CarCollection.Select(s => s.EquipmentCutOffPage.Value.HighPressSwitchState)
                    .Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnKnow));
            foreach (var it in d)
            {

                it.State = GetHighPressSwitchState(args, it);
            }
        }
        private HighPressSwitchState GetHighPressSwitchState(CommunicationDataChangedArgs args,
            CarItem<HighPressSwitchState, CarHighPressSwitchConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Open))
            {
                return HighPressSwitchState.Open;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Close))
            {
                return HighPressSwitchState.Close;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnKnow ))
            {
                return HighPressSwitchState.Unknow;
            }
            return HighPressSwitchState.CutOff;
        }

    }

}
