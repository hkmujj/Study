﻿using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    public class CarAirCompressorUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirCompressorUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainBodyViewData.AirCompressorStatus).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.AirCompressorUnknow));

            foreach (var it in d)
            {
                it.State = GetNormalBrakeState(args, it);
            }
        }

        private AirCompressorStatus GetNormalBrakeState(CommunicationDataChangedArgs args, CarItem<AirCompressorStatus, AirCompressorConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AirCompressorFault))
            {
                return AirCompressorStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AirCompressorRunning))
            {
                return AirCompressorStatus.Running;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AirCompressorStop))
            {
                return AirCompressorStatus.Stop;
            }
            return AirCompressorStatus.Unknow;
        }
    }
}