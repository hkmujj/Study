using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// 旁路数据适配
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class BypasssModelAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var bypass = Model.BypassModel;
            args.UpdateIfContain(InBoolKeys.InBoTC1门零速旁路, b => bypass.ZeroF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2门零速旁路, b => bypass.ZeroF006 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1司机室门旁路, b => bypass.DriveDoorF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2司机室门旁路, b => bypass.DriveDoorF006 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1总风压力可用旁路, b => bypass.TotalFanF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2总风压力可用旁路, b => bypass.TotalFanF006 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1运行升弓旁路, b => bypass.PantographF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2运行升弓旁路, b => bypass.PantographF006 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1警惕按钮旁路, b => bypass.VigilantF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2警惕按钮旁路, b => bypass.VigilantF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1门关好旁路, b => bypass.DoorF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2门关好旁路, b => bypass.DoorF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1停放制动旁路, b => bypass.ParkingF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2停放制动旁路, b => bypass.ParkingF006 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1ATC切除, b => bypass.ATCCutF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2ATC切除, b => bypass.ATCCutF006 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC1所有制动缓解旁路, b => bypass.BrakeF001 = b);
            args.UpdateIfContain(InBoolKeys.InBoTC2所有制动缓解旁路, b => bypass.BrakeF006 = b);
        }
    }
}