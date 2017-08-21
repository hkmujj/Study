using System.ComponentModel;

namespace Motor.ATP.Domain.Interface.Infomation
{
    /// <summary>
    /// 显示类型
    /// </summary>
    public enum InfomationShowType
    {
        [Description("普通文本显示")]
        Normal,

        [Description("带黄色闪框")]
        Flash,

        [Description("带弹出框")]
        Ensure,

        [Description("带黄色闪框和弹出框")]
        FlashAndEnsure,
    }

    public static class InfomationShowTypeExtionstion
    {
        /// <summary>
        /// 是否有弹出框 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool HasPopupView(this InfomationShowType type)
        {
            return type == InfomationShowType.Ensure ||
                   type == InfomationShowType.FlashAndEnsure;
        }
    }
}