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
    public class CarHightSwitchState1UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarHightSwitchState1UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.MainViewPage.HightSwitchState1).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.PreOnIndex));

            foreach (var it in d)
            {
                it.State = GetHightSwitchState(args, it);
            }
        }

        private SwitchState GetHightSwitchState(CommunicationDataChangedArgs args, CarItem<SwitchState, CarHightSwitchConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.PreOffIndex))
            {
                return SwitchState.Off;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.PreOnIndex))
            {
                return SwitchState.On;
            }
           
            return SwitchState.Unkown;
        }
    }
}