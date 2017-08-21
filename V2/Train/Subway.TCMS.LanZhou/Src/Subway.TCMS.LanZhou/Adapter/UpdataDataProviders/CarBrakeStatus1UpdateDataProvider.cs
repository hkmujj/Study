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
    class CarBrakeStatus1UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarBrakeStatus1UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainBodyViewData.Brake1Status).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.CommonBrakeUnknow1));

            foreach (var it in d)
            {
                it.State = GetNormalBrakeState(args, it);
            }
        }

        private BrakeStatus GetNormalBrakeState(CommunicationDataChangedArgs args, CarItem<BrakeStatus, BrakeStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CommonBrakeFault1))
            {
                return BrakeStatus.CommonBrakeFault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CommonBrakeApplication1))
            {
                return BrakeStatus.CommonBrakeApplication;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CommonBrakeIsolation1))
            {
                return BrakeStatus.CommonBrakeIsolation;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CommonBrakeRelease1))
            {
                return BrakeStatus.CommonBrakeRelease;
            }
            return BrakeStatus.CommonBrakeUnknow;
        }
    }
}
