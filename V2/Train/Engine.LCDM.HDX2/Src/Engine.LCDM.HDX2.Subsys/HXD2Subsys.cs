using Engine.LCDM.HDX2.DataAdapter;
using Engine.LCDM.HDX2.Entity;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Microsoft.Practices.Prism;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.LCDM.HDX2.Subsys
{
    [SubsystemExport(typeof(HXD2Subsys))]
    public class HXD2Subsys : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            HXD2Param.Instance.Initalize(initParam);

            
            var serviceManager = initParam.DataPackage.ServiceManager;

            var adpt = ServiceLocator.Current.GetInstance<HXD2DataAdapter>();
            adpt.Initalize(initParam);

            var form = new HXD2Form(initParam);
            var viewService = serviceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, form);
            viewService.Active(initParam.AppConfig.AppName);

            initParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
        }
    }
}