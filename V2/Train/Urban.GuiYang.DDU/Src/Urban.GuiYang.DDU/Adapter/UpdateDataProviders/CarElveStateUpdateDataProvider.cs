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
    public class CarElveStateUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarElveStateUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.TowPage.Value.ElveState).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.ValueIndex));

            foreach (var it in doors)
            {
                it.State.GeneralState= Getbool(args,it);
                it.State.Value=DataService.ReadService.GetInFloatOf(it.ItemConfig.ValueIndex);
            }
        }

        private bool Getbool(CommunicationDataChangedArgs args, CarItem<ValidateFloatItem, CarElveConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnkownIndex))
            {
                return true;
            }
            return false;
        }
    }
}