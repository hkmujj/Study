using System.Collections.Generic;

namespace SH_Reconnect
{
    public interface IDataChangedListener
    {
        void OnBoolItemChanged(ref KeyValuePair<int, bool> item);

        void OnFloatItemChanged(ref KeyValuePair<int, float> item);
    }
}