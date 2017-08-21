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
    public class CarGroundConnectStateUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarGroundConnectStateUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.TowPage.Value.GroundConnectState).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.GroundIndex));
            ;

            foreach (var it in d)
            {
                it.State = GetGroundConnectState(args, it);
            }
        }

        private GroundConnectState GetGroundConnectState(CommunicationDataChangedArgs args, CarItem<GroundConnectState, CarGroundConnectConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.GroundIndex))
            {
                return GroundConnectState.Ground;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.PantographIndex))
            {
                return GroundConnectState.Pantograph;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.PowerIndex))
            {
                return GroundConnectState.Power;
            }

            return GroundConnectState.Unkown;
        }
    }
}