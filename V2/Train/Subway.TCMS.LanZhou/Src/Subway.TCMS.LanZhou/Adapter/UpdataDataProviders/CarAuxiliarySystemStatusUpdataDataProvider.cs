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

namespace Subway.TCMS.LanZhou.Adapter.UpdataDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    class CarAuxiliarySystemStatusUpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAuxiliarySystemStatusUpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainBodyViewData.AuxiliarySystemStatus).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.AuxiliarySystemStatusUnknow));

            foreach (var it in d)
            {
                it.State = GetNormalBrakeState(args, it);
            }
        }

        private AuxiliarySystemStatus GetNormalBrakeState(CommunicationDataChangedArgs args, CarItem<AuxiliarySystemStatus, AuxiliarySystemStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AuxiliarySystemStatusNormal))
            {
                return AuxiliarySystemStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AuxiliarySystemStatusRunning))
            {
                return AuxiliarySystemStatus.Running;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AuxiliarySystemStatusFailure))
            {
                return AuxiliarySystemStatus.Failure;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AuxiliarySystemStatusResection))
            {
                return AuxiliarySystemStatus.Resection;
            }
            return AuxiliarySystemStatus.Unknow;
        }
    }
}
