using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.Control.Service;
using MMI.Facility.DataType.Log;
using MMI.Facility.DataType.Running;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Addins;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Running;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Data
{
    internal class RunningParamManager : IRunningParamManager
    {
        private readonly IConfig m_Config;
        private readonly IAddInLoader m_AddInLoader;
        private readonly IIndexDescriptionService m_IndexDescriptionService;

        public RunningParamManager(IConfig config, IAddInLoader addInLoader, ICommunicationDataFacadeService dataService,
            StartModel startModel, IIndexDescriptionService indexDescriptionService)
        {
            m_Config = config;
            m_AddInLoader = addInLoader;
            m_IndexDescriptionService = indexDescriptionService;
            var rp = new RunningParam(startModel) {CommunicationFacadeDataService = dataService};
            RunningParam = rp;
        }

        public IRunningParam RunningParam { get; private set; }

        public void Initalize()
        {
            foreach (var appConfig in m_Config.AppConfigs)
            {
                RunningParam.AppRunningParamDictionary.Add(appConfig.AppName, new AppRunningParam()
                {
                    ParentParam = RunningParam,
                    AppName = appConfig.AppName,
                    AppConfig =  appConfig,
                    IndexInterpreter = m_IndexDescriptionService.GetIndexInterpreter(appConfig.ProjectType, appConfig.AppName)
                });
            }

            InitalizeView();

            InitalizePostCmdService();

            InitalizeLogicCaculate();

        }


        private void InitalizeLogicCaculate()
        {
            foreach (var appConfig in m_Config.AppConfigs)
            {
                ((AppRunningParam) RunningParam.AppRunningParamDictionary[appConfig.AppName]).RunningLogicCaculate =
                    new RunningLogicCaculate(appConfig, RunningParam);
            }
        }

        private void InitalizePostCmdService()
        {
            Func<string, ProjectType, IAppPostCmdService> createPostCmdServiceFunc;
            switch (RunningParam.StartModel)
            {
                case StartModel.Normal:
                    createPostCmdServiceFunc = (name, proType) => new NormalModelAppPostCmdService(this, name, proType);
                    break;
                case StartModel.Edit:
                    createPostCmdServiceFunc = (name, proType) => new EditModelAppPostCmdService(this, name, proType);
                    break;
                case StartModel.PTT:
                    createPostCmdServiceFunc = (name, proType) => new NormalModelAppPostCmdService(this, name, proType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (var appConfig in m_Config.AppConfigs)
            {
                ((AppRunningParam) RunningParam.AppRunningParamDictionary[appConfig.AppName]).AppPostCmdService =
                    createPostCmdServiceFunc(appConfig.AppName, appConfig.ProjectType);
            }
        }

        /// <summary>
        /// 初始化视图
        /// </summary>
        private void InitalizeView()
        {
            foreach (var appConfig in m_Config.AppConfigs.Where(w => w.SubsystemType == SubsystemType.Addin))
            {
                var runViewParam = new RunningViewParam(appConfig.AppName);

                ((AppRunningParam)RunningParam.AppRunningParamDictionary[appConfig.AppName]).RunningViewParam =
                    runViewParam;

                runViewParam.ViewUnitParamDic = appConfig.AppViewConfig.AppViewConfigDic.ToDictionary(kvp => kvp.Key,
                    kvp => (IRunningViewUnitParam) (new RunningViewUnitParam() {ViewConfig = kvp.Value}));

                var addins = m_AddInLoader.ProjetAddinInstanceDic[appConfig.AppName];
                foreach (var uiObject in appConfig.AppObjcetConfig.UIObjects)
                {
                    var ins = GetValidatedInstance(addins, uiObject, appConfig);

                    foreach (var viewIndex in uiObject.ViewList)
                    {
                        if (runViewParam.ViewUnitParamDic.ContainsKey(viewIndex))
                        {
                            if (!runViewParam.ViewUnitParamDic[viewIndex].Objects.Contains(ins))
                            {
                                runViewParam.ViewUnitParamDic[viewIndex].Objects.Add(ins);
                            }
                        }
                        else
                        {
                            AppLog.Error(
                                string.Format(
                                    "Can not found view index {0} of {1} in View index config, which file is {2}",
                                    viewIndex, ins.GetType(), appConfig.AppFileConfig.ViewConfigFile));
                        }
                    }
                }
            }
        }

        private IObjectBase GetValidatedInstance(Dictionary<string, IObjectBase> addins, IUIObject uiObject,
            IAppConfig appConfig)
        {
            if (!addins.ContainsKey(uiObject.ObjectKey))
            {
                var msg = string.Format("Can not find the object({0}) of project \"{1}\" in addin directory({2}).",
                    uiObject.ObjectKey,
                    appConfig.AppName,
                    m_Config.SystemDicrectory.AddinDirectory);
                SysLog.Error(msg);
                throw new DllNotFoundException(msg);
            }
            var ins = addins[uiObject.ObjectKey];
            return ins;
        }
    }
}
