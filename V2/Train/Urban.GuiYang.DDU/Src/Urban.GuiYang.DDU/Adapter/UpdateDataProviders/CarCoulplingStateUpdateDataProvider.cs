using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Model.Train.CarPart;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Adapter.UpdateDataProviders
{
    [Export(typeof (IUpdateDataProvider))]
    public class CarCoulplingStateUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarCoulplingStateUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.MainPageByPass2.Value.CoulplingState).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnkownIndex));
            ;

            foreach (var it in d)
            {
                it.State = GetCoulplingState(args, it);
            }
        }

        private CTSState GetCoulplingState(CommunicationDataChangedArgs args, CarItem<CTSState, CarCoulplingStateConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CTEIndex))
            {
                return CTSState.CTE;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.NormalIndex))
            {
                return CTSState.Normal;
            }

            return CTSState.Unkown;
        }
    }
}