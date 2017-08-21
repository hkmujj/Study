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
    public class CarMainWindLowPresByPassUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarMainWindLowPresByPassUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.MainPageByPass1.Value.MainWindLowPresByPass).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnkownIndex));
            ;

            foreach (var it in d)
            {
                it.State = GetMainWindLowPresByPass(args, it);
            }
        }

        private SwitchState GetMainWindLowPresByPass(CommunicationDataChangedArgs args, CarItem<SwitchState, CarMainWindLowPresByPassConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.OnIndex))
            {
                return SwitchState.On;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.OffIndex))
            {
                return SwitchState.Off;
            }

            return SwitchState.Unkown;
        }
    }
}