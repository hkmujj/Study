using System;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;
using Motor.LKJ._2000.Entity.ViewModel;

namespace Motor.LKJ._2000.Entity.Controller
{
    public class LKJ2000MainController : NotificationObject
    {
        public LKJ2000MainController(LKJ2000MainViewModel lkj2000MainViewModel)
        {
            ViewModel = lkj2000MainViewModel;
            IndexDescription =
                ViewModel.SubsystemInitParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(ViewModel.SubsystemInitParam.AppConfig));
        }

        protected LKJ2000MainViewModel ViewModel { private set; get; }

        protected IProjectIndexDescriptionConfig IndexDescription { private set; get; }

    }
}