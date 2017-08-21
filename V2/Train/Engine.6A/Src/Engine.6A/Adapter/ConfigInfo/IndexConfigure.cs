using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel.Interface;
//using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine._6A.Adapter.ConfigInfo
{
    public class IndexConfigure
    {
        public static readonly IndexConfigure Instance = new IndexConfigure();

        public IProjectIndexDescriptionConfig IndexFacade { private set; get; }

        private IndexConfigure()
        {
        }

        public void Initalize(SubsystemInitParam initParam)
        {
            IndexFacade = initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                new CommunicationDataKey(initParam.AppConfig));
        }
    }
}