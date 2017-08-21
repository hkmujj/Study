using System;
using System.Linq;

namespace CRH2MMI.Fault
{
    partial class FaultMgr
    {
        class ViewFaultGetter : IViewFaultGetter
        {
            public ViewFaultGetter()
            {
                CurrentShowFaultIndex = 0;
            }

            /// <summary>
            /// 当前显示的故障
            /// </summary>
            public FaultItemInfo CurrentShowFaultItemInfo { get; private set; }

            /// <summary>
            /// 当前显示的故障的索引
            /// </summary>
            private int m_CurrentShowFaultIndex;

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

            public bool HasFaultToBeActived { get { return Instance.m_ActiveFaultItemInfos.Any(a => a.ToBeActived); } }

            public void ViewFinishied()
            {
                FaultMgr.Instance.m_ActiveFaultItemInfos.ForEach(e => e.ToBeActived = false);
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
        }
    }
}
