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
    class CarBrakeStatus2UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarBrakeStatus2UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainBodyViewData.Brake2Status).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.CommonBrakeUnknow2));

            foreach (var it in d)
            {
                it.State = GetNormalBrakeState(args, it);
            }
        }

        private BrakeStatus GetNormalBrakeState(CommunicationDataChangedArgs args, CarItem<BrakeStatus, BrakeStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CommonBrakeFault2))
            {
                return BrakeStatus.CommonBrakeFault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CommonBrakeApplication2))
            {
                return BrakeStatus.CommonBrakeApplication;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CommonBrakeIsolation2))
            {
                return BrakeStatus.CommonBrakeIsolation;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CommonBrakeRelease2))
            {
                return BrakeStatus.CommonBrakeRelease;
            }
            return BrakeStatus.CommonBrakeUnknow;
        }
    }
}
