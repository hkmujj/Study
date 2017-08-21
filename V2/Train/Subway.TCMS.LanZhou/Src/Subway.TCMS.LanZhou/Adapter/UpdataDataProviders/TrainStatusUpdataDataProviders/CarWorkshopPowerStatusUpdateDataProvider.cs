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
    class CarWorkshopPowerStatusUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarWorkshopPowerStatusUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainStatusDatas.TrainStatusTowViewLazy.Value.WorkshopPowerStatus).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.WorkshopPowerUnConnect));

            foreach (var it in d)
            {
                it.State = GetNormalBrakeState(args, it);
            }
        }

        private WorkshopPowerStatus GetNormalBrakeState(CommunicationDataChangedArgs args, CarItem<WorkshopPowerStatus, WorkshopPowerStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.WorkshopPowerConnect))
            {
                return WorkshopPowerStatus.WorkshopPowerConnect;
            }
            return WorkshopPowerStatus.WorkshopPowerUnConnect;
        }
    }
}
