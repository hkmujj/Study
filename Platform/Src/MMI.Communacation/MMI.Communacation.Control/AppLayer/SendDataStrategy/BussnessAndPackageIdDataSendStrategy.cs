using System;
using System.Collections.Generic;
using CommonUtil.Util;
using MMI.Communacation.Control.ConcreateProtocol;
using MMI.Communacation.Control.ProtocolLayer.SendPackage;
using MMI.Communacation.Interface.AppLayer;
using MMI.Communacation.Interface.ProtocolLayer;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.NetDataPackage;
using MMI.Facility.Interface.Extension;

namespace MMI.Communacation.Control.AppLayer.SendDataStrategy
{
    internal class BussnessAndPackageIdDataSendStrategy : IDataSendStrategy
    {
        private readonly ICommunacationDataSender m_NetService;

        private readonly NetCommunicationDataWriteService m_CommunicationDataWriteService;

        private readonly IConfig m_Config;

        private readonly List<SendPackage> m_SendPackageCollection;

        private readonly INetDataConfig m_NetDataConfig;

        public BussnessAndPackageIdDataSendStrategy(ICommunacationDataSender netService,
            NetCommunicationDataWriteService communicationDataWriteService,
            IConfig config, INetProjectDataConfig projectDataConfig)
        {
            m_NetService = netService;
            m_CommunicationDataWriteService = communicationDataWriteService;
            m_Config = config;

            m_SendPackageCollection = new List<SendPackage>();

            try
            {
                m_NetDataConfig = config.NetConfig.GetNetDataConfig(projectDataConfig);
            }
            catch (Exception e)
            {
                LogMgr.Error("Can not get net data config where project = {0}, Exception = {1}",
                    projectDataConfig.ProjectType, e);
            }

            if (m_NetDataConfig.NetOutputDataPackage.PackageCount <= 0)
            {
                SysLog.Warn("NetDataConfig.NetOutputDataPackage.PackageCount = 0, 没有输出数据");
            }
            else if (m_NetDataConfig.NetOutputDataPackage.PackageCount >= 1)
            {
                var packageCount = Math.Min(m_NetDataConfig.NetOutputDataPackage.PackageCount,
                    SendDataTypeHelper.AllSendDataType.Length);
                if (m_NetDataConfig.NetOutputDataPackage.PackageCount > SendDataTypeHelper.AllSendDataType.Length)
                {
                    SysLog.Error(
                        "NetDataConfig.NetOutputDataPackage.PackageCount = {0}, 目前只支持发送{1}包数据，剩下的数据发送都将被忽略 !!!!",
                        m_NetDataConfig.NetOutputDataPackage.PackageCount,
                        SendDataTypeHelper.AllSendDataType.Length);
                }
                for (var i = 0; i < packageCount; i++)
                {
                    var packge = new SendPackage(m_NetDataConfig.NetOutputDataPackage.FloatCountOfByte,
                        m_NetDataConfig.NetOutputDataPackage.BoolCountOfByte)
                    {
                        Head =new SendPackageHeadWrapper()
                    };
                    packge.Head.PackageHead.TypeD.SetDataPackageIndex(i);
                    packge.Head.PackageHead.TypeD.SetProjctType(projectDataConfig.ProjectType);
                    m_SendPackageCollection.Add(packge);
                }
            }
        }

        public void SendDatas()
        {
            var bools = new SortedDictionary<int, bool>(m_CommunicationDataWriteService.BoolDic);
            var floats = new SortedDictionary<int, float>(m_CommunicationDataWriteService.FloatDic);

            for (int i = 0; i < m_SendPackageCollection.Count; i++)
            {
                var sendPackage = m_SendPackageCollection[i];
                sendPackage.ConvertToBytes(bools, i * (m_NetDataConfig.NetOutputDataPackage.BoolCountOfByte * 8),
                    floats, i * (m_NetDataConfig.NetOutputDataPackage.FloatCountOfByte / 4));
                m_NetService.Send(sendPackage.ToSendBytes(), m_Config.NetConfig.NetChannelConfig.BNetConfig.CpuIP,
                    m_Config.NetConfig.NetChannelConfig.BNetConfig.CpuPort.GetActurePort(BNetPortType.Data));
            }
        }
    }
}