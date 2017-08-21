using System;

namespace Urban.Phillippine.View.Interface
{
    public interface IDataClear
    {
        void Clear();

        void Clear(object obj, Type type);
    }
}