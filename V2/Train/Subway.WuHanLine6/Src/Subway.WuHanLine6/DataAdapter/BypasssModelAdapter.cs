using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// ��·��������
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class BypasssModelAdapter : Adapterbase
    {
        /// <summary>
        /// ���ݱ任
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var bypass = Model.BypassModel;
            args.UpdateIfContain(InBoolKeys.InBoTC1��������·, b => bypass.ZeroF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2��������·, b => bypass.ZeroF006 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1˾��������·, b => bypass.DriveDoorF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2˾��������·, b => bypass.DriveDoorF006 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1�ܷ�ѹ��������·, b => bypass.TotalFanF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2�ܷ�ѹ��������·, b => bypass.TotalFanF006 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1����������·, b => bypass.PantographF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2����������·, b => bypass.PantographF006 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1���谴ť��·, b => bypass.VigilantF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2���谴ť��·, b => bypass.VigilantF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1�Źغ���·, b => bypass.DoorF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2�Źغ���·, b => bypass.DoorF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1ͣ���ƶ���·, b => bypass.ParkingF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2ͣ���ƶ���·, b => bypass.ParkingF006 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1ATC�г�, b => bypass.ATCCutF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2ATC�г�, b => bypass.ATCCutF006 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1�����ƶ�������·, b => bypass.BrakeF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2�����ƶ�������·, b => bypass.BrakeF006 = b);
        }
    }
}