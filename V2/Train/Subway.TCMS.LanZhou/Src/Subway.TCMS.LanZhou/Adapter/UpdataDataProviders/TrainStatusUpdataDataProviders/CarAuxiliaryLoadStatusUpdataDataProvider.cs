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
    class CarAuxiliaryLoadStatusUpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAuxiliaryLoadStatusUpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainStatusDatas.TrainStatusAsisstViewLazy.Value.AuxiliaryLoadStatus).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.AuxiliaryLoadOpen));
            ;

            foreach (var it in d)
            {
                it.State = GetGroundConnectState(args, it);
            }
        }

        private OpenOrClosedStatus GetGroundConnectState(CommunicationDataChangedArgs args, CarItem<OpenOrClosedStatus, AuxiliaryLoadStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.AuxiliaryLoadClosed))
            {
                return OpenOrClosedStatus.Closed;
            }
            return OpenOrClosedStatus.Open;
        }
    }
}
