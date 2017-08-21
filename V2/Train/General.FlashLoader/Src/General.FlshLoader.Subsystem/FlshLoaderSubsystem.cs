using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Forms;
using CommonUtil.Util;
using General.FlashLoader.Subsystem.Adpter;
using General.FlashLoader.Subsystem.Interface;
using General.FlashLoader.Subsystem.Model.Config;
using General.FlashLoader.Subsystem.View;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using MMI.Tester.BatchDataSender;

namespace General.FlashLoader.Subsystem
{
    [SubsystemExport(typeof (FlshLoaderSubsystem))]
    public class FlshLoaderSubsystem : ISubsystem, IDisposable
    {
        private IFlashInteractive m_FlashInteractive;

        private FlashDataAdapter m_FlashDataAdapter;

        public void Initalize(SubsystemInitParam initParam)
        {
            SubSysParam.Instance.Initalize(initParam);

            var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();
            var disposeService = initParam.DataPackage.ServiceManager.GetService<IDisposeService>();

            InitalizeThread(initParam, disposeService, viewService);

            disposeService.RegistDisposableObject(m_FlashDataAdapter);
            disposeService.RegistDisposableObject(m_FlashInteractive);
            disposeService.RegistDisposableObject(this);

            if (initParam.DataPackage.Config.SystemConfig.StartModel == StartModel.Edit)
            {
                var tstConfig =
                    DataSerialization.DeserializeFromXmlFile<FlashLoaderTesterConfig>(
                        Path.Combine(initParam.AppConfig.AppPaths.ConfigDirectory,
                            "..\\Tester\\FlashLoaderTesterConfig.xml"));
                if (tstConfig != null)
                {
                    var bd = new BatchDataSenderFacade();
                    var sysConfig = initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory;

                    bd.AttachSenderView(new BatchDataSenderInitParam(initParam,
                        Path.Combine(sysConfig, tstConfig.BoolFile),
                        Path.Combine(sysConfig, tstConfig.FloatFile), App.Current.MainForm as Form));
                }
            }

            initParam.CommunicationDataService.ReadService.RaiseAllDataChanged();

            SetDataWhenDebug(initParam.CommunicationDataService);
        }

        private void SetDataWhenDebug(ICommunicationDataService dataService)
        {
            dataService.WritableReadService.ChangeBool(
                SubSysParam.Instance.IndexDescriptionConfig.InBoolDescriptionDictionary["屏点亮"], true, true);
        }

        private void InitalizeThread(SubsystemInitParam initParam, IDisposeService disposeService, IViewService viewService)
        {
            var form = new FlashLoaderForm(initParam.AppConfig.AppName, initParam.DataPackage);
            m_FlashInteractive = form;
            //m_FlashInteractive  = new ProcessFlashInteractive(initParam);
            m_FlashDataAdapter = new FlashDataAdapter(initParam, m_FlashInteractive);

            m_FlashDataAdapter.Run();


            viewService.Regist(initParam.AppConfig.AppName, form);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private void InitalizeProcess(SubsystemInitParam initParam, IDisposeService disposeService, IViewService viewService)
        {
            m_FlashInteractive = new ProcessFlashInteractive(initParam);
            m_FlashDataAdapter = new FlashDataAdapter(initParam, m_FlashInteractive);
            m_FlashDataAdapter.Run();
        }

        public void Dispose()
        {
        }
    }
}
