using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Model.Train.CarPart;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Adapter.UpdateDataProviders
{
    [Export(typeof (IUpdateDataProvider))]
    public class CarAirConditionUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirConditionUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors =
                ViewModel.TrainViewModel.Model.CarCollection.Select(
                    s => s.AirCondition.CurrentAirConditionPage1).Where(w => w != null && w.AirCondition1 != null && !string.IsNullOrWhiteSpace(w.AirCondition1.ItemConfig.Group1UnkownIndex)).Select(s => s.AirCondition1);
            ;
            var d =
                 ViewModel.TrainViewModel.Model.CarCollection.Select(
                     s => s.AirCondition.CurrentAirConditionPage1).Where(w => w != null && w.AirCondition2 != null && !string.IsNullOrWhiteSpace(w.AirCondition2.ItemConfig.Group2UnkownIndex)).Select(s => s.AirCondition2);
            ;
            foreach (var it in doors)
            {
                it.State = GetAirCondition1State(args, it);
            }
            foreach (var it in d)
            {
                it.State = GetAirCondition2State(args, it);
            }
        }

        private AirConditionState GetAirCondition1State(CommunicationDataChangedArgs args, CarItem<AirConditionState, CarAirConditionConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group1AllWarmIndex))
            {
                return AirConditionState.AllWarm;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group1EmergencyWindIndex))
            {
                return AirConditionState.EmergencyWind;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group1FaultIndex))
            {
                return AirConditionState.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group1HalfWarmIndex))
            {
                return AirConditionState.HalfWarm;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group1MakeCoolIndex))
            {
                return AirConditionState.MakeCool;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group1NormalWindIndex))
            {
                return AirConditionState.NormalWind;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group1PreCoolIndex))
            {
                return AirConditionState.PreCool;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group1PreWarmIndex))
            {
                return AirConditionState.PreWarm;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group1StopedIndex))
            {
                return AirConditionState.Stoped;
            }

            return AirConditionState.Unkown;
        }

        private AirConditionState GetAirCondition2State(CommunicationDataChangedArgs args, CarItem<AirConditionState, CarAirConditionConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group2AllWarmIndex))
            {
                return AirConditionState.AllWarm;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group2EmergencyWindIndex))
            {
                return AirConditionState.EmergencyWind;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group2FaultIndex))
            {
                return AirConditionState.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group2HalfWarmIndex))
            {
                return AirConditionState.HalfWarm;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group2MakeCoolIndex))
            {
                return AirConditionState.MakeCool;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group2NormalWindIndex))
            {
                return AirConditionState.NormalWind;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group2PreCoolIndex))
            {
                return AirConditionState.PreCool;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group2PreWarmIndex))
            {
                return AirConditionState.PreWarm;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Group2StopedIndex))
            {
                return AirConditionState.Stoped;
            }

            return AirConditionState.Unkown;
        }

    }
}