using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Extension;
using Engine.TAX2.SS7C.Resources.Keys;
using Engine.TAX2.SS7C.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace Engine.TAX2.SS7C.Adapter.Detail
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateTruckStateProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public UpdateTruckStateProvider(IEventAggregator eventAggregator, SS7CViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var ts = ViewModel.TrainStateViewModel.Model;

            args.ChangedFloats.UpdateIfContains(Inf.机车状态_IM11_一架, f => ts.TruckState1.IM1 = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态_IM12_一架, f => ts.TruckState1.IM2 = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态_IM13_一架, f => ts.TruckState1.IM3 = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态_IF11_一架, f => ts.TruckState1.IF1 = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态_UM11_一架, f => ts.TruckState1.UM1 = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态_V1_一架, f => ts.TruckState1.V = f);

            args.ChangedFloats.UpdateIfContains(Inf.机车状态_IM21_二架, f => ts.TruckState2.IM1 = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态_IM22_二架, f => ts.TruckState2.IM2 = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态_IM23_二架, f => ts.TruckState2.IM3 = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态_IF21_二架, f => ts.TruckState2.IF1 = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态_UM21_二架, f => ts.TruckState2.UM1 = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态_V2_二架, f => ts.TruckState2.V = f);

            args.ChangedFloats.UpdateIfContains(Inf.机车状态_Isin, f => ts.Isin = f);

            args.ChangedFloats.UpdateIfContains(Inf.机车状态一_机车供电1电压, f => ts.TruckState1.PowerSupplyV = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态一_机车供电1电流, f => ts.TruckState1.PowerSupplyA = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态一_机车供电2电压, f => ts.TruckState2.PowerSupplyV = f);
            args.ChangedFloats.UpdateIfContains(Inf.机车状态一_机车供电2电流, f => ts.TruckState2.PowerSupplyA = f);
        }
    }
}