using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Config.TrainStatus;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdataDataProviders.TrainStatusUpdataDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    class CarEmergencyBrakeAvailableStatus1UpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarEmergencyBrakeAvailableStatus1UpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainStatusDatas.TrainStatusBrakeViewLazy.Value.EmergencyBrakeAvailableStatus1).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.EmergencyBrake1AvailableNotavailable));
            ;

            foreach (var it in d)
            {
                it.State = GetGroundConnectState(args, it);
            }
        }

        private EmergencyBrakeAvailableStatus GetGroundConnectState(CommunicationDataChangedArgs args, CarItem<EmergencyBrakeAvailableStatus, EmergencyBrakeAvailableStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.EmergencyBrake1AvailableNormal))
            {
                return EmergencyBrakeAvailableStatus.EmergencyBrakeAvailableNormal;
            }
            return EmergencyBrakeAvailableStatus.EmergencyBrakeAvailableNotavailable;
        }
    }
}
