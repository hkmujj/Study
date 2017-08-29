using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Interfaces;
using Motor.HMI.CRH380D.Models.State;
using Motor.HMI.CRH380D.Service;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    [Export(typeof(IModels))]
    public class EventInfoModel : ModelBase
    {
        private ObservableCollection<IEventInfo> m_EventInfoDisplayItems;
        
        private int m_CurPageNum;
       
        private int m_MaxPageNum;

        private int m_TotalCount;
        
        private IEventInfo m_SelectEvent;

        private IEventInfo m_WaitConfirmEvent;
        
        public int m_UnConfirmEventCount = 0;

        [ImportingConstructor]
        public EventInfoModel(EventInfoController eventInfoController)
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                EventManagerService =
                 GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<EventManagerService>();

                EventInfoDisplayItems = new ObservableCollection<IEventInfo>();
            }

            EventInfoController = eventInfoController;
            EventInfoController.ViewModel = this;
            EventInfoController.Initalize();
        }

        public override void Initialize()
        {
            EventManagerService.Initialize();
        }

        public override void Clear()
        {
            EventInfoDisplayItems.Clear();
            EventManagerService.Clear();
        }

        /// <summary>
        /// 故障管理服务
        /// </summary>
        public EventManagerService EventManagerService { get; private set; }

        /// <summary>
        /// 故障控制
        /// </summary>
        public EventInfoController EventInfoController { get; private set; }

        /// <summary>
        /// 需要显示的事件表
        /// </summary>
        public ObservableCollection<IEventInfo> EventInfoDisplayItems
        {
            get { return m_EventInfoDisplayItems; }
            set
            {
                if (Equals(value, m_EventInfoDisplayItems))
                {
                    return;
                }
                m_EventInfoDisplayItems = value;
                RaisePropertyChanged(() => EventInfoDisplayItems);
            }
        }


        /// <summary>
        /// 当前页编号
        /// </summary>
        public int CurPageNum
        {
            get { return m_CurPageNum; }
            set
            {
                if (value == m_CurPageNum)
                {
                    return;
                }
                m_CurPageNum = value;
                RaisePropertyChanged(() => CurPageNum);
            }
        }


        /// <summary>
        /// 总页编号
        /// </summary>
        public int MaxPageNum
        {
            get { return m_MaxPageNum; }
            set
            {
                if (value == m_MaxPageNum)
                {
                    return;
                }
                m_MaxPageNum = value;
                RaisePropertyChanged(() => MaxPageNum);
            }
        }


        /// <summary>
        /// 选中的事件
        /// </summary>
        public IEventInfo SelectEvent
        {
            get { return m_SelectEvent; }
            set
            {
                if (Equals(value, m_SelectEvent))
                {
                    return;
                }
                m_SelectEvent = value;
                RaisePropertyChanged(() => SelectEvent);
            }
        }



        /// <summary>
        /// 等待确认的事件
        /// </summary>
        public IEventInfo WaitConfirmEvent
        {
            get { return m_WaitConfirmEvent; }
            set
            {
                if (Equals(value, m_WaitConfirmEvent))
                {
                    return;
                }
                m_WaitConfirmEvent = value;
                RaisePropertyChanged(() => WaitConfirmEvent);
            }
        }
    }
}