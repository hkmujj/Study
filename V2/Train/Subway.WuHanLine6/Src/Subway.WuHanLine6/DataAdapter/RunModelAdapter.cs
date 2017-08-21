using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Models;
using Subway.WuHanLine6.Models.States;
using Subway.WuHanLine6.Models.Units;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    ///
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class RunModelAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var runModel = Model.RunModel;

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车车间电源故障, (b => runModel.WorkPowerStateOne = WorkPowerState.Fault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车车间电源未连接, (b => runModel.WorkPowerStateOne = WorkPowerState.UnConnect));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车车间电源连接且供电, (b => runModel.WorkPowerStateTwo = WorkPowerState.ConnectPower));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车车间电源连接未供电, (b => runModel.WorkPowerStateOne = WorkPowerState.ConnectUnPower));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车车间电源故障, (b => runModel.WorkPowerStateTwo = WorkPowerState.Fault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车车间电源未连接, (b => runModel.WorkPowerStateTwo = WorkPowerState.UnConnect));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车车间电源连接且供电, (b => runModel.WorkPowerStateTwo = WorkPowerState.ConnectPower));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车车间电源连接未供电, (b => runModel.WorkPowerStateTwo = WorkPowerState.ConnectUnPower));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车高速断路器故障, (b => runModel.HighSpeedStateOne = HighSpeedState.Fault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车高速断路器合上, (b => runModel.HighSpeedStateOne = HighSpeedState.Close));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车高速断路器断开, (b => runModel.HighSpeedStateOne = HighSpeedState.Open));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车高速断路器故障, (b => runModel.HighSpeedStateTwo = HighSpeedState.Fault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车高速断路器合上, (b => runModel.HighSpeedStateTwo = HighSpeedState.Close));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车高速断路器断开, (b => runModel.HighSpeedStateTwo = HighSpeedState.Open));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车高速断路器故障, (b => runModel.HighSpeedStateThree = HighSpeedState.Fault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车高速断路器合上, (b => runModel.HighSpeedStateThree = HighSpeedState.Close));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车高速断路器断开, (b => runModel.HighSpeedStateThree = HighSpeedState.Open));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车高速断路器故障, (b => runModel.HighSpeedStateFour = HighSpeedState.Fault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车高速断路器合上, (b => runModel.HighSpeedStateFour = HighSpeedState.Close));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车高速断路器断开, (b => runModel.HighSpeedStateFour = HighSpeedState.Open));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车牵引系统1状态切除, (b => runModel.TractionStateF002 = TractionState.Cut));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车牵引系统1状态故障, (b => runModel.TractionStateF002 = TractionState.Fault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车牵引系统1状态断开, (b => runModel.TractionStateF002 = TractionState.Close));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车牵引系统1状态警告, (b => runModel.TractionStateF002 = TractionState.Warn));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车牵引系统1状态运行, (b => runModel.TractionStateF002 = TractionState.Run));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车牵引系统1状态切除, (b => runModel.TractionStateF003 = TractionState.Cut));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车牵引系统1状态故障, (b => runModel.TractionStateF003 = TractionState.Fault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车牵引系统1状态断开, (b => runModel.TractionStateF003 = TractionState.Close));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车牵引系统1状态警告, (b => runModel.TractionStateF003 = TractionState.Warn));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车牵引系统1状态运行, (b => runModel.TractionStateF003 = TractionState.Run));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车牵引系统1状态切除, (b => runModel.TractionStateF004 = TractionState.Cut));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车牵引系统1状态故障, (b => runModel.TractionStateF004 = TractionState.Fault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车牵引系统1状态断开, (b => runModel.TractionStateF004 = TractionState.Close));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车牵引系统1状态警告, (b => runModel.TractionStateF004 = TractionState.Warn));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车牵引系统1状态运行, (b => runModel.TractionStateF004 = TractionState.Run));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车牵引系统1状态切除, (b => runModel.TractionStateF005 = TractionState.Cut));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车牵引系统1状态故障, (b => runModel.TractionStateF005 = TractionState.Fault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车牵引系统1状态断开, (b => runModel.TractionStateF005 = TractionState.Close));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车牵引系统1状态警告, (b => runModel.TractionStateF005 = TractionState.Warn));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车牵引系统1状态运行, (b => runModel.TractionStateF005 = TractionState.Run));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车辅助系统1状态切除, (b => runModel.AssistACStateF001One = AssistACState.ACCut));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车辅助系统1状态故障, (b => runModel.AssistACStateF001One = AssistACState.ACFault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车辅助系统1状态断开, (b => runModel.AssistACStateF001One = AssistACState.ACClose));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车辅助系统1状态警告, (b => runModel.AssistACStateF001One = AssistACState.ACWarn));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车辅助系统1状态运行, (b => runModel.AssistACStateF001One = AssistACState.ACRun));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车辅助系统2状态切除, (b => runModel.AssistACStateF001Two = AssistACState.DCCut));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车辅助系统2状态故障, (b => runModel.AssistACStateF001Two = AssistACState.DCFault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车辅助系统2状态断开, (b => runModel.AssistACStateF001Two = AssistACState.DCClose));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车辅助系统2状态警告, (b => runModel.AssistACStateF001Two = AssistACState.DCWarn));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车辅助系统2状态运行, (b => runModel.AssistACStateF001Two = AssistACState.DCRun));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车辅助系统1状态切除, (b => runModel.AssistACStateF006One = AssistACState.ACCut));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车辅助系统1状态故障, (b => runModel.AssistACStateF006One = AssistACState.ACFault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车辅助系统1状态断开, (b => runModel.AssistACStateF006One = AssistACState.ACClose));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车辅助系统1状态警告, (b => runModel.AssistACStateF006One = AssistACState.ACWarn));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车辅助系统1状态运行, (b => runModel.AssistACStateF006One = AssistACState.ACRun));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车辅助系统2状态切除, (b => runModel.AssistACStateF006Two = AssistACState.DCCut));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车辅助系统2状态故障, (b => runModel.AssistACStateF006Two = AssistACState.DCFault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车辅助系统2状态断开, (b => runModel.AssistACStateF006Two = AssistACState.DCClose));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车辅助系统2状态警告, (b => runModel.AssistACStateF006Two = AssistACState.DCWarn));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车辅助系统2状态运行, (b => runModel.AssistACStateF006Two = AssistACState.DCRun));

            args.UpdateIfContain(InBoolKeys.InBo扩展供电, (b => runModel.ExtentionPower = b));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车空压机1状态切除, (b => runModel.AirPumpStateF003 = AirPumpState.Cut));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车空压机1状态故障, (b => runModel.AirPumpStateF003 = AirPumpState.Fault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车空压机1状态断开, (b => runModel.AirPumpStateF003 = AirPumpState.Off));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车空压机1状态警告, (b => runModel.AirPumpStateF003 = AirPumpState.Warn));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车空压机1状态运行, (b => runModel.AirPumpStateF003 = AirPumpState.Run));

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车空压机1状态切除, (b => runModel.AirPumpStateF004 = AirPumpState.Cut));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车空压机1状态故障, (b => runModel.AirPumpStateF004 = AirPumpState.Fault));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车空压机1状态断开, (b => runModel.AirPumpStateF004 = AirPumpState.Off));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车空压机1状态警告, (b => runModel.AirPumpStateF004 = AirPumpState.Warn));
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车空压机1状态运行, (b => runModel.AirPumpStateF004 = AirPumpState.Run));

            args.UpdateIfContain(InBoolKeys.InBo运行界面烟火报警信息显示, b =>
            {
                runModel.SmokeVisibility = b ? Visibility.Visible : Visibility.Hidden;
            });
            args.UpdateIfContain(InBoolKeys.InBo运行界面乘客报警信息显示, b =>
            {
                runModel.EmergencyTalkVisibility = b ? Visibility.Visible : Visibility.Hidden;
            });
            args.UpdateIfContain(InBoolKeys.InBo运行界面旁路报警信息显示, b =>
            {
                runModel.BypassVisibility = b ? Visibility.Visible : Visibility.Hidden;
            });

            runModel.BrakeUnits.ForEach(f => f.Changed(args));
            runModel.AirConditionUnits.ForEach(f => f.Changed(args));
            runModel.AllDoorStates.ForEach(f => f.Changed(args));
            args.UpdateAllTrue(GlobalParams.Instance.RunModelUnits.ToDictionary(t => t.LogicName, t => new Action<bool>(a => { runModel.CurrentModel = new List<RunModelUnit>() { t }; })), () => runModel.CurrentModel = null);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车司1号门关好完全, b => runModel.Drive1Door1 = DriveDoorState.Close);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车司1号门未完全关好, b => runModel.Drive1Door1 = DriveDoorState.Open);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车司2号门关好完全, b => runModel.Drive1Door2 = DriveDoorState.Close);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车司2号门未完全关好, b => runModel.Drive1Door2 = DriveDoorState.Open);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车司1号门关好完全, b => runModel.Drive2Door1 = DriveDoorState.Close);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车司1号门未完全关好, b => runModel.Drive2Door1 = DriveDoorState.Open);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车司2号门关好完全, b => runModel.Drive2Door2 = DriveDoorState.Close);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车司2号门未完全关好, b => runModel.Drive2Door2 = DriveDoorState.Open);

            //GlobalParams.Instance.RunModelUnits.ForEach(f => { args.UpdateIfContainWhereTrue(f.LogicName, b => runModel.CurrentModel = new List<RunModelUnit>() { f }); });
            base.DataChanged(args);
        }

        /// <summary>
        /// 数据变化
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, float> args)
        {
            args.UpdateIfContain(InFloatKeys.InF牵引级位, (f => Model.RunModel.Traction = f));
            args.UpdateIfContain(InFloatKeys.InF制动级位, (f => Model.RunModel.Brake = f));
        }
    }
}