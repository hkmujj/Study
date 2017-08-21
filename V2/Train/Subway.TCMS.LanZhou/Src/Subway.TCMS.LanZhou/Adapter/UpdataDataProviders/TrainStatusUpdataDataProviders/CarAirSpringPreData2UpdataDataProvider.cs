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
    class CarAirSpringPreData2UpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirSpringPreData2UpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainStatusDatas.TrainStatusBrakeViewLazy.Value.AirSpringPreData2).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Unkown2Index));

            foreach (var it in doors)
            {
                it.State.GeneralState = Getbool(args, it);
                it.State.Value = DataService.ReadService.GetInFloatOf(it.ItemConfig.Value2Index);
            }
        }

        private bool Getbool(CommunicationDataChangedArgs args, CarItem<ValidateFloatItem, AirSpringPreDataConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Unkown2Index))
            {
                return true;
            }
            return false;
        }
    }
}
