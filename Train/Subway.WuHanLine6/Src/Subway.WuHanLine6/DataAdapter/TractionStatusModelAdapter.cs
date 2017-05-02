using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// TractionStatusModel 适配
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class TractionStatusModelAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var traction = Model.TractionStatusModel;

            args.UpdateIfContain(InBoolKeys.InBoMP1车牵引系统状态通信异常, b => traction.TractionExceptionF002 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1车牵引系统状态通信异常, b => traction.TractionExceptionF003 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2车牵引系统状态通信异常, b => traction.TractionExceptionF004 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2车牵引系统状态通信异常, b => traction.TractionExceptionF005 = b);

            args.UpdateIfContain(InBoolKeys.InBoMP1车线路断路器1, b => traction.LineBrakerF0021 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1车线路断路器1, b => traction.LineBrakerF0031 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2车线路断路器1, b => traction.LineBrakerF0041 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2车线路断路器1, b => traction.LineBrakerF0051 = b);

            args.UpdateIfContain(InBoolKeys.InBoMP1车线路断路器2, b => traction.LineBrakerF0022 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1车线路断路器2, b => traction.LineBrakerF0032 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2车线路断路器2, b => traction.LineBrakerF0042 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2车线路断路器2, b => traction.LineBrakerF0052 = b);
        }

        /// <summary>
        /// 数据变化
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, float> args)
        {
            var traction = Model.TractionStatusModel;

            args.UpdateIfContain(InFloatKeys.InFMP1车实际电制力, f => traction.ActualElectricityF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车实际电制力, f => traction.ActualElectricityF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车实际电制力, f => traction.ActualElectricityF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车实际电制力, f => traction.ActualElectricityF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1车牵引力, f => traction.TarctionF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车牵引力, f => traction.TarctionF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车牵引力, f => traction.TarctionF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车牵引力, f => traction.TarctionF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1车电机电流, f => traction.ElectricityMachineF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车电机电流, f => traction.ElectricityMachineF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车电机电流, f => traction.ElectricityMachineF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车电机电流, f => traction.ElectricityMachineF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1车牵引能耗, f => traction.TractionEnergyF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车牵引能耗, f => traction.TractionEnergyF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车牵引能耗, f => traction.TractionEnergyF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车牵引能耗, f => traction.TractionEnergyF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1车再生制动能耗, f => traction.RebrithBrakeF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车再生制动能耗, f => traction.RebrithBrakeF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车再生制动能耗, f => traction.RebrithBrakeF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车再生制动能耗, f => traction.RebrithBrakeF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1车电网电压, f => traction.ElectricityNetF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车电网电压, f => traction.ElectricityNetF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车电网电压, f => traction.ElectricityNetF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车电网电压, f => traction.ElectricityNetF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1车滤波器电容电压, f => traction.FliterF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车滤波器电容电压, f => traction.FliterF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车滤波器电容电压, f => traction.FliterF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车滤波器电容电压, f => traction.FliterF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1车电阻制动能耗, f => traction.ResistanceBrakeF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1车电阻制动能耗, f => traction.ResistanceBrakeF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2车电阻制动能耗, f => traction.ResistanceBrakeF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2车电阻制动能耗, f => traction.ResistanceBrakeF005 = f);
        }
    }
}