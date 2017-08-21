using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Subway.ShenZhenLine9.Controller.ViewModelController;
using Subway.ShenZhenLine9.Interfaces;
using Subway.ShenZhenLine9.Models;
using Subway.ShenZhenLine9.Service;

namespace Subway.ShenZhenLine9.ViewModels
{
    /// <summary>
    /// 事件信息ViewModel
    /// </summary>
    [Export]
    public class EventInfoViewModel : ViewModelBase
    {
        private ObservableCollection<IEventInfo> m_DisplayInfos;
        private int m_TotalPage;
        private int m_Page;
        private IEventInfo m_Select;
        private IEventInfo m_ConfirmEventInfo;
        private EventLevel m_CurrentLevel;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller">控制类</param>
        [ImportingConstructor]
        public EventInfoViewModel(EventInfoController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
            EventManagerService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<EventManagerService>();
            DisplayInfos = new ObservableCollection<IEventInfo>();
            Controller.Initalize();
        }

        /// <summary>
        /// 控制类
        /// </summary>
        public EventInfoController Controller { get; }
        /// <summary>
        /// 故障管理服务
        /// </summary>
        public EventManagerService EventManagerService { get; private set; }

        /// <summary>
        /// 显示信息
        /// </summary>
        public ObservableCollection<IEventInfo> DisplayInfos
        {
            get { return m_DisplayInfos; }
            private set
            {
                if (Equals(value, m_DisplayInfos))
                {
                    return;
                }
                m_DisplayInfos = value;
                RaisePropertyChanged(() => DisplayInfos);
            }
        }

        /// <summary>
        /// 确认信息
        /// </summary>
        public IEventInfo ConfirmEventInfo
        {
            get { return m_ConfirmEventInfo; }
            set
            {
                if (Equals(value, m_ConfirmEventInfo))
                {
                    return;
                }
                m_ConfirmEventInfo = value;
                RaisePropertyChanged(() => ConfirmEventInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public EventLevel CurrentLevel
        {
            get { return m_CurrentLevel; }
            set
            {
                if (value == m_CurrentLevel)
                {
                    return;
                }
                m_CurrentLevel = value;
                RaisePropertyChanged(() => CurrentLevel);
            }
        }

        /// <summary>
        /// 选中项
        /// </summary>
        public IEventInfo Select
        {
            get { return m_Select; }
            set
            {
                if (Equals(value, m_Select))
                {
                    return;
                }
                m_Select = value;
                RaisePropertyChanged(() => Select);
            }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int Page
        {
            get { return m_Page; }
            set
            {
                if (value == m_Page)
                {
                    return;
                }
                m_Page = value;
                RaisePropertyChanged(() => Page);
            }
        }

        /// <summary>
        /// 总页
        /// </summary>
        public int TotalPage
        {
            get { return m_TotalPage; }
            set
            {
                if (value == m_TotalPage)
                {
                    return;
                }
                m_TotalPage = value;
                RaisePropertyChanged(() => TotalPage);
            }
        }
    }
}