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
    public class CarGroup2NewAirValveUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarGroup2NewAirValveUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.AirCondition.AirConditionPage2.Value.Group2NewAirValve1).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Valve1UnkownIndex));
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.AirCondition.AirConditionPage2.Value.Group2NewAirValve2).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Valve2UnkownIndex));
            foreach (var it in doors)
            {
                it.State= GetNewAirValveState1(args,it);
            }
            foreach (var it in d)
            {
                it.State = GetNewAirValveState2(args, it);
            }
        }

        private NewAirValveState GetNewAirValveState1(CommunicationDataChangedArgs args, CarItem<NewAirValveState, CarGroup2NewAirValveConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve1ClosedIndex))
            {
                return NewAirValveState.Closed;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve1FaultIndex))
            {
                return NewAirValveState.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve1OneIndex))
            {
                return NewAirValveState.One;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve1TwoIndex))
            {
                return NewAirValveState.Two;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve1ThreeIndex))
            {
                return NewAirValveState.Three;
            }
             return NewAirValveState.Unkown; 
        }

        private NewAirValveState GetNewAirValveState2(CommunicationDataChangedArgs args, CarItem<NewAirValveState, CarGroup2NewAirValveConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve2ClosedIndex))
            {
                return NewAirValveState.Closed;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve2FaultIndex))
            {
                return NewAirValveState.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve2OneIndex))
            {
                return NewAirValveState.One;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve2TwoIndex))
            {
                return NewAirValveState.Two;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve2ThreeIndex))
            {
                return NewAirValveState.Three;
            }
            return NewAirValveState.Unkown;
        }
    }
}