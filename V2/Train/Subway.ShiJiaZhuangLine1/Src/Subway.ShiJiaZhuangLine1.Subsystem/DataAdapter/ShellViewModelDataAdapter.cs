using System;
using System.ComponentModel.Composition;
using System.Linq;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Interface.Enum;
using Subway.ShiJiaZhuangLine1.Interface.Resouce;
using Subway.ShiJiaZhuangLine1.Subsystem.Extension;
using Subway.ShiJiaZhuangLine1.Subsystem.Model;
using Subway.ShiJiaZhuangLine1.Subsystem.ViewModels;

namespace Subway.ShiJiaZhuangLine1.Subsystem.DataAdapter
{
    [Export]
    public class ShellViewModelDataAdapter : IUpdateStatusProvider, IDisposable, IDataListener
    {
        protected ICommunicationDataReadService ReadService
        {
            get { return SubsysParams.Instance.SubsystemInitParam.CommunicationDataService.ReadService; }
        }

        protected ICommunicationDataWriteService WriteService
        {
            get { return SubsysParams.Instance.SubsystemInitParam.CommunicationDataService.WriteService; }
        }

        [ImportingConstructor]
        public ShellViewModelDataAdapter(ShellViewModel shellViewModel)
        {
            ShellViewModel = shellViewModel;
        }

        public ShellViewModel ShellViewModel { private set; get; }

        public void UpdateState(object sender, CommunicationDataChangedArgs e)
        {
            UpdateEmergTalkStates(e);

            UpdateDoorStatues(e);

            UpdateMainRunBrakeState(e);

            UpdateTractionLockState(e);

            UpdateBypassStates(e);

            UpdatePantographStates(e);

            UpdateTitle(e);


        }

        private void UpdateTitle(CommunicationDataChangedArgs e)
        {
            var args = e.ChangedFloats;
            var argsb = e.ChangedBools;
            var model = ShellViewModel.Model.TitleModel;
            args.UpdateIfContains(InFloatKeys.网压, f => model.NetPressValue = f);
            args.UpdateIfContains(InFloatKeys.蓄电池电压, f => model.BatteryValue = f);

            argsb.UpdateIfContains(InBoolKeys.Inb蓄电池电压红闪, b => { model.IsBatteryFlicker = b; });
            argsb.UpdateIfContains(InBoolKeys.Inb网压值显示红闪, b => model.IsNetPressFlicker = b);

        }

        private void UpdatePantographStates(CommunicationDataChangedArgs e)
        {
            var args = e.ChangedBools;
            var model = ShellViewModel.Model.FrsmHighSpeedModel;
            args.UpdateIfContains(InBoolKeys.Inb车2号受电弓故障, () => model.Car2Pantograph = FrsmHighSpeed.PantographFault);
            args.UpdateIfContains(InBoolKeys.Inb车2号受电弓升起, () => model.Car2Pantograph = FrsmHighSpeed.PantographUp);
            args.UpdateIfContains(InBoolKeys.Inb车2号受电弓降下, () => model.Car2Pantograph = FrsmHighSpeed.PantographDown);
            args.UpdateIfContains(InBoolKeys.Inb2号车受电弓状态未知, () => model.Car2Pantograph = FrsmHighSpeed.PantographStateUnkown);
            args.UpdateIfContains(InBoolKeys.Inb2号车受电弓切除, () => model.Car2Pantograph = FrsmHighSpeed.PantographCut);

            args.UpdateIfContains(InBoolKeys.Inb车5号受电弓故障, () => model.Car5Pantograph = FrsmHighSpeed.PantographFault);
            args.UpdateIfContains(InBoolKeys.Inb车5号受电弓升起, () => model.Car5Pantograph = FrsmHighSpeed.PantographUp);
            args.UpdateIfContains(InBoolKeys.Inb车5号受电弓降下, () => model.Car5Pantograph = FrsmHighSpeed.PantographDown);
            args.UpdateIfContains(InBoolKeys.Inb5号车受电弓状态未知, () => model.Car5Pantograph = FrsmHighSpeed.PantographStateUnkown);
            args.UpdateIfContains(InBoolKeys.Inb5号车受电弓切除, () => model.Car5Pantograph = FrsmHighSpeed.PantographCut);


            args.UpdateIfContains(InBoolKeys.Inb车2号高速断路器合, () => model.Car2HighSpeed = FrsmHighSpeed.HighJoin);
            args.UpdateIfContains(InBoolKeys.Inb车2号高速断路器断, () => model.Car2HighSpeed = FrsmHighSpeed.HighDisconnect);
            args.UpdateIfContains(InBoolKeys.Inb车2号高速断路器故障, () => model.Car2HighSpeed = FrsmHighSpeed.HighFalut);
            args.UpdateIfContains(InBoolKeys.Inb2号车高速断路器通讯故障, () => model.Car2HighSpeed = FrsmHighSpeed.HighCommunicationFault);

            args.UpdateIfContains(InBoolKeys.Inb车3号高速断路器合, () => model.Car3HighSpeed = FrsmHighSpeed.HighJoin);
            args.UpdateIfContains(InBoolKeys.Inb车3号高速断路器断, () => model.Car3HighSpeed = FrsmHighSpeed.HighDisconnect);
            args.UpdateIfContains(InBoolKeys.Inb车3号高速断路器故障, () => model.Car3HighSpeed = FrsmHighSpeed.HighFalut);
            args.UpdateIfContains(InBoolKeys.Inb3号车高速断路器通讯故障, () => model.Car3HighSpeed = FrsmHighSpeed.HighCommunicationFault);

            args.UpdateIfContains(InBoolKeys.Inb车4号高速断路器合, () => model.Car4HighSpeed = FrsmHighSpeed.HighJoin);
            args.UpdateIfContains(InBoolKeys.Inb车4号高速断路器断, () => model.Car4HighSpeed = FrsmHighSpeed.HighDisconnect);
            args.UpdateIfContains(InBoolKeys.Inb车4号高速断路器故障, () => model.Car4HighSpeed = FrsmHighSpeed.HighFalut);
            args.UpdateIfContains(InBoolKeys.Inb4号车高速断路器通讯故障, () => model.Car4HighSpeed = FrsmHighSpeed.HighCommunicationFault);

            args.UpdateIfContains(InBoolKeys.Inb车5号高速断路器合, () => model.Car5HighSpeed = FrsmHighSpeed.HighJoin);
            args.UpdateIfContains(InBoolKeys.Inb车5号高速断路器断, () => model.Car5HighSpeed = FrsmHighSpeed.HighDisconnect);
            args.UpdateIfContains(InBoolKeys.Inb车5号高速断路器故障, () => model.Car5HighSpeed = FrsmHighSpeed.HighFalut);
            args.UpdateIfContains(InBoolKeys.Inb5号车高速断路器通讯故障, () => model.Car5HighSpeed = FrsmHighSpeed.HighCommunicationFault);

            args.UpdateIfContains(InBoolKeys.Inb车2号车间电源供电, () => model.Car2Fram = FrsmHighSpeed.FramConect);
            args.UpdateIfContains(InBoolKeys.Inb车2号车间电源未供电, () => model.Car2Fram = FrsmHighSpeed.FramCut);
            args.UpdateIfContains(InBoolKeys.Inb车2号车间电源故障, () => model.Car2Fram = FrsmHighSpeed.FramFault);

            args.UpdateIfContains(InBoolKeys.Inb车5号车间电源供电, () => model.Car5Fram = FrsmHighSpeed.FramConect);
            args.UpdateIfContains(InBoolKeys.Inb车5号车间电源未供电, () => model.Car5Fram = FrsmHighSpeed.FramCut);
            args.UpdateIfContains(InBoolKeys.Inb车5号车间电源故障, () => model.Car5Fram = FrsmHighSpeed.FramFault);
        }

        private void UpdateEmergTalkStates(CommunicationDataChangedArgs e)
        {
            foreach (
                var unit in
                    ShellViewModel.Model.EmergencyTalk.EmergencyTalkUnitCollection.Where(
                        w => !string.IsNullOrWhiteSpace(w.UnitFalt)))
            {
                unit.State = EmergencyTalkState.UnitNormal;
                foreach (var statu in EmergencyTalkStateHelper.AllTalkStates)
                {
                    var indexName = unit.GetIndexName(statu);
                    if (!string.IsNullOrWhiteSpace(indexName) &&
                        ReadService.ReadOnlyBoolDictionary.GetValutAt(indexName))
                    {
                        unit.State = statu;
                    }
                }
            }
        }

        private void UpdateDoorStatues(CommunicationDataChangedArgs e)
        {
            foreach (var unit in ShellViewModel.Model.DoorModel.DoorUnitCollection)
            {
                // unit.DoorStatus = DoorStatus.Null;
                foreach (var statu in DoorStatusHelper.AllDoorStatus)
                {
                    var indexName = unit.GetIndexName(statu);
                    if (!string.IsNullOrWhiteSpace(indexName) &&
                        ReadService.ReadOnlyBoolDictionary.GetValutAt(indexName))
                    {
                        unit.DoorStatus = statu;
                    }
                }

            }
        }

        private void UpdateMainRunBrakeState(CommunicationDataChangedArgs e)
        {
            foreach (var unit in ShellViewModel.Model.BrakeModel.BrakeItemCollection)
            {
                //unit.BrakeStatus = BrakeStatus.Null;
                foreach (var brake in BrakeStatusHelper.AllStateCollection)
                {
                    var indexName = unit.GetIndexName(brake);
                    if (!string.IsNullOrWhiteSpace(indexName) &&
                        ReadService.ReadOnlyBoolDictionary.GetValutAt(indexName))
                    {
                        unit.BrakeStatus = brake;
                    }

                }
            }
            foreach (var unit in ShellViewModel.Model.EmergencyBrakeCauseModel.EmergencyBrakeCauseUnits)
            {
                if (!unit.LogicName.Equals("预留"))
                {
                    unit.Emergency = ReadService.ReadOnlyBoolDictionary.GetValutAt(unit.LogicName);
                }
            }
        }

        private void UpdateBypassStates(CommunicationDataChangedArgs e)
        {
            foreach (var bypassUnit in ShellViewModel.Model.BypassModel.BypassUnitCollecion)
            {
                var unit = bypassUnit;
                if (e.ChangedBools.ContainsKey(IndexNameType.In, unit.Car1SwithOn, unit.Car1SwithOff,
                    unit.Car1Unkown, unit.Car1Fault))
                {
                    unit.Car1BypassState = GetBypassState(unit.Car1SwithOn, unit.Car1SwithOff,
                        unit.Car1Unkown, unit.Car1Fault);
                }

                if (e.ChangedBools.ContainsKey(IndexNameType.In, unit.Car6SwithOn, unit.Car6SwithOff,
                    unit.Car6Unkown, unit.Car6Fault))
                {
                    unit.Car6BypassState = GetBypassState(unit.Car6SwithOn, unit.Car6SwithOff,
                        unit.Car6Unkown, unit.Car6Fault);
                }
            }
        }

        private BypassState GetBypassState(string car1SwithOn, string car1SwithOff, string car1Unkown, string car1Fault)
        {
            if (ReadService.ReadOnlyBoolDictionary.GetValutAt(car1Fault))
            {
                return BypassState.Fault;
            }
            if (ReadService.ReadOnlyBoolDictionary.GetValutAt(car1Unkown))
            {
                return BypassState.Unknown;
            }
            if (ReadService.ReadOnlyBoolDictionary.GetValutAt(car1SwithOff))
            {
                return BypassState.SwitchOff;
            }
            if (ReadService.ReadOnlyBoolDictionary.GetValutAt(car1SwithOn))
            {
                return BypassState.SwitchOn;
            }
            return BypassState.SwitchOff;
        }

        private void UpdateTractionLockState(CommunicationDataChangedArgs e)
        {
            foreach (
                var lockUnit in
                    ShellViewModel.TractionLockViewModel.Model.TractionLockUnitCollection.Where(
                        w => w != TractionLockUnit.Empty))
            {
                var unit = lockUnit;
                e.ChangedBools.UpdateIfContains(lockUnit.IndexName, b => unit.IsLocked = b);
            }
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// bool 值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {

        }

        /// <summary>
        /// float值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {

        }

        /// <summary>
        /// data值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            UpdateState(sender, dataChangedArgs);
        }
    }
}