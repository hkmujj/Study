using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.DataType.Log;
using MMI.Facility.DataType.Running;
using MMI.Facility.DataType.View.Form;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Addins;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Hook;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Running;
using MMI.Facility.Interface.Service;
using MMI.Facility.PublicUI.Interface;
using MMI.Facility.View.Views.CommunicationData;
using MMI.Facility.View.Views.Course;

namespace MMI.Facility.View.Views.Common
{
    /// <summary>
    /// 主控制的基础窗体
    /// 该类中预设了数据初始化、对象控制等相关方法
    /// 该类应作为主要控制类而存在
    /// </summary>
    public partial class MainBaseForm : System.Windows.Forms.Form, IMainForm
    {
        protected IDebugViewService m_DebugViewService;

        public ReadOnlyCollection<ProjectFormBase> ViewForms
        {
            get { return RunningViewService.ActivedFormCollection; }
        }

        public IShareForm ShareForm { get; private set; }

        public IProjectListInfoForm ProjectListInfoForm { get; private set; }

        public ILogicForm LogicForm { get; private set; }

        public IAttributeForm AttributeForm { get; private set; }

        public IServiceManager ServiceManager { get { return DataPackage.ServiceManager; } }

        // ReSharper disable once InconsistentNaming
        protected List<IViewForm> m_ViewForms = new List<IViewForm>();

        /// <summary>
        /// 图元对象管理
        /// </summary>
        public IViewObjectManager ViewObjectMgr { set; get; }

        private IRunningViewService m_RunningViewService;

        public IRunningViewService RunningViewService
        {
            get
            {
                return m_RunningViewService ?? ( m_RunningViewService = DataPackage.ServiceManager.GetService<IRunningViewService>() );
            }
        }

        public IRunningParam RunningParam { get { return DataPackage.RunningParam; } }

        /// <summary>
        /// 配置文件管理 
        /// </summary>
        public IConfigManager ConfigManager { set; get; }

        public IConfig Config { get { return DataPackage.Config; } }


        public IDataPackage DataPackage { get; set; }


        public ICommunicationDataFacadeService CommunicationDataService { get { return DataPackage.CommunicationDataFacadeService; } }


        /// <summary>
        /// 保存类对象类型 ,key :文件名+类名 value: type
        /// </summary>
        public IReadOnlyDictionary<string, Type> AddinTypeDic { get { return AddInLoader.AddinTypeDictionary; } }

        /// <summary>
        /// 
        /// </summary>
        public IAddInLoader AddInLoader { get { return DataPackage.AddInLoader; } }


        /// <summary>
        /// 构造函数
        /// </summary>
        public MainBaseForm()
        {
            InitializeComponent();

            App.Current.MainForm = this;

            Load += MainBaseForm_Load;

            Closed += OnClosed;
        }



        /// <summary>
        /// MainBaseForm初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MainBaseForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                m_DebugViewService = ServiceManager.GetService<IDebugViewService>();

                if (DataPackage.Config.SystemConfig.IsDebugModel)
                {
                    m_DebugViewService.DebugFormCollection.CollectionChanged += DebugFormCollectionOnCollectionChanged;
                }

                Initalize();

                SetRunInitValue();

                ShowAllWindows();

                DataPackage.RunningParam.CommunicationFacadeDataService.NetServiceBegin += (o, args) => SetCourseStart();
                DataPackage.RunningParam.CommunicationFacadeDataService.NetServiceEnd += (o, args) => SetCourseOver();
            }
        }

        private void DebugFormCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            switch (notifyCollectionChangedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ShowItems(notifyCollectionChangedEventArgs.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    CloseItems(notifyCollectionChangedEventArgs.NewItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    CloseItems(notifyCollectionChangedEventArgs.OldItems);
                    ShowItems(notifyCollectionChangedEventArgs.NewItems);
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    CloseItems(m_DebugViewService.DebugFormCollection);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ShowItems(IEnumerable items)
        {
            foreach (System.Windows.Forms.Form item in items)
            {
                item.Show();
                item.Bounds = new Rectangle(item.RestoreBounds.Location, item.RestoreBounds.Size);
            }
        }

        private void CloseItems(IEnumerable items)
        {
            foreach (System.Windows.Forms.Form item in items)
            {
                item.Close();
            }
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {

            CloseItems(ViewForms);

            CloseItems(m_DebugViewService.DebugFormCollection);
        }

        /// <summary>
        /// 热键处理函数
        /// </summary>
        /// <param name="eHotKeyValue"></param>
        public virtual void HotKeyEvent(HotKeys eHotKeyValue) { }

        /// <summary>
        /// 设置视图开始编号
        /// </summary>
        protected void SetRunInitValue()
        {
            foreach (var runningViewParam in RunningParam.AppRunningParamDictionary.Where(w => w.Value.AppConfig.SubsystemType == SubsystemType.Addin))
            {
                var appName = runningViewParam.Key;
                runningViewParam.Value.RunningViewParam.CurrentRunningViewUnitParam =
                    runningViewParam.Value.RunningViewParam.ViewUnitParamDic[Config.AppConfigs.First(f => f.AppName == appName).ActureFormViewConfig.CourseStartViewIndex];
            }
        }


        protected void SetCourseOver()
        {
            foreach (var runningViewParam in RunningParam.AppRunningParamDictionary.Where(w => w.Value.AppConfig.SubsystemType == SubsystemType.Addin))
            {
                var appName = runningViewParam.Key;

                var config = Config.AppConfigs.FirstOrDefault(f => f.AppName == appName);
                if (config != null)
                {
                    var endViewIndex = config.ActureFormViewConfig.CourseStopViewIndex;
                    var running = runningViewParam.Value.RunningViewParam;
                    if (running.ViewUnitParamDic.ContainsKey(endViewIndex))
                    {
                        running.CurrentRunningViewUnitParam = running.ViewUnitParamDic[endViewIndex];
                    }
                    else
                    {
                        LogMgr.Error(
                            string.Format(
                                "can not switch to view {0} when course stopped. There is no view index of it !!!!!!",
                                endViewIndex));
                    }
                }
                else
                {
                    LogMgr.Error(string.Format("Can not found app config when course stopped where appName is {0}", appName));
                }
            }
        }

        protected void SetCourseStart()
        {
            foreach (var runningViewParam in RunningParam.AppRunningParamDictionary.Where(w => w.Value.AppConfig.SubsystemType == SubsystemType.Addin))
            {
                var appName = runningViewParam.Key;
                runningViewParam.Value.RunningViewParam.CurrentRunningViewUnitParam =
                    runningViewParam.Value.RunningViewParam.ViewUnitParamDic[Config.AppConfigs.First(f => f.AppName == appName).ActureFormViewConfig.CourseStartViewIndex];
            }
        }

        public void Initalize()
        {
            ShareForm = CreateShareForm();
            ProjectListInfoForm = CreateProjectListInfoForm();
            LogicForm = CreateLogicForm();
            AttributeForm = CreateAttributeForm();

            if (ShareForm != null)
            {
                m_DebugViewService.DebugFormCollection.Add(ShareForm as System.Windows.Forms.Form);
            }
            if (ProjectListInfoForm != null)
            {
                m_DebugViewService.DebugFormCollection.Add(ProjectListInfoForm as System.Windows.Forms.Form);
            }
            if (LogicForm != null)
            {
                m_DebugViewService.DebugFormCollection.Add(LogicForm as System.Windows.Forms.Form);
            }
            if (AttributeForm != null)
            {
                m_DebugViewService.DebugFormCollection.Add(AttributeForm as System.Windows.Forms.Form);
            }

            m_DebugViewService.DebugFormCollection.Add(new CourseForm(DataPackage));

            m_ViewForms = CreateViewForms().ToList();
        }


        public virtual void ShowAllWindows()
        {

            foreach (ProjectFormBase viewForm in RunningViewService.AllViewFormCollection)
            {
                viewForm.Show();
                viewForm.Invalidate();
                viewForm.Bounds = new Rectangle(viewForm.RestoreBounds.Location, viewForm.RestoreBounds.Size);
                if (!RunningViewService.ActivedFormCollection.Contains(viewForm))
                {
                    viewForm.Hide();
                }
            }

            if (DataPackage.Config.SystemConfig.IsDebugModel)
            {
                ShowItems(m_DebugViewService.DebugFormCollection);
            }
        }

        public virtual IShareForm CreateShareForm()
        {
            SysLog.Debug(string.Format("CreateShareForm do nothing, main form of type({0}) is not need share form .", GetType()));
            return null;
        }

        public virtual IProjectListInfoForm CreateProjectListInfoForm()
        {
            SysLog.Info(string.Format("CreateProjectListInfoForm do nothing, main form of type({0}) is not need ProjectListInfo form .", GetType()));
            return null;
        }

        public virtual ILogicForm CreateLogicForm()
        {
            SysLog.Info(string.Format("CreateLogicForm do nothing, main form of type({0}) is not need logic form .", GetType()));
            return null;
        }

        public virtual IAttributeForm CreateAttributeForm()
        {
            SysLog.Info(string.Format("CreateAttributeForm do nothing, main form of type({0}) is not need Attribute form .", GetType()));
            return null;
        }

        public virtual IEnumerable<IViewForm> CreateViewForms()
        {
            SysLog.Info(string.Format("CreateViewForms do nothing, main form of type({0}) is not need view form .", GetType()));
            return new List<IViewForm>();
        }
    }
}