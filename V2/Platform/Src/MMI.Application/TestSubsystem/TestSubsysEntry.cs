using System.IO;
using CommonUtil.Util;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using MMI.Tester.BatchDataSender;
using TestSubsystem.Constant;
using TestSubsystem.View;
using TestSubsystem.ViewModel;

namespace TestSubsystem
{
    [SubsystemExport(typeof (TestSubsysEntry))]
    public class TestSubsysEntry : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            AppLog.Debug("TestSubsysEntry Initalize");

            SubParam.Instance.SubsystemInitParam = initParam;

            //ServiceLocator.Current.GetInstance<SubView1>();

            var form = new TestSubsysForm(initParam.AppConfig.AppName, initParam.DataPackage);

            initParam.DataPackage.ServiceManager.GetService<IViewService>().Regist(initParam.AppConfig.AppName, form);

            var vm = ServiceLocator.Current.GetInstance<TestSubMainViewModel>();

            initParam.RegistDataListener(vm);

            var bd = new BatchDataSenderFacade();
            bd.AttachSenderView(new BatchDataSenderInitParam(initParam,
                Path.Combine(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                    "Tester_HXD2制动屏接口_InBool.xls"),
                Path.Combine(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                    "Tester_HXD2制动屏接口_InFloat.xls"), form));
        }
    }
}