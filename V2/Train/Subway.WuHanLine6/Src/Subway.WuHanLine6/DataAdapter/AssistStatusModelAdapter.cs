using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// ����ϵͳ״̬ ����
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class AssistStatusModelAdapter : Adapterbase
    {
        /// <summary>
        /// ���ݱ任
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var assist = Model.AssistStatusModel;

            args.UpdateIfContain(InBoolKeys.InBoTC1������ϵͳ״̬ͨ���쳣, b => assist.AssistStatusF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2������ϵͳ״̬ͨ���쳣, b => assist.AssistStatusF006 = b);

            args.UpdateIfContain(InBoolKeys.InBoTC1������ϵͳ״̬ͨ���쳣, b => assist.DynamoStatusF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2������ϵͳ״̬ͨ���쳣, b => assist.DynamoStatusF006 = b);
        }

        /// <summary>
        /// ���ݱ仯
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, float> args)
        {
            var assist = Model.AssistStatusModel;

            args.UpdateIfContain(InFloatKeys.InFTC1�������ѹ, f => assist.AssistInputVoltageF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2�������ѹ, f => assist.AssistInputVoltageF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1��ĸ�ߵ�ѹ, f => assist.AssistGeneratrixVoltageF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2��ĸ�ߵ�ѹ, f => assist.AssistGeneratrixVoltageF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1�������ѹ, f => assist.AssistOutputVoltageF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2�������ѹ, f => assist.AssistOutputVoltageF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1�������ܺ�, f => assist.AssistEnergyF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2�������ܺ�, f => assist.AssistEnergyF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1�����Ƶ��, f => assist.AssistOutputRateF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2�����Ƶ��, f => assist.AssistOutputRateF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1���������, f => assist.AssistOutputCurrentF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2���������, f => assist.AssistOutputCurrentF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1���������, f => assist.AssistInputCurrentF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2���������, f => assist.AssistInputCurrentF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1������ĸ�ߵ�ѹ, f => assist.DynamoGeneratrixVoltageF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2������ĸ�ߵ�ѹ, f => assist.DynamoGeneratrixVoltageF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1�����������ѹ, f => assist.DynamoOutputVoltageF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2�����������ѹ, f => assist.DynamoOutputVoltageF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1��������ص���, f => assist.OutPutLoadCurrentF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2��������ص���, f => assist.OutPutLoadCurrentF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1�����������, f => assist.OutpoutChargeCurrentF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2�����������, f => assist.OutpoutChargeCurrentF006 = f);

            args.UpdateIfContain(InFloatKeys.InFTC1�������¶�, f => assist.AccumulatorF001 = f);
            args.UpdateIfContain(InFloatKeys.InFTC2�������¶�, f => assist.AccumulatorF006 = f);
        }
    }
}