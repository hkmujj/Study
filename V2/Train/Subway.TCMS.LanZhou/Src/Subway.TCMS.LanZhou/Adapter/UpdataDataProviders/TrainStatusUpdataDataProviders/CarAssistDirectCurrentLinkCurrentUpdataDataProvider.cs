using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Config.TrainStatus;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdataDataProviders.TrainStatusUpdataDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    class CarDirectCurrentLinkCurrentUpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarDirectCurrentLinkCurrentUpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainStatusDatas.TrainStatusAsisstViewLazy.Value.DirectCurrentLinkCurrentAssistStatus).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnkownIndex));

            foreach (var it in doors)
            {
                it.State.GeneralState = Getbool(args, it);
                it.State.Value = DataService.ReadService.GetInFloatOf(it.ItemConfig.ValueIndex);
            }
        }

        private bool Getbool(CommunicationDataChangedArgs args, CarItem<ValidateFloatItem, TrainAssistDirectCurrentLinkCurrentConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnkownIndex))
            {
                return true;
            }
            return false;
        }
    }
}
