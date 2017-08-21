using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    class CarBrakePressure2UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarBrakePressure2UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors = ViewModel.TrainViewModel.Model.CarModels.Select(s => s.RunningViewData.RunningViewFloatDataLazy.Value.BrakePressure2).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Branch2UnkownIndex));

            foreach (var it in doors)
            {
                it.State.GeneralState = Getbool(args, it);
                it.State.Value = DataService.ReadService.GetInFloatOf(it.ItemConfig.Branch2ValueIndex);
            }
        }

        private bool Getbool(CommunicationDataChangedArgs args, CarItem<ValidateFloatItem, CarBrakePressureConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Branch2UnkownIndex))
            {
                return true;
            }
            return false;
        }
    }
}
