using System;
using System.Collections.Generic;
using System.Linq;
using MMI.Communacation.Control.PresentationLayer;
using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.DataType.Config.Net;
using MMI.Facility.DataType.Config.Net.DataProtocol;
using MMI.Facility.DataType.Data;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Service;

namespace MMI.Communacation.Control.AppLayer
{
    public class NetCommunicationDataFacadeService : CommunicationDataFacadeServiceBase
    {
        private readonly PresentationLayerNetService m_PresentationLayerNetService;

        private readonly IConfig m_Config;

        public NetCommunicationDataFacadeService(IConfig config)
        {
            m_Config = config;
            m_PresentationLayerNetService = new PresentationLayerNetService(config);
            
            m_PresentationLayerNetService.Begin += OnPresentationLayerNetServiceBegin;
            m_PresentationLayerNetService.End += OnPresentationLayerNetServiceEnd;
            m_PresentationLayerNetService.StationCollectionUpdated += OnStationCollectionUpdated;
        }



        public override void InitalizeDataServiceDictionary()
        {
            CommunicationDataServiceDictionary =
                new Dictionary<ICommunicationDataKey, ICommunicationDataService>(CommunicationDataKey.Empty);

            switch (m_Config.NetConfig.NetDataProtocolConfig.ProtocolType)
            {
                case NetDataProtocolType.PackageIdOnly:
                    InitalizeCommunicationServiceOther();
                    break;
                case NetDataProtocolType.BussnessIdAndPackageId:
                    InitalizeCommunicationServiceD();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void StartNet()
        {
            m_PresentationLayerNetService.Initialize(m_Config.NetConfig);
        }

        private void InitalizeCommunicationServiceOther()
        {
            var dataConfig = (NetDataConfig)m_Config.NetConfig.NetDataProtocolConfig.PackageIdOnlyConfig.NetDataConfig;
            var readdDteEntity = new CommunicationDataEntity(m_Config, dataConfig.NetInputDataPackage);
            var writeDateEntity = new CommunicationDataEntity(m_Config, dataConfig.NetOutputDataPackage);

            foreach (var pt in m_Config.AppConfigs.Select(s => s.ProjectType).Distinct())
            {
                var config = new NetProjectDataConfig()
                {
                    NetDataConfig = dataConfig,
                    ProjectType = pt
                };

                var apps = m_Config.AppConfigs.Where(w => w.ProjectType == config.ProjectType);
                foreach (var key in
                    apps.Select(app => new CommunicationDataKey(app))
                        .Where(key => !CommunicationDataServiceDictionary.ContainsKey(key)))
                {
                    var dataService = new NetCommunicationDataService(m_Config, m_PresentationLayerNetService, config,
                        readdDteEntity, writeDateEntity);

                    CommunicationDataServiceDictionary.Add(key, dataService);
                }
            }

            m_PresentationLayerNetService.DataReveived += readdDteEntity.PresentationLayerNetServiceOnDataReceived;
        }


        private void InitalizeCommunicationServiceD()
        {
            foreach (var config in m_Config.NetConfig.NetDataProtocolConfig.BussnessIdAndPackageIdConfig.ProjectDataConfigCollection)
            {
                var readdDteEntity = new CommunicationDataEntity(m_Config, config.NetDataConfig.NetInputDataPackage);
                var writeDateEntity = new CommunicationDataEntity(m_Config, config.NetDataConfig.NetOutputDataPackage);

                m_PresentationLayerNetService.DataReveived += datas =>
                {
                    if (datas.ProjectType == config.ProjectType)
                    {
                        readdDteEntity.PresentationLayerNetServiceOnDataReceived(datas);
                    }
                };

                var apps = m_Config.AppConfigs.Where(w => w.ProjectType == config.ProjectType);
                foreach (var key in
                    apps.Select(app => new CommunicationDataKey(app))
                        .Where(key => !CommunicationDataServiceDictionary.ContainsKey(key)))
                {
                    var dataService = new NetCommunicationDataService(m_Config, m_PresentationLayerNetService, config,
                        readdDteEntity, writeDateEntity);

                    CommunicationDataServiceDictionary.Add(key, dataService);
                }
            }
        }

        public override void Dispose()
        {
            m_PresentationLayerNetService.Close();
            base.Dispose();
        }
    }
}