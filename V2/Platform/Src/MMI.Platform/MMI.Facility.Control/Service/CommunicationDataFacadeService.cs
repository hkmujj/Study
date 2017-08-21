using System;
using System.Collections.Generic;
using System.Linq;
using MMI.Facility.DataType.Config.Net;
using MMI.Facility.DataType.Config.Net.DataProtocol;
using MMI.Facility.DataType.Data;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Data.Config.NetDataPackage;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    public class EditCommunicationDataFacadeService : CommunicationDataFacadeServiceBase
    {
        private readonly IConfig m_Config;

        public EditCommunicationDataFacadeService(IConfig config)
        {
            m_Config = config;
        }

        public override void InitalizeDataServiceDictionary()
        {
            CommunicationDataServiceDictionary =
                new Dictionary<ICommunicationDataKey, ICommunicationDataService>(CommunicationDataKey.Empty);

            switch (m_Config.NetConfig.NetDataProtocolConfig.ProtocolType)
            {
                case NetDataProtocolType.PackageIdOnly:

                    foreach (var pt in m_Config.AppConfigs.Select(s => s.ProjectType).Distinct())
                    {
                        InitalizeProjectDataService(new NetProjectDataConfig()
                        {
                            NetDataConfig =
                                (NetDataConfig)
                                m_Config.NetConfig.NetDataProtocolConfig.PackageIdOnlyConfig.NetDataConfig,
                            ProjectType = pt
                        });
                    }

                    break;
                case NetDataProtocolType.BussnessIdAndPackageId:

                    foreach (var dataConfig in m_Config.NetConfig.NetDataProtocolConfig.BussnessIdAndPackageIdConfig.ProjectDataConfigCollection)
                    {
                        InitalizeProjectDataService(dataConfig);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void InitalizeProjectDataService(INetProjectDataConfig dataConfig)
        {
            var readdDteEntity = new CommunicationDataEntity(m_Config, dataConfig.NetDataConfig.NetInputDataPackage);
            var writeDateEntity = new CommunicationDataEntity(m_Config, dataConfig.NetDataConfig.NetOutputDataPackage);
            var apps = m_Config.AppConfigs.Where(w => w.ProjectType == dataConfig.ProjectType);
            foreach (
                var key in
                apps.Select(app => new CommunicationDataKey(app))
                    .Where(key => !CommunicationDataServiceDictionary.ContainsKey(key)))
            {
                CommunicationDataServiceDictionary.Add(key,
                    new EditableCommunicationDataService(m_Config, dataConfig, readdDteEntity, writeDateEntity));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void StartNet()
        {
            // not need net
        }
    }
}