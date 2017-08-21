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
    class CarIesContactorStatusUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarIesContactorStatusUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainStatusDatas.TrainStatusTowViewLazy.Value.IesContactorStatus).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.IesContactorUnknow));

            foreach (var it in d)
            {
                it.State = GetNormalBrakeState(args, it);
            }
        }

        private IesContactorStatus GetNormalBrakeState(CommunicationDataChangedArgs args, CarItem<IesContactorStatus, IesContactorStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.IesContactorEarthcircuit))
            {
                return IesContactorStatus.IesContactorEarthcircuit;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.IesContactorRunning))
            {
                return IesContactorStatus.IesContactorRunning;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.IesContactorWorkshoppower))
            {
                return IesContactorStatus.IesContactorWorkshoppower;
            }
            return IesContactorStatus.IesContactorUnknow;
        }
    }
}
