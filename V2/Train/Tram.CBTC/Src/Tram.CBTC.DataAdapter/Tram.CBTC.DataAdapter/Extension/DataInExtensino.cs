using Tram.CBTC.DataAdapter.Model;
using Tram.CBTC.DataAdapter.Util;

namespace Tram.CBTC.DataAdapter.Extension
{
    public static class DataInExtension
    {
        public static T Copy<T>(this T data) where T : SignalDataIn
        {
            return DeepCopy.CopyDataIn(data);
        }
    }
}