using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using CommonUtil.Util;
using MMI.Communacation.Control.ConcreateProtocol;
using MMI.Communacation.Control.PresentationLayer.BroadcastStragy;
using MMI.Communacation.Control.ProtocolLayer;
using MMI.Communacation.Control.ProtocolLayer.RecvPackage;
using MMI.Communacation.Interface.AppLayer;
using MMI.Communacation.Interface.ProtocolLayer;
using MMI.Facility.DataType.Course;
using MMI.Facility.DataType.Extension;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Event;
using MMI.YDCommunicationWrapper;

namespace MMI.Communacation.Control.PresentationLayer
{
    internal class PresentationLayerNetService : IPresentationLayerNetService, IIPresentationLayerDataBroadcastProvider,
        IDisposable
    {
        private NetProtocolServiceBase m_ActureNetProtocolService;

        private readonly IConfig m_Config;

        private RecvRealTimeDataBroadcastStragy m_BroadcastStragy;

        /// <summary>
        /// 登录教员系统的数据
        /// </summary>
        private readonly byte[] m_TheLoginTeacherData =
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


        public event EventHandler<NetCommandEventArgs> End;

        public event EventHandler<NetCommandEventArgs> Begin;

        public event EventHandler<UpdateStationCollectionEventArgs> StationCollectionUpdated;
        public event EventHandler<TimeTableEventArgs> TimeTableUpdate;


        /// <summary>
        /// 数据接收
        /// </summary>
        public event Action<NetDatas> DataReveived;

        public event Action<CommandType> CommandReceived;

        public INetConfig NetConfig { private set; get; }

        public PresentationLayerNetService(IConfig config)
        {
            m_Config = config;
        }

        public void Initialize(INetConfig netconfig)
        {
            SysLog.Info(string.Format("Initalize by NetType={0}", netconfig.NetChannelConfig.NetType));

            m_BroadcastStragy = CreatBroadcastStragy(netconfig);

            NetConfig = netconfig;
            switch (netconfig.NetChannelConfig.NetType)
            {
                case NetType.None:
                    m_ActureNetProtocolService = new NoneNetProtocolService();
                    break;
                case NetType.B:
                    var bNetConfig = netconfig.NetChannelConfig.BNetConfig;
                    m_ActureNetProtocolService = new BNetProtocolService();
                    SysLog.Info("教员IP:{0} 教员端口：{1} 主控IP:{2} 主控端口：{3} 网卡号：{4}（{5}）监听端口:{6}", bNetConfig.TeachIP,
                        bNetConfig.TeachPort, bNetConfig.CpuIP, bNetConfig.CpuPort,
                        bNetConfig.LocIpNum,
                        Dns.GetHostAddresses(Dns.GetHostName())
                            .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                            .ToList()[bNetConfig.LocIpNum], bNetConfig.ListerPort);
                    break;
                case NetType.C:
                    m_ActureNetProtocolService = new CNetProtocolService();
                    break;
                case NetType.PTT2D:
                    m_ActureNetProtocolService = new PTTNetProtocolService();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("netType");
            }

            m_ActureNetProtocolService.Begin += (sender, args) => OnBegin(args);
            m_ActureNetProtocolService.End += (sender, args) => OnEnd(args);
            m_ActureNetProtocolService.CmdReceived += ActureNetProtocolServiceCmdReceived;
            m_ActureNetProtocolService.DataReceived += ActureNetProtocolServiceDataReceived;
            m_ActureNetProtocolService.CirCmdReceived += ActureNetProtocolServiceCirCmdReceived;
            m_ActureNetProtocolService.StationUpdated += ActureNetProtocolServiceOnStationUpdated;
            m_ActureNetProtocolService.TimeTableReceiived += ActureNetProtocolServiceOnTimeTableChanged;
            m_ActureNetProtocolService.Initialize(m_Config);
        }



        private RecvRealTimeDataBroadcastStragy CreatBroadcastStragy(INetConfig netconfig)
        {
            return new LazyRecvRealTimeDataBroadcastStragy(this);
        }

        /// <summary>
        /// 通知自己在线
        /// </summary>
        public void NotifyOnLine()
        {
            if (m_Config.NetConfig.NetChannelConfig.NetType != NetType.None)
            {
                var bNetConfig = m_Config.NetConfig.NetChannelConfig.BNetConfig;
                var hostInfo = Dns.GetHostEntry(Dns.GetHostName());
                if (hostInfo.AddressList.Length > 0)
                {
                    var tmpNetIP = hostInfo.AddressList[bNetConfig.LocIpNum].GetAddressBytes();
                    var tmpNetPort = BitConverter.GetBytes((int)bNetConfig.TeachPort);

                    var tmpInt = 24;
                    m_TheLoginTeacherData[tmpInt++] = tmpNetIP[0];
                    m_TheLoginTeacherData[tmpInt++] = tmpNetIP[1];
                    m_TheLoginTeacherData[tmpInt++] = tmpNetIP[2];
                    m_TheLoginTeacherData[tmpInt++] = tmpNetIP[3];
                    m_TheLoginTeacherData[tmpInt++] = tmpNetPort[1];
                    m_TheLoginTeacherData[tmpInt++] = tmpNetPort[0];

                    m_TheLoginTeacherData[4] = 18;
                }
                Send(m_TheLoginTeacherData, bNetConfig.TeachIP,
                    bNetConfig.TeachPort.GetActurePort(BNetPortType.Data));
            }
        }

        private void ActureNetProtocolServiceCirCmdReceived(CirCmd cirCmd)
        {
            // TODO 确定是否有用
        }

        private void ActureNetProtocolServiceOnStationUpdated(object sender, NetCommandEventArgs args)
        {
            if (args.Command.GetParamLen() <= CommandConstant.MaxParamLength)
            {
                var stationstr = Encoding.Default.GetString(args.Command.cParam, 0, args.Command.GetParamLen());
                SysLog.Info("Parserd station update command, the raw string = {0}", stationstr);
                OnStationCollectionUpdated(new UpdateStationCollectionEventArgs(stationstr));
            }
            else
            {
                SysLog.Error("Can not parser station updat command, because the data len={0} > 1000",
                    args.Command.GetParamLen());
            }
        }
        private void ActureNetProtocolServiceOnTimeTableChanged(NetCommandEventArgs obj)
        {
            OnTimeTableUpdate(new TimeTableEventArgs(obj.Command.cParam));
        }
        private void ActureNetProtocolServiceDataReceived(byte[] bytes, RecvPackageHead dataType)
        {
            ReceiveDataModel<bool> boolmodel = null;
            ReceiveDataModel<float> floatModel = null;
            ProjectType type = ProjectType.Unkown;

            if (dataType.TypeC.DataType >= RecvDataType.BCTypeMin && dataType.TypeC.DataType <= RecvDataType.BCTypeMax &&
                m_Config.NetConfig.NetDataProtocolConfig.ProtocolType == NetDataProtocolType.PackageIdOnly)
            {
                ParserBCBytes(bytes, dataType.TypeC.DataType, out boolmodel, out floatModel);
            }
            else if (dataType.TypeD.GetProjectType().IsValidateProjectType() &&
                     m_Config.NetConfig.NetDataProtocolConfig.ProtocolType == NetDataProtocolType.BussnessIdAndPackageId)
            {
                ParserDBytes(bytes, dataType, out boolmodel, out floatModel, out type);
            }

            if (boolmodel != null && floatModel != null)
            {
                m_BroadcastStragy.OnDataReceived(type,
                    new NetDatas(boolmodel, floatModel, dataType.TypeD.GetProjectType()));
            }
        }

        private void ParserDBytes(byte[] bytes, RecvPackageHead dataType, out ReceiveDataModel<bool> boolmodel,
            out ReceiveDataModel<float> floatModel, out ProjectType type)
        {
            type = dataType.TypeD.GetProjectType();

            var netDataConfig =
                m_Config.NetConfig.NetDataProtocolConfig.BussnessIdAndPackageIdConfig.ProjectDataConfigCollection
                    .First(f => f.ProjectType == dataType.TypeD.GetProjectType()).NetDataConfig;

            var floatsourceBeginIdx = netDataConfig.NetInputDataPackage.FloatStartByte;
            var floatdesLegth = netDataConfig.NetInputDataPackage.FloatCountOfByte;

            var boolsourceBeginIdx = netDataConfig.NetInputDataPackage.BoolStartByte;
            var booldesLegth = netDataConfig.NetInputDataPackage.BoolCountOfByte;

            var floatdesBeginIdx = netDataConfig.NetInputDataPackage.FloatCountOfByte / 4 *
                                   dataType.TypeD.GetDataPackageIndex();
            var booldesBeginIdx = netDataConfig.NetInputDataPackage.BoolCountOfByte * 8 *
                                  dataType.TypeD.GetDataPackageIndex();

            var bs = bytes.Skip(boolsourceBeginIdx).Take(booldesLegth).ToArray();
            var ba = new BitArray(bs);
            var boolList = new List<bool>(ba.Count);
            for (var i = 0; i < ba.Count; i++)
            {
                boolList.Add(ba[i]);
            }

            var fs = bytes.Skip(floatsourceBeginIdx).Take(floatdesLegth).ToArray();
            var floatList = new List<float>(fs.Count() / 4);
            for (var i = 0; i < fs.Length; i += 4)
            {
                floatList.Add(BitConverter.ToSingle(fs, i));
            }

            boolmodel = new ReceiveDataModel<bool>() { DataList = boolList, StartIndex = booldesBeginIdx };
            floatModel = new ReceiveDataModel<float> { DataList = floatList, StartIndex = floatdesBeginIdx };
        }

        private void ParserBCBytes(byte[] bytes, RecvDataType dataType, out ReceiveDataModel<bool> boolmodel,
            out ReceiveDataModel<float> floatModel)
        {
            var netDataConfig = m_Config.NetConfig.NetDataProtocolConfig.PackageIdOnlyConfig.NetDataConfig;

            var floatsourceBeginIdx = netDataConfig.NetInputDataPackage.FloatStartByte;
            int floatdesBeginIdx;
            var floatdesLegth = netDataConfig.NetInputDataPackage.FloatCountOfByte;

            var boolsourceBeginIdx = netDataConfig.NetInputDataPackage.BoolStartByte;
            int booldesBeginIdx;
            var booldesLegth = netDataConfig.NetInputDataPackage.BoolCountOfByte;
            switch (dataType)
            {
                case RecvDataType.FirstPackage:
                    floatdesBeginIdx = 0;
                    booldesBeginIdx = 0;
                    break;
                case RecvDataType.SecondPackage:
                    floatdesBeginIdx = netDataConfig.NetInputDataPackage.FloatCountOfByte / 4;
                    booldesBeginIdx = netDataConfig.NetInputDataPackage.BoolCountOfByte * 8;
                    break;
                case RecvDataType.ThirdPackage:
                    floatdesBeginIdx = netDataConfig.NetInputDataPackage.FloatCountOfByte / 4 * 2;
                    booldesBeginIdx = netDataConfig.NetInputDataPackage.BoolCountOfByte * 8 * 2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("dataType");
            }

            var bs = bytes.Skip(boolsourceBeginIdx).Take(booldesLegth).ToArray();
            var ba = new BitArray(bs);
            var boolList = new List<bool>(ba.Count);
            for (var i = 0; i < ba.Count; i++)
            {
                boolList.Add(ba[i]);
            }

            var fs = bytes.Skip(floatsourceBeginIdx).Take(floatdesLegth).ToArray();
            var floatList = new List<float>(fs.Count() / 4);
            for (var i = 0; i < fs.Length; i += 4)
            {
                floatList.Add(BitConverter.ToSingle(fs, i));
            }

            boolmodel = new ReceiveDataModel<bool>() { DataList = boolList, StartIndex = booldesBeginIdx };
            floatModel = new ReceiveDataModel<float> { DataList = floatList, StartIndex = floatdesBeginIdx };
        }

        private void ActureNetProtocolServiceCmdReceived(CommandType commandType)
        {

        }

        public void Close()
        {
            if (m_ActureNetProtocolService != null)
            {
                m_ActureNetProtocolService.Close();
                m_ActureNetProtocolService = null;
            }

            Dispose();
        }


        public bool Send(byte[] datas, string ip, int port)
        {
            if (m_ActureNetProtocolService == null)
            {
                LogMgr.Error("can not send datas to net work , the net work serivce has shut down.");
                return false;
            }

            try
            {
                m_ActureNetProtocolService.Send(ip,
                    port,
                    datas);
            }
            catch (Exception e)
            {
                LogMgr.Error(
                    string.Format("Send data to net error Target ip : {0} \r\n Target port : {1}({2})\r\n, {3} ",
                        m_Config.NetConfig.NetChannelConfig.BNetConfig.TeachIP,
                        m_Config.NetConfig.NetChannelConfig.BNetConfig.TeachPort,
                        m_Config.NetConfig.NetChannelConfig.BNetConfig.TeachPort.GetActurePort(BNetPortType.Data),
                        e));
            }
            return true;
        }

        /// <summary>
        /// 触发 OnBegin 事件 
        /// </summary>
        protected void OnBegin(NetCommandEventArgs args)
        {
            m_BroadcastStragy.OnCourseStarted();

            HandleUtil.OnHandle(Begin, this, args);
        }

        /// <summary>
        /// 触发 OnEnd 事件 
        /// </summary>
        protected void OnEnd(NetCommandEventArgs args)
        {
            HandleUtil.OnHandle(End, this, args);

            m_BroadcastStragy.OnCourseStopped();
        }

        /// <summary>
        /// 触发 OnCommandReceived 事件 
        /// </summary>
        protected void OnCommandReceived(CommandType data)
        {
            if (CommandReceived != null)
            {
                CommandReceived(data);
            }
        }

        public virtual void OnDataReceived(ProjectType type, NetDatas data)
        {
            var handler = DataReveived;
            if (handler != null)
            {
                handler(data);
            }
        }

        public void Dispose()
        {
            m_BroadcastStragy.Dispose();
        }

        protected virtual void OnStationCollectionUpdated(UpdateStationCollectionEventArgs e)
        {
            if (StationCollectionUpdated != null)
            {
                StationCollectionUpdated.Invoke(this, e);
            }
        }

        protected virtual void OnTimeTableUpdate(TimeTableEventArgs e)
        {
            TimeTableUpdate?.Invoke(this, e);
        }
    }
}
