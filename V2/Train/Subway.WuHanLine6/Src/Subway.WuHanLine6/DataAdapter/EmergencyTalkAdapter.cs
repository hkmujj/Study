using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Models.States;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// 紧急对讲转换
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class EmergencyTalkAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var emerncy = Model.EmergencyTalkModel;
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车乘客报警1乘客紧急通讯单元故障, b => emerncy.StateF0011 = EmergencyTalkState.Fault);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车乘客报警1乘客紧急通讯单元正常未激活, b => emerncy.StateF0011 = EmergencyTalkState.Normal);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车乘客报警1乘客紧急通讯单元激活乘客要求紧急对讲, b => emerncy.StateF0011 = EmergencyTalkState.Active);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车乘客报警1乘客紧急通讯单元激活司机已打开通讯通道, b => emerncy.StateF0011 = EmergencyTalkState.DriveActive);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车乘客报警1乘客紧急通讯单元故障, b => emerncy.StateF0021 = EmergencyTalkState.Fault);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车乘客报警1乘客紧急通讯单元正常未激活, b => emerncy.StateF0021 = EmergencyTalkState.Normal);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车乘客报警1乘客紧急通讯单元激活乘客要求紧急对讲, b => emerncy.StateF0021 = EmergencyTalkState.Active);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车乘客报警1乘客紧急通讯单元激活司机已打开通讯通道, b => emerncy.StateF0021 = EmergencyTalkState.DriveActive);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车乘客报警1乘客紧急通讯单元故障, b => emerncy.StateF0031 = EmergencyTalkState.Fault);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车乘客报警1乘客紧急通讯单元正常未激活, b => emerncy.StateF0031 = EmergencyTalkState.Normal);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车乘客报警1乘客紧急通讯单元激活乘客要求紧急对讲, b => emerncy.StateF0031 = EmergencyTalkState.Active);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车乘客报警1乘客紧急通讯单元激活司机已打开通讯通道, b => emerncy.StateF0031 = EmergencyTalkState.DriveActive);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车乘客报警1乘客紧急通讯单元故障, b => emerncy.StateF0041 = EmergencyTalkState.Fault);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车乘客报警1乘客紧急通讯单元正常未激活, b => emerncy.StateF0041 = EmergencyTalkState.Normal);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车乘客报警1乘客紧急通讯单元激活乘客要求紧急对讲, b => emerncy.StateF0041 = EmergencyTalkState.Active);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车乘客报警1乘客紧急通讯单元激活司机已打开通讯通道, b => emerncy.StateF0041 = EmergencyTalkState.DriveActive);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车乘客报警1乘客紧急通讯单元故障, b => emerncy.StateF0051 = EmergencyTalkState.Fault);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车乘客报警1乘客紧急通讯单元正常未激活, b => emerncy.StateF0051 = EmergencyTalkState.Normal);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车乘客报警1乘客紧急通讯单元激活乘客要求紧急对讲, b => emerncy.StateF0051 = EmergencyTalkState.Active);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车乘客报警1乘客紧急通讯单元激活司机已打开通讯通道, b => emerncy.StateF0051 = EmergencyTalkState.DriveActive);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车乘客报警1乘客紧急通讯单元故障, b => emerncy.StateF0061 = EmergencyTalkState.Fault);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车乘客报警1乘客紧急通讯单元正常未激活, b => emerncy.StateF0061 = EmergencyTalkState.Normal);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车乘客报警1乘客紧急通讯单元激活乘客要求紧急对讲, b => emerncy.StateF0061 = EmergencyTalkState.Active);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车乘客报警1乘客紧急通讯单元激活司机已打开通讯通道, b => emerncy.StateF0061 = EmergencyTalkState.DriveActive);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车乘客报警2乘客紧急通讯单元故障, b => emerncy.StateF0012 = EmergencyTalkState.Fault);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车乘客报警2乘客紧急通讯单元正常未激活, b => emerncy.StateF0012 = EmergencyTalkState.Normal);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车乘客报警2乘客紧急通讯单元激活乘客要求紧急对讲, b => emerncy.StateF0012 = EmergencyTalkState.Active);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC1车乘客报警2乘客紧急通讯单元激活司机已打开通讯通道, b => emerncy.StateF0012 = EmergencyTalkState.DriveActive);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车乘客报警2乘客紧急通讯单元故障, b => emerncy.StateF0022 = EmergencyTalkState.Fault);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车乘客报警2乘客紧急通讯单元正常未激活, b => emerncy.StateF0022 = EmergencyTalkState.Normal);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车乘客报警2乘客紧急通讯单元激活乘客要求紧急对讲, b => emerncy.StateF0022 = EmergencyTalkState.Active);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车乘客报警2乘客紧急通讯单元激活司机已打开通讯通道, b => emerncy.StateF0022 = EmergencyTalkState.DriveActive);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车乘客报警2乘客紧急通讯单元故障, b => emerncy.StateF0032 = EmergencyTalkState.Fault);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车乘客报警2乘客紧急通讯单元正常未激活, b => emerncy.StateF0032 = EmergencyTalkState.Normal);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车乘客报警2乘客紧急通讯单元激活乘客要求紧急对讲, b => emerncy.StateF0032 = EmergencyTalkState.Active);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM1车乘客报警2乘客紧急通讯单元激活司机已打开通讯通道, b => emerncy.StateF0032 = EmergencyTalkState.DriveActive);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车乘客报警2乘客紧急通讯单元故障, b => emerncy.StateF0042 = EmergencyTalkState.Fault);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车乘客报警2乘客紧急通讯单元正常未激活, b => emerncy.StateF0042 = EmergencyTalkState.Normal);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车乘客报警2乘客紧急通讯单元激活乘客要求紧急对讲, b => emerncy.StateF0042 = EmergencyTalkState.Active);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoM2车乘客报警2乘客紧急通讯单元激活司机已打开通讯通道, b => emerncy.StateF0042 = EmergencyTalkState.DriveActive);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车乘客报警2乘客紧急通讯单元故障, b => emerncy.StateF0052 = EmergencyTalkState.Fault);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车乘客报警2乘客紧急通讯单元正常未激活, b => emerncy.StateF0052 = EmergencyTalkState.Normal);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车乘客报警2乘客紧急通讯单元激活乘客要求紧急对讲, b => emerncy.StateF0052 = EmergencyTalkState.Active);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车乘客报警2乘客紧急通讯单元激活司机已打开通讯通道, b => emerncy.StateF0052 = EmergencyTalkState.DriveActive);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车乘客报警2乘客紧急通讯单元故障, b => emerncy.StateF0062 = EmergencyTalkState.Fault);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车乘客报警2乘客紧急通讯单元正常未激活, b => emerncy.StateF0062 = EmergencyTalkState.Normal);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车乘客报警2乘客紧急通讯单元激活乘客要求紧急对讲, b => emerncy.StateF0062 = EmergencyTalkState.Active);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoTC2车乘客报警2乘客紧急通讯单元激活司机已打开通讯通道, b => emerncy.StateF0062 = EmergencyTalkState.DriveActive);
        }
    }
}