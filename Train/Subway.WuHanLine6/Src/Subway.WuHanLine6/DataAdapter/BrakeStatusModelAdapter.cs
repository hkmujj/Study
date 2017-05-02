using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// 制动状态数据适配
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class BrakeStatusModelAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var brake = Model.BrakeStatusModel;

            args.UpdateIfContain(InBoolKeys.InBoTC1车转向架1隔离, b => brake.BogieF0011 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP1车转向架1隔离, b => brake.BogieF0021 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1车转向架1隔离, b => brake.BogieF0031 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2车转向架1隔离, b => brake.BogieF0041 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2车转向架1隔离, b => brake.BogieF0051 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2车转向架1隔离, b => brake.BogieF0061 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1车转向架2隔离, b => brake.BogieF0012 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP1车转向架2隔离, b => brake.BogieF0022 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1车转向架2隔离, b => brake.BogieF0032 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2车转向架2隔离, b => brake.BogieF0042 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2车转向架2隔离, b => brake.BogieF0052 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2车转向架2隔离, b => brake.BogieF0062 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1车制动1施加, b => brake.BrakeInflictionF0011 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP1车制动1施加, b => brake.BrakeInflictionF0021 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1车制动1施加, b => brake.BrakeInflictionF0031 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2车制动1施加, b => brake.BrakeInflictionF0041 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2车制动1施加, b => brake.BrakeInflictionF0051 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2车制动1施加, b => brake.BrakeInflictionF0061 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1车制动2施加, b => brake.BrakeInflictionF0012 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP1车制动2施加, b => brake.BrakeInflictionF0022 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1车制动2施加, b => brake.BrakeInflictionF0032 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2车制动2施加, b => brake.BrakeInflictionF0042 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2车制动2施加, b => brake.BrakeInflictionF0052 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2车制动2施加, b => brake.BrakeInflictionF0062 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1车停放制动施加, b => brake.ParkingStatusF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP1车停放制动施加, b => brake.ParkingStatusF002 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1车停放制动施加, b => brake.ParkingStatusF003 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2车停放制动施加, b => brake.ParkingStatusF004 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2车停放制动施加, b => brake.ParkingStatusF005 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2车停放制动施加, b => brake.ParkingStatusF006 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1车MP1车M1车自检间断测超过24小时, b => brake.AutoCheck24One = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2车MP2车M2车自检间断测超过24小时, b => brake.AutoCheck24Two = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1车MP1车M1车保持制动已施加, b => brake.KeepBrakeOne = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2车MP2车M2车保持制动已施加, b => brake.KeepBrakeTwo = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1车MP1车M1车自检时间间隔超过26小时, b => brake.AutoCheck26One = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2车MP2车M2车自检时间间隔超过26小时, b => brake.AutoCheck26Two = b);
        }

        /// <summary>
        /// 数据变化
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, float> args)
        {
            var brake = Model.BrakeStatusModel;

            args.UpdateIfContain(InFloatKeys.InFTC1车载荷1, f => brake.LoadF0011 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1车载荷1, f => brake.LoadF0021 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车载荷1, f => brake.LoadF0031 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车载荷1, f => brake.LoadF0041 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车载荷1, f => brake.LoadF0051 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车载荷1, f => brake.LoadF0061 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车载荷2, f => brake.LoadF0012 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1车载荷2, f => brake.LoadF0022 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车载荷2, f => brake.LoadF0032 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车载荷2, f => brake.LoadF0042 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车载荷2, f => brake.LoadF0052 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车载荷2, f => brake.LoadF0062 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车制动压力1, f => brake.BrakeF0011 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1车制动压力1, f => brake.BrakeF0021 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车制动压力1, f => brake.BrakeF0031 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车制动压力1, f => brake.BrakeF0041 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车制动压力1, f => brake.BrakeF0051 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车制动压力1, f => brake.BrakeF0061 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车制动压力2, f => brake.BrakeF0012 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1车制动压力2, f => brake.BrakeF0022 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车制动压力2, f => brake.BrakeF0032 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车制动压力2, f => brake.BrakeF0042 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车制动压力2, f => brake.BrakeF0052 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车制动压力2, f => brake.BrakeF0062 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车ASP压力值1, f => brake.APSPressureF0011 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1车ASP压力值1, f => brake.APSPressureF0021 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车ASP压力值1, f => brake.APSPressureF0031 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车ASP压力值1, f => brake.APSPressureF0041 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车ASP压力值1, f => brake.APSPressureF0051 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车ASP压力值1, f => brake.APSPressureF0061 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车ASP压力值2, f => brake.APSPressureF0012 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1车ASP压力值2, f => brake.APSPressureF0022 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车ASP压力值2, f => brake.APSPressureF0032 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车ASP压力值2, f => brake.APSPressureF0042 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车ASP压力值2, f => brake.APSPressureF0052 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车ASP压力值2, f => brake.APSPressureF0062 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1车电制动载荷2, f => brake.ElectricityBrakeF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车电制动载荷1, f => brake.ElectricityBrakeF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车电制动载荷1, f => brake.ElectricityBrakeF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车电制动载荷1, f => brake.ElectricityBrakeF005 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车常用制动力, f => brake.CommonBrakeF001 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1车常用制动力, f => brake.CommonBrakeF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车常用制动力, f => brake.CommonBrakeF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车常用制动力, f => brake.CommonBrakeF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车常用制动力, f => brake.CommonBrakeF005 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车常用制动力, f => brake.CommonBrakeF006 = f);

            args.UpdateIfContain(InFloatKeys.InF反馈总制动力1, f => brake.TickingBrakeOne = f);
            args.UpdateIfContain(InFloatKeys.InF反馈总制动力2, f => brake.TickingBrakeTwo = f);

            args.UpdateIfContain(InFloatKeys.InF总风管压力1, f => brake.MasterPressureOne = f);
            args.UpdateIfContain(InFloatKeys.InF总风管压力2, f => brake.MasterPressureTwo = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车1轴旋转速度, f => brake.Axle1SpeedF001 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1车1轴旋转速度, f => brake.Axle1SpeedF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车1轴旋转速度, f => brake.Axle1SpeedF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车1轴旋转速度, f => brake.Axle1SpeedF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车1轴旋转速度, f => brake.Axle1SpeedF005 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车1轴旋转速度, f => brake.Axle1SpeedF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车2轴旋转速度, f => brake.Axle2SpeedF001 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1车2轴旋转速度, f => brake.Axle2SpeedF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车2轴旋转速度, f => brake.Axle2SpeedF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车2轴旋转速度, f => brake.Axle2SpeedF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车2轴旋转速度, f => brake.Axle2SpeedF005 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车2轴旋转速度, f => brake.Axle2SpeedF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车3轴旋转速度, f => brake.Axle3SpeedF001 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1车3轴旋转速度, f => brake.Axle3SpeedF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车3轴旋转速度, f => brake.Axle3SpeedF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车3轴旋转速度, f => brake.Axle3SpeedF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车3轴旋转速度, f => brake.Axle3SpeedF005 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车3轴旋转速度, f => brake.Axle3SpeedF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1车4轴旋转速度, f => brake.Axle4SpeedF001 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1车4轴旋转速度, f => brake.Axle4SpeedF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车4轴旋转速度, f => brake.Axle4SpeedF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车4轴旋转速度, f => brake.Axle4SpeedF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车4轴旋转速度, f => brake.Axle4SpeedF005 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2车4轴旋转速度, f => brake.Axle4SpeedF006 = f);
        }
    }
}