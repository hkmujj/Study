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
    public class CarGroup1BackAirValveUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarGroup1BackAirValveUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.AirCondition.AirConditionPage2.Value.Group1BackAirValve1).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Valve1UnkownIndex));
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.AirCondition.AirConditionPage2.Value.Group1BackAirValve2).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Valve2UnkownIndex));
            foreach (var it in doors)
            {
                it.State= GetBackAirValveState1(args,it);
            }
            foreach (var it in d)
            {
                it.State = GetBackAirValveState2(args, it);
            }
        }

        private BackAirValveState GetBackAirValveState1(CommunicationDataChangedArgs args, CarItem<BackAirValveState, CarGroup1BackAirValveConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve1ClosedIndex))
            {
                return BackAirValveState.Closed;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve1FaultIndex))
            {
                return BackAirValveState.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve1OpenedIndex))
            {
                return BackAirValveState.Opened;
            }
            return BackAirValveState.Unkown; 
        }

        private BackAirValveState GetBackAirValveState2(CommunicationDataChangedArgs args, CarItem<BackAirValveState, CarGroup1BackAirValveConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve2ClosedIndex))
            {
                return BackAirValveState.Closed;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve2FaultIndex))
            {
                return BackAirValveState.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Valve2OpenedIndex))
            {
                return BackAirValveState.Opened;
            }
            return BackAirValveState.Unkown;
        }

    }
}