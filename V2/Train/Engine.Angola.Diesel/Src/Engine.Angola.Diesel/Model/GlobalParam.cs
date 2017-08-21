using System.IO;
using CommonUtil.Util;
using Engine.Angola.Diesel.Config.ConfigModel;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.Angola.Diesel.Model
{
    public class GlobalParam
    {
        private const string DefalutProjectName = "Engine.Angola.Diesel";

        public static readonly GlobalParam Instance = new GlobalParam();

        public LocationConfig LocationConfig { private set; get; }

        public SubsystemInitParam InitParam { private set; get; }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        public void Initalize(string basePath)
        {
            InitalizeLocationConfig(Path.Combine(basePath, DefalutProjectName, "Config", LocationConfig.FileName));
        }

        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            IndexDescription =
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig));

            InitalizeLocationConfig(Path.Combine(initParam.AppConfig.AppPaths.ConfigDirectory, LocationConfig.FileName));
        }

        private void InitalizeLocationConfig(string file)
        {
            LocationConfig = DataSerialization.DeserializeFromXmlFile<LocationConfig>(file);
        }
    }
}