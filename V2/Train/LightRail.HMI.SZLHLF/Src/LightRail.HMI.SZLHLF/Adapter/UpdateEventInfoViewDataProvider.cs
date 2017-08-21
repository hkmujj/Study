using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace LightRail.HMI.SZLHLF.Adapter
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateEventInfoViewDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public UpdateEventInfoViewDataProvider(IEventAggregator eventAggregator, SZLHLFViewModel viewModel)
            : base(eventAggregator, viewModel)
        {

        }

        public override void Initalize(bool isDebugModel)
        {

        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var EventInfoModel = ViewModel.EventInfoViewModel.Model;

            foreach (var newValueKey in args.ChangedBools.NewValue.Values)
            {
                if (newValueKey)
                {
                    EventInfoModel.EventManagerService.HappenFault(args.ChangedBools.NewValue.Keys.FirstOrDefault());
                }
                else
                {
                    EventInfoModel.EventManagerService.DeleteFault(args.ChangedBools.NewValue.Keys.FirstOrDefault());
                }
            }
        }
    }
}
