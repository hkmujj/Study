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
    class CarAirConditionCondensingMachine1UpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirConditionCondensingMachine1UpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.AirConditionControl.AirConditionStatusLazy.Value.AirConditionCondensing1Status).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.CondensingStop1));
            
            foreach (var it in d)
            {
                it.State = GetGroundConnectState(args, it);
            }
        }

        private AirConditionRunningStatus GetGroundConnectState(CommunicationDataChangedArgs args, CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CondensingRunning1))
            {
                return AirConditionRunningStatus.Running;
            }
            return AirConditionRunningStatus.Stop;
        }
    }
}
