using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface.Infomation
{
    /// <summary>
    /// 消息自动清除类型
    /// </summary>
    public enum InfomationAutoDeleteType
    {
        /// <summary>
        /// 永不    
        /// </summary>
        [Description("永不")]
        Never,

        /// <summary>
        /// 确定后        
        ///  </summary>
        [Description("确定后")]
        AfterOk,

        /// <summary>
        /// 取消后        
        /// </summary>
        [Description("取消后")]
        AfterCancel,

        /// <summary>
        /// 确定或取消后
        /// </summary>
        [Description("确定或取消后")]
        AfterOkOrCancel,

        /// <summary>
        /// ID对应的值为0时
        /// </summary>
        [Description("ID对应的值为0时")]
        IndexValueReturnZero,
    }

    /// <summary>
    /// 
    /// </summary>
    public static class InfomationAutoDeleteTypeExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool DeleteAfterOk(this InfomationAutoDeleteType type)
        {
            return type == InfomationAutoDeleteType.AfterOk || type == InfomationAutoDeleteType.AfterOkOrCancel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool DeleteAfterCancel(this InfomationAutoDeleteType type)
        {
            return type == InfomationAutoDeleteType.AfterCancel || type == InfomationAutoDeleteType.AfterOkOrCancel;
        }
    }
}