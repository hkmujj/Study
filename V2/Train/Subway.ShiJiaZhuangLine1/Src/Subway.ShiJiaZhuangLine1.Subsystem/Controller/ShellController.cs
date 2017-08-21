using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShiJiaZhuangLine1.Interface.Attibutes;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.Model;
using Subway.ShiJiaZhuangLine1.Subsystem.ViewModels;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Controller
{
    [Export]
    public class ShellController : ControllerBase<ShellViewModel>, IDisposable
    {
        public DelegateCommand<string> ShellContentNavigateCommand { private set; get; }

        public DelegateCommand<string> MainContentNavigateCommand { private set; get; }
        public ICommand Naigator { get; private set; }
        public DelegateCommand<MainRunningViewNavigateParam> MainRuningNaviageteCommand { private set; get; }

        protected IRegionManager RegionManager { private set; get; }

        [ImportingConstructor]
        public ShellController(IRegionManager regionManager)
        {
            RegionManager = regionManager;
            Naigator = new DelegateCommand(NaigatorAction);
            ShellContentNavigateCommand = new DelegateCommand<string>(OnShellContentNavigate);
            MainContentNavigateCommand = new DelegateCommand<string>(OnMainContentNavigate);
            MainRuningNaviageteCommand = new DelegateCommand<MainRunningViewNavigateParam>(OnMainRuningNaviagete);
        }

        private void NaigatorAction()
        {

            RegionManager.RequestNavigate(RegionNames.ShellContentRegion, ViewNames.EventView);
            RegionManager.RequestNavigate(RegionNames.ShellBottomRegion, ViewNames.ButtonView);

        }

        private void OnMainRuningNaviagete(MainRunningViewNavigateParam obj)
        {
            ViewModel.Model.ButtonModel.ResetButtonState();
            OnMainContentNavigate(ViewNames.MainRunningView);
            RegionManager.RequestNavigate(RegionNames.MainRunningChildrenBreakerRegion, obj.BreakerRegionViewName);
            RegionManager.RequestNavigate(RegionNames.MainRunningChildrenTrainRegion, obj.TrainRegionViewName);
            ViewModel.Model.ButtonModel.CurrentView = obj.TrainRegionViewName;
        }

        public void UpdateTitleName(NavigationContext context)
        {
            UpdateTitleName(context.Uri.ToString());
        }

        public void UpdateTitleName(string typeName)
        {
            try
            {
                var type = Type.GetType(typeName, false);

                UpdateTitleName(type);
            }
            catch (Exception e)
            {
                AppLog.Error("Can not found type {0}", typeName);
                AppLog.Error(e.ToString());
            }
        }

        private void UpdateTitleName(Type type)
        {
            var att =
                type.GetCustomAttributes(typeof(TitleNameAttribute), false).FirstOrDefault() as TitleNameAttribute;

            if (att != null)
            {
                ViewModel.Model.TitleModel.TitleName = att.Name;
            }
        }

        private void OnMainContentNavigate(string obj)
        {
            OnShellContentNavigate(ViewNames.ShellContentMainContentView);
            RegionManager.RequestNavigate(RegionNames.MainContentContentRegion, obj);
        }

        private void OnShellContentNavigate(string obj)
        {
            RegionManager.RequestNavigate(RegionNames.ShellContentRegion, obj);
        }

        public void Dispose()
        {

        }
    }
}