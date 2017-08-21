using CommonUtil.Util;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.XiaMenLine1.Interface;
using Subway.XiaMenLine1.Subsystem.DataAdapter;
using Subway.XiaMenLine1.Subsystem.View.Shell;
using Subway.XiaMenLine1.Subsystem.ViewModels;
using Subway.XiaMenLine1.Interface.Resouce;
using Subway.XiaMenLine1.Subsystem.Extension;

namespace Subway.XiaMenLine1.Subsystem
{
    [SubsystemExport(typeof(XiaMenLine1Subsystem))]
    public class XiaMenLine1Subsystem : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            SubsysParams.Instance.Initalize(initParam);

            AppLog.Info("----------ShenZhenLine1 System is Start-------------");
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
            initParam.RegistDataListener(shellViewModel);
            initParam.RegistDataListener(vmAdpt);
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
