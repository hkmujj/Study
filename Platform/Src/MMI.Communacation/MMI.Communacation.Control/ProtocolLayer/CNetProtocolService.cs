using System;
using System.Net;
using CommonUtil.Util;
using MMI.Communacation.Control.ProtocolLayer.RecvPackage;
using MMI.YDCommunicationWrapper;
using MMI.Communacation.Interface.AppLayer;
using MMI.Communacation.Interface.ProtocolLayer;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Communacation.Control.ProtocolLayer
{
    class CNetProtocolService : NetProtocolServiceBase
    {
        /// <summary>
        /// 网络连接
        /// </summary>        
        private readonly Net m_CNet = new Net();

        /// <summary>
        /// 子系统准备就绪
        /// </summary>
        protected long RunStatus = 2;

        private readonly ICommandTypeInterpreter m_CommandTypeInterpreter;

        public CNetProtocolService()
        {
            m_CommandTypeInterpreter = new CommandTypeInterpreter();
        }

        public override void Initialize(IConfig config)
        {
            m_CNet.Init();
            m_CNet.OnRecvRealData += TCPRealDataReceive;
            m_CNet.OnRecvCmdData += TCPCmdDataReceive;
        }

        public override void Send(string ip, int port, byte[] data)
        {
            if (data == null)
            {
                return;
            }

            var sysid = m_CNet.GetSystemID() / 1000;
            sysid = sysid * 1000 + 1;

            SendRealData(sysid, data, data.Length);
        }

        public override void Send(IPEndPoint ipEndPoint, byte[] data)
        {
            // IP , Port not used
            Send(null, ipEndPoint.Port, data);
        }

        protected bool SendRealData(int sysID, byte[] bt, int dataLen)
        {
            if (m_CNet != null)
            {
                if (m_CNet.SendRealData(sysID, bt, dataLen) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        private void TCPCmdDataReceive(int sysID, byte[] data)
        {
            var vNetCmdId = m_CommandTypeInterpreter.InterpreterCommandType(data);

            switch (vNetCmdId)
            {
                case (CommandType) 106: //课程准备
                    RunStatus = 5; //课程准备好
                    break;
                case CommandType.HeartbeatPacketRequest:
                    Command cmd;
                    cmd.cmd = 1101;
                    cmd.nParamLen = 0;
                    cmd.subType = (short) RunStatus;
                    cmd.sTo = sysID;
                    cmd.sFrom = m_CNet.GetSystemID();
                    var sd = new byte[24];

                    sd[0] = 0x4d;
                    sd[1] = 0x4;
                    sd[2] = (byte) RunStatus;
                    sd[3] = 0;
                    long id = m_CNet.GetSystemID()/1000;
                    id = id*1000 + 1;
                    long sid = m_CNet.GetSystemID();
                    var a = BitConverter.GetBytes((Int32) sid);
                    sd[4] = a[0];
                    sd[5] = a[1];
                    sd[6] = a[2];
                    sd[7] = a[3];
                    var b = BitConverter.GetBytes((Int32) id);
                    sd[8] = b[0];
                    sd[9] = b[1];
                    sd[10] = b[2];
                    sd[11] = b[3];
                    sd[12] = 0;
                    sd[13] = 0;
                    sd[14] = 0;
                    sd[15] = 0;
                    sd[16] = 0;
                    sd[17] = 0;
                    sd[18] = 0;
                    m_CNet.SendCmdData(cmd.sTo, sd);
                    break;
                case CommandType.CourseStart:
                    OnBegin(new NetCommandEventArgs(BytesStructConverter.ByteToStruct<Command>(data), data));
                    break;
                case (CommandType) 1105:
                    break;
                case CommandType.CourseOver:
                    OnEnd(new NetCommandEventArgs(BytesStructConverter.ByteToStruct<Command>(data), data));
                    break;
                case CommandType.CirCmdRecv:
                    break;
                case CommandType.UpdateStation:
                    OnStationUpdated(new NetCommandEventArgs(BytesStructConverter.ByteToStruct<Command>(data), data));
                    break;
            }
        }

        private void TCPRealDataReceive(int sysid, byte[] data, int len)
        {
            var dataIndex = BitConverter.ToInt32(data, 0);
            OnDataReceived(data, new RecvPackageHead() {Value = dataIndex});
        }
    }
}
