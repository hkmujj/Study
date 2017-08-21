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
    public class CarControlModelUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarControlModelUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var doors =
                ViewModel.TrainViewModel.Model.CarCollection.Select(
                    s => s.AirCondition.CurrentAirConditionPage1)
                    .Where(
                        w =>
                            w != null && w.CarTemperature != null
                            && !string.IsNullOrWhiteSpace(w.ControlModel.ItemConfig.UnkownIndex))
                    .Select(s => s.ControlModel);


            foreach (var it in doors)
            {
                it.State = GetControlModel(args, it);
            }
        }

        private ControlState GetControlModel(CommunicationDataChangedArgs args, CarItem<ControlState, CarControlModelConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.FocusControlIndex))
            {
                return ControlState.FocusControl;
            }
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.SelfControlIndex))
            {
                return ControlState.SelfControl;
            }
            return ControlState.Unkown;
        }


    }
}