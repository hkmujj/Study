using System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Subway.TCMS.LanZhou.Constant;
using Subway.TCMS.LanZhou.Event;

namespace Subway.TCMS.LanZhou.Extension
{
    internal static class RegionManagerExtension
    {
        /// <summary>
        /// 导航内容
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="contentViewType"></param>
        public static void RequestNavigateToContent(this IRegionManager regionManager, Type contentViewType)
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<ViewChangedEvent>()
                .Publish(new ViewChangedEvent.Args(contentViewType));
            regionManager.RequestNavigate(RegionNames.ContentContent, contentViewType.FullName);
        }
    }
}
