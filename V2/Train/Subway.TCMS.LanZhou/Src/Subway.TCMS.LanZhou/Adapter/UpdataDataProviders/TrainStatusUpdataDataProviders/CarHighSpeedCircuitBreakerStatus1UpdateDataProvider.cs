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
    class CarHighSpeedCircuitBreakerStatus1UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarHighSpeedCircuitBreakerStatus1UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainStatusDatas.TrainStatusTowViewLazy.Value.HighSpeedCircuitBreaker1Status).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.HighSpeedCircuitBreakerOpen));

            foreach (var it in d)
            {
                it.State = GetNormalBrakeState(args, it);
            }
        }

        private OpenOrClosedStatus GetNormalBrakeState(CommunicationDataChangedArgs args, CarItem<OpenOrClosedStatus, HighSpeedCircuitBreakerStatus1Config> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.HighSpeedCircuitBreakerClosed))
            {
                return OpenOrClosedStatus.Closed;
            }
            return OpenOrClosedStatus.Open;
        }
    }
}
