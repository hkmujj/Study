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
    public class CarNormalBrake1UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarNormalBrake1UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.MainViewPage.NormalBrake1).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.PreFaultIndex));

            foreach (var it in d)
            {
                it.State = GetNormalBrakeState(args, it);
            }
        }

        private NormalBrakeState GetNormalBrakeState(CommunicationDataChangedArgs args, CarItem<NormalBrakeState, CarNormalBrakeConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.PreFaultIndex))
            {
                return NormalBrakeState.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.PreApplyIndex))
            {
                return NormalBrakeState.Apply;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.PreCutOffIndex))
            {
                return NormalBrakeState.CutOff;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.PrelaseIndex))
            {
                return NormalBrakeState.Relase;
            }
            return NormalBrakeState.Unkown;
        }
    }
}