using System;
using System.Diagnostics;
using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Communacation.Control.AppLayer.SendDataStrategy
{
    internal class DataSendStrategyFactory
    {
        public static readonly DataSendStrategyFactory Instance = new DataSendStrategyFactory();

        private DataSendStrategyFactory()
        {

        }

        public IDataSendStrategy CreateDataSendStrategy(CreateParam para)
        {
            switch (para.Config.NetConfig.NetDataProtocolConfig.ProtocolType)
            {
                case NetDataProtocolType.PackageIdOnly:
                    return new TypeCDataSendStrategy(para.NetSendService, para.CommunicationDataWriteService,
                        para.Config, para.NetProjectDataConfig);
                case NetDataProtocolType.BussnessIdAndPackageId:
                    return new BussnessAndPackageIdDataSendStrategy(para.NetSendService,
                        para.CommunicationDataWriteService, para.Config, para.NetProjectDataConfig);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal class CreateParam
        {
            [DebuggerStepThrough]
            public CreateParam(ICommunacationDataSender netSendService,
                NetCommunicationDataWriteService communicationDataWriteService, IConfig config,
                INetProjectDataConfig netProjectDataConfig)
            {
                NetSendService = netSendService;
                CommunicationDataWriteService = communicationDataWriteService;
                Config = config;
                NetProjectDataConfig = netProjectDataConfig;
            }

            public ICommunacationDataSender NetSendService { get; private set; }

            public NetCommunicationDataWriteService CommunicationDataWriteService { get; private set; }

            public IConfig Config { get; private set; }

            public INetProjectDataConfig NetProjectDataConfig { get; private set; }
        }
    }
}