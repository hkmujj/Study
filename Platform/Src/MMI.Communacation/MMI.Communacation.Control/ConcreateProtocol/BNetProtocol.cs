using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using CommonUtil.Util;
using MMI.YDCommunicationWrapper;
using ES.Facility.PublicModule.Win32;
using MMI.Communacation.Control.ProtocolLayer;
using MMI.Communacation.Control.ProtocolLayer.RecvPackage;
using MMI.Communacation.Interface.AppLayer;
using MMI.Communacation.Interface.ProtocolLayer;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.Net;

namespace MMI.Communacation.Control.ConcreateProtocol
{
    /// <summary>
    /// B 类网络的协议
    /// </summary>
    internal class BNetProtocol : IDisposable
    {
        /// <summary>
        /// 屏的监听
        /// </summary>
        private UdpClient m_ListenDataClient;

        private UdpClient m_ListenCmdClient;

        private IPEndPoint m_NoneRemoteIp = new IPEndPoint(IPAddress.None, IPEndPoint.MinPort);

        /// <summary>
        /// 监听线程是否活动
        /// </summary>
        private volatile bool m_IsListeningTheadActiving;

        /// <summary>
        /// 发到教员
        /// </summary>
        private IPEndPoint m_TechIPEndPoint;

        /// <summary>
        /// 发到主控
        /// </summary>
        private IPEndPoint m_MainNodeIPEndPoint;

        /// <summary>
        /// 发送到主控和教员
        /// </summary>
        private Socket m_Sender;

        /// <summary>
        /// protected byte[] vNetCmdLoginData = new byte[] { 1, 0, 0, 0, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 168, 0, 0, 208, 07, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        /// </summary>
        protected byte[] m_NotifyOnlineDatas = new byte[]
        {
            1,
            0,
            0,
            0,
            18,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            192,
            168,
            0,
            0,
            208,
            07,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0
        };

        public event EventHandler<NetCommandEventArgs> StationUpdated;

        public event EventHandler<NetCommandEventArgs> OnFmsSysEnd;

        public void appendFmsSysEnd(object sender, NetCommandEventArgs args)
        {
            if (OnFmsSysEnd != null)
            {
                OnFmsSysEnd(sender, args);
            }
        }


        public event EventHandler<NetCommandEventArgs> OnFmsSysBegin;

        public void appendFmsSysBegin(object sender, NetCommandEventArgs args)
        {
            if (OnFmsSysBegin != null)
            {
                OnFmsSysBegin(sender, args);
            }
        }

        public event Action<byte[], int> OnFmsMMIRealDataReceive;

        public void appendFmsMMIRealDataReceive(byte[] bt, int nDataIndex)
        {
            if (OnFmsMMIRealDataReceive != null)
            {
                OnFmsMMIRealDataReceive(bt, nDataIndex);
            }
        }

        //主控呼叫子系统,
        public delegate void FmsNetSysMainCall(int nFrom, int nTo, int nPara);

        public event FmsNetSysMainCall OnFmsNetSysMainCall;

        public void appendFmsNetSysMainCall(int nFrom, int nTo, int nPara)
        {
            if (OnFmsNetSysMainCall != null)
            {
                OnFmsNetSysMainCall(nFrom, nTo, nPara);
            }
        }

        //主控返回自身状态
        public delegate void FmsNetSysMainStat(int nStat);

        public event FmsNetSysMainStat OnFmsNetSysMainStat;

        public void appendFmsNetSysMainStat(int nStat)
        {
            if (OnFmsNetSysMainStat != null)
            {
                OnFmsNetSysMainStat(nStat);
            }
        }

        /// <summary>
        /// 所有未知扩展数据 都是用该接口处理
        /// </summary>
        /// <param name="bt"></param>
        public delegate void ExNetDataRec(ref byte[] bt, string nIpString);

        public event ExNetDataRec OnExNetDataRec;

        public void appendExNetDataRec(ref byte[] bt, string nIpString)
        {
            if (OnExNetDataRec != null)
            {
                OnExNetDataRec(ref bt, nIpString);
            }
        }

        public bool Intalize(IConfig config)
        {
            m_IsListeningTheadActiving = true;

            m_Sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            m_Sender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

            var fmsConfig = config.NetConfig.NetChannelConfig.BNetConfig;
            var ips =
                Dns.GetHostAddresses(Dns.GetHostName())
                    .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                    .ToList();

            if (ips.Count < fmsConfig.LocIpNum + 1)
            {
                SysLog.Error(string.Format(
                    "You config local listen ip index is {0}. but the machie has not enough IP .", fmsConfig.LocIpNum));
                return false;
            }

            return InintalizeListener(ips, config) && InitalizeTechSender(config) && InitalizeMainNodeSender(config) &&
                   InitalizeNofifyOnlineCmd(ips, fmsConfig);
        }

        public void NotifyOnline()
        {
            try
            {
                var sended = m_Sender.SendTo(m_NotifyOnlineDatas, m_TechIPEndPoint);
                //sended = m_Sender.SendTo(vNetCmdLoginData, m_MainNodeIPEndPoint);
            }
            catch (Exception e)
            {
                SysLog.Error(string.Format("send online command to teacher error. {0}", e));
            }
        }

        private bool InintalizeListener(List<IPAddress> ips, IConfig config)
        {
            var fmsConfig = config.NetConfig.NetChannelConfig.BNetConfig;
            try
            {

                m_ListenCmdClient =
                    new UdpClient(new IPEndPoint(ips[fmsConfig.LocIpNum],
                        fmsConfig.ListerPort.GetActurePort(BNetPortType.Commnad)));

                m_ListenDataClient =
                    new UdpClient(new IPEndPoint(ips[fmsConfig.LocIpNum],
                        fmsConfig.ListerPort.GetActurePort(BNetPortType.Data)));

            }
            catch (Exception e)
            {
                SysLog.Error(string.Format("Can not create udpclient, where ip is {0}. \r\n{1}", ips[fmsConfig.LocIpNum],
                    e));
                return false;
            }

            return true;
        }

        public static void ReceiveCallback(IAsyncResult ar)
        {
            var target = (UpdState)(ar.AsyncState);

            if (target.SourceObjce.m_IsListeningTheadActiving)
            {
                target.SourceObjce.AsncReciveData(ar, target.UdpClient);

                try
                {
                    target.UdpClient.BeginReceive(ReceiveCallback, target);
                }
                catch (Exception e)
                {
                    SysLog.Error("Can not BeginReceive datas,{0}", e);
                }
            }
        }

        private bool InitalizeNofifyOnlineCmd(List<IPAddress> ips, IBNetConfig ibNetConfig)
        {

            switch (ibNetConfig.TeachType)
            {
                case BNetTeachType.Engine:
                    InitalizeEngineNotifyCmd(ips, ibNetConfig);
                    break;
                case BNetTeachType.Urban:
                    InitalizeUrbanNotifyCmd(ips, ibNetConfig);
                    break;
                default:
                    var msg = string.Format("Can not start bnet , which teach type={0}", ibNetConfig.TeachType);
                    SysLog.Error(msg);
                    throw new ArgumentException(msg);
            }

            return true;
        }

        private void InitalizeUrbanNotifyCmd(List<IPAddress> ips, IBNetConfig ibNetConfig)
        {
            BitConverter.GetBytes(ibNetConfig.SystemID).CopyTo(m_NotifyOnlineDatas, 4);

            var ipBytes = ips[ibNetConfig.LocIpNum].GetAddressBytes();

            var tmpPort = ibNetConfig.ListerPort.GetActurePort(BNetPortType.Commnad);

            var portBytes = BitConverter.GetBytes(tmpPort);

            m_NotifyOnlineDatas[24] = ipBytes[0];
            m_NotifyOnlineDatas[25] = ipBytes[1];
            m_NotifyOnlineDatas[26] = ipBytes[2];
            m_NotifyOnlineDatas[27] = ipBytes[3];

            m_NotifyOnlineDatas[28] = portBytes[1];
            m_NotifyOnlineDatas[29] = portBytes[0];
        }

        private void InitalizeEngineNotifyCmd(List<IPAddress> ips, IBNetConfig ibNetConfig)
        {
            BitConverter.GetBytes(ibNetConfig.SystemID).CopyTo(m_NotifyOnlineDatas, 4);

            var ipBytes = ips[ibNetConfig.LocIpNum].GetAddressBytes();

            var tmpPort = ibNetConfig.ListerPort.GetPortNumber();

            var portBytes = BitConverter.GetBytes(tmpPort);

            m_NotifyOnlineDatas[24] = ipBytes[0];
            m_NotifyOnlineDatas[25] = ipBytes[1];
            m_NotifyOnlineDatas[26] = ipBytes[2];
            m_NotifyOnlineDatas[27] = ipBytes[3];

            m_NotifyOnlineDatas[28] = portBytes[0];
            m_NotifyOnlineDatas[29] = portBytes[1];
        }

        private void AsncReciveData(IAsyncResult ar, UdpClient udpClient)
        {
            if (m_IsListeningTheadActiving)
            {
                try
                {
                    var datas = udpClient.EndReceive(ar, ref m_NoneRemoteIp);

                    if (ValidateRemoteIp(m_NoneRemoteIp))
                    {
                        DecodeNetData(datas, m_NoneRemoteIp);
                    }
                }
                catch (Exception e)
                {
                    SysLog.Error(string.Format("Recive udp message error. {0}", e));
                }
            }
        }

        private bool DecodeNetData(byte[] datas, IPEndPoint remoteIp)
        {
            int cmdId = BitConverter.ToInt16(datas, 0);
            var subCmdId = BitConverter.ToInt16(datas, 2);
            switch (cmdId)
            {
                case 1103:
                    //开始训练
                    appendFmsSysBegin(this,
                        new NetCommandEventArgs(BytesStructConverter.ByteToStruct<Command>(datas), datas));
                    break;
                case 1105:
                    //暂停训练
                    break;
                case 1109:
                    //结束训练
                    appendFmsSysEnd(this,
                        new NetCommandEventArgs(BytesStructConverter.ByteToStruct<Command>(datas), datas));
                    break;
                case -100:
                    //重启屏程序
                    switch ((ApplicationRestartCommand)subCmdId)
                    {
                        case ApplicationRestartCommand.Nomal:
                            break;
                        case ApplicationRestartCommand.ReBoot:
                            ServerRoot.Reboot();
                            break;
                        case ApplicationRestartCommand.Shutdown:
                            ServerRoot.PowerOff();
                            break;
                        case ApplicationRestartCommand.LogOff:
                            ServerRoot.LogOff();
                            break;
                        case ApplicationRestartCommand.ReStart:
                            ServerRoot.ReStart();
                            break;
                        case ApplicationRestartCommand.Close:
                            ServerRoot.Shutdown();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    //ServerRoot.ReStart();
                    break;
                case 196508:
                    //要求关闭计算机
                    ServerRoot.PowerOff();
                    break;
                case 130972:
                    //要求重启计算机
                    ServerRoot.Reboot();
                    break;

                case (int)RecvDataType.FirstPackage:
                case (int)RecvDataType.SecondPackage:
                case (int)RecvDataType.ThirdPackage:
                    //MMI需要处理的实时数据
                    appendFmsMMIRealDataReceive(datas, cmdId);
                    break;

                case 257:
                    var theTempIntInDeCodeNetData = BitConverter.ToInt32(datas, 12);
                    appendFmsNetSysMainStat(theTempIntInDeCodeNetData);
                    break;
                case 1100:
                    //主控呼叫各个子系统
                    if (datas.Length == 32)
                    {
                        appendFmsNetSysMainCall(BitConverter.ToInt32(datas, 4),
                            //from
                            BitConverter.ToInt32(datas, 8),
                            //to
                            BitConverter.ToInt32(datas, 12)); //para
                    }
                    break;
                case 1101:
                    if (datas.Length <= 24)
                    {
                        //主控呼叫各个子系统
                        //string tempStr1101 = BitConverter.ToString(bt);
                        appendFmsNetSysMainCall(BitConverter.ToInt32(datas, 4),
                            //from
                            BitConverter.ToInt32(datas, 8),
                            //to
                            BitConverter.ToInt32(datas, 12)); //para
                    }
                    break;
                case (int)CommandType.UpdateStation:
                    OnStationUpdated(new NetCommandEventArgs(BytesStructConverter.ByteToStruct<Command>(datas), datas));
                    break;
                default:
                    appendExNetDataRec(ref datas, remoteIp.Address.ToString());
                    return false;
            }

            return true;
        }

        private bool ValidateRemoteIp(IPEndPoint remoteIp)
        {
            return remoteIp.Address.Equals(m_TechIPEndPoint.Address) ||
                   remoteIp.Address.Equals(m_MainNodeIPEndPoint.Address);
        }

        private bool InitalizeTechSender(IConfig config)
        {
            var fmsConfig = config.NetConfig.NetChannelConfig.BNetConfig;
            try
            {
                m_TechIPEndPoint = new IPEndPoint(IPAddress.Parse(fmsConfig.TeachIP),
                    fmsConfig.TeachPort.GetActurePort(BNetPortType.Commnad));
            }
            catch (Exception)
            {
                SysLog.Error(string.Format("Can no recored teacher ip : {0} : {1}", fmsConfig.TeachIP,
                    fmsConfig.TeachPort));
                throw;
            }

            return true;
        }

        private bool InitalizeMainNodeSender(IConfig config)
        {
            var fmsConfig = config.NetConfig.NetChannelConfig.BNetConfig;
            try
            {
                m_MainNodeIPEndPoint = new IPEndPoint(IPAddress.Parse(fmsConfig.CpuIP),
                    fmsConfig.CpuPort.GetActurePort(BNetPortType.Commnad));
            }
            catch (Exception)
            {
                SysLog.Error(string.Format("Can no recored teacher ip : {0} : {1}", fmsConfig.CpuIP, fmsConfig.CpuPort));
                throw;
            }

            return true;
        }

        public void Start()
        {
            m_ListenDataClient.BeginReceive(ReceiveCallback, new UpdState(this, m_ListenDataClient));

            m_ListenCmdClient.BeginReceive(ReceiveCallback, new UpdState(this, m_ListenCmdClient));

            m_IsListeningTheadActiving = true;
        }

        public void Stop()
        {
            SysLog.Debug("Stop B net protocol , dispose the udp client , shut down the listen thread.");
            m_IsListeningTheadActiving = false;
        }

        public void Dispose()
        {
            SysLog.Debug("Dispose the udp client , shut down the listen thread.");

            m_IsListeningTheadActiving = false;
            try
            {
                m_ListenCmdClient.Close();
            }
            catch (Exception e)
            {
                SysLog.Error(string.Format("Dispose listen command udp client error. {0}", e));
            }
            m_ListenCmdClient = null;

            try
            {
                m_ListenDataClient.Close();
            }
            catch (Exception e)
            {
                SysLog.Error(string.Format("Dispose listen data udp client error. {0}", e));
            }
            m_ListenDataClient = null;
            SysLog.Debug("Dispose the udp client , shut down the listen thread sucessed.");
        }

        public void SendUdpMessage(IPEndPoint targetIp, byte[] data)
        {
            m_Sender.SendTo((byte[])data.Clone(), targetIp);
        }

        public class UpdState
        {
            public UpdState(BNetProtocol sourceObjce, UdpClient udpClient)
            {
                SourceObjce = sourceObjce;
                UdpClient = udpClient;
            }

            public BNetProtocol SourceObjce { private set; get; }

            public UdpClient UdpClient { private set; get; }
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