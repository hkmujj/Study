using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Config.TrainStatus;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;
using Subway.TCMS.LanZhou.Model.Domain.Constant.TrainStatus;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdataDataProviders.TrainStatusUpdataDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    class CarAirBrakeAvailableStatus2UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirBrakeAvailableStatus2UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainStatusDatas.TrainStatusBrakeViewLazy.Value.AirBrakeAvailableStatus2).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.AirBrake2AvailableNotavailable));
            ;

            foreach (var it in d)
            {
                it.State = GetGroundConnectState(args, it);
            }
        }

        private AirBrakeAvailableStatus GetGroundConnectState(CommunicationDataChangedArgs args, CarItem<AirBrakeAvailableStatus, AirBrakeAvailableStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AirBrake2AvailableNormal))
            {
                return AirBrakeAvailableStatus.AirBrakeAvailableNormal;
            }
            return AirBrakeAvailableStatus.AirBrakeAvailableNotavailable;
        }
    }
}
