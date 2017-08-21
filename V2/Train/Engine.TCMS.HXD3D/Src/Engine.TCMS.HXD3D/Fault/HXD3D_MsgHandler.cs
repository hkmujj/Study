using System;
using System.Collections.Generic;
using Engine.TCMS.HXD3D.Title;
using MsgReceiveMod;

namespace Engine.TCMS.HXD3D.Fault
{
    public class HXD3DMsgHandler<T> : MsgHandlerFault0<T> where T : BaseMsgContent
    {
        // Fields
        private List<T> m_CurLowLevelMsgList;
        private List<T> m_TestMsgList;
        private List<T> m_UnFlagCurrentMsgList;
        public static SortCriteriaOfMsg UnFlagMsgSortName { set; get; }

        // Methods
        public HXD3DMsgHandler(SortCriteriaOfMsg unreadCurrentMsgSort)
        {
            m_UnFlagCurrentMsgList = new List<T>();
            m_TestMsgList = new List<T>();
            SubMsgList = new List<T>();
            m_CurLowLevelMsgList = new List<T>();
            CurHighLevelMsgList = new List<T>();
            UnFlagMsgSortName = unreadCurrentMsgSort;
        }

        public override void AddNewMsg(T newMsg)
        {
            TestMsgListSort(newMsg);
            if ((newMsg.TheMsgLevel == MsgLevels.Level0) || (newMsg.TheMsgLevel == MsgLevels.Level1) || (newMsg.TheMsgLevel == MsgLevels.Level2) || (newMsg.TheMsgLevel == MsgLevels.Level3))
            {
                CurHighLevelMsgList.Add(newMsg);
                m_UnFlagCurrentMsgList.Add(newMsg);
            }
            else
            {
                m_CurLowLevelMsgList.Add(newMsg);
                SubMsgList.Add(newMsg);
            }
            base.AddNewMsg(newMsg);
        }

        public override void ClearAllList()
        {
            base.ClearAllList();
            m_UnFlagCurrentMsgList.Clear();
            m_TestMsgList.Clear();
            m_CurLowLevelMsgList.Clear();
            CurHighLevelMsgList.Clear();
        }

        public override void MsgBeRead(int theMsgInList)
        {
            if (theMsgInList < 0)
            {
                return;
            }

            if (m_UnFlagCurrentMsgList.Count > theMsgInList)
            {
                T local2;
                foreach (var local in allMsgsList)
                {
                    local2 = m_UnFlagCurrentMsgList[theMsgInList];
                    if (local.MsgID == local2.MsgID)
                    {
                        local.TheMsgFlag = true;
                        break;
                    }
                }
                foreach (var local in currentMsgList)
                {
                    local2 = m_UnFlagCurrentMsgList[theMsgInList];
                    if (local.MsgID == local2.MsgID)
                    {
                        local.TheMsgFlag = true;
                        break;
                    }
                }
                foreach (var local in CurHighLevelMsgList)
                {
                    local2 = m_UnFlagCurrentMsgList[theMsgInList];
                    if (local.MsgID == local2.MsgID)
                    {
                        local.TheMsgFlag = true;
                        break;
                    }
                }
                foreach (var local in m_TestMsgList)
                {
                    local2 = m_UnFlagCurrentMsgList[theMsgInList];
                    if (local.MsgID == local2.MsgID)
                    {
                        local.TheMsgFlag = true;
                        break;
                    }
                }
                m_UnFlagCurrentMsgList.Remove(m_UnFlagCurrentMsgList[theMsgInList]);
            }
        }

        public void MsgOver(string msgId)
        {
            MsgOver(Convert.ToInt32(msgId));
        }

        public override void MsgOver(int msgID)
        {
            var id = msgID.ToString();
            foreach (var local in CurHighLevelMsgList)
            {
                if (local.MsgID == id)
                {
                    CurHighLevelMsgList.Remove(local);
                    break;
                }
            }
            foreach (var local in m_CurLowLevelMsgList)
            {
                if (local.MsgID == id)
                {
                    m_CurLowLevelMsgList.Remove(local);
                    break;
                }
            }
            foreach (var local in m_UnFlagCurrentMsgList)
            {
                if (local.MsgID == id)
                {
                    m_UnFlagCurrentMsgList.Remove(local);
                    break;
                }
            }
            foreach (var local in m_TestMsgList)
            {
                if ((local.MsgID == id ) && local.FaultBeOver)
                {
                    local.MsgOverTime = DateTime.Now;
                    local.FaultBeOver = true;
                    break;
                }
            }
            foreach (var local in SubMsgList)
            {
                if ((local.MsgID == id ) && local.FaultBeOver)
                {
                    local.MsgOverTime = DateTime.Now;
                    local.FaultBeOver = true;
                    if (local.TheMsgLevel == MsgLevels.Level3)
                    {
                        TopTitleScreen.Countdown.CounterStart();
                    }
                    break;
                }
            }
            base.MsgOver(msgID);
        }

        public void TestMsgListSort(T newMsg)
        {
            if (m_TestMsgList.Count > 0)
            {
                foreach (var local in m_TestMsgList)
                {
                    if (newMsg.MsgID == local.MsgID)
                    {
                        newMsg.TheSameMsgNumb += local.TheSameMsgNumb;
                        m_TestMsgList.Add(newMsg);
                        m_TestMsgList.Remove(local);
                    }
                    else
                    {
                        m_TestMsgList.Add(newMsg);
                    }
                    break;
                }
            }
            else
            {
                m_TestMsgList.Add(newMsg);
            }
        }

        public void UnFlagMsgListSort(SortCriteriaOfMsg sortCriteria)
        {
            MsgListSort(ref m_UnFlagCurrentMsgList, sortCriteria);
            MsgListSort(ref m_TestMsgList, sortCriteria);
            MsgListSort(ref m_CurLowLevelMsgList, sortCriteria);
        }

        // Properties
        public List<T> CurHighLevelMsgList { get; private set; }

        public List<T> CurLowLevelMsgList
        {
            get
            {
                return m_CurLowLevelMsgList;
            }
        }

        public List<T> SubMsgList { get; private set; }

        public List<T> TestMsgList
        {
            get
            {
                return m_TestMsgList;
            }
        }

        public List<T> UnFlagCurrentMsgList
        {
            get
            {
                return m_UnFlagCurrentMsgList;
            }
        }
    }
}