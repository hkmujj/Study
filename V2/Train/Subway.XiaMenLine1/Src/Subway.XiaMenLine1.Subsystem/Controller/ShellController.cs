using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.XiaMenLine1.Interface.Attibutes;
using Subway.XiaMenLine1.Interface.Resouce;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.Model;
using Subway.XiaMenLine1.Subsystem.ViewModels;
using Subway.XiaMenLine1.Subsystem.Extension;

namespace Subway.XiaMenLine1.Subsystem.Controller
{
    [Export]
    public class ShellController : ControllerBase<ShellViewModel>, IDisposable
    {
        public DelegateCommand<string> ShellContentNavigateCommand { private set; get; }

        public DelegateCommand<string> MainContentNavigateCommand { private set; get; }
        public DelegateCommand<string> MainContentContentRegionCommand { get; private set; }

        public DelegateCommand<MainRunningViewNavigateParam> MainRuningNaviageteCommand { private set; get; }
        public ICommand MainRootNaviageteCommand { private set; get; }
        protected IRegionManager RegionManager { private set; get; }




        public ICommand SendBoolData { get; private set; }
        [ImportingConstructor]
        public ShellController(IRegionManager regionManager)
        {
            RegionManager = regionManager;
            MainRootNaviageteCommand = new DelegateCommand<string>(OnMainRootNaviagete);
            ShellContentNavigateCommand = new DelegateCommand<string>(OnShellContentNavigate);
            MainContentNavigateCommand = new DelegateCommand<string>(OnMainContentNavigate);
            MainContentContentRegionCommand=new DelegateCommand<string>(OnMainContentContentRegion);
            MainRuningNaviageteCommand = new DelegateCommand<MainRunningViewNavigateParam>(OnMainRuningNaviagete);
            SendBoolData = new DelegateCommand<string>(logicName =>
              {
                  if (string.IsNullOrEmpty(logicName))
                  {
                      return;
                  }
                  logicName.SendData(true, true);
              });

        }

        private void OnMainContentContentRegion(string obj)
        {
            RegionManager.RequestNavigate(RegionNames.MainContentContentRegion, obj);
        }

        private void OnMainRootNaviagete(string obj)
        {
            RegionManager.RequestNavigate(RegionNames.MainRootShell, obj);
        }

        private void OnMainRuningNaviagete(MainRunningViewNavigateParam obj)
        {
            OnMainRootNaviagete(ViewNames.MainRooeShell);
            ViewModel.Model.ButtonModel.ResetButtonState();
            OnMainContentNavigate(ViewNames.MainRunningView);
            RegionManager.RequestNavigate(RegionNames.MainRunningChildrenBreakerRegion, obj.BreakerRegionViewName);
            RegionManager.RequestNavigate(RegionNames.MainRunningChildrenTrainRegion, obj.TrainRegionViewName);
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
                if (att.Name == "烟温探测系统")
                {

                    ViewModel.Model.ButtonModel.Button8Str = "消音";
                    ViewModel.Model.ButtonModel.Button9Str = "复位";
                    ViewModel.Btn8CommandPara = OutBoolKeys.烟火报警消音;
                    ViewModel.Btn9CommandPara = OutBoolKeys.烟火报警复位;
                }
                else if (att.Name == "紧急对讲")
                {
                    ViewModel.Model.ButtonModel.Button8Str = "接听";
                    ViewModel.Model.ButtonModel.Button9Str = "复位";
                    ViewModel.Btn8CommandPara = OutBoolKeys.紧急对讲接听;
                    ViewModel.Btn9CommandPara = OutBoolKeys.紧急对讲复位;
                }
                else if (att.Name == "空调系统")
                {
                    ViewModel.Model.ButtonModel.Button8Str = "空气净化\r\n器启动";
                    ViewModel.Model.ButtonModel.Button9Str = "手动冷";
                    ViewModel.Btn8CommandPara = OutBoolKeys.空调空气净化器启动;
                    ViewModel.Btn9CommandPara = OutBoolKeys.空调手动冷;
                }
                else
                {

                    ViewModel.Model.ButtonModel.Button8Str = "";
                    ViewModel.Model.ButtonModel.Button9Str = "";
                }

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