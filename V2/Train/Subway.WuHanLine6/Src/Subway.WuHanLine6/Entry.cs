using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using CommonUtil.Util;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.WuHanLine6.DataAdapter;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Models;
using Subway.WuHanLine6.Views.App;

namespace Subway.WuHanLine6
{
    /// <summary>
    /// 平台调用入口
    /// </summary>
    [SubsystemExport(typeof(Entry))]
    public class Entry : ISubsystem
    {
        /// <summary>
        /// 所有IModelAdapter导出
        /// </summary>
        [ImportMany]
        public List<Lazy<IModelAdapter>> AllModelAdapter;

        /// <summary/>
        /// <param name="initParam"/>
        public void Initalize(SubsystemInitParam initParam)
        {
            AppLog.Info(string.Format("{0} Is Start", "WuHanLine6"));

            try
            {
                GlobalParams.Instance.Initialize(initParam);

                var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();
                var frm = new WuHanLine6Form();
                viewService.Regist(initParam.AppConfig.AppName, frm);
                viewService.Active(initParam.AppConfig.AppName);

                var ada = ServiceLocator.Current.GetInstance<WuHanDadaAdapter>();
                ada.Initialize();
                AllModelAdapter.ForEach(f => f.Value.Initialize());
            }
            catch (Exception e)
            {
                AppLog.Info(e.ToString());
                
            }


        }
    }
}