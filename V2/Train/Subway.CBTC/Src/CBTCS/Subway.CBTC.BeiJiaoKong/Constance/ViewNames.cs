using Subway.CBTC.BeiJiaoKong.Views.Root;
using Subway.CBTC.BeiJiaoKong.Views.Shell;

namespace Subway.CBTC.BeiJiaoKong.Constance
{
    /// <summary>
    /// 视图名称
    /// </summary>
    public class ViewNames
    {
        /// <summary>
        /// 主页面
        /// </summary>
        public static readonly string MainView = typeof(MainView).FullName;

        /// <summary>
        /// 设置初始界面
        /// </summary>
        public static readonly string SettingMenuView = typeof(SettingMenuView).FullName;

        /// <summary>
        /// 司机号输入界面
        /// </summary>
        public static readonly string DriverNumberInputView = typeof(DriverNumberInputView).FullName;

        /// <summary>
        /// 日检界面
        /// </summary>
        public static readonly string DailyTestView = typeof(DailyTestView).FullName;

        /// <summary>
        /// 屏保界面
        /// </summary>
        public static readonly string ScreenSaver = typeof(ScreenSaver).FullName;
    }
}