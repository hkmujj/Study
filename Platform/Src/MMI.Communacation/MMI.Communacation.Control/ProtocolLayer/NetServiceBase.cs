using System;
using System.Net;
using CommonUtil.Util;
using MMI.Communacation.Control.ProtocolLayer.RecvPackage;
using MMI.Communacation.Interface.AppLayer;
using MMI.Communacation.Interface.ProtocolLayer;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Communacation.Control.ProtocolLayer
{
    public abstract class NetProtocolServiceBase : INetProtocolService
    {
        public virtual void Initialize(IConfig config)
        {
            LogMgr.Debug("Initialize net service by NetServiceBase, nothing todo.");
        }

        public virtual void Close()
        {
            LogMgr.Debug("Close net service by NetServiceBase, nothing todo.");
        }

        public abstract void Send(string ip, int port, byte[] data);

        public abstract void Send(IPEndPoint ipEndPoint, byte[] data);

        /// <summary>
        /// 触发 OnBegin 事件 
        /// </summary>
        protected void OnBegin(NetCommandEventArgs args)
        {
            SysLog.Info("Recv course start command. type={0}, subtype={1}", args.Command.cmd, args.Command.subType);
            HandleUtil.OnHandle(Begin, this, args);
        }

        /// <summary>
        /// 触发 OnEnd 事件 
        /// </summary>
        protected void OnEnd(NetCommandEventArgs args)
        {
            SysLog.Info("Recv course stop command. type={0}, subtype={1}", args.Command.cmd, args.Command.subType);
            HandleUtil.OnHandle(End, this, args);
        }

        /// <summary>
        /// 触发 OnDataReceived 事件 
        /// </summary>
        protected void OnDataReceived(byte[] bytes, RecvPackageHead index)
        {
            if (DataReceived!=null)
            {
                DataReceived(bytes, index);
            }
        }

        /// <summary>
        /// 触发 OnCmdReceived 事件 
        /// </summary>
        protected void OnCommandReceived(CommandType cmd)
        {
            if (CmdReceived != null)
            {
                CmdReceived(cmd);
            }
        }


        /// <summary>
        /// 触发 OnCirCmdReceived 事件 
        /// </summary>
        protected void OnCirCommandReceived(CirCmd cmd)
        {
            if (CirCmdReceived != null)
            {
                CirCmdReceived(cmd);
            }
        }

        public event EventHandler<NetCommandEventArgs> End;

        public event EventHandler<NetCommandEventArgs> Begin;


        public event Action<byte[], RecvPackageHead> DataReceived;

        public event Action<CommandType> CmdReceived;

        /// <summary>
        /// cir 命令接收
        /// </summary>
        public event Action<CirCmd> CirCmdReceived;

        public event EventHandler<NetCommandEventArgs> StationUpdated;

        /// <summary>执行与释放或重置非托管资源相关的应用程序定义的任务。</summary>
        public virtual void Dispose()
        {
            
        }

        protected virtual void OnStationUpdated(NetCommandEventArgs e)
        {
            if (StationUpdated != null)
            {
                StationUpdated.Invoke(this, e);
            }
        }
    }
}
