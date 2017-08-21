using System;
using System.Collections.Generic;
using System.Linq;
using Engine.LCDM.HXD3.Config;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.LCDM.HXD3.Common
{
    class GlobalParam
    {
        public static GlobalParam Instance { get; private set; }

        static GlobalParam()
        {
            Instance = new GlobalParam();
        }

        public SubsystemInitParam InitPara { private set; get; }

        private Lazy<IProjectIndexDescriptionConfig> m_IndexCOnfig;

        public IProjectIndexDescriptionConfig IndexConfig
        {
            get
            {
                return m_IndexCOnfig.Value;
            }
        }

        public Lazy<List<InitialSet>> InitSets { get; private set; }

        public ViewSetInterpreter ViewSets { get; private set; }
        
        public void Initalize(SubsystemInitParam initParam)
        {
            InitPara = initParam;

            m_IndexCOnfig =
                new Lazy<IProjectIndexDescriptionConfig>(
                    () => InitPara.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                        new CommunicationDataKey(InitPara.AppConfig)));
            InitSets =
                new Lazy<List<InitialSet>>(
                    () => ExcelParser.Parser<InitialSet>(InitPara.AppConfig.AppPaths.ConfigDirectory)
                        .ToList());

            ViewSets = new ViewSetInterpreter(InitSets);
        }
    }
}
