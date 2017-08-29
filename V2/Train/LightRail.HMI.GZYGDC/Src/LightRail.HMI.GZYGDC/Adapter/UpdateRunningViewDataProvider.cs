using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting.Native;
using LightRail.HMI.GZYGDC.Extension;
using LightRail.HMI.GZYGDC.Model;
using LightRail.HMI.GZYGDC.Model.State;
using LightRail.HMI.GZYGDC.Resources.Keys;
using LightRail.HMI.GZYGDC.Service;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace LightRail.HMI.GZYGDC.Adapter
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateRunningViewDataProvider : UpdateDataProviderBase
    {

        [ImportingConstructor]
        public UpdateRunningViewDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel) : base(eventAggregator, viewModel)
        {

        }

        public override void Initalize(bool isDebugModel)
        {

        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var RunningModel = ViewModel.RunningViewModel.Model;

            RunningModel.DoorUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
            RunningModel.AssistUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
            RunningModel.BrakeUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
            RunningModel.TractionUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
            RunningModel.PantographUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
            RunningModel.HSCBUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
            RunningModel.BatteryUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
            RunningModel.SpringUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
           

            //工况默认为无模式
            RunningModel.CurWorkCondition = WorkCondition.None;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.牵引, () => RunningModel.CurWorkCondition = WorkCondition.Traction);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.惰行, () => RunningModel.CurWorkCondition = WorkCondition.Lazy);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.常用制动, () => RunningModel.CurWorkCondition = WorkCondition.ConmonBrake);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.自动紧急制动, () => RunningModel.CurWorkCondition = WorkCondition.AutoEmergencyBrake);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.保持制动, () => RunningModel.CurWorkCondition = WorkCondition.KeepBrake);


            args.ChangedBools.UpdateIfContains(InBoolKeys.头车司机室激活, b => RunningModel.HeadTrainActive = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.尾车司机室激活, b => RunningModel.TailTrainActive = b);

  
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.列车方向向前, () => RunningModel.CurTrainDirectionDirection = TrainDirection.Forward);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.列车无方向, () => RunningModel.CurTrainDirectionDirection = TrainDirection.None);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKeys.列车方向向后, () => RunningModel.CurTrainDirectionDirection = TrainDirection.Back);

            args.ChangedFloats.UpdateIfContains(InFloatKeys.车1电池电量百分比, f => RunningModel.Car1BatteryInfo.PowerQuantityPercent = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.车1电池温度, f => RunningModel.Car1BatteryInfo.Temperature = f);

            args.ChangedFloats.UpdateIfContains(InFloatKeys.车2电池电量百分比, f => RunningModel.Car2BatteryInfo.PowerQuantityPercent = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.车2电池温度, f => RunningModel.Car2BatteryInfo.Temperature = f);

            args.ChangedFloats.UpdateIfContains(InFloatKeys.车4电池电量百分比, f => RunningModel.Car4BatteryInfo.PowerQuantityPercent = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.车4电池温度, f => RunningModel.Car4BatteryInfo.Temperature = f);


            args.ChangedFloats.UpdateIfContains(InFloatKeys.牵引百分比, f => RunningModel.TractionPercent = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.制动百分比, f => RunningModel.BrakePercent = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.限速, f => RunningModel.LimitSpeed = f);



        }
    }
}
