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

namespace Subway.TCMS.LanZhou.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    class CarParkingBrakeUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarParkingBrakeUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainBodyViewData.ParkingBrakeStatus).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.ParkingBrakeUnknow));

            foreach (var it in d)
            {
                it.State = GetNormalBrakeState(args, it);
            }
        }

        private TrainParkingBrakeStatus GetNormalBrakeState(CommunicationDataChangedArgs args, CarItem<TrainParkingBrakeStatus, ParkingBrakeConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.ParkingBrakeApplication))
            {
                return TrainParkingBrakeStatus.ParkingBrakeApplication;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.ParkingBrakeRelease))
            {
                return TrainParkingBrakeStatus.ParkingBrakeRelease;
            }
            return TrainParkingBrakeStatus.ParkingBrakeUnknow;
        }
    }
}
