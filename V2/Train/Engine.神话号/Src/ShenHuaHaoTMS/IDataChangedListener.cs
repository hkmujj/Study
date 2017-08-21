using System.Collections.Generic;

namespace ShenHuaHaoTMS
{
    public interface IDataChangedListener
    {
        bool IsListening { get; }

        void OnBoolItemChanged(ref KeyValuePair<int, bool> item);

        void OnFloatItemChanged(ref KeyValuePair<int, float> item);
    }
}