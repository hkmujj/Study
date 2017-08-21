using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Models.States;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    ///     空调状态 适配
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class AirConditionStatusAdapter : Adapterbase
    {
        /// <summary>
        ///     数据变化
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, float> args)
        {
            args.UpdateIfContain(InFloatKeys.InFTC1车车外温度, f => Model.AirConditionModel.CarTemperatureF001 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1车车外温度, f => Model.AirConditionModel.CarTemperatureF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车车外温度, f => Model.AirConditionModel.CarTemperatureF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车车外温度, f => Model.AirConditionModel.CarTemperatureF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车车外温度, f => Model.AirConditionModel.CarTemperatureF005 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车车外温度, f => Model.AirConditionModel.CarTemperatureF006 = f);
        }

        /// <summary>
        ///     数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var air = Model.AirConditionModel;
            args.UpdateAllTrue(
                new Dictionary<string, Action<bool>>
                {
                    {InBoolKeys.InBoTC1车控制模式本地控制, b => air.ControlModelF001 = AirControlModel.CurrentControl},
                    {InBoolKeys.InBoTC1车控制模式集控自动制冷, b => air.ControlModelF001 = AirControlModel.CollectCOntrol}
                },
                () => air.ControlModelF001 = AirControlModel.Normal);
            args.UpdateAllTrue(
                new Dictionary<string, Action<bool>>
                {
                    {InBoolKeys.InBoMP1车控制模式本地控制, b => air.ControlModelF002 = AirControlModel.CurrentControl},
                    {InBoolKeys.InBoMP1车控制模式集控自动制冷, b => air.ControlModelF002 = AirControlModel.CollectCOntrol}
                },
                () => air.ControlModelF002 = AirControlModel.Normal);
            args.UpdateAllTrue(
                new Dictionary<string, Action<bool>>
                {
                    {InBoolKeys.InBoM1车控制模式本地控制, b => air.ControlModelF003 = AirControlModel.CurrentControl},
                    {InBoolKeys.InBoM1车控制模式集控自动制冷, b => air.ControlModelF003 = AirControlModel.CollectCOntrol}
                },
                () => air.ControlModelF003 = AirControlModel.Normal);
            args.UpdateAllTrue(
                new Dictionary<string, Action<bool>>
                {
                    {InBoolKeys.InBoM2车控制模式本地控制, b => air.ControlModelF004 = AirControlModel.CurrentControl},
                    {InBoolKeys.InBoM2车控制模式集控自动制冷, b => air.ControlModelF004 = AirControlModel.CollectCOntrol}
                },
                () => air.ControlModelF004 = AirControlModel.Normal);
            args.UpdateAllTrue(
                new Dictionary<string, Action<bool>>
                {
                    {InBoolKeys.InBoMP2车控制模式本地控制, b => air.ControlModelF005 = AirControlModel.CurrentControl},
                    {InBoolKeys.InBoMP2车控制模式集控自动制冷, b => air.ControlModelF005 = AirControlModel.CollectCOntrol}
                },
                () => air.ControlModelF005 = AirControlModel.Normal);
            args.UpdateAllTrue(
                new Dictionary<string, Action<bool>>
                {
                    {InBoolKeys.InBoTC2车控制模式本地控制, b => air.ControlModelF006 = AirControlModel.CurrentControl},
                    {InBoolKeys.InBoTC2车控制模式集控自动制冷, b => air.ControlModelF006 = AirControlModel.CollectCOntrol}
                },
                () => air.ControlModelF006 = AirControlModel.Normal);

            args.UpdateAllTrue(new Dictionary<string, Action<bool>>() { { InBoolKeys.InBoTC1车运行模式自动制冷, b => air.RunModelF001 = AirRunModel.Auto } }, () => { air.RunModelF001 = AirRunModel.Normal; });
            args.UpdateAllTrue(new Dictionary<string, Action<bool>>() { { InBoolKeys.InBoMP1车运行模式自动制冷, b => air.RunModelF002 = AirRunModel.Auto } }, () => { air.RunModelF002 = AirRunModel.Normal; });
            args.UpdateAllTrue(new Dictionary<string, Action<bool>>() { { InBoolKeys.InBoM1车运行模式自动制冷, b => air.RunModelF003 = AirRunModel.Auto } }, () => { air.RunModelF003 = AirRunModel.Normal; });
            args.UpdateAllTrue(new Dictionary<string, Action<bool>>() { { InBoolKeys.InBoM2车运行模式自动制冷, b => air.RunModelF004 = AirRunModel.Auto } }, () => { air.RunModelF004 = AirRunModel.Normal; });
            args.UpdateAllTrue(new Dictionary<string, Action<bool>>() { { InBoolKeys.InBoMP2车运行模式自动制冷, b => air.RunModelF005 = AirRunModel.Auto } }, () => { air.RunModelF005 = AirRunModel.Normal; });
            args.UpdateAllTrue(new Dictionary<string, Action<bool>>() { { InBoolKeys.InBoTC2车运行模式自动制冷, b => air.RunModelF006 = AirRunModel.Auto } }, () => { air.RunModelF006 = AirRunModel.Normal; });

            air.AllOpenStatus.ForEach(f => f.Changed(args));
        }
    }
}