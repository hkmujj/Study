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
    class CarOvervoltageAbsorptionDeviceUpdateProvides : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarOvervoltageAbsorptionDeviceUpdateProvides(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainStatusDatas.TrainStatusTowViewLazy.Value.OvervoltageStatus).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.OvervoltageRunning));
            ;

            foreach (var it in d)
            {
                it.State = GetGroundConnectState(args, it);
            }
        }

        private OvervoltageStatus GetGroundConnectState(CommunicationDataChangedArgs args, CarItem<OvervoltageStatus, OvervoltageAbsorptionDeviceConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.OvervoltageStop))
            {
                return OvervoltageStatus.OvervoltageStop;
            }
            return OvervoltageStatus.OvervoltageRunning;
        }
    }
}

