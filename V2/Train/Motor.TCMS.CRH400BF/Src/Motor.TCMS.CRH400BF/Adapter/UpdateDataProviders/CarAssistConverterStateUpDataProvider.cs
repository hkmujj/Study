using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Motor.TCMS.CRH400BF.Config;
using Motor.TCMS.CRH400BF.Extension;
using Motor.TCMS.CRH400BF.Model.Constant;
using Motor.TCMS.CRH400BF.Model.Train;
using Motor.TCMS.CRH400BF.Model.Train.CartPart;

namespace Motor.TCMS.CRH400BF.Adapter.UpdateDataProviders
{
    
    [Export(typeof(IUpdateDataProvider))]
    public class CarAssistConverterStateUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarAssistConverterStateUpDataProvider(IEventAggregator eventAggregator, TrainModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }
        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d =
                TrainModel.CarCollection.Select(s => s.EquipmentCutOffPage.Value.AssistConverterState)
                    .Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.UnKnow));
            ;

            foreach (var it in d)
            {

                it.State = GetAssistConverterState(args, it);
            }
        }
        private AssistConverterState GetAssistConverterState(CommunicationDataChangedArgs args,
            CarItem<AssistConverterState, CarAssistConverterConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.Fault))
            {
                return AssistConverterState.Fault;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.NotRun))
            {
                return AssistConverterState.NotRun;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CutOff))
            {
                return AssistConverterState.CutOff;
            }

            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.UnKnow ))
            {
                return AssistConverterState.UnKnow ;
            }


            return AssistConverterState.Run;
        }

    }
}
