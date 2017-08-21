using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Event;
using Urban.Phillippine.View.EventArgs;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    public class ViewModelBase : NotificationObject, IViewModelBase, INavigationAware
    {
        public ViewModelBase()
        {
            RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        public virtual void Clear()
        {
        }

        public virtual void Clear(object obj, Type type)
        {
        }

        public IRegionManager RegionManager { get; set; }
        public IEventAggregator EventAggregator { get; set; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var uri = navigationContext.Uri.ToString();
            var navigitor = EventAggregator.GetEvent<NavigatorEvent>();
            //跳转VAC界面时，较转到VAC第一页
            if (uri == ControlNames.VACShell)
            {
                navigitor.Publish(new NavigatorEventArgs()
                {
                    Name = ControlNames.VACViewPageOne,
                    Region = RegionNames.VACRegion
                });
            }

            //跳转Softversion界面时，同时跳转SoftVersionButton界面
            if (uri == ControlNames.SoftwareVersionView || uri == ControlNames.InstructionView || uri == ControlNames.VACTestView)
            {
                navigitor.Publish(new NavigatorEventArgs()
                {
                    Name = ControlNames.SoftVwesionButton,
                    Region = RegionNames.ButtonRegion
                });
            }
            //跳转RIOM 页面时同时跳转RIOMbutton
            if (uri == ControlNames.RIOM1DI1View)
            {
                navigitor.Publish(new NavigatorEventArgs()
                {
                    Name = ControlNames.RIOMButtonView,
                    Region = RegionNames.ButtonRegion
                });
                EventAggregator.GetEvent<ButtonInitEvent>().Publish(new ButtonInitEvenArgs()
                {
                    ViewName = ControlNames.RIOMButtonView
                });
            }
            if (uri == ControlNames.FaultRecordView)
            {
                navigitor.Publish(new NavigatorEventArgs()
                {
                    Name = ControlNames.FaultRecordButton,
                    Region = RegionNames.ButtonRegion,
                });
                EventAggregator.GetEvent<ButtonInitEvent>().Publish(new ButtonInitEvenArgs()
                {
                    ViewName = ControlNames.FaultRecordView
                });
            }
            //返回Mainter界面   调转到MainButton
            if (uri == ControlNames.MaintainView)
            {
                navigitor.Publish(new NavigatorEventArgs()
                {
                    Name = ControlNames.MainButtonView,
                    Region = RegionNames.ButtonRegion
                });
            }
            if (uri == ControlNames.MainContentShell)
            {
                navigitor.Publish(new NavigatorEventArgs()
                {
                    Name = ControlNames.MainButtonView,
                    Region = RegionNames.ButtonRegion
                });

            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if (navigationContext.Uri.ToString() == ControlNames.MainContentShell)
            {
                EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                {
                    Name = ControlNames.MainContentView,
                    Region = RegionNames.ContentRegion
                });
                EventAggregator.GetEvent<ButtonInitEvent>().Publish(new ButtonInitEvenArgs()
                {
                    ViewName = ControlNames.MainButtonView
                });
            }
        }
    }
}