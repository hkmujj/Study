using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using CommonUtil.Util;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Control.Data;
using MMI.Facility.Control.Hook;
using MMI.Facility.Control.Service;
using MMI.Facility.DataType;
using MMI.Facility.DataType.Data;
using MMI.Facility.DataType.Log;
using MMI.Facility.DataType.Running;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Course;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Hook;
using MMI.Facility.Interface.IndexDescription;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Running;
using MMI.Facility.Interface.Service;
using MMI.Facility.View.Views.Common;

namespace MMI.Facility.Control.Flow
{
    internal abstract class FlowControllerBuilder
    {
        protected MainBaseForm MainForm
        {
            set
            {
                m_MainForm = value;
                if (m_DebugViewService != null)
                {
                    m_DebugViewService.MdiParent = m_MainForm;
                }
            }
            get { return m_MainForm; }
        }
        // ReSharper disable InconsistentNaming
        protected IHookProcess m_HookProcess;

        protected AddInLoader m_AddInLoader;

        protected IRunningParamManager m_RunningParamManager;

        protected ICommunicationDataFacadeService m_CommunicatonDataFacadeService;

        protected IDataPackage m_DataPackage;

        protected IObjectService m_ObjectService;

        protected IServiceManager m_ServiceManager;

        protected IRunningViewService m_ViewService;

        protected DataChangeListenService m_DataChangeListenService;

        protected StationNameProviderService m_StationNameProviderService;
        protected TimeTableProviderService m_TimeTableProviderService;

        protected DisposeService m_DisposeService;

        protected ICourseService m_CourseService;

        protected IEventService m_EventService;

        protected DebugViewService m_DebugViewService;

        protected IIndexDescriptionService m_IndexDescriptionService;

        /// <summary>
        /// 子系统 
        /// </summary>
        protected List<ISubsystem> m_SubsystemCollection;

        // ReSharper restore InconsistentNaming

        protected IConfig Config
        {
            get { return ConfigManager.Instance.Config; }
        }

        private MainBaseForm m_MainForm;

        /// <summary>
        /// 初始化对象
        /// </summary>
        public virtual void InitalizeObjects()
        {
            foreach (var projectAddinKvp in m_AddInLoader.ProjetAddinInstanceDic)
            {
                var config = ConfigManager.Instance.Config.AppConfigs.First(f => f.AppName == projectAddinKvp.Key);

                var objs = config.AppObjcetConfig.UIObjects;
                foreach (var objConfig in objs)
                {
                    // by this way , the order of object is depends on you config file. the fronter is initalized earlier
                    var addin = projectAddinKvp.Value[objConfig.ObjectKey];
                    addin.RecPath = config.AppPaths.ResourceDirectory;
                    addin.AppPaths = config.AppPaths;
                    addin.AppConfig = config;

                    var typeBase = addin as TypeBase;
                    if (typeBase != null)
                    {
                        // 都共用一个数据
                        typeBase.DataPackage = m_DataPackage;
                        typeBase.CommunicationDataService =
                            m_CommunicatonDataFacadeService.GetCommunicationDataService(config);
                        typeBase.IConfig = ConfigManager.Instance.Config;
                        typeBase.ObjectService = m_ObjectService;
                        typeBase.IndexInterpreter =
                            m_RunningParamManager.RunningParam.AppRunningParamDictionary[projectAddinKvp.Key]
                                .IndexInterpreter;
                        typeBase.onPostCmdA +=
                            m_RunningParamManager.RunningParam.AppRunningParamDictionary[projectAddinKvp.Key]
                                .AppPostCmdService.PostCmdA;
                        var errorInfo = -1;

                        try
                        {
                            if (typeBase.init(ref errorInfo))
                            {
                                AppLog.Debug(string.Format("Initalize object {0} success.", typeBase));
                            }
                            else
                            {
                                SysLog.Error(
                                    string.Format(
                                        "Initalize object {0} fail, when call object's init method ,the return value is false. the error code is {1}",
                                        typeBase,
                                        errorInfo));
                            }
                        }
                        catch (Exception e)
                        {
                            var msg = string.Format("initalize object {0} error. {1}", typeBase, e);
                            throw new ArgumentException(msg);
                        }
                    }
                    else
                    {
                        SysLog.Warn(string.Format("Addin instance of {0} is not type of TypeBase !!!", addin));
                    }
                }
            }
        }

        public virtual void InitalizeSubsystem()
        {
            m_SubsystemCollection = new List<ISubsystem>();

            if (m_DataPackage.Config.SystemConfig.SubsystemConfigCollection == null)
            {
                LogMgr.Info("There is no subsystem config .");
                return;
            }

            var config =
                m_DataPackage.Config.SystemConfig.SubsystemConfigCollection.Where(
                    w => w.SubsystemType == SubsystemType.Subsys).ToList();


            if (!config.Any())
            {
                LogMgr.Info("There is no subsystem to iniatlize.");
                return;
            }


            foreach (var subsystem in config)
            {
                var initParam = new SubsystemInitParam(m_DataPackage,
                    m_DataPackage.Config.AppConfigs.FirstOrDefault(f => f.SubsystemConfig == subsystem));
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subsystem.Dll);
                ReflectSubsystem(file, subsystem, initParam);
            }
        }

        private void ReflectSubsystem(string file, ISubsystemConfig subsystem, SubsystemInitParam initParam)
        {
            try
            {
                var subSys = ServiceLocator.Current.GetInstance(typeof(ISubsystem), subsystem.EntryClass) as ISubsystem;

                if (subSys == null)
                {
                    SysLog.Error("Sub system class 【{0}】 of app【{1}】 is not Inherited from {2}", subsystem.EntryClass,
                        subsystem.Name, typeof(ISubsystem).FullName);
                    return;
                }

                subSys.Initalize(initParam);
                m_SubsystemCollection.Add(subSys);

                SysLog.Info("Get subsystem entry class 【{0}】 of app【{1}】 success. Assembly={2}", subsystem.EntryClass,
                    subsystem.Name, subSys.GetType().Assembly);

            }
            catch (Exception e)
            {
                SysLog.Error(string.Format("Get subsystem entry class 【{0}】  of app【{1}】 error. {2}",
                    subsystem.EntryClass, subsystem.Name, e));
            }
        }

        public virtual void InitalizeService()
        {
            m_DisposeService = new DisposeService();
            m_DisposeService.Regist<IDisposeService>();

            m_EventService = new EventService();
            m_EventService.Regist<IEventService>();

            m_CourseService = new CourseService();
            m_CourseService.Regist<ICourseService>();

            m_DebugViewService = new DebugViewService();
            m_DebugViewService.Regist<IDebugViewService>();

            var viewService = new ViewService();
            m_ViewService = viewService;
            m_ViewService.Regist<IViewService>();
            m_ViewService.Regist<IRunningViewService>();

            var stationService = new StationNameProviderService();
            m_StationNameProviderService = stationService;
            m_StationNameProviderService.Regist<IStationNameProviderService>();

            var timeTableService = new TimeTableProviderService();
            m_TimeTableProviderService = timeTableService;
            m_TimeTableProviderService.Regist<ITimeTableProviderService>();

            m_DataChangeListenService = new DataChangeListenService();
            m_DataChangeListenService.Regist<IDataChangeListenService>();

            viewService.AcitvedFormChanged += (service, action) =>
            {
                if (action != NotifyCollectionChangedAction.Add)
                {
                    foreach (var source in service.AllViewFormCollection.Except(service.ActivedFormCollection))
                    {
                        if (source.InvokeRequired)
                        {
                            source.Invoke(new Action<ProjectFormBase>((f) => f.Hide()), source);
                        }
                        else
                        {
                            source.Hide();
                        }
                    }
                }
                else
                {
                    foreach (var viewForm in service.ActivedFormCollection)
                    {
                        if (viewForm.InvokeRequired)
                        {
                            viewForm.Invoke(new Action<ProjectFormBase>((f) => f.Active()), viewForm);
                        }
                        else
                        {
                            viewForm.Active();
                        }
                    }
                }
            };
            m_EventService.CoursStarting += args =>
            {
                if (m_CourseService.CurrentCourseState == CourseState.Started)
                {
                    var startParam = args.StartParam as ICourseStartParameter;
                    m_DataChangeListenService.Filter = s => true;
                    if (startParam != null && string.CompareOrdinal(startParam.Version, "2.0") >= 0)
                    {
                        if (startParam.SelectedScreens.Any())
                        {
                            var all = Config.ScreenTableConfig.ScreenItems.FindAll(f => startParam.SelectedScreens.Contains(f.Key));
                            if (all.Any())
                            {
                                m_DataChangeListenService.Filter = s => all.Exists(e => e.ProjectCollection.Contains(s));
                                m_ViewService.UpdateCurrentScreenItems(all);
                                LogMgr.Info(
                                    "Switch screen sucess in version 2.0 , selected items={0}, switch to screen={1}",
                                    string.Join("|", startParam.SelectedScreens), string.Join("|", all.SelectMany(s => s.ProjectCollection)));
                            }
                            else
                            {
                                LogMgr.Warn(
                                    "The start param version >= 2.0, but can not found any screen it where selecte items={0}, we will ignore .",
                                    string.Join(",", startParam.SelectedScreens));
                            }
                        }
                        else
                        {
                            LogMgr.Warn("The start param version >= 2.0, but SelectedScreens has not any item, we will ignore .");
                        }
                    }
                }
            };
        }


        public virtual void InitalizeMainForm(MainBaseForm mainBaseForm)
        {
            m_ServiceManager = ServiceManager.Instance;

            MainForm = mainBaseForm;
            App.Current.MainForm = MainForm;
            App.Current.MainThread = Thread.CurrentThread;
            App.Current.SetMainDispatcher(Dispatcher.CurrentDispatcher);
            m_DataPackage = new DataPackage
            {
                AddInLoader = m_AddInLoader,
                RunningParam = m_RunningParamManager.RunningParam,
                CommunicationDataFacadeService = m_CommunicatonDataFacadeService,
                Config = ConfigManager.Instance.Config,
                ObjectService = m_ObjectService,
                ServiceManager = m_ServiceManager,
            };
            MainForm.DataPackage = m_DataPackage;

            MainForm.FormClosed += (sender, args) => m_DisposeService.DisposeRegisttedObjects();

        }

        public virtual void LoadAddIns()
        {
            SysLog.Info("加载所有插件。");
            m_AddInLoader = new AddInLoader();
            m_AddInLoader.LoadAddin(ConfigManager.Instance.Config);
            m_ObjectService = new ObjectService(m_AddInLoader);
            m_ObjectService.Regist<IObjectService>();
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        public virtual void LoadConfig()
        {
            SysLog.Info("加载配置文件.");
            ConfigManager.Instance.LoadConfig(AppDomain.CurrentDomain.BaseDirectory);
        }

        /// <summary>
        /// 初始化网络
        /// </summary>
        public virtual void InitalizeNet()
        {
        }

        /// <summary>
        /// 注册网络
        /// </summary>
        public void RegistNet()
        {
            App.Current.ServiceManager.RegistService<ICommunicationDataFacadeService>(m_CommunicatonDataFacadeService);
        }

        /// <summary>
        /// 初始化键盘钩子
        /// </summary>
        public virtual void InitalizeHook()
        {
            m_HookProcess = new DefaultHookProcess(MainForm.Handle);
            m_HookProcess.HotKeyEvent += MainForm.HotKeyEvent;
        }

        public abstract MainBaseForm CreateForm();

        public virtual void FinalInitalize()
        {
        }

        /// <summary>
        /// 初始化运行时参数
        /// </summary>
        public virtual void InitalizeRunningParam()
        {

        }

        public virtual void InitalizeIndexDescriptionService()
        {
            var service = new IndexDescriptionService();
            service.Initalize((CommunicationDataFacadeServiceBase)m_CommunicatonDataFacadeService, Config);
            m_IndexDescriptionService = service;

            service.Regist<IIndexDescriptionService>();
        }

        public virtual void LoadDefaultIndexDescription()
        {
            if (Config.IndexDescriptionConfig != null)
            {
                foreach (var config in Config.IndexDescriptionConfig.IndexDescriptionConfigCollection)
                {
                    foreach (var appName in config.AppNameCollection)
                    {
                        var inter =
                            m_IndexDescriptionService.GetIndexInterpreter(new CommunicationDataKey(config.ProjectType,
                                appName));
                        if (inter != null)
                        {
                            UpdateIndexDescription(inter, config.InBoolDescriptionDictionary, AppIndexType.InBool);
                            UpdateIndexDescription(inter, config.InFloatDescriptionDictionary, AppIndexType.InFloat);
                            UpdateIndexDescription(inter, config.OutBoolDescriptionDictionary, AppIndexType.OutBool);
                            UpdateIndexDescription(inter, config.OutFloatDescriptionDictionary, AppIndexType.OutFloat);
                        }
                    }
                }
            }
            else
            {
                SysLog.Info("There is no default index description config to load.");
            }
        }

        private void UpdateIndexDescription(IIndexInterpreter inter,
            IReadOnlyDictionary<string, int> indexDescriptionDic, AppIndexType indexType)
        {
            if (indexDescriptionDic != null)
            {
                foreach (var inbkvp in indexDescriptionDic)
                {
                    inter.RegistIndexMeaning(indexType, inbkvp.Value, inbkvp.Key);
                }
            }
        }

        protected void RegistDataChangedEvents()
        {
            foreach (var appConfig in Config.AppConfigs)
            {
                var cd = m_CommunicatonDataFacadeService.GetCommunicationDataService(new CommunicationDataKey(appConfig));
                ((CommunicatonDataReadServiceBase)cd.ReadService).DataChanged +=
                    (sender, args) => m_DataChangeListenService.OnDataChanged(appConfig.AppName, sender, args);
                ((CommunicatonDataReadServiceBase)cd.ReadService).BoolChanged +=
                    (sender, args) => m_DataChangeListenService.OnBoolChanged(appConfig.AppName, sender, args);
                ((CommunicatonDataReadServiceBase)cd.ReadService).FloatChanged +=
                    (sender, args) => m_DataChangeListenService.OnFloatChanged(appConfig.AppName, sender, args);
            }
        }

        public virtual void StartNet()
        {
            ((CommunicationDataFacadeServiceBase)m_CommunicatonDataFacadeService).StartNet();
        }

    }
}
