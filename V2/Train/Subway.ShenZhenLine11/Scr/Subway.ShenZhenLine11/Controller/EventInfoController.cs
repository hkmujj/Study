using System;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine11.ViewModels;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine11.Info;
using Subway.ShenZhenLine11.ShenZhenAttribute;

namespace Subway.ShenZhenLine11.Controller
{
    [Export(typeof(EventInfoController))]
    public class EventInfoController : ControllerBase<EventInfoViewModel>
    {

        protected IRegionManager RegionManager;
        [ImportingConstructor]
        public EventInfoController(IRegionManager regionManager)
        {
            RegionManager = regionManager;
            NexPage = new DelegateCommand(() =>
            {
                ViewModel.Manager.NextPage();
            });
            LastPage = new DelegateCommand(() =>
              {
                  ViewModel.Manager.LastPage();
              });
            Confirm = new DelegateCommand(() =>
              {
                  if (ViewModel.CurrenInfo != null)
                  {
                      ViewModel.Manager.Confirm(ViewModel.CurrenInfo.LogicID);
                  }

              });
        }

        public void RequestNavigator(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return;
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
            var control = col.GetCustomAttributes(typeof(RelevanceNavigatorAttribute), false).Cast<RelevanceNavigatorAttribute>().ToList();
            control?.ForEach(f => RequestNavigator(f.ControlName));

        }
        public ICommand Confirm { get; private set; }
        public ICommand NexPage { get; private set; }

        public ICommand LastPage { get; private set; }
    }
}