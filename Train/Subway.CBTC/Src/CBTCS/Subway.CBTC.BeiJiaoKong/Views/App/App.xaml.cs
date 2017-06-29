using System.Windows;

namespace Subway.CBTC.BeiJiaoKong.Views.App
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// 引发 <see cref="E:System.Windows.Application.Startup"/> 事件。
        /// </summary>
        /// <param name="e">一个包含事件数据的 <see cref="T:System.Windows.StartupEventArgs"/>。</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var boot = new BeiJiaoKongBootStarpter();
            boot.Run();
        }
    }
}
