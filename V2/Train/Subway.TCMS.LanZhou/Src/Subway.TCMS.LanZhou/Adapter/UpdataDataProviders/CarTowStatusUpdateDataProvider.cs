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
    class CarTowStatusUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarTowStatusUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainBodyViewData.TowState).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.TractionSystemUnknow));

            foreach (var it in d)
            {
                it.State = GetNormalBrakeState(args, it);
            }
        }

        private TowState GetNormalBrakeState(CommunicationDataChangedArgs args, CarItem<TowState, TowSystemConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.TractionSystemNormal))
            {
                return TowState.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.TractionSystemRunning))
            {
                return TowState.Running;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.TractionSystemFualt))
            {
                return TowState.Fualt;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.TractionSystemResection))
            {
                return TowState.Resection;
            }     
            return TowState.Unknow;
        }
    }
}
