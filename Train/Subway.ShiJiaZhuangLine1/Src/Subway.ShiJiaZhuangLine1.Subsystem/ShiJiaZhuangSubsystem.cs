using CommonUtil.Util;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Interface.Resouce;
using Subway.ShiJiaZhuangLine1.Subsystem.DataAdapter;
using Subway.ShiJiaZhuangLine1.Subsystem.Extension;
using Subway.ShiJiaZhuangLine1.Subsystem.View.Shell;
using Subway.ShiJiaZhuangLine1.Subsystem.ViewModels;

namespace Subway.ShiJiaZhuangLine1.Subsystem
{
    [SubsystemExport(typeof(ShiJiaZhuangSubsystem))]
    public class ShiJiaZhuangSubsystem : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            SubsysParams.Instance.Initalize(initParam);

            AppLog.Info("----------ShiJiaZhuang System is Start-------------");
            var serverManage = initParam.DataPackage.ServiceManager;
            IndexConfigure.Instance.Load(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory);
            var frm = new MainForm(initParam.AppConfig, initParam.DataPackage);
            var viewService = serverManage.GetService<IViewService>();

            var shellViewModel = ServiceLocator.Current.GetInstance<ShellViewModel>();
            var vmAdpt = ServiceLocator.Current.GetInstance<ShellViewModelDataAdapter>();

            var disp = serverManage.GetService<IDisposeService>();
            disp.RegistDisposableObject(shellViewModel);
            disp.RegistDisposableObject(vmAdpt);

            viewService.Regist(initParam.AppConfig.AppName, frm);

            var listenService = serverManage.GetService<IDataChangeListenService>();
            listenService.RegistListener(shellViewModel, initParam.AppConfig);
            listenService.RegistListener(vmAdpt, initParam.AppConfig);
            //initParam.CommunicationDataService.ReadService.DataChanged += shellViewModel.UpdateState;
            //initParam.CommunicationDataService.ReadService.DataChanged += vmAdpt.UpdateState;

            SetValueWhenDebug(initParam);

        }

        private void SetValueWhenDebug(SubsystemInitParam initParam)
        {
            if (initParam.DataPackage.Config.SystemConfig.StartModel == StartModel.Edit)
            {
                initParam.CommunicationDataService.WritableReadService.ChangeBool(InBoolKeys.Inb车辆屏黑屏标志0黑1亮, true, true);
                initParam.CommunicationDataService.WritableReadService.ChangeBool(InBoolKeys.Inb列车运行方向_6号车_, true, true);
                initParam.CommunicationDataService.WritableReadService.ChangeBool(InBoolKeys.Inb列车运行方向_1号车_, true, true);
            }
        }
    }
}
