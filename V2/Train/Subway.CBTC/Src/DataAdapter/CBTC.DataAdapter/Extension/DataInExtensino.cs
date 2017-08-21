using CBTC.DataAdapter.Model;
using CBTC.DataAdapter.Util;

namespace CBTC.DataAdapter.Extension
{
    public static class DataInExtension
    {
        public static T Copy<T>(this T data) where T : SignalDataIn
        {
            return DeepCopy.CopyDataIn(data);
        }
    }
}