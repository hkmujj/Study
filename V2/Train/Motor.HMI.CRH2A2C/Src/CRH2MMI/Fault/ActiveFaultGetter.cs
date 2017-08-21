using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CRH2MMI.Fault
{
    partial class FaultMgr
    {
        /// <summary>
        ///  所有未被删除的故障
        /// </summary>
        class ActiveFaultGetter : ICanDeleteFaultGetter
        {
            public ActiveFaultGetter()
            {
                CurrentShowFaultIndex = 0;
                m_IterPath = new List<FaultItemInfo>() { CurrentShowFaultItemInfo };
            }

            /// <summary>
            /// 当前显示的故障
            /// </summary>
            public FaultItemInfo CurrentShowFaultItemInfo { get; private set; }

            /// <summary>
            /// 当前显示的故障的索引
            /// </summary>
            private int m_CurrentShowFaultIndex;

            private readonly List<FaultItemInfo> m_IterPath;

            public bool HasFault { get { return FaultMgr.Instance.m_ActiveFaultItemInfos.Any(); } }

            public bool HasPreviousFault
            {
                get { return FaultMgr.Instance.m_ActiveFaultItemInfos.Count >= 2; }
            }

            public bool HasNextFault
            {
                get { return FaultMgr.Instance.m_ActiveFaultItemInfos.Count >= 2; }
            }

            /// <summary>
            /// 当前显示的故障的索引
            /// </summary>
            public int CurrentShowFaultIndex
            {
                get { return m_CurrentShowFaultIndex; }
                set
                {
                    m_CurrentShowFaultIndex = value;

                    if (value >= 0 && FaultMgr.Instance.m_ActiveFaultItemInfos.Any() && value < FaultMgr.Instance.m_ActiveFaultItemInfos.Count)
                    {
                        CurrentShowFaultItemInfo = FaultMgr.Instance.m_ActiveFaultItemInfos[value];
                        m_IterPath.Add(CurrentShowFaultItemInfo);
                    }
                    else
                    {
                        m_CurrentShowFaultIndex = 0;
                        CurrentShowFaultItemInfo = null;
                    }
                }
            }

            /// <summary>
            /// 重置当前显示的索引
            /// </summary>
            public void ResetCurrentShowFaultIndex()
            {
                CurrentShowFaultIndex = FaultMgr.Instance.m_ActiveFaultItemInfos.Count - 1;
            }

            public void GotoPreviousFault()
            {
                CurrentShowFaultIndex = (CurrentShowFaultIndex + FaultMgr.Instance.m_ActiveFaultItemInfos.Count - 1) % FaultMgr.Instance.m_ActiveFaultItemInfos.Count;

            }

            public void GotoNextFault()
            {
                CurrentShowFaultIndex = (CurrentShowFaultIndex + 1 + FaultMgr.Instance.m_ActiveFaultItemInfos.Count) % FaultMgr.Instance.m_ActiveFaultItemInfos.Count;
            }

            public void OnFaultChangedEvent(object sender, FaultChangedArgs faultChangedArgs)
            {
                switch (faultChangedArgs.Type)
                {
                    case FaultChangedType.Add:
                        ResetCurrentShowFaultIndex();
                        break;
                    case FaultChangedType.Remove:
                        if (CurrentShowFaultIndex >= FaultMgr.Instance.m_AllFaultItemInfos.Count)
                        {
                            if (HasPreviousFault)
                            {
                                GotoPreviousFault();
                            }
                        }
                        break;
                    case FaultChangedType.Clear:
                        ResetCurrentShowFaultIndex();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            /// <summary>
            /// 页面信息
            /// </summary>
            public string PageInfo
            {
                get
                {
                    return FaultMgr.Instance.m_AllFaultItemInfos.Any()
                        ? string.Format("{0}/{1}", CurrentShowFaultIndex + 1,
                            FaultMgr.Instance.m_AllFaultItemInfos.Count)
                        : "0/0";
                }
            }

            public bool CanDeleteFaultItem
            {
                get { return FaultMgr.Instance.m_HasViewdFaultItemInfos.Any(); }
            }

            /// <summary>
            /// 删除故障
            /// </summary>
            /// <param name="faultItemInfo"></param>
            public void DeleteFaultItem(FaultItemInfo faultItemInfo)
            {
                if (faultItemInfo == null)
                {
                    return;
                }
                if (!HasNextFault )
                {
                    if ( HasPreviousFault)
                    {
                        // 保证删除后 索引正确
                        GotoPreviousFault();
                    }
                    else
                    {
                        CurrentShowFaultIndex = 0;
                    }
                }
                Instance.DeactiveFault(faultItemInfo);
            }

            public ReadOnlyCollection<FaultItemInfo> IterPath { get { return m_IterPath.AsReadOnly(); } }

            public void ClearIterPath()
            {
                m_IterPath.Clear();
            }
        }
    }
}
