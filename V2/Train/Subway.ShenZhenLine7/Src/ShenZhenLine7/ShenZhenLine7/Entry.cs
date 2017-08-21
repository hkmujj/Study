using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.ShenZhenLine7.Extention;
using Subway.ShenZhenLine7.Models;
using Subway.ShenZhenLine7.Resource.Keys;
using Subway.ShenZhenLine7.ViewModels;

namespace Subway.ShenZhenLine7
{
    /// <summary>
    /// 入口
    /// </summary>
    [SubsystemExport(typeof(Entry))]
    public class Entry : ISubsystem
    {
        /// <summary/>
        /// <param name="initParam"/>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initialize(initParam);
            var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, new ShenZhenLine7Form());
            viewService.Active(initParam.AppConfig.AppName);
            initParam.RegistDataListener(ServiceLocator.Current.GetInstance<ShenZhenLine7ViewModel>());
        
            if (initParam.DataPackage.Config.SystemConfig.IsDebugModel)
            {
                SetDebugValue();
            }

        }

     
        private static void SetDebugValue()
        {
            InBoolKeys.黑屏.SendBoolValue(true, isInBool: true);
        }
    }
}