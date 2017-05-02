using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// 制动自检 转换
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class BrakeAutoCheckAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var brake = Model.BrakeAutoCheckModel;
            args.UpdateIfContain(InBoolKeys.InBoTC1制动隔离1, b => brake.BrakeIsolationF0011 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP1制动隔离1, b => brake.BrakeIsolationF0021 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1制动隔离1, b => brake.BrakeIsolationF0031 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2制动隔离1, b => brake.BrakeIsolationF0041 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2制动隔离1, b => brake.BrakeIsolationF0051 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2制动隔离1, b => brake.BrakeIsolationF0061 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1制动隔离2, b => brake.BrakeIsolationF0012 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP1制动隔离2, b => brake.BrakeIsolationF0022 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1制动隔离2, b => brake.BrakeIsolationF0032 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2制动隔离2, b => brake.BrakeIsolationF0042 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2制动隔离2, b => brake.BrakeIsolationF0052 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2制动隔离2, b => brake.BrakeIsolationF0062 = b);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBo制动自检终止, b => brake.IsAutoChecking = false);
        }

        /// <summary>
        /// 数据变化
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, float> args)
        {
            var brake = Model.BrakeAutoCheckModel;

            args.UpdateIfContain(InFloatKeys.InFTC1空气簧压力1, f => brake.AirSpringF0011 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1空气簧压力1, f => brake.AirSpringF0021 = f);
            args.UpdateIfContain(InFloatKeys.InFM1空气簧压力1, f => brake.AirSpringF0031 = f);
            args.UpdateIfContain(InFloatKeys.InFM2空气簧压力1, f => brake.AirSpringF0041 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2空气簧压力1, f => brake.AirSpringF0051 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2空气簧压力1, f => brake.AirSpringF0061 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1空气簧压力2, f => brake.AirSpringF0012 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1空气簧压力2, f => brake.AirSpringF0022 = f);
            args.UpdateIfContain(InFloatKeys.InFM1空气簧压力2, f => brake.AirSpringF0032 = f);
            args.UpdateIfContain(InFloatKeys.InFM2空气簧压力2, f => brake.AirSpringF0042 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2空气簧压力2, f => brake.AirSpringF0052 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2空气簧压力2, f => brake.AirSpringF0062 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1制动缸压力压力1, f => brake.BrakeF0011 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1制动缸压力压力1, f => brake.BrakeF0021 = f);
            args.UpdateIfContain(InFloatKeys.InFM1制动缸压力压力1, f => brake.BrakeF0031 = f);
            args.UpdateIfContain(InFloatKeys.InFM2制动缸压力压力1, f => brake.BrakeF0041 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2制动缸压力压力1, f => brake.BrakeF0051 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2制动缸压力压力1, f => brake.BrakeF0061 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1制动缸压力压力2, f => brake.BrakeF0012 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1制动缸压力压力2, f => brake.BrakeF0022 = f);
            args.UpdateIfContain(InFloatKeys.InFM1制动缸压力压力2, f => brake.BrakeF0032 = f);
            args.UpdateIfContain(InFloatKeys.InFM2制动缸压力压力2, f => brake.BrakeF0042 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2制动缸压力压力2, f => brake.BrakeF0052 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2制动缸压力压力2, f => brake.BrakeF0062 = f);
        }
    }
}