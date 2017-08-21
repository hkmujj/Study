using System.ComponentModel;

namespace Motor.ATP.Domain.Interface.Infomation
{
    public enum InfomationAutoDeleteType
    {
        [Description("永不")]
        Never,
        [Description("确定后")]
        AfterOk,
        [Description("取消后")]
        AfterCancel,
        [Description("确定或取消后")]
        AfterOkOrCancel,
        [Description("ID对应的值为0时")]
        IndexValueReturnZero,
    }

    public static class InfomationAutoDeleteTypeExtension
    {
        public static bool DeleteAfterOk(this InfomationAutoDeleteType type)
        {
            return type == InfomationAutoDeleteType.AfterOk || type == InfomationAutoDeleteType.AfterOkOrCancel;
        }

        public static bool DeleteAfterCancel(this InfomationAutoDeleteType type)
        {
            return type == InfomationAutoDeleteType.AfterCancel || type == InfomationAutoDeleteType.AfterOkOrCancel;
        }
    }
}