using System;
using System.Collections.Generic;

namespace Subway.XiaMenLine1.Interface
{
    public interface IEventMgr : IPaging<IEventInfo>, IInfo<int, IEventInfo>
    {
        IList<IEventInfo> HistoryEvent { get; }
        IList<IEventInfo> CurrentEvent { get; }
        IList<IEventInfo> DisplayEvent { get; }
        int MaxFalut { get; }
        bool HasEvent(IEventInfo info);
        bool HasEvent(int logic);
        IEventInfo GetEvent(int logic);
        event Action MaxFaultChanged;
        event Action CurrentPageChanged;
        event Action MaxPageChanged;
        event Action CofirmChanged;
        void Clear();
        void Cofirm(int logic);
        void Cofirm(IEventInfo info);
        void Add(int logic);
        void Remove(int logic);
        void Sort(EventLevel level);
        void Reset();
        void InitPara();

    }
}