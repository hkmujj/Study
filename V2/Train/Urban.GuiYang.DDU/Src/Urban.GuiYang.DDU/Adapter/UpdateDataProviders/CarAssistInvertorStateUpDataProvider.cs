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
    public class CarAssistInvertorStateUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAssistInvertorStateUpDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.MainViewPage.AssistInvertorState).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.ExtendedPowerOffIndex));

            foreach (var it in d)
            {
                it.State = GetAssistInvertorState(args, it);
            }
        }
        private AssistInvertorState GetAssistInvertorState(CommunicationDataChangedArgs args, CarItem<AssistInvertorState, CarAssistInvertorConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.ExtendedPowerOffIndex))
            {
                return AssistInvertorState.ExtendedPowerOff;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.ExtendedPowerOnIndex))
            {
                return AssistInvertorState.ExtendedPowerOn;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.ExtendedPowerUnkownIndex))
            {
                return AssistInvertorState.ExtendedPowerUnkown;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.FaultIndex))
            {
                return AssistInvertorState.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.NormalIndex))
            {
                return AssistInvertorState.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.WorkingIndex))
            {
                return AssistInvertorState.Working;
            }
            return AssistInvertorState.Unkown;
        }
    }
}