using System;
using System.Collections.Generic;
using MMI.Facility.Interface.Data.Config.NetDataPackage;
using MMI.Facility.Interface.Event;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CommunicationDataFacadeServiceBase : ICommunicationDataFacadeService
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<ICommunicationDataKey, ICommunicationDataService> CommunicationDataServiceDictionary { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler NetServiceEnd;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler NetServiceBegin;

        /// <summary>车站更新</summary>
        public event EventHandler<UpdateStationCollectionEventArgs> StationCollectionUpdated;

        /// <summary>
        /// 
        /// </summary>
        public abstract void InitalizeDataServiceDictionary();

        public ICommunicationDataService GetCommunicationDataService(ICommunicationDataKey dataKey)
        {
            if (CommunicationDataServiceDictionary.ContainsKey(dataKey))
            {
                return CommunicationDataServiceDictionary[dataKey];
            }
            throw new KeyNotFoundException(string.Format("Can not found communication data service of type={0}, name={1}",
                dataKey.ProjectType, dataKey.AppName));
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void StartNet();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        protected virtual void OnPresentationLayerNetServiceEnd(object sender, EventArgs eventArgs)
        {
            var handler = NetServiceEnd;
            if (handler != null)
            {
                handler(sender, eventArgs);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        protected virtual void OnPresentationLayerNetServiceBegin(object sender, EventArgs eventArgs)
        {
            var handler = NetServiceBegin;
            if (handler != null)
            {
                handler(sender, eventArgs);
            }
        }

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public virtual void Dispose()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnStationCollectionUpdated(object sender, UpdateStationCollectionEventArgs e)
        {
            if (StationCollectionUpdated != null)
            {
                StationCollectionUpdated.Invoke(sender, e);
            }
        }
    }
}