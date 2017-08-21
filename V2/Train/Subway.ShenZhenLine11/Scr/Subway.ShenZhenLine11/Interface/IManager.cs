using System;
using System.Collections.Generic;

namespace Subway.ShenZhenLine11.Interface
{
    public interface IManager<T> where T : ICloneable
    {
        string FileName { get; }
        IDictionary<int, T> AllInfo { get; }
        IList<T> Display { get; }
        IList<T> History { get; }
        IList<T> Current { get; }
        int MaxPage { get; }
        int CurrentPage { get; }
        int PageNum { get; }
        void Add(int key);
        void Confirm(int key);
        void Remove(int key);
        void NextPage();
        void LastPage();
        void Load(string path);
        T Cast(object obj);
        void Reset();

        event Action<IList<T>, IList<T>> CurrentChanged;
        event Action<int, int> MaxPageChanged;
        event Action<int, int> CurrentPageChanged;
        event Action<int, int> MaxNumChanged;
    }
}