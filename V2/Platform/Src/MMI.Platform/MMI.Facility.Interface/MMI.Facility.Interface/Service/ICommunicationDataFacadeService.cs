using System;
using MMI.Facility.Interface.Event;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommunicationDataFacadeService : IService, IDisposable
    {
        /// <summary>
        /// 结束 
        /// </summary>
        event EventHandler NetServiceEnd;

        /// <summary>
        /// 开始 
        /// </summary>
        event EventHandler NetServiceBegin;

        /// <summary>车站更新</summary>
        event EventHandler<UpdateStationCollectionEventArgs> StationCollectionUpdated;

        /// <summary>
        /// 时刻表更新
        /// </summary>
        event EventHandler<TimeTableEventArgs> TimeTableUpdate;
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        ICommunicationDataService GetCommunicationDataService(ICommunicationDataKey dataKey);
    }
}