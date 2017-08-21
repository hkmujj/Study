using System;
using System.Collections.Generic;
using System.Linq;
using MMI.Facility.WPFInfrastructure.Interfaces;
using LightRail.HMI.SZLHLF.ViewModel;
using System.ComponentModel.Composition;
using DevExpress.Mvvm;
using System.Windows.Input;
using LightRail.HMI.SZLHLF.SZLHLFAtt;
using CommonUtil.Util;
using MMI.Facility.WPFInfrastructure.Behaviors;
using LightRail.HMI.SZLHLF.Event;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

namespace LightRail.HMI.SZLHLF.Controller
{
    [Export]
    public class OtherController : ControllerBase<OtherViewModel>
    {
        [ImportingConstructor]
        public OtherController()
        {
            Navigator = new DelegateCommand<string>(NavigatorAction);
        }

        private void NavigatorAction(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return;
            }
            var type = Type.GetType(fullName);
            if (type != null)
            {
                var deactive = type.GetCustomAttributes(typeof(DeactiveAttribute), false).Cast<DeactiveAttribute>();
                DeActive(deactive);
                var active = type.GetCustomAttributes(typeof(ActiveAttribute), false).Cast<ActiveAttribute>();
                Active(active);
                Active(fullName);
            }
        }

        public void InterNavigator(string fullName)
        {
            NavigatorAction(fullName);
        }

        private void DeActive(IEnumerable<DeactiveAttribute> deActive)
        {
            if (deActive == null)
            {
                return;
            }
            foreach (
                var region in deActive.Where(
                w => RegionManager.Regions.ContainsRegionWithName(w.RegionName))
                .Select(attribute => RegionManager.Regions[attribute.RegionName]))
            {
                try
                {
                    region.ActiveViews.ToList().ForEach(f => region.Deactivate(f));
                }
                catch (Exception e)
                {
                    AppLog.Error(e.ToString());
                }
            }
        }
        private void Active(string fulleName)
        {
            var type = Type.GetType(fulleName);
            if (type != null)
            {
                var viewExport =
                    type.GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault() as
                        ViewExportAttribute;
                if (viewExport != null)
                {
                    EventAggregator.GetEvent<ViewChangedEvent>().Publish(new ViewChangedEventArgs()
                    {
                        Att = viewExport,
                        FullName = fulleName,
                    });
                    EventAggregator.GetEvent<ViewChangedNotifiEvent>().Publish(new ViewChangedNotifiEvent.NotifiArgs() { FullName = fulleName });
                }
            }
        }

        private void ChangedViewAction(ViewExportAttribute view)
        {

        }
        private void Active(IEnumerable<ActiveAttribute> active)
        {
            if (active == null)
            {
                return;
            }
            foreach (var attribute in active)
            {
                NavigatorAction(attribute.ControlType.FullName);
            }
        }

        public IEventAggregator EventAggregator { get; private set; }

        protected IRegionManager RegionManager { get; private set; }
        public ICommand Navigator { get; private set; }
    }
}
