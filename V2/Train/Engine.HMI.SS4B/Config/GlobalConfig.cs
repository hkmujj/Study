using CommonUtil.Util;
using MMI.Facility.DataType;
using System.IO;

namespace SS4B_TMS.Config
{
    public static class GlobalConfig
    {
        static GlobalConfig()
        {
        }

        private static TrainNumberConfig TrainConfig;

        public static TrainNumberConfig GetTrainConfig(this baseClass value)
        {
            if (TrainConfig == null)
            {
                TrainConfig = DataSerialization.DeserializeFromXmlFile<TrainNumberConfig>(Path.Combine(value.AppPaths.ConfigDirectory, TrainNumberConfig.FileName));
            }
            return TrainConfig;
        }
    }
}