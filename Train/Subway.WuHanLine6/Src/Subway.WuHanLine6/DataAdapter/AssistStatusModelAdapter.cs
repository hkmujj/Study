using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// 辅助系统状态 适配
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class AssistStatusModelAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var assist = Model.AssistStatusModel;

            args.UpdateIfContain(InBoolKeys.InBoTC1车辅助系统状态通信异常, b => assist.AssistStatusF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2车辅助系统状态通信异常, b => assist.AssistStatusF006 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1车充电机系统状态通信异常, b => assist.DynamoStatusF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2车充电机系统状态通信异常, b => assist.DynamoStatusF006 = b);
        }

        /// <summary>
        /// 数据变化
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, float> args)
        {
            var assist = Model.AssistStatusModel;

            args.UpdateIfContain(InFloatKeys.InFTC1车输入电压, f => assist.AssistInputVoltageF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车输入电压, f => assist.AssistInputVoltageF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车母线电压, f => assist.AssistGeneratrixVoltageF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车母线电压, f => assist.AssistGeneratrixVoltageF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车输出电压, f => assist.AssistOutputVoltageF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车输出电压, f => assist.AssistOutputVoltageF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车辅助能耗, f => assist.AssistEnergyF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车辅助能耗, f => assist.AssistEnergyF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车输出频率, f => assist.AssistOutputRateF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车输出频率, f => assist.AssistOutputRateF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车输出电流, f => assist.AssistOutputCurrentF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车输出电流, f => assist.AssistOutputCurrentF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车输入电流, f => assist.AssistInputCurrentF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车输入电流, f => assist.AssistInputCurrentF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车充电机母线电压, f => assist.DynamoGeneratrixVoltageF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车充电机母线电压, f => assist.DynamoGeneratrixVoltageF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车充电机输出电压, f => assist.DynamoOutputVoltageF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车充电机输出电压, f => assist.DynamoOutputVoltageF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车输出负载电流, f => assist.OutPutLoadCurrentF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车输出负载电流, f => assist.OutPutLoadCurrentF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车输出充电电流, f => assist.OutpoutChargeCurrentF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车输出充电电流, f => assist.OutpoutChargeCurrentF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车蓄电池温度, f => assist.AccumulatorF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车蓄电池温度, f => assist.AccumulatorF006 = f);
        }
    }
}