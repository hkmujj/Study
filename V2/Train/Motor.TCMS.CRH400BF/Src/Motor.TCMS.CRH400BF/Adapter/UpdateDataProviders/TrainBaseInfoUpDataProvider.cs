using System.ComponentModel.Composition;
using Motor.TCMS.CRH400BF.Extension;
using Motor.TCMS.CRH400BF.Model.Constant;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Motor.TCMS.CRH400BF.Model;
using Motor.TCMS.CRH400BF.Model.Train;
using Motor.TCMS.CRH400BF.Resources.Keys;

namespace Motor.TCMS.CRH400BF.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    internal class TrainBaseInfoUpDataProvider : UpdateDataProviderBase
    {
        protected DomainModel DomainModel;
        [ImportingConstructor]
        public TrainBaseInfoUpDataProvider(IEventAggregator eventAggregator, TrainModel trainModel, DomainModel domainModel)
            : base(eventAggregator, trainModel)
        {
            DomainModel = domainModel;
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            args.ChangedBools.UpdateIfContains(InbKeys.InbMMI亮屏, b => DomainModel.IsVisble = b);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf实时速度值, f => TrainModel.Speed = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf网压值, f => TrainModel.TouchNetV = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf网流值, f => TrainModel.TouchNetA = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf总风管数值, f => TrainModel.WindPipeValue = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf制动级位, f => TrainModel.BrakeLevelValue = f);


            args.ChangedBools.UpdateIfContains(InbKeys.Inb1车司机室占用, b => TrainModel.FirstDriverRoom = b);
            args.ChangedBools.UpdateIfContains(InbKeys.Inb8车司机室占用, b => TrainModel.EighthDriverRoom = b);

            args.ChangedBools.UpdateIfContains(InbKeys.Inb左侧门释放, b => TrainModel.LeftDoorUnRelease = b);
            args.ChangedBools.UpdateIfContains(InbKeys.Inb右侧门释放, b => TrainModel.RightDoorUnRelease = b);


            args.ChangedBools.UpdateIfContains(InbKeys.Inb操作模式屏显示_手柄恒速模式, b => TrainModel.ConstantSpeed = b);
            args.ChangedBools.UpdateIfContains(InbKeys.Inb操作模式屏显示_手柄级位模式, b => TrainModel.Level = b);

            args.ChangedFloats.UpdateIfContains(InfKeys.Inf制动级位, f => TrainModel.BrakeLevel = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf牵引级位, f => TrainModel.TractionLevel = f);
            args.ChangedFloats.UpdateIfContains(InfKeys.Inf设定速度, f => TrainModel.SettingSpeed = f);


            var d = TrainModel;
            d.TrainFaultState = GetTrainFaultState();
            //d.BrakeLevel = GetBrakeLevel();
            d.DirectionState = GetDirectionState();
            d.AtpLevel = GetAtpLevel();
            d.TouchNetV1 = d.TouchNetV - 17.5;
        }

        private AtpLevelState GetAtpLevel()
        {
            if (DataService.GetInBoolOf(InbKeys.InbATP级位隔离))
            {
                return AtpLevelState.Isolation;
            }
            return AtpLevelState.Other;
        }

        private DirectionState GetDirectionState()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb运行方向向后))
            {
                return DirectionState.Backwards;
            }
            return DirectionState.Forward;
        }

        //private BrakeLevelState GetBrakeLevel()
        //{
        //    if (DataService.GetInBoolOf(InbKeys.Inb制动级位1级))
        //    {
        //        return BrakeLevelState.One;
        //    }
        //    if (DataService.GetInBoolOf(InbKeys.Inb制动级位2级))
        //    {
        //        return BrakeLevelState.Two;
        //    }
        //    if (DataService.GetInBoolOf(InbKeys.Inb制动级位3级))
        //    {
        //        return BrakeLevelState.Three;
        //    }
        //    if (DataService.GetInBoolOf(InbKeys.Inb制动级位4级 ))
        //    {
        //        return BrakeLevelState.Four;
        //    }
        //    if (DataService.GetInBoolOf(InbKeys.Inb制动级位5级))
        //    {
        //        return BrakeLevelState.Five;
        //    }
        //    return BrakeLevelState.None;
        //}

        private TrainFaultState GetTrainFaultState()
        {
            if (DataService.GetInBoolOf(InbKeys.Inb故障提示1级故障))
            {
                return TrainFaultState.One;
            }
            if (DataService.GetInBoolOf(InbKeys.Inb故障提示2级故障))
            {
                return TrainFaultState.Two;
            }
            return TrainFaultState.None;
        }






    }
}

