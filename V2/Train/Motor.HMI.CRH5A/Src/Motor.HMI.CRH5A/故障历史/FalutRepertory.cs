using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtil.Model;
using Motor.HMI.CRH5A.底层共用.消息;

namespace Motor.HMI.CRH5A.故障历史
{
    internal class FalutRepertory : List<FaultMsg5>
    {

        private FalutSortType m_LastSortType;

        public FalutRepertory(int countPerPage)
        {
            CountPerPage = countPerPage;
        }

        public int CountPerPage { private set; get; }

        public int TotalPage { private set; get; }

        public int CurrentPageIndex { private set; get; }

        public FaultMsg5 CurrentSelectedMsg
        {
            get
            {
                return CurrentSelectedMsgIndex == -1
                    ? null
                    : CurrentPage.Take(CurrentSelectedMsgIndex + 1).LastOrDefault();
            }
        }

        public int CurrentSelectedMsgIndex { private set; get; }

        public IEnumerable<FaultMsg5> CurrentPage
        {
            get { return this.Skip(CurrentPageIndex*CountPerPage).Take(CountPerPage); }
        }

        public void UpdateHistory(IList<FaultMsg5> allFaultMsgs)
        {
            m_LastSortType = FalutSortType.Time;
            TotalPage = 0;
            CurrentPageIndex = 0;

            CurrentSelectedMsgIndex = -1;
            Clear();
            if (allFaultMsgs != null)
            {
                AddRange(allFaultMsgs);
                Sort(FalutSortType.Time);
                TotalPage = (int) Math.Ceiling(Count/10.0d);
                CurrentSelectedMsgIndex = Count > 0 ? 0 : -1;
            }
        }

        public void Goto(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (CurrentSelectedMsgIndex <= 0)
                    {
                        if (CurrentPageIndex > 0)
                        {
                            --CurrentPageIndex;
                            CurrentSelectedMsgIndex = CountPerPage - 1;
                        }
                    }
                    else
                    {
                        --CurrentSelectedMsgIndex;
                    }
                    break;
                case Direction.Down:
                    if (CurrentSelectedMsgIndex >= CurrentPage.Count() - 1)
                    {
                        if (CurrentPageIndex < TotalPage)
                        {
                            ++CurrentPageIndex;
                            CurrentSelectedMsgIndex = 0;
                        }
                    }
                    else
                    {
                        ++CurrentSelectedMsgIndex;
                    }
                    break;

                case Direction.Left:
                    if (CurrentPageIndex > 0)
                    {
                        --CurrentPageIndex;
                    }
                    break;

                case Direction.Right:
                    if (CurrentPageIndex < TotalPage - 1)
                    {
                        ++CurrentPageIndex;
                    }
                    if (CurrentSelectedMsgIndex > CurrentPage.Count())
                    {
                        CurrentSelectedMsgIndex = CurrentPage.Count() - 1;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }

        public void Sort(FalutSortType sortType)
        {
            switch (sortType)
            {
                case FalutSortType.None:
                    break;
                case FalutSortType.Time:
                    if (m_LastSortType != FalutSortType.Time)
                    {
                        Sort((a, b) => a.MsgReceiveTime.CompareTo(b.MsgReceiveTime));
                        m_LastSortType = sortType;
                    }
                    else
                    {
                        Sort((a, b) => b.MsgReceiveTime.CompareTo(a.MsgReceiveTime));
                        m_LastSortType = FalutSortType.None;
                    }
                    break;
                case FalutSortType.CarNo:
                    if (m_LastSortType != FalutSortType.CarNo)
                    {
                        Sort((a, b) => a.TrainID.CompareTo(b.TrainID));
                        m_LastSortType = sortType;
                    }
                    else
                    {
                        Sort((a, b) => b.TrainID.CompareTo(a.TrainID));
                        m_LastSortType = FalutSortType.None;
                    }
                    break;
                case FalutSortType.Dev:
                    if (m_LastSortType != FalutSortType.Dev)
                    {
                        Sort((a, b) => String.Compare(a.SubSysName, b.SubSysName, StringComparison.Ordinal));
                        m_LastSortType = sortType;

                    }
                    else
                    {
                        Sort((a, b) => String.Compare(b.SubSysName, a.SubSysName, StringComparison.Ordinal));
                        m_LastSortType = FalutSortType.None;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("sortType", sortType, null);
            }
        }
    }
}