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
    public class CarMotoTemperatureState4UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarMotoTemperatureState4UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.TowPage.Value.MotoTemperatureState4).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Moto4UnkownIndex));

            foreach (var it in doors)
            {
                it.State.GeneralState= Getbool(args,it);
                it.State.Value=DataService.ReadService.GetInFloatOf(it.ItemConfig.Moto4ValueIndex);
            }
        }

        private bool Getbool(CommunicationDataChangedArgs args, CarItem<ValidateFloatItem, CarMotoTemperatureConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Moto4UnkownIndex))
            {
                return true;
            }
            return false;
        }
    }
}