

using System.Collections.Generic;
using CommonUtil.Model;

namespace Urban.NanJing.NingTian.DDU.Flash.Adapter
{
    public interface IFlashDataProvider
    {
        IReadOnlyDictionary<string, Dictionary<string, string>> CommandDataDictionary { get; }
    }
}