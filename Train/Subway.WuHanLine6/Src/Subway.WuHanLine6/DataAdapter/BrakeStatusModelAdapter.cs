using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// �ƶ�״̬��������
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class BrakeStatusModelAdapter : Adapterbase
    {
        /// <summary>
        /// ���ݱ任
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var brake = Model.BrakeStatusModel;

            args.UpdateIfContain(InBoolKeys.InBoTC1��ת���1����, b => brake.BogieF0011 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP1��ת���1����, b => brake.BogieF0021 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1��ת���1����, b => brake.BogieF0031 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2��ת���1����, b => brake.BogieF0041 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2��ת���1����, b => brake.BogieF0051 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2��ת���1����, b => brake.BogieF0061 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1��ת���2����, b => brake.BogieF0012 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP1��ת���2����, b => brake.BogieF0022 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1��ת���2����, b => brake.BogieF0032 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2��ת���2����, b => brake.BogieF0042 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2��ת���2����, b => brake.BogieF0052 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2��ת���2����, b => brake.BogieF0062 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1���ƶ�1ʩ��, b => brake.BrakeInflictionF0011 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP1���ƶ�1ʩ��, b => brake.BrakeInflictionF0021 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1���ƶ�1ʩ��, b => brake.BrakeInflictionF0031 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2���ƶ�1ʩ��, b => brake.BrakeInflictionF0041 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2���ƶ�1ʩ��, b => brake.BrakeInflictionF0051 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2���ƶ�1ʩ��, b => brake.BrakeInflictionF0061 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1���ƶ�2ʩ��, b => brake.BrakeInflictionF0012 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP1���ƶ�2ʩ��, b => brake.BrakeInflictionF0022 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1���ƶ�2ʩ��, b => brake.BrakeInflictionF0032 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2���ƶ�2ʩ��, b => brake.BrakeInflictionF0042 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2���ƶ�2ʩ��, b => brake.BrakeInflictionF0052 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2���ƶ�2ʩ��, b => brake.BrakeInflictionF0062 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1��ͣ���ƶ�ʩ��, b => brake.ParkingStatusF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP1��ͣ���ƶ�ʩ��, b => brake.ParkingStatusF002 = b);
            args.UpdateIfContain(InBoolKeys.InBoM1��ͣ���ƶ�ʩ��, b => brake.ParkingStatusF003 = b);
            args.UpdateIfContain(InBoolKeys.InBoM2��ͣ���ƶ�ʩ��, b => brake.ParkingStatusF004 = b);
            args.UpdateIfContain(InBoolKeys.InBoMP2��ͣ���ƶ�ʩ��, b => brake.ParkingStatusF005 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2��ͣ���ƶ�ʩ��, b => brake.ParkingStatusF006 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1��MP1��M1���Լ��ϲⳬ��24Сʱ, b => brake.AutoCheck24One = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2��MP2��M2���Լ��ϲⳬ��24Сʱ, b => brake.AutoCheck24Two = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1��MP1��M1�������ƶ���ʩ��, b => brake.KeepBrakeOne = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2��MP2��M2�������ƶ���ʩ��, b => brake.KeepBrakeTwo = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1��MP1��M1���Լ�ʱ��������26Сʱ, b => brake.AutoCheck26One = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2��MP2��M2���Լ�ʱ��������26Сʱ, b => brake.AutoCheck26Two = b);
        }

        /// <summary>
        /// ���ݱ仯
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, float> args)
        {
            var brake = Model.BrakeStatusModel;

            args.UpdateIfContain(InFloatKeys.InFTC1���غ�1, f => brake.LoadF0011 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1���غ�1, f => brake.LoadF0021 = f);
            args.UpdateIfContain(InFloatKeys.InFM1���غ�1, f => brake.LoadF0031 = f);
            args.UpdateIfContain(InFloatKeys.InFM2���غ�1, f => brake.LoadF0041 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2���غ�1, f => brake.LoadF0051 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2���غ�1, f => brake.LoadF0061 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1���غ�2, f => brake.LoadF0012 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1���غ�2, f => brake.LoadF0022 = f);
            args.UpdateIfContain(InFloatKeys.InFM1���غ�2, f => brake.LoadF0032 = f);
            args.UpdateIfContain(InFloatKeys.InFM2���غ�2, f => brake.LoadF0042 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2���غ�2, f => brake.LoadF0052 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2���غ�2, f => brake.LoadF0062 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1���ƶ�ѹ��1, f => brake.BrakeF0011 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1���ƶ�ѹ��1, f => brake.BrakeF0021 = f);
            args.UpdateIfContain(InFloatKeys.InFM1���ƶ�ѹ��1, f => brake.BrakeF0031 = f);
            args.UpdateIfContain(InFloatKeys.InFM2���ƶ�ѹ��1, f => brake.BrakeF0041 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2���ƶ�ѹ��1, f => brake.BrakeF0051 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2���ƶ�ѹ��1, f => brake.BrakeF0061 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1���ƶ�ѹ��2, f => brake.BrakeF0012 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1���ƶ�ѹ��2, f => brake.BrakeF0022 = f);
            args.UpdateIfContain(InFloatKeys.InFM1���ƶ�ѹ��2, f => brake.BrakeF0032 = f);
            args.UpdateIfContain(InFloatKeys.InFM2���ƶ�ѹ��2, f => brake.BrakeF0042 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2���ƶ�ѹ��2, f => brake.BrakeF0052 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2���ƶ�ѹ��2, f => brake.BrakeF0062 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1��ASPѹ��ֵ1, f => brake.APSPressureF0011 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1��ASPѹ��ֵ1, f => brake.APSPressureF0021 = f);
            args.UpdateIfContain(InFloatKeys.InFM1��ASPѹ��ֵ1, f => brake.APSPressureF0031 = f);
            args.UpdateIfContain(InFloatKeys.InFM2��ASPѹ��ֵ1, f => brake.APSPressureF0041 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2��ASPѹ��ֵ1, f => brake.APSPressureF0051 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2��ASPѹ��ֵ1, f => brake.APSPressureF0061 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1��ASPѹ��ֵ2, f => brake.APSPressureF0012 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1��ASPѹ��ֵ2, f => brake.APSPressureF0022 = f);
            args.UpdateIfContain(InFloatKeys.InFM1��ASPѹ��ֵ2, f => brake.APSPressureF0032 = f);
            args.UpdateIfContain(InFloatKeys.InFM2��ASPѹ��ֵ2, f => brake.APSPressureF0042 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2��ASPѹ��ֵ2, f => brake.APSPressureF0052 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2��ASPѹ��ֵ2, f => brake.APSPressureF0062 = f);

            args.UpdateIfContain(InFloatKeys.InFMP1�����ƶ��غ�2, f => brake.ElectricityBrakeF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1�����ƶ��غ�1, f => brake.ElectricityBrakeF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2�����ƶ��غ�1, f => brake.ElectricityBrakeF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2�����ƶ��غ�1, f => brake.ElectricityBrakeF005 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1�������ƶ���, f => brake.CommonBrakeF001 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1�������ƶ���, f => brake.CommonBrakeF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1�������ƶ���, f => brake.CommonBrakeF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2�������ƶ���, f => brake.CommonBrakeF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2�������ƶ���, f => brake.CommonBrakeF005 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2�������ƶ���, f => brake.CommonBrakeF006 = f);

            args.UpdateIfContain(InFloatKeys.InF�������ƶ���1, f => brake.TickingBrakeOne = f);
            args.UpdateIfContain(InFloatKeys.InF�������ƶ���2, f => brake.TickingBrakeTwo = f);

            args.UpdateIfContain(InFloatKeys.InF�ܷ��ѹ��1, f => brake.MasterPressureOne = f);
            args.UpdateIfContain(InFloatKeys.InF�ܷ��ѹ��2, f => brake.MasterPressureTwo = f);

            args.UpdateIfContain(InFloatKeys.InFTC1��1����ת�ٶ�, f => brake.Axle1SpeedF001 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1��1����ת�ٶ�, f => brake.Axle1SpeedF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1��1����ת�ٶ�, f => brake.Axle1SpeedF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2��1����ת�ٶ�, f => brake.Axle1SpeedF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2��1����ת�ٶ�, f => brake.Axle1SpeedF005 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2��1����ת�ٶ�, f => brake.Axle1SpeedF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1��2����ת�ٶ�, f => brake.Axle2SpeedF001 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1��2����ת�ٶ�, f => brake.Axle2SpeedF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1��2����ת�ٶ�, f => brake.Axle2SpeedF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2��2����ת�ٶ�, f => brake.Axle2SpeedF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2��2����ת�ٶ�, f => brake.Axle2SpeedF005 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2��2����ת�ٶ�, f => brake.Axle2SpeedF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1��3����ת�ٶ�, f => brake.Axle3SpeedF001 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1��3����ת�ٶ�, f => brake.Axle3SpeedF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1��3����ת�ٶ�, f => brake.Axle3SpeedF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2��3����ת�ٶ�, f => brake.Axle3SpeedF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2��3����ת�ٶ�, f => brake.Axle3SpeedF005 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2��3����ת�ٶ�, f => brake.Axle3SpeedF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1��4����ת�ٶ�, f => brake.Axle4SpeedF001 = f);
            args.UpdateIfContain(InFloatKeys.InFMP1��4����ת�ٶ�, f => brake.Axle4SpeedF002 = f);
            args.UpdateIfContain(InFloatKeys.InFM1��4����ת�ٶ�, f => brake.Axle4SpeedF003 = f);
            args.UpdateIfContain(InFloatKeys.InFM2��4����ת�ٶ�, f => brake.Axle4SpeedF004 = f);
            args.UpdateIfContain(InFloatKeys.InFMP2��4����ת�ٶ�, f => brake.Axle4SpeedF005 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2��4����ת�ٶ�, f => brake.Axle4SpeedF006 = f);
        }
    }
}