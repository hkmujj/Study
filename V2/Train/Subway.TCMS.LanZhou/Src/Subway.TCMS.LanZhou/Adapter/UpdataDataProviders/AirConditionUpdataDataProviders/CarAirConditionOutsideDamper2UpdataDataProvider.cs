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
    class CarAirConditionOutsideDamper2UpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirConditionOutsideDamper2UpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.AirConditionControl.AirConditionStatusLazy.Value.AirConditionOutsideDamper2Status).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.OutsideDamper2Unknow));
            
            foreach (var it in d)
            {
                it.State = GetGroundConnectState(args, it);
            }
        }

        private AirConditionOutsideDamper GetGroundConnectState(CommunicationDataChangedArgs args, CarItem<AirConditionOutsideDamper, CarAirConditionOutsideDamperStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.OutsideDamper2Closed))
            {
                return AirConditionOutsideDamper.FullyClosed;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.OutsideDamper2Half))
            {
                return AirConditionOutsideDamper.Half;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.OutsideDamper2SeventyFive))
            {
                return AirConditionOutsideDamper.SeventyFive;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.OutsideDamper2Totally))
            {
                return AirConditionOutsideDamper.Totally;
            }
            return AirConditionOutsideDamper.Unkonw;
        }
    }
}
