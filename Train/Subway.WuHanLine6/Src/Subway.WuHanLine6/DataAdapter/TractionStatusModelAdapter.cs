using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// TractionStatusModel ����
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class TractionStatusModelAdapter : Adapterbase
    {
        /// <summary>
        /// ���ݱ任
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var traction = Model.TractionStatusModel;

            args.UpdateIfContain(InBoolKeys.InBoMP1��ǣ��ϵͳ״̬ͨ���쳣, b => traction.TractionExceptionF002 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1��ǣ��ϵͳ״̬ͨ���쳣, b => traction.TractionExceptionF003 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2��ǣ��ϵͳ״̬ͨ���쳣, b => traction.TractionExceptionF004 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2��ǣ��ϵͳ״̬ͨ���쳣, b => traction.TractionExceptionF005 = b);

            args.UpdateIfContain(InBoolKeys.InBoMP1����·��·��1, b => traction.LineBrakerF0021 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1����·��·��1, b => traction.LineBrakerF0031 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2����·��·��1, b => traction.LineBrakerF0041 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2����·��·��1, b => traction.LineBrakerF0051 = b);

            args.UpdateIfContain(InBoolKeys.InBoMP1����·��·��2, b => traction.LineBrakerF0022 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1����·��·��2, b => traction.LineBrakerF0032 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2����·��·��2, b => traction.LineBrakerF0042 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2����·��·��2, b => traction.LineBrakerF0052 = b);
        }

        /// <summary>
        /// ���ݱ仯
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, float> args)
        {
            var traction = Model.TractionStatusModel;

            args.UpdateIfContain(InFloatKeys.InFMP1��ʵ�ʵ�����, f => traction.ActualElectricityF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1��ʵ�ʵ�����, f => traction.ActualElectricityF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2��ʵ�ʵ�����, f => traction.ActualElectricityF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2��ʵ�ʵ�����, f => traction.ActualElectricityF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1��ǣ����, f => traction.TarctionF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1��ǣ����, f => traction.TarctionF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2��ǣ����, f => traction.TarctionF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2��ǣ����, f => traction.TarctionF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1���������, f => traction.ElectricityMachineF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1���������, f => traction.ElectricityMachineF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2���������, f => traction.ElectricityMachineF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2���������, f => traction.ElectricityMachineF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1��ǣ���ܺ�, f => traction.TractionEnergyF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1��ǣ���ܺ�, f => traction.TractionEnergyF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2��ǣ���ܺ�, f => traction.TractionEnergyF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2��ǣ���ܺ�, f => traction.TractionEnergyF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1�������ƶ��ܺ�, f => traction.RebrithBrakeF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1�������ƶ��ܺ�, f => traction.RebrithBrakeF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2�������ƶ��ܺ�, f => traction.RebrithBrakeF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2�������ƶ��ܺ�, f => traction.RebrithBrakeF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1��������ѹ, f => traction.ElectricityNetF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1��������ѹ, f => traction.ElectricityNetF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2��������ѹ, f => traction.ElectricityNetF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2��������ѹ, f => traction.ElectricityNetF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1���˲������ݵ�ѹ, f => traction.FliterF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1���˲������ݵ�ѹ, f => traction.FliterF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2���˲������ݵ�ѹ, f => traction.FliterF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2���˲������ݵ�ѹ, f => traction.FliterF005 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1�������ƶ��ܺ�, f => traction.ResistanceBrakeF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1�������ƶ��ܺ�, f => traction.ResistanceBrakeF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2�������ƶ��ܺ�, f => traction.ResistanceBrakeF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2�������ƶ��ܺ�, f => traction.ResistanceBrakeF005 = f);
        }
    }
}