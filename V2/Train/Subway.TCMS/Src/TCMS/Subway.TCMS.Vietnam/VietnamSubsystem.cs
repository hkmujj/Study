using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.TCMS.DataAdapter;
using Subway.TCMS.Infrasturcture.Monitor;
using Subway.TCMS.Infrasturcture.Service;
using Subway.TCMS.Infrasturcture.ViewModel;
using Subway.TCMS.Vietnam.Model;
using Subway.TCMS.Vietnam.ViewModel;
using Subway.TCMS.Vietnam.WpfViews.Shells;

namespace Subway.TCMS.Vietnam
{
    [SubsystemExport(typeof(VietnamSubsystem))]
    public class VietnamSubsystem : ISubsystem
    {
        private DataAdapterBase m_DataAdapterBase;
        /// <summary>
        /// </summary>
        /// <param name="initParam"></param>
        public void Initalize(SubsystemInitParam initParam)
        {
            //初始化全局参数
            GlobalParams.Instance.Initalize(initParam);
            //初始化窗口
            var form = new VietnamForm();
            //获取视图服务
            var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();
            //注册窗口到视图服务
            viewService.Regist(initParam.AppConfig.AppName, form);
            //激活窗口
            viewService.Active(initParam.AppConfig.AppName);
            //初始化ViewModel
            var data = ServiceLocator.Current.GetInstance<DoMainViewModel>();
            //创建项目对应数据适配层
            var dataAdapter = DataAdapterFactory.CreatDataAdapter(data.DoMainModel);
            m_DataAdapterBase = dataAdapter;
            //初始化项目接口描述服务
            InitService(initParam, data.DoMainModel);
            //获取课程服务
            var courceService = initParam.DataPackage.ServiceManager.GetService<ICourseService>();
            //注册课程结束事件
            courceService.CourseStateChanged += CourceService_CourseStateChanged;
            //启动调试窗口时，同时启动数据监视窗口
            if (initParam.DataPackage.Config.SystemConfig.IsDebugModel)
            {
                InitDataMonitorView(initParam, data);
            }
            //注册项目的数据监听服务
            initParam.DataPackage.ServiceManager.GetService<IDataChangeListenService>().RegistListener(dataAdapter, initParam.AppConfig);
            //引起所有数据变化
            initParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
        }
        /// <summary>
        /// 课程状态变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CourceService_CourseStateChanged(object sender, CourseStateChangedArgs e)
        {
            //清除数据
            m_DataAdapterBase.ClearDataOnCourceStop();
        }

        /// <summary>
        /// 初始化数据监视窗口
        /// </summary>
        /// <param name="initParam">子系统参数</param>
        /// <param name="data">视图模型</param>
        private void InitDataMonitorView(SubsystemInitParam initParam, DoMainViewModel data)
        {
            var domainMonitor =
                new DataMonitorForm(
                    new MonitorViewModel<DoMainModel>(data.DoMainModel));
            var debugViewService = initParam.DataPackage.ServiceManager.GetService<IDebugViewService>();
            domainMonitor.Top = 10;
            domainMonitor.Left = 200;
            domainMonitor.Width = 640;
            domainMonitor.Height = 480;
            debugViewService.DebugFormCollection.Add(domainMonitor);
        }

        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="initParam"></param>
        /// <param name="tcms"></param>
        private void InitService(SubsystemInitParam initParam, Infrasturcture.Model.TCMS tcms)
        {
            //实例化项目描述文件服务
            var projectDecriptionService = new ProjectDecriptionService(tcms.Type);
            projectDecriptionService.Initaliza(initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(new CommunicationDataKey(initParam.AppConfig)));
            //注册项目描述文件服务
            var serviceManager = initParam.DataPackage.ServiceManager;
            serviceManager.RegistService<IProjectDecriptionService>(tcms.Type.ToString(), projectDecriptionService);

        }
    }
}
