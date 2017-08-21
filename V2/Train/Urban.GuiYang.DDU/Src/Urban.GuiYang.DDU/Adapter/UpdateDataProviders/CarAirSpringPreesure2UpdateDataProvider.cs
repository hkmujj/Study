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
    public class CarAirSpringPreesure2UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAirSpringPreesure2UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.Brake.BrakePage2.Value.AirSpringPreesure2).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.Branch2UnkownIndex));

            foreach (var it in doors)
            {
                it.State.GeneralState= Getbool(args,it);
                it.State.Value=DataService.ReadService.GetInFloatOf(it.ItemConfig.Branch2ValueIndex);
            }
        }

        private bool Getbool(CommunicationDataChangedArgs args, CarItem<ValidateFloatItem, CarAirSpringPreesureConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Branch2UnkownIndex))
            {
                return true;
            }
            return false;
        }
    }
}