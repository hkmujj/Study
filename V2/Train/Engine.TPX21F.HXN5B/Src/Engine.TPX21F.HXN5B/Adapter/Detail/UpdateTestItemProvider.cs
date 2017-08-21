using System.Collections.Generic;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Extension;
using Engine.TPX21F.HXN5B.Model.Domain.Test.Detail;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace Engine.TPX21F.HXN5B.Adapter.Detail
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateTestItemProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public UpdateTestItemProvider(IEventAggregator eventAggregator, HXN5BViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var ts = ViewModel.Domain.TestViewModel.Model.TestSpeedConditions;
            if (ts.IsValueCreated)
            {
                UpdateItem(args, ts.Value);
            }

            var tv = ViewModel.Domain.TestViewModel.Model.VigilantModel;
            if (tv.Conditions.IsValueCreated)
            {
                UpdateItem(args, tv.Conditions.Value);
            }
        }

        private static void UpdateItem(CommunicationDataChangedArgs args, IEnumerable<TestItem> ts)
        {
            foreach (var it in ts)
            {
                if (!string.IsNullOrWhiteSpace(it.ItemConfig.IndexBool))
                {
                    args.ChangedBools.UpdateIfContains(it.ItemConfig.IndexBool, b => it.Passed = b);
                }
                else if (!string.IsNullOrWhiteSpace(it.ItemConfig.IndexFloat))
                {
                    args.ChangedFloats.UpdateIfContains(it.ItemConfig.IndexFloat, f => it.Value = f);
                }
            }
        }
    }
}