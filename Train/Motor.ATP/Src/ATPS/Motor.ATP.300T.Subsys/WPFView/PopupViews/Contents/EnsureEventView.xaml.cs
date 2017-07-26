using System;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._300T.Subsys.Constant;
using Motor.ATP._300T.Subsys.ViewModel.PopupViewModels;

namespace Motor.ATP._300T.Subsys.WPFView.PopupViews.Contents
{
    /// <summary>
    /// EnsureEventView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionPopupViewContent)]
    public partial class EnsureEventView : INavigationAware
    {
        public EnsureEventView()
        {
            InitializeComponent();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                var content =
                    ServiceLocator.Current.GetInstance(
                        GetType().Assembly.GetType(navigationContext.Parameters["ViewModelClassFullName"])) as IContentOfEnsurce;

                DataContext = content;

            }
            catch (Exception e)
            {
                AppLog.Error("Can not set rtf to EnsureEventView , {0}", e);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
