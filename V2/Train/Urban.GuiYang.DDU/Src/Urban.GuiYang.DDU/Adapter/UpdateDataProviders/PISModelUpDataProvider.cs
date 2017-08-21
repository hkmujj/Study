using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Resources.Keys;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Adapter.UpdateDataProviders
{
    [Export(typeof (IUpdateDataProvider))]
    internal class PISModelUpDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public PISModelUpDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var ps = ViewModel.PISViewModel.Model;
            var sm = ps.StationModel;
            var stationCollection = ps.StationCollection;

            args.ChangedFloats.UpdateIfContains(InfKeys.Inf线路ID, f => sm.LineId = f.ToString("0"));
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf下一站距离, f => sm.NextStationDistance = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf当前站距离, f => sm.CurrentStationDistance = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf开门方式, f => sm.OpenDoorType = (OpenDoorType) f);

            
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf当前站, f =>
            {
                //if (stationCollection.IsValueCreated)
                {
                    sm.CurrentStation =
                        stationCollection.Value.FirstOrDefault(s => s.StationConfig.StationKey == (ulong) f);
                }
            });

            args.ChangedFloats.UpdateIfContains(InfKeys.Inf下一站, f =>
            {
                //if (stationCollection.IsValueCreated)
                {
                    sm.NextStation =
                        stationCollection.Value.FirstOrDefault(s => s.StationConfig.StationKey == (ulong)f);
                }
            });

            args.ChangedFloats.UpdateIfContains(InfKeys.Inf终点站, f =>
            {
                //if (stationCollection.IsValueCreated)
                {
                    sm.EndStatiion =
                        stationCollection.Value.FirstOrDefault(s => s.StationConfig.StationKey == (ulong)f);
                }
            });

            args.ChangedFloats.UpdateIfContains(InfKeys.Inf跳站1, f =>
            {
                if (stationCollection.IsValueCreated)
                {
                    sm.SkipStation1 =
                        stationCollection.Value.FirstOrDefault(s => s.StationConfig.StationKey == (ulong)f);
                }
            });

            args.ChangedFloats.UpdateIfContains(InfKeys.Inf跳站2, f =>
            {
                if (stationCollection.IsValueCreated)
                {
                    sm.SkipStation2 =
                        stationCollection.Value.FirstOrDefault(s => s.StationConfig.StationKey == (ulong)f);
                }
            });
        }
    }
}