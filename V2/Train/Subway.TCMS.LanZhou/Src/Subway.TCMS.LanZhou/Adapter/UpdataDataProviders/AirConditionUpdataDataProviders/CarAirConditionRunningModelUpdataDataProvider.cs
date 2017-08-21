using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdataDataProviders.AirConditionUpdataDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    public class CarAirConditionRunningModelUpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirConditionRunningModelUpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.AirConditionControl.AirConditionStatusLazy.Value.AirConditionRunningStatus).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.AirConditionUnknow));
            
            foreach (var it in d)
            {
                it.State = GetGroundConnectState(args, it);
            }
        }

        private AirConditionRunningModel GetGroundConnectState(CommunicationDataChangedArgs args, CarItem<AirConditionRunningModel, CarAirConditionRunningModelConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AirConditionLocalControl))
            {
                return AirConditionRunningModel.LocalControl;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AirConditionAirout))
            {
                return AirConditionRunningModel.Airout;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AirConditionAutoCold))
            {
                return AirConditionRunningModel.AutoCold;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AirConditionShutdown))
            {
                return AirConditionRunningModel.EmergencyShutdown;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AirConditionStrongwind))
            {
                return AirConditionRunningModel.Strongwind;
            }
            return AirConditionRunningModel.Unknow;
        }
    }
}
