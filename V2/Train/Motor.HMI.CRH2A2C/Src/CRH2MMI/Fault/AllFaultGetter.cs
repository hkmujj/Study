using System;
using System.Linq;

namespace CRH2MMI.Fault
{
    partial class FaultMgr
    {
        /// <summary>
        /// 所有的故障
        /// </summary>
        public class AllFaultGetter : IFaultGetter
        {
            public AllFaultGetter()
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

            /// <summary>
            ///  是否存在故障
            /// </summary>
            public bool HasFault { get { return FaultMgr.Instance.m_AllFaultItemInfos.Any(); } }

            public bool HasPreviousFault
            {
                get { return CurrentShowFaultIndex - 1 >= 0; }
            }

            public bool HasNextFault
            {
                get { return CurrentShowFaultIndex + 1 < FaultMgr.Instance.m_AllFaultItemInfos.Count; }
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

                    if (value >= 0 && FaultMgr.Instance.m_AllFaultItemInfos.Any() && value < FaultMgr.Instance.m_AllFaultItemInfos.Count)
                    {
                        CurrentShowFaultItemInfo = FaultMgr.Instance.m_AllFaultItemInfos[value];
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
                CurrentShowFaultIndex = 0;
            }

            public void GotoPreviousFault()
            {
                --CurrentShowFaultIndex;
            }

            public void GotoNextFault()
            {
                ++CurrentShowFaultIndex;
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

            public void OnFaultChangedEvent(object sender, FaultChangedArgs faultChangedArgs)
            {
                switch (faultChangedArgs.Type)
                {
                    case FaultChangedType.Add :
                        break;
                    case FaultChangedType.Remove :
                        if (CurrentShowFaultIndex >= FaultMgr.Instance.m_AllFaultItemInfos.Count)
                        {
                            if (HasPreviousFault)
                            {
                                GotoPreviousFault();
                            }
                        }
                        break;
                    case FaultChangedType.Clear :
                        CurrentShowFaultIndex = 0;
                        break;
                    default :
                        throw new ArgumentOutOfRangeException();
                }

            }
        }
    }
    
}
