using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.Interface.Data;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.Event;
using Subway.ShenZhenLine11.ShenZhenAttribute;
using Subway.ShenZhenLine11.ViewModels;

namespace Subway.ShenZhenLine11.Controller
{
    [Export(typeof(ShenZhengController))]
    public class ShenZhengController : ControllerBase<ShenZhenViewModel>
    {
        protected IEventAggregator EventAggregator;
        protected IRegionManager RegionManager;
        [ImportingConstructor]
        public ShenZhengController(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            RegionManager = regionManager;
            EventAggregator = eventAggregator;
            eventAggregator.GetEvent<DeactiveRegionEvent>().Subscribe((args) =>
            {
                if (args.Name.Equals(RegionNames.PasswordSetteing))
                {
                    Deactive(string.Empty);
                }

            });
            eventAggregator.GetEvent<NavigatorToEvent>().Subscribe((args) =>
            {
                RequestNavigator(args.Names);

            }, ThreadOption.UIThread);

        }

        public void RequestNavigator(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return;
            }
            if (fullName == ViewNames.PasswordSetteingView)
            {
                ViewModel.PasswordSetting.Controller.Visibility = Visibility.Visible;
            }
            var col = Type.GetType(fullName);
            if (col == null)
            {
                return;
            }

            var superControl =
                col.GetCustomAttributes(typeof(SuperiorNavigatorAttribute), false).FirstOrDefault() as SuperiorNavigatorAttribute;

            RequestNavigator(superControl?.Type.FullName);

            var reginame =
                (col?.GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault() as
                    ViewExportAttribute)?.RegionName;
            if (string.IsNullOrEmpty(reginame))
            {
                return;
            }

            RegionManager.RequestNavigate(reginame, fullName);
            Deactive(reginame);
            var control = col.GetCustomAttributes(typeof(RelevanceNavigatorAttribute), false).Cast<RelevanceNavigatorAttribute>().ToList();
            control?.ForEach(f => RequestNavigator(f.ControlName));

        }

        private void Deactive(string regionName)
        {
            if (regionName.Equals(RegionNames.PasswordSetteing))
            {
                return;
            }
            if (RegionManager.Regions.ContainsRegionWithName(regionName))
            {
                var region = RegionManager.Regions.FirstOrDefault(f => f.Name.Equals(RegionNames.PasswordSetteing));
                if (region != null && region.ActiveViews.Count() != 0)
                {
                    region.ActiveViews.ToList().ForEach(f => region.Deactivate(f));
                }
            }

        }
        public void UpdateTitleName(NavigationContext context)
        {
            var uri = context.Uri.ToString();
            var type = Type.GetType(uri);
            var att = type?.GetCustomAttributes(typeof(TitleNameAttribute), true).FirstOrDefault() as TitleNameAttribute;
            if (att != null)
            {
                ViewModel.Title.TitleName = att.Name;
            }
        }
    }
}