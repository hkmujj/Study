using System.Collections.Generic;
using System.Linq;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Data.Config;

namespace CRH2MMI.Common.Global
{
    internal class ProjectCommunicationPortFacade :ICommunicationPortFacade
    {
        private readonly IProjectIndexDescriptionConfig m_ProjectIndexDescriptionConfig;

        public ProjectCommunicationPortFacade( IProjectIndexDescriptionConfig projectIndexDescriptionConfig)
        {
            m_ProjectIndexDescriptionConfig = projectIndexDescriptionConfig;
        }

        public List<int> GetInBoolIndexs(IEnumerable<string> rowNames)
        {
            
            return rowNames.Select(GetInBoolIndex).ToList();
        }

        public List<int> GetOutBoolIndexs(IEnumerable<string> rowNames)
        {
            return rowNames.Select(GetOutBoolIndex).ToList();
        }

        public List<int> GetInFloatIndexs(IEnumerable<string> rowNames)
        {
            return rowNames.Select(GetInFloatIndex).ToList();
        }

        public List<int> GetOutFloatIndexs(IEnumerable<string> rowNames)
        {
            return rowNames.Select(GetOutFloatIndex).ToList();
        }

        public List<int> GetInBoolIndexs(CRH2CommunicationPortModel configModel)
        {
            return GetInBoolIndexs(configModel.InBoolColoumNames);
        }

        public List<int> GetOutBoolIndexs(CRH2CommunicationPortModel configModel)
        {
            return GetOutBoolIndexs(configModel.OutBoolColoumNames);
        }

        public List<int> GetInFloatIndexs(CRH2CommunicationPortModel configModel)
        {
            return GetInFloatIndexs(configModel.InFloatColoumNames);
        }

        public List<int> GetOutFloatIndexs(CRH2CommunicationPortModel configModel)
        {
            return GetOutFloatIndexs(configModel.OutFloatColoumNames);
        }

        public int GetInBoolIndex(string name)
        {
            return m_ProjectIndexDescriptionConfig.InBoolDescriptionDictionary[name];
        }

        public int GetOutBoolIndex(string name)
        {
            return m_ProjectIndexDescriptionConfig.OutBoolDescriptionDictionary[name];
        }

        public int GetInFloatIndex(string name)
        {
            return m_ProjectIndexDescriptionConfig.InFloatDescriptionDictionary[name];
        }

        public int GetOutFloatIndex(string name)
        {
            return m_ProjectIndexDescriptionConfig.OutFloatDescriptionDictionary[name];
        }
    }
}