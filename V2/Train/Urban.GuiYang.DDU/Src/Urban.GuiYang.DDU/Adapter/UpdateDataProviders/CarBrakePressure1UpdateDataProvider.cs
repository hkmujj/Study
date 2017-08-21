using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model.Train.CarPart;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Adapter.UpdateDataProviders
{
    [Export(typeof (IUpdateDataProvider))]
    public class CarBrakePressure1UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarBrakePressure1UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.Brake.BrakePage2.Value.BrakePressure1).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Branch1UnkownIndex));

            foreach (var it in doors)
            {
                it.State.GeneralState = Getbool(args, it);
                it.State.Value = DataService.ReadService.GetInFloatOf(it.ItemConfig.Branch1ValueIndex);
            }
        }

        private bool Getbool(CommunicationDataChangedArgs args, CarItem<ValidateFloatItem, CarBrakePressureConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Branch1UnkownIndex))
            {
                return true;
            }
            return false;
        }
    }
}