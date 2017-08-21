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
    class CarAirSpringPreData1UpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirSpringPreData1UpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.TrainStatusDatas.TrainStatusBrakeViewLazy.Value.AirSpringPreData1).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Unkown1Index));

            foreach (var it in doors)
            {
                it.State.GeneralState = Getbool(args, it);
                it.State.Value = DataService.ReadService.GetInFloatOf(it.ItemConfig.Value1Index);
            }
        }

        private bool Getbool(CommunicationDataChangedArgs args, CarItem<ValidateFloatItem, AirSpringPreDataConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Unkown1Index))
            {
                return true;
            }
            return false;
        }
    }
}
