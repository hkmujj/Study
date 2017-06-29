using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Subway.CBTC.BeiJiaoKong.ViewModel
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    public abstract class ViewModelBase : NotificationObject
    {
        protected BeiJiaoKongViewModel Parent;
        static ViewModelBase()
        {
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
        }
        /// <summary>
        /// 事件聚合器
        /// </summary>
        protected static IEventAggregator EventAggregator { get; private set; }
        /// <summary>
        /// 区域管理器
        /// </summary>
        protected static IRegionManager RegionManager { get; private set; }
    }
}