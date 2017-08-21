using CommonUtil.Util;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using System;
using System.IO;
using MMI.Facility.Interface.Extension;
using Urban.ATC.Siemens.Model;
using Urban.ATC.Siemens.View.View;
using Urban.ATC.Siemens.WPF.Control.Config;
using Urban.ATC.Siemens.WPF.Control.ViewModel;
using Urban.Info.Interface.ACK;

namespace Urban.ATC.Siemens.Subsystem
{
    [SubsystemExport(typeof(SiemensATCSubsystem))]
    public class SiemensATCSubsystem : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {

            AppLog.Info("----------XiMenZi System is Start-------------");
            var serverManager = initParam.DataPackage.ServiceManager;

            IndexConfigure.Instance.Load(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory);
            var config =
                DataSerialization.DeserializeFromXmlFile<StartConfig>(
                    Path.Combine(initParam.AppConfig.AppPaths.ConfigDirectory, StartConfig.File));

            GlobalParam.StartConfig = config;
            var testFrm = new WpfForm(initParam.AppConfig, initParam.DataPackage);
            var data = new SiemensData
            {
                DataService = initParam.CommunicationDataService,
                ACKManage = new ACKManage()
            };
            data.ACKManage.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\MessageInfo.txt"));
            data.ButtonReactivationMgr.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\ButtonReactivationInfo.txt"));
            testFrm.Data = data;
            var viewService = serverManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, testFrm);
            initParam.RegistDataListener(data);
            SetValueWhenDebug(initParam);
        }

        private void SetValueWhenDebug(SubsystemInitParam initParam)
        {
            // initParam.CommunicationDataService.WritableReadService.ChangeBool(1, true);

            // initParam.CommunicationDataService.WritableReadService.ChangeFloat(128, 101);
        }
    }
}