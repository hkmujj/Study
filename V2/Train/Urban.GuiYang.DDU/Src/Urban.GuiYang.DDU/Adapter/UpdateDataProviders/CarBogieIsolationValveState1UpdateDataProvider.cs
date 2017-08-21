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
    public class CarBogieIsolationValveState1UpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarBogieIsolationValveState1UpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarCollection.Select(s => s.Brake.BrakePage1.Value.BogieIsolationValveState1).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.PreUnkownIndex));
            ;

            foreach (var it in d)
            {
                it.State = GetBogieIsolationValveState(args, it);
            }
        }

        private IsolationValveState GetBogieIsolationValveState(CommunicationDataChangedArgs args, CarItem<IsolationValveState, CarBogieIsolationValveConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.PreIsolationIndex))
            {
                return IsolationValveState.Isolation;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.PreUnisolationIndex))
            {
                return IsolationValveState.Unisolation;
            }
            return IsolationValveState.Unkown;
        }
    }
}