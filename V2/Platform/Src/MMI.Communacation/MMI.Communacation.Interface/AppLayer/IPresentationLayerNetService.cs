using System;
using MMI.Communacation.Interface.ProtocolLayer;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Event;

namespace MMI.Communacation.Interface.AppLayer
{
    public interface IPresentationLayerNetService : ICommunacationDataSender
    {
        INetConfig NetConfig { get; }

        void Initialize(INetConfig netConfig);

        void Close();


        /// <summary>
        /// 通知自己在线
        /// </summary>
        void NotifyOnLine();

        /// <summary>
        /// 结束 
        /// </summary>
        event EventHandler<NetCommandEventArgs> End;

        /// <summary>
        /// 开始 
        /// </summary>
        event EventHandler<NetCommandEventArgs> Begin;

        /// <summary>车站更新</summary>
        event EventHandler<UpdateStationCollectionEventArgs> StationCollectionUpdated;
        /// <summary>
        /// 时刻表
        /// </summary>

        event EventHandler<TimeTableEventArgs> TimeTableUpdate;

        /// <summary>
        /// 数据接收
        /// </summary>
        event Action<NetDatas> DataReveived;

        /// <summary>
        /// 命令接收
        /// </summary>
        event Action<CommandType> CommandReceived;

    }
}
