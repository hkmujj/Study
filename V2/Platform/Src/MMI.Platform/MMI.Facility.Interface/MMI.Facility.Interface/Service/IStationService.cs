using System;
using MMI.Facility.Interface.Data.Station;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStationNameProviderService : IService
    {
        /// <summary>
        /// key : 车站索引 
        /// </summary>
        IReadOnlyDictionary<int, IStationInfo> StationDictionary { get; }


        /// <summary>
        /// StationDictionary 变化 
        /// </summary>
        event EventHandler StationDictionaryChanged;
    }
}