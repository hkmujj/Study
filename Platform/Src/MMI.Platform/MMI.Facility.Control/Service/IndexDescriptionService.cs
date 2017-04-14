using System.Collections.Generic;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    public class IndexDescriptionService : IIndexDescriptionService
    {
        private Dictionary<ICommunicationDataKey, IIndexInterpreter> m_IndexInterpreterDictionary;

        public void Initalize(CommunicationDataFacadeServiceBase dataFacadeService, IConfig config)
        {
            m_IndexInterpreterDictionary =
                new Dictionary<ICommunicationDataKey, IIndexInterpreter>(CommunicationDataKey.Empty);
            foreach (var appConfig in config.AppConfigs)
            {
                var key = new CommunicationDataKey(appConfig.ProjectType, appConfig.AppName);
                m_IndexInterpreterDictionary.Add(key,
                    new IndexInterpreter(dataFacadeService.GetCommunicationDataService(appConfig), key));
            }
        }

        public IEnumerable<IIndexInterpreter> AllIndexInterpreters { get { return m_IndexInterpreterDictionary.Values; } }

        public IIndexInterpreter GetIndexInterpreter(ICommunicationDataKey communicationDataKey)
        {
            if (m_IndexInterpreterDictionary.ContainsKey(communicationDataKey))
            {
                return m_IndexInterpreterDictionary[communicationDataKey];
            }
            return null;
        }
    }
}