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
    class CarBogieResectionStatus2UpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarBogieResectionStatus2UpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainStatusDatas.TrainStatusBrakeViewLazy.Value.BogieResectionStatus2).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Bogie2ResectionNormal));
            ;

            foreach (var it in d)
            {
                it.State = GetGroundConnectState(args, it);
            }
        }

        private BogieResectionStatus GetGroundConnectState(CommunicationDataChangedArgs args, CarItem<BogieResectionStatus, BogieResectionStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Bogie2Resection))
            {
                return BogieResectionStatus.BogieResection;
            }
            return BogieResectionStatus.BogieResectionNormal;
        }
    }
}
