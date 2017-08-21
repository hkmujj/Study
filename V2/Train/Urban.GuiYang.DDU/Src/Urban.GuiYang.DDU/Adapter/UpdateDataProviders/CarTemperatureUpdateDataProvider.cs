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
    public class CarTemperatureUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarTemperatureUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
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
                            && !string.IsNullOrWhiteSpace(w.CarTemperature.ItemConfig.UnkownIndex))
                    .Select(s => s.CarTemperature);


            foreach (var it in doors)
            {
                it.State.GeneralState = Getbool(args, it);
                it.State.Value1 = DataService.ReadService.GetInFloatOf(it.ItemConfig.InsideTemIndex);
                it.State.Value2 = DataService.ReadService.GetInFloatOf(it.ItemConfig.OutTemIndex);
            }
        }

        private bool Getbool(CommunicationDataChangedArgs args, CarItem<ValidateValueStateItem, CarTemperatureConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnkownIndex))
            {
                return true;
            }
            return false;
        }
    }
}