using System;
using System.Collections.Generic;

namespace MMI.Common.Msg.Interface
{
    public interface IManage<T, TK>
    {
        event Action CurrentInfoChanged;
        string FileName { get; }
        IDictionary<T, TK> AllInfo { get; }
        IList<TK> CurrentInfo { get; }
        IList<TK> HistoryInfo { get; }
        void Add(TK tPara);
        void Add(T tPara);
        void Remove(T tPara);
        void Remove(TK tPara);
        void Affirm(T tPara);
        void Affirm(TK tPara);
        void LoadFile(string path);
    }
}