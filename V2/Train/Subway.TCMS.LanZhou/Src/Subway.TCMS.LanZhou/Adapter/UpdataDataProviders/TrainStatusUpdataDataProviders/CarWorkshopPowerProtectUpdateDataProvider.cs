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
    class CarWorkshopPowerProtectUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarWorkshopPowerProtectUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainStatusDatas.TrainStatusTowViewLazy.Value.WorkshopPowerProtectStatus).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.WorkshopPowerProtectClosed));

            foreach (var it in d)
            {
                it.State = GetNormalBrakeState(args, it);
            }
        }

        private OpenOrClosedStatus GetNormalBrakeState(CommunicationDataChangedArgs args, CarItem<OpenOrClosedStatus, WorkshopPowerProtectStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.WorkshopPowerProtectOpen))
            {
                return OpenOrClosedStatus.Open;
            }
            return OpenOrClosedStatus.Closed;
        }
    }
}
