using System.Collections.Generic;
using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Common;
using CRH2MMI.Common.Models;

namespace CRH2MMI.Notify
{
    class NotifyManager
    {
        public static NotifyManager Instance { private set; get; }

        static NotifyManager()
        {
            Instance = new NotifyManager();
        }

        private NotifyManager()
        {
            m_NotifyGetterDictionary = new Dictionary<string, NotifyGetter>();
        }

        public Dictionary<NotifyType, List<NotifyState>> NotifyDictionary { get; private set; }

        private readonly Dictionary<string, NotifyGetter> m_NotifyGetterDictionary;

        /// <summary>
        /// 有新通知
        /// </summary>
        public bool HasNewNotify { set; get; }

        public CRH2BaseClass ViewClass { get; private set; }

        public bool Initalize(CRH2BaseClass viewClass, NotifyConfig notifyConfig)
        {
            if (ViewClass != viewClass)
            {
                ViewClass = viewClass;
                NotifyDictionary = new Dictionary<NotifyType, List<NotifyState>>();
                foreach (GridRowConfig gridRowConfig in notifyConfig.Grid.Rows)
                {
                    NotifyType type;
                    switch (gridRowConfig.Name)
                    {
                        case "紧急报警":
                            type = NotifyType.Enmagerce;
                            break;
                        default:
                            type = NotifyType.Fire;
                            break;
                    }
                    var ns = gridRowConfig.ColumnConfigs.Select(gridColumnConfig => new NotifyState()
                                                                                    {
                                                                                        InboolName = gridColumnConfig.InBoolColoumNames[0],
                                                                                        Type = type,
                                                                                        InboolIndex =
                                                                                            ViewClass.GetInBoolIndex(
                                                                                                gridColumnConfig.InBoolColoumNames[0])
                                                                                    }).ToList();

                    NotifyDictionary.Add(type, ns);
                }
                return true;
            }
            return false;
        }

        public NotifyGetter GetOrCreateNotifyGetter(string appName)
        {
            if (m_NotifyGetterDictionary.ContainsKey(appName))
            {
                LogMgr.Debug(string.Format("Get exist notify geeter where appName = {0}", appName));
                return m_NotifyGetterDictionary[appName];
            }

            LogMgr.Debug(string.Format("Create new notify getter where appName = {0}.", appName));
            var geter = new NotifyGetter() { Tag = appName };
            geter.HasResponseHasChanged += getter =>
            {
                if (geter.HasResponse)
                {
                    if (m_NotifyGetterDictionary.Values.All(a => a.HasResponse))
                    {
                        LogMgr.Debug("All of notify has response, set nHasNewNotify = false.");
                        HasNewNotify = false;
                    }
                }
            };
            m_NotifyGetterDictionary.Add(appName, geter);
            return geter;
        }


        /// <summary>
        /// 有通知
        /// </summary>
        public bool HasNotify() { return NotifyDictionary.Any(a => a.Value.Any(an => an.HasOccur)); }

        /// <summary>
        /// 有通知
        /// </summary>  
        public bool HasNotify(NotifyType type)
        {
            return NotifyDictionary[type].Any(a => a.HasOccur);
        }

        public void UpdateNotifyState()
        {
            foreach (var kvp in NotifyDictionary)
            {
                foreach (var notifyState in kvp.Value)
                {
                    var current = ViewClass.BoolList[notifyState.InboolIndex];
                    if (current && !notifyState.HasOccur)
                    {
                        HasNewNotify = true;
                        LogMgr.Debug("Has new notify.");
                        foreach (var notifyGetter in m_NotifyGetterDictionary)
                        {
                            LogMgr.Debug(string.Format("set {0} response = false.", notifyGetter.Key));
                            notifyGetter.Value.HasResponse = false;
                        }
                    }
                    notifyState.HasOccur = current;
                }
            }
        }
    }
}
