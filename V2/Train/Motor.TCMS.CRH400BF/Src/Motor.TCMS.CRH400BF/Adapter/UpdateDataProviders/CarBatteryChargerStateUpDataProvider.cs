using System.ComponentModel.Composition;
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
    public class CarBatteryChargerStateUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarBatteryChargerStateUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }
        //public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        //{
        //    var d1 =
        //        ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.EquipmentCutOffPage.Value.BatteryChargerState1)
        //            .Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.BatteryCharger1UnKnow));
        //    ;

        //    var d2 =
        //       ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.EquipmentCutOffPage.Value.BatteryChargerState2)
        //           .Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.BatteryCharger2UnKnow));
        //    ;

        //    foreach (var it in d1)
        //    {

        //        it.State = GetBatteryChargerState1(args, it);
        //    }
        //    foreach (var it in d2)
        //    {

        //        it.State = GetBatteryChargerState2(args, it);
        //    }
        //}
        private BatteryChargerState GetBatteryChargerState1(CommunicationDataChangedArgs args,
            CarItem<BatteryChargerState, CarBatteryChargerConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.BatteryCharger1CutOff ))
            {
                return BatteryChargerState.CutOff;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.BatteryCharger1Fault ))
            {
                return BatteryChargerState.Fault;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.BatteryCharger1NotRun))
            {
                return BatteryChargerState.NotRun;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.BatteryCharger1UnKnow ))
            {
                return BatteryChargerState.UnKnow ;
            }

            return BatteryChargerState.Run;
        }
        private BatteryChargerState GetBatteryChargerState2(CommunicationDataChangedArgs args,
           CarItem<BatteryChargerState, CarBatteryChargerConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.BatteryCharger2CutOff))
            {
                return BatteryChargerState.CutOff;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.BatteryCharger2Fault))
            {
                return BatteryChargerState.Fault;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.BatteryCharger2NotRun))
            {
                return BatteryChargerState.NotRun;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.BatteryCharger2UnKnow ))
            {
                return BatteryChargerState.UnKnow;
            }

            return BatteryChargerState.Run;
        }
    }
}
