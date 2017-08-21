using System.ComponentModel.Composition;
using DevExpress.XtraPrinting.Native;
using LightRail.HMI.SZLHLF.Model.TrainModel;
using LightRail.HMI.SZLHLF.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using LightRail.HMI.SZLHLF.Resources.Keys;
using LightRail.HMI.SZLHLF.Extension;
using LightRail.HMI.SZLHLF.Model.AirModel;

namespace LightRail.HMI.SZLHLF.Adapter
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateTrainInfoViewDataProvider : UpdateDataProviderBase
    {

        [ImportingConstructor]
        public UpdateTrainInfoViewDataProvider(IEventAggregator eventAggregator, SZLHLFViewModel viewModel) : base(eventAggregator, viewModel)
        {

        }

        public override void Initalize(bool isDebugModel)
        {

        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            UpdateDatasTrainModel(sender, args);
            UpdateDatasEnmergencyTalkModel(sender,args);
            UpdateDatasAirConditionModel(sender, args);
            UpdateDatasNetWorkModel(sender,args);
        }

        private void UpdateDatasTrainModel(object sender, CommunicationDataChangedArgs args)
        {
            var RunningModel = ViewModel.TrainInfoViewModel.Model;

            RunningModel.DoorUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
            RunningModel.BrakeUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
            RunningModel.TractionUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
            RunningModel.BatteryUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            RunningModel.AssistPowerSource = AssistPowerSource.Normal;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1辅助电源正常, () => RunningModel.AssistPowerSource = AssistPowerSource.Normal);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1辅助电源严重故障, () => RunningModel.AssistPowerSource = AssistPowerSource.SeriousTrouble);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1辅助电源次要故障, () => RunningModel.AssistPowerSource = AssistPowerSource.SlightTrouble);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1辅助电源未运行, () => RunningModel.AssistPowerSource = AssistPowerSource.NotOperation);

            RunningModel.HighSpeedBreaker = HighSpeedBreaker.HSCBPart;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2高速断路器合, () => RunningModel.HighSpeedBreaker = HighSpeedBreaker.HSCBCombine);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2高速断路器分, () => RunningModel.HighSpeedBreaker = HighSpeedBreaker.HSCBPart);

            RunningModel.Pantograph = Pantograph.Down;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2受电弓升起, () => RunningModel.Pantograph = Pantograph.Raise);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2受电弓降下, () => RunningModel.Pantograph = Pantograph.Down);

            RunningModel.BatteryCharger = BatteryCharger.NotOperation;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4充电机运行, () => RunningModel.BatteryCharger = BatteryCharger.Operation);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4充电机未运行, () => RunningModel.BatteryCharger = BatteryCharger.NotOperation);

            RunningModel.DirectionArrow = DirectionArrow.UnNormal;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.方向前, () => RunningModel.DirectionArrow = DirectionArrow.Front);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.方向零, () => RunningModel.DirectionArrow = DirectionArrow.UnNormal);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.方向后, () => RunningModel.DirectionArrow = DirectionArrow.After);

            args.ChangedBools.UpdateIfContains(InBoolKey.MC1侧司机室激活, b =>
            {
                RunningModel.MC1Drivers = b ? Drivers.Occupy : Drivers.NotOccupy;
            });
            args.ChangedBools.UpdateIfContains(InBoolKey.MC2侧司机室激活, b =>
            {
                RunningModel.MC2Drivers = b ? Drivers.Occupy : Drivers.NotOccupy;
            });

            args.ChangedBools.UpdateIfContains(InBoolKey.转向灯左, b =>
            {
                RunningModel.TurnLeftLight = b ? TurnLight.Press : TurnLight.Restoration;
            });
            args.ChangedBools.UpdateIfContains(InBoolKey.转向灯右, b =>
            {
                RunningModel.TurnRightLight = b ? TurnLight.Press : TurnLight.Restoration;
            });

            RunningModel.RunModel = RunModel.Normal;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.模式_洗车模式, () => RunningModel.RunModel = RunModel.CarWash);

            RunningModel.WorkState = WorkState.Normal;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.工况_牵引, () => RunningModel.WorkState = WorkState.Traction);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.工况_制动, () => RunningModel.WorkState = WorkState.Brake);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.工况_惰性, () => RunningModel.WorkState = WorkState.Lazy);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.工况_紧急牵引, () => RunningModel.WorkState = WorkState.EmergencyTraction);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.工况_紧急制动, () => RunningModel.WorkState = WorkState.EmergencyBrake);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.工况_快速制动, () => RunningModel.WorkState = WorkState.FastBrake);

            args.ChangedBools.UpdateIfContains(InBoolKey.车1火警报出, b =>
            {
                RunningModel.Fire1 = b;
            });
            args.ChangedBools.UpdateIfContains(InBoolKey.车2火警报出, b =>
            {
                RunningModel.Fire2 = b;
            });
            args.ChangedBools.UpdateIfContains(InBoolKey.车3火警报出, b =>
            {
                RunningModel.Fire3 = b;
            });
            args.ChangedBools.UpdateIfContains(InBoolKey.车4火警报出, b =>
            {
                RunningModel.Fire4 = b;
            });

            
            args.ChangedBools.UpdateIfContains(InBoolKey.转向架裙板故障, b =>
            {
                RunningModel.BogieApron = b;
            });

            args.ChangedFloats.UpdateIfContains(InFolatKey.牵引百分比, f => RunningModel.TractionPercent = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.制动百分比, f => RunningModel.BrakePercent = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1电池电量百分比, f => RunningModel.RemainElectricQuantity1 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2电池电量百分比, f => RunningModel.RemainElectricQuantity2 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4电池电量百分比, f => RunningModel.RemainElectricQuantity3 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车1电池温度, f => RunningModel.PowerSourceTemperature1 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2电池温度, f => RunningModel.PowerSourceTemperature2 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4电池温度, f => RunningModel.PowerSourceTemperature3 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.限速, f => RunningModel.SpeedLimit = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.剩余里程, f => RunningModel.ResidueMileage = f);
        }

        private void UpdateDatasEnmergencyTalkModel(object sender, CommunicationDataChangedArgs args)
        {
            var EnmergencyTalkModel = ViewModel.TrainInfoViewModel.EnmergencyTalkModel;

            EnmergencyTalkModel.EnmergencyTalkUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
        }

        private void UpdateDatasAirConditionModel(object sender, CommunicationDataChangedArgs args)
        {
            var AirConditionModel = ViewModel.TrainInfoViewModel.AirConditionModel;

            AirConditionModel.AirConditionUnits.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));

            AirConditionModel.AirModelState1 = AirModelState.Normal;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1空调模式_制冷模式, () => AirConditionModel.AirModelState1 = AirModelState.Cold);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1空调模式_制热模式, () => AirConditionModel.AirModelState1 = AirModelState.Hot);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1空调模式_除湿模式, () => AirConditionModel.AirModelState1 = AirModelState.Arefaction);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车1空调模式_通风模式, () => AirConditionModel.AirModelState1 = AirModelState.Ventilation);

            AirConditionModel.AirModelState2 = AirModelState.Normal;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2空调模式_制冷模式, () => AirConditionModel.AirModelState2 = AirModelState.Cold);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2空调模式_制热模式, () => AirConditionModel.AirModelState2 = AirModelState.Hot);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2空调模式_除湿模式, () => AirConditionModel.AirModelState2 = AirModelState.Arefaction);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车2空调模式_通风模式, () => AirConditionModel.AirModelState2 = AirModelState.Ventilation);

            AirConditionModel.AirModelState3 = AirModelState.Normal;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3空调模式_制冷模式, () => AirConditionModel.AirModelState3 = AirModelState.Cold);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3空调模式_制热模式, () => AirConditionModel.AirModelState3 = AirModelState.Hot);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3空调模式_除湿模式, () => AirConditionModel.AirModelState3 = AirModelState.Arefaction);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车3空调模式_通风模式, () => AirConditionModel.AirModelState3 = AirModelState.Ventilation);

            AirConditionModel.AirModelState4 = AirModelState.Normal;
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4空调模式_制冷模式, () => AirConditionModel.AirModelState4 = AirModelState.Cold);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4空调模式_制热模式, () => AirConditionModel.AirModelState4 = AirModelState.Hot);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4空调模式_除湿模式, () => AirConditionModel.AirModelState4 = AirModelState.Arefaction);
            args.ChangedBools.UpdateIfContainsWhenTrue(InBoolKey.车4空调模式_通风模式, () => AirConditionModel.AirModelState4 = AirModelState.Ventilation);

            args.ChangedFloats.UpdateIfContains(InFolatKey.车1空调温度, f => AirConditionModel.Temperature1 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车2空调温度, f => AirConditionModel.Temperature2 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车3空调温度, f => AirConditionModel.Temperature3 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.车4空调温度, f => AirConditionModel.Temperature4 = f);
            args.ChangedFloats.UpdateIfContains(InFolatKey.室外温度, f => AirConditionModel.TemperatureOut = f);
        }

        private void UpdateDatasNetWorkModel(object sender, CommunicationDataChangedArgs args)
        {
            var NetWorkInfoModel = ViewModel.TrainInfoViewModel.NetWorkInfoModel;

            NetWorkInfoModel.AllSatte.ForEach(b => b.DataChanged(args.ChangedBools.NewValue));
        }
    }
}
