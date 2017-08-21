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
    class CarAirConditionVentilator2UpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirConditionVentilator2UpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.AirConditionControl.AirConditionStatusLazy.Value.AirConditionVentilator2Status).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.VentilatorStop2));
            
            foreach (var it in d)
            {
                it.State = GetGroundConnectState(args, it);
            }
        }

        private AirConditionRunningStatus GetGroundConnectState(CommunicationDataChangedArgs args, CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.VentilatorRunning2))
            {
                return AirConditionRunningStatus.Running;
            }
            return AirConditionRunningStatus.Stop;
        }
    }
}
