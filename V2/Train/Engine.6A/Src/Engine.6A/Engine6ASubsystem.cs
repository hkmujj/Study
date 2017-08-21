using System.IO;
using CommonUtil.Util;
using Engine._6A.Adapter;
using Engine._6A.Adapter.ConfigInfo;
using Engine._6A.Config;
using Engine._6A.Views.Shell;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine._6A
{
    [SubsystemExport(typeof(Engine6ASubsystem))]
    public class Engine6ASubsystem : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            LogMgr.Info("-------6ASystem is Starting-----");
            var form = new ShellForm(initParam);
            GlobalParam.Engine6AConfig = DataSerialization.DeserializeFromXmlFile<Engine6AConfig>(
                    Path.Combine(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory, Engine6AConfig.FileName));
            GlobalParam.InitParam = initParam;
            IndexConfigure.Instance.Initalize(initParam);
         
            var adapter = ServiceLocator.Current.GetInstance<IEngineAdapter>();
            form.Init();
            initParam.DataPackage.ServiceManager.GetService<IViewService>().Regist(initParam.AppConfig.AppName, form);

            initParam.RegistDataListener(adapter);

            initParam.CommunicationDataService.ReadService.RaiseAllDataChanged();

        }

        public Engine6ASubsystem()
        {

        }
    }
}
