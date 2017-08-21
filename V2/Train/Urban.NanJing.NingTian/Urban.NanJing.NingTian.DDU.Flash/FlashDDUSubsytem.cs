using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Urban.NanJing.NingTian.DDU.Flash.Adapter;
using Urban.NanJing.NingTian.DDU.Flash.Interface;
using Urban.NanJing.NingTian.DDU.Flash.Model;
using Urban.NanJing.NingTian.DDU.Flash.View;
using Urban.NanJing.NingTian.DDU.Index;

namespace Urban.NanJing.NingTian.DDU.Flash
{
    public class FlashDDUSubsytem : ISubsystem
    {
        private IFlashInteractive m_FlashInteractive;

        public void Initalize(SubsystemInitParam initParam)
        {
            var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();

            IndexConfigure.Instance.Load(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory);

            var configService = new NingTianConfig();
            configService.Initalize(initParam.DataPackage.Config.SystemDicrectory.BaseDirectory);

            initParam.DataPackage.ServiceManager.RegistService<NingTianConfig>(configService);

            var domain  = new NingTianDDUDomain(IndexConfigure.Instance.CommunicationIndexFacade);
            

            var form = new FlashDDUForm(initParam.AppConfig.AppName, initParam.DataPackage) { };

            m_FlashInteractive = form;
            var adpt = new FlashDataAdapter(initParam, domain, m_FlashInteractive, IndexConfigure.Instance.CommunicationIndexFacade, initParam.CommunicationDataService);


            var dataService =
                initParam.DataPackage.ServiceManager.GetService<ICommunicationDataFacadeService>()
                    .GetCommunicationDataService(initParam.AppConfig.ProjectType);

            domain.VoluntaryResponse(dataService);
            adpt.VoluntaryResponse(dataService);

            dataService.ReadService.BoolChanged += domain.OnBoolChanged;
            dataService.ReadService.FloatChanged+= domain.OnFloatChanged;

            dataService.ReadService.DataChanged += adpt.OnDataChanged;

            viewService.Regist(initParam.AppConfig.AppName, form);
        }
    }
}
