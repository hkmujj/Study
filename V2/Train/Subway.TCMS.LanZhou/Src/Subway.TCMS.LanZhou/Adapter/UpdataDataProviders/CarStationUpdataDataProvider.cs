using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Resources.Keys;
using Subway.TCMS.LanZhou.ViewModel;
using Subway.TCMS.LanZhou.Extension;

namespace Subway.TCMS.LanZhou.Adapter.UpdataDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    public class CarStationUpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarStationUpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var ps = ViewModel.PISViewModel.Model;
            
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf当前站, f =>
            {
                if (ps.StationCollection.IsValueCreated)
                {
                    ps.CurrentStation =
                         ps.StationCollection.Value.FirstOrDefault(s => s.StationConfig.StationKey == (ulong)f);
                }
                
            });
            
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf终点站, f =>
            {
                if (ps.StationCollection.IsValueCreated)
                {
                    ps.EndStation =
                        ps.StationCollection.Value.FirstOrDefault(s => s.StationConfig.StationKey == (ulong)f);
                }
            });
        }
    }
}
