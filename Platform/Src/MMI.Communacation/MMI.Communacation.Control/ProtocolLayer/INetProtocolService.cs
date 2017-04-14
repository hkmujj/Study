using System;
using System.Net;
using MMI.Communacation.Control.ProtocolLayer.RecvPackage;
using MMI.Communacation.Interface.AppLayer;
using MMI.Communacation.Interface.ProtocolLayer;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Communacation.Control.ProtocolLayer
{
    public interface INetProtocolService : IDisposable
    {
        void Initialize(IConfig config);

        void Close();

        void Send(string ip, int port, byte[] data);

        void Send(IPEndPoint ipEndPoint, byte[] data);

        /// <summary>
        /// 结束 
        /// </summary>
        event EventHandler<NetCommandEventArgs> End;

        /// <summary>
        /// 开始 
        /// </summary>
        event EventHandler<NetCommandEventArgs> Begin;

        /// <summary>
        /// 数据接收
        /// </summary>
        event Action<byte[], RecvPackageHead> DataReceived;

        /// <summary>
        /// 命令接收
        /// </summary>
        event Action<CommandType> CmdReceived;

        /// <summary>
        /// cir 命令接收
        /// </summary>
        event Action<CirCmd> CirCmdReceived;

        /// <summary>
        /// 更新车站
        /// </summary>
        event EventHandler<NetCommandEventArgs> StationUpdated;

    }
}
