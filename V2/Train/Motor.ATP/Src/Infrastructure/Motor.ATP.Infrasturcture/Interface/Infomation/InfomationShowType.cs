using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface.Infomation
{
    /// <summary>
    /// 显示类型
    /// </summary>
    public enum InfomationShowType
    {
        /// <summary>
        /// 普通文本显示
        /// </summary>
        [Description("普通文本显示")]
        Normal,

        /// <summary>
        /// 带黄色闪框
        /// </summary>
        [Description("带黄色闪框")]
        Flash,

        /// <summary>
        /// 带弹出框
        /// </summary>
        [Description("带弹出框")]
        Ensure,

        /// <summary>
        /// 带黄色闪框和弹出框
        /// </summary>
        [Description("带黄色闪框和弹出框")]
        FlashAndEnsure,
    }

    /// <summary>
    /// 
    /// </summary>
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