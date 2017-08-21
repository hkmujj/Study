using System;
using System.Collections.Generic;
using CommonUtil.Util;
using CRH2MMI.Common.Global;

namespace CRH2MMI.CutState
{
    class RemovalStateMgr
    {
        private static RemovalStateMgr m_Instance;

        /// <summary>
        /// 切除状态变化 
        /// </summary>
        public EventHandler<RemovalStateChangedArgs> RemovalStateChanged;

        private CutInfoConfig m_CutInfoConfig;

        private Dictionary<int, RemovalStateData> m_States;

        public static RemovalStateMgr Instance
        {
            get { return m_Instance ?? (m_Instance = new RemovalStateMgr()); }
        }

        private RemovalStateMgr()
        {
            m_States = new Dictionary<int, RemovalStateData>();
            if (GlobalParam.Instance.ConfigLoadCompleted)
            {
                UpdateConfig();
            }
            else
            {
                GlobalEvent.Instance.ConfigLoadCompleteEvent += (sender, args) => UpdateConfig();
            }

        }

        private void UpdateConfig()
        {
            m_CutInfoConfig = GlobalInfo.CurrentCRH2Config.CutInfoConfig;
            foreach (var row in m_CutInfoConfig.GridLine[0].Rows)
            {
                foreach (var column in row.ColumnConfigs)
                {
                    var logicName = column.InBoolColoumNames[0];
                    var name = string.Format("{0}车厢{1}", column.ColumIndex + 1, row.Name);
                    var rsd = new RemovalStateData(name, logicName);
                    if (m_States.ContainsKey(rsd.LogicIndex))
                    {
                        LogMgr.Error(string.Format("{0} has config...", name));
                    }
                    else
                    {
                        m_States.Add(rsd.LogicIndex, rsd);
                    }
                }
            }
        }

        public void RefreshStates()
        {
            foreach (var kvp in m_States)
            {
                var curent = GlobalInfo.Instance.BoolList[kvp.Key];
                if ( curent != kvp.Value.State)
                {
                    kvp.Value.State = curent;
                    HandleUtil.OnHandle(RemovalStateChanged,
                        this,
                        curent
                            ? new RemovalStateChangedArgs(RemovalStateChangedType.Add, kvp.Value.Name)
                            : new RemovalStateChangedArgs(RemovalStateChangedType.Removel, kvp.Value.Name));
                }
            }
        }

        protected class RemovalStateData
        {
            public RemovalStateData(string name, string logicName)
            {
                LogicName = logicName;
                LogicIndex = GlobalInfo.Instance.GetInBoolIndex(LogicName);
                Name = name;
            }

            public string Name { private set; get; }

            public bool State { set; get; }

            public string LogicName { private set; get; }

            public int LogicIndex { private set; get; }
        }
    }
}
