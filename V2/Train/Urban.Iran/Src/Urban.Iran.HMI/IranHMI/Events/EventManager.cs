using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using Excel.Interface;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI.Events
{
    internal class EventManager
    {
        public static EventManager Instance { private set; get; }

        /// <summary>
        /// key : EventInfo.LogicIndex
        /// </summary>
        public IReadOnlyDictionary<int, EventInfo> AllEvent { get; set; }

        /// <summary>
        /// key : EventLife.EventKey  =  EventInfo.LogicIndex
        /// </summary>
        public ReadOnlyDictionary<int, EventLife> ActivedEventCollection { get; private set; }

        private readonly Dictionary<int, EventLife> m_ConcreateActivedEventCollection;

        public event Action<EventLife> EventAdded;

        public event Action<EventLife> EventRemoved;


        /// <summary>
        /// CurrentActivingEvent 变化
        /// </summary>
        public event Action CurrentActivingEventChanged;

        public ObservableCollection<EventLife> HistoryEventList { get; private set; }


        static EventManager()
        {
            Instance = new EventManager();
        }


        public EventManager()
        {
            HistoryEventList = new ObservableCollection<EventLife>();
            ActivedEventCollection =
                new ReadOnlyDictionary<int, EventLife>(
                    m_ConcreateActivedEventCollection = new Dictionary<int, EventLife>());
        }

        public EventLife GetCurrentActivableEvent()
        {
            return ActivedEventCollection.Values.FirstOrDefault(f => !f.IsAcknownleaged);
        }

        public bool AcknownleageAllActivingEvents()
        {
            while (AcknownleageCurrentActivingEventIfHas())
            {

            }
            return true;
        }

        public bool AcknownleageCurrentActivingEventIfHas()
        {
            var current = GetCurrentActivableEvent();
            if (current != null)
            {
                current.IsAcknownleaged = true;
                AddHistoryEvent(current);
                OnCurrentActivingEventChanged();
                return true;
            }
            return false;
        }

        public void RefreshEvents(MMI.Facility.Interface.IReadOnlyDictionary<int, bool> boolList)
        {
            var current = GetCurrentActivableEvent();
            foreach (var kvp in Instance.AllEvent)
            {
                var key = kvp.Key;
                if (boolList[key] && !m_ConcreateActivedEventCollection.ContainsKey(key))
                {
                    var el = new EventLife(key, DateTime.Now);
                    m_ConcreateActivedEventCollection.Add(key, el);
                    OnEventAdded(el);

                }
                else if (!boolList[key] && m_ConcreateActivedEventCollection.ContainsKey(key))
                {
                    var el = ActivedEventCollection[key];
                    el.EndTime = DateTime.Now;
                    AddHistoryEvent(el);
                    m_ConcreateActivedEventCollection.Remove(el.EventKey);

                    OnEventRemoved(el);
                }
            }

            if (current != GetCurrentActivableEvent())
            {
                OnCurrentActivingEventChanged();
            }
        }

        private void AddHistoryEvent(EventLife el)
        {
            if (!HistoryEventList.Contains(el))
            {
                HistoryEventList.Add(el);
            }
        }


        public void InitalizeEventInfos(string file)
        {
            var readConfig = new ExcelReaderConfig()
            {
                Coloumns = new List<ColoumnConfig>() {new ColoumnConfig() {IsPrimaryKey = false, Name = "*"}},
                File = file,
                SheetNames = new List<string>() {"事件信息表"}
            };

            var ds = readConfig.Adapter();

            var dic = new Dictionary<int, EventInfo>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int logic =int.MaxValue;
                try
                {
                    logic = Convert.ToInt32(row["事件逻辑位"]);
                }
                catch (Exception e)
                {
                    LogMgr.Debug("Can not parser 事件逻辑位 value={0}", row["事件逻辑位"]);
                }
                EventPriority priority;
                var eventType = row["事件类型"].ToString();
                if (!Enum.TryParse(eventType, true, out priority))
                {
                    LogMgr.Debug("Can not parser 事件类型 where 事件逻辑位={0} value={1}", logic, eventType);
                    priority = EnumUtil.FindEnumByDescriptio<EventPriority>(eventType);
                }

                var groupInfo = row["事件分组信息"].ToString();
                EventGroup eventGroup;
                if (!Enum.TryParse(groupInfo, true, out eventGroup))
                {
                    LogMgr.Debug("Can not parser 事件分组信息 where 事件逻辑位={0} value={1}", logic, groupInfo);
                    eventGroup = EnumUtil.FindEnumByDescriptio<EventGroup>(groupInfo);
                }

                var info = new EventInfo(logic, priority,
                    Convert.ToInt32(row["事件号"]),
                    eventGroup,
                    row["描述信息"].ToString(),
                    row["事件信息第一页"].ToString().Replace('/', '\n'),
                    row["事件信息第二页"].ToString().Replace('/', '\n'),
                    row["车厢号"].ToString());
                dic.Add(info.LogicIndex, info);
            }

            AllEvent = dic.AsReadOnlyDictionary();

            LogMgr.Info("Parsered {0} count of event of file {1}， all fault logic index = 【{2}】", AllEvent.Count,
                readConfig.File, string.Join(",", AllEvent.Keys));
        }

        protected virtual void OnEventAdded(EventLife obj)
        {
            var handler = EventAdded;
            if (handler != null)
            {
                handler(obj);
            }
        }

        protected virtual void OnEventRemoved(EventLife obj)
        {
            var handler = EventRemoved;
            if (handler != null)
            {
                handler(obj);
            }
        }

        protected virtual void OnCurrentActivingEventChanged()
        {
            var handler = CurrentActivingEventChanged;
            if (handler != null)
            {
                handler();
            }
        }
    }
}
