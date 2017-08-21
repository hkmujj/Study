using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.WuHanLine6.GIS.Interfaces;
using Subway.WuHanLine6.GIS.Models;
using Subway.WuHanLine6.GIS.ViewModels;
using Subway.WuHanLine6.GIS.Views.App;

namespace Subway.WuHanLine6.GIS
{
    [SubsystemExport(typeof(WuHanLine6GISEntry))]
    public class WuHanLine6GISEntry : ISubsystem
    {
        /// <summary/>
        /// <param name="initParam"/>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParams.Instance.Initlization(initParam);
            var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();
            var frm1 = new WuHanGISForm(initParam, initParam.AppConfig.AppName.Contains("Left") ? WindowsLocation.Left : WindowsLocation.Right);
            viewService.Regist(initParam.AppConfig.AppName, frm1);
            //viewService.Active(initParam.AppConfig.AppName);
            initParam.RegistDataListener(ServiceLocator.Current.GetInstance<WuHanLine6GisViewModel>());
        }
    }
}