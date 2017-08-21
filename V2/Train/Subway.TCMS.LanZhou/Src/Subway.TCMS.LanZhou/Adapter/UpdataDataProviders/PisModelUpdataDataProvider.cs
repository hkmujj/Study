using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Resources.Keys;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdataDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    class PisModelUpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public PisModelUpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel) : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var ps = ViewModel.PISViewModel.Model;
           
            var stationCollection = ps.StationCollection;
            
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf当前站, f =>
            {
                if (stationCollection.IsValueCreated)
                {
                    ps.CurrentStation =
                        stationCollection.Value.FirstOrDefault(s => s.StationConfig.StationKey == (ulong)f);
                }
            });
            
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf终点站, f =>
            {
                if (stationCollection.IsValueCreated)
                {
                    ps.EndStation =
                        stationCollection.Value.FirstOrDefault(s => s.StationConfig.StationKey == (ulong)f);
                }
            });
        }
    }
}

