using System;
using System.IO;
using CommonUtil.Util;
using Engine.HMI.SS3B.View;
using Engine.HMI.SS3B.View.Config;
using Engine.HMI.SS3B.View.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.HMI.SS3B.Subsystem
{
    [SubsystemExport(typeof(SS3BHMISubsystem))]
    public class SS3BHMISubsystem : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            LogMgr.Info("----------SS3B System is Start-------------");
            GlobalParam.Instance.InitParam = initParam;

            
            var serverManager = initParam.DataPackage.ServiceManager;
            ISS3BViewModel model = null;

            switch (GlobalParam.Instance.SS3BConfig.Type)
            {
                case SS3BType.LiuZhou:
                    model = ServiceLocator.Current.GetInstance<View.ViewModel.LiuZhou.SS3BViewModel>();
                    break;
                case SS3BType.KunMing:
                    model = ServiceLocator.Current.GetInstance<View.ViewModel.KunMing.SS3BViewModel>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            IndexConfigure.Instance.Load(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory);
            var frm = new MainForm(initParam.AppConfig, initParam.DataPackage, model) { };
            var viewService = serverManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, frm);
            initParam.RegistDataListener(model);
            SetValueDebug(initParam);
        }

        private void SetValueDebug(SubsystemInitParam initParam)
        {
            for (int i = 0; i < 52; i++)
            {
                initParam.CommunicationDataService.WritableReadService.ChangeBool(125 + i, true);
            }
        }
    }
}
