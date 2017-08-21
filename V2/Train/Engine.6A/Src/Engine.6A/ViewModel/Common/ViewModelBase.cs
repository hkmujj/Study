using System;
using System.Linq;
using Engine._6A.Constance;
using Engine._6A.Views.Axle8;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine._6A.ViewModel.Common
{
    public class ViewModelBase : NotificationObject, IViewModelBase
    {
        public IRegionManager RegionManager { get; private set; }
        public IEventAggregator EventAggregator { get; private set; }

        public ViewModelBase()
        {
            RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            var view = ServiceLocator.Current.GetInstance<Axle8DataMonitorShell>();
            view.IsCurrent = false;
            IRegion region = null;
            if (navigationContext.Uri.OriginalString == Axle6ControlName.SystemSettingShell)
            {
                region = RegionManager.Regions[RegionNames.SystemTabTabRegion];
            }
            if (navigationContext.Uri.OriginalString == Axle6ControlName.DataMonitorShell)
            {
                region = RegionManager.Regions[RegionNames.DataMonitorTabRegion];
            }
            if (navigationContext.Uri.OriginalString == Axle6ControlName.FaultShell)
            {
                region = RegionManager.Regions[RegionNames.FaultTabRegion];
            }
            if (navigationContext.Uri.OriginalString == CoontrolNameBase.Axle8DataMonitorShell)
            {
                region = RegionManager.Regions[RegionNames.Axle8DataMonitorTabRegion];
                view.IsCurrent = true;
            }
            if (navigationContext.Uri.OriginalString == CoontrolNameBase.Axle8FaultShell)
            {
                region = RegionManager.Regions[RegionNames.Axle8FaultTabRegion];
            }
            if (region != null)
            {
                if (region.Views.Count() != 0)
                {
                    region.Activate(region.Views.FirstOrDefault());
                }
            }
        }


        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        public virtual void Clear()
        {

        }

        protected virtual void Clear(Type type, object obj)
        {
            foreach (var info in type.GetProperties())
            {
                if (info.PropertyType == typeof(string))
                {
                    info.SetValue(obj, string.Empty, null);
                }
                if (info.PropertyType == typeof(double))
                {
                    info.SetValue(obj, 0, null);
                }
            }
        }
    }
}