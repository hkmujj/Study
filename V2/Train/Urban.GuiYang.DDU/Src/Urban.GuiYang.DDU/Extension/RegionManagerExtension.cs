using System;
using Microsoft.Practices.Prism.Regions;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.View.Contents;

namespace Urban.GuiYang.DDU.Extension
{
    internal static class RegionManagerExtension
    {
        /// <summary>
        /// 导航内容，包含Train
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="contentContentContentViewType"></param>
        public static void RequestNavigateToContentContent(this IRegionManager regionManager, Type contentContentContentViewType)
        {
            regionManager.RequestNavigate(RegionNames.ContentContent, typeof(ContentContentLayout).FullName);
            regionManager.RequestNavigate(RegionNames.ContentContentAll, typeof(ContentShell1).FullName);
            regionManager.RequestNavigate(RegionNames.ContentContentContent, contentContentContentViewType.FullName);
        }

        /// <summary>
        /// 导航内容，不需要 Train
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="contentViewType"></param>
        public static void RequestNavigateToContent(this IRegionManager regionManager, Type contentViewType)
        {
            regionManager.RequestNavigate(RegionNames.ContentContentAll, contentViewType.FullName);
        }
    }
}