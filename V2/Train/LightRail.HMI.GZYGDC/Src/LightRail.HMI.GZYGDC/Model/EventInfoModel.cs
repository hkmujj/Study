using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.Interface;
using LightRail.HMI.GZYGDC.Model.Units;
using LightRail.HMI.GZYGDC.Service;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.Model
{
    [Export]
    public class EventInfoModel : NotificationObject
    {
        private ObservableCollection<IEventInfo> m_EventInfoDisplayItems;

        
        private int m_CurPageNum;

       
        private int m_MaxPageNum;

        private int m_TotalCount;


        private IEventInfo m_SelectEvent;

        private IEventInfo m_WaitConfirmEvent;

        private EventLevel m_CurEventLevel;

        public int m_UnConfirmEventCount = 0;


        public EventInfoModel()
        {
            EventManagerService =
                GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<EventManagerService>();

            EventInfoDisplayItems = new ObservableCollection<IEventInfo>();

            
        }


        /// <summary>
        /// 故障管理服务
        /// </summary>
        public EventManagerService EventManagerService { get; private set; }


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
        /// 总记录条数
        /// </summary>
        public int TotalCount
        {
            get { return m_TotalCount; }
            set
            {
                if (value == m_TotalCount)
                {
                    return;
                }
                m_TotalCount = value;
                RaisePropertyChanged(() => TotalCount);
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



        /// <summary>
        /// 事件等级
        /// </summary>
        public EventLevel CurEventLevel
        {
            get { return m_CurEventLevel; }
            set
            {
                if (Equals(value, m_CurEventLevel))
                {
                    return;
                }
                m_CurEventLevel = value;
                RaisePropertyChanged(() => CurEventLevel);
            }
        }

        /// <summary>
        /// 获取没有确认的事件数量
        /// </summary>
        public int UnConfirmEventCount
        {
            get
            {
                return m_UnConfirmEventCount;
            }
            set
            {
                if (value == m_UnConfirmEventCount)
                {
                    return;
                }
                m_UnConfirmEventCount = value;
                RaisePropertyChanged(() => UnConfirmEventCount);
            }
        }

    }
}