using CommonUtil.Util;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Config.ConfigModel;

namespace Motor.HMI.CRH3C380B.Config
{
    public static class ProjectConfigExtension
    {
        public static void CorrectionProjectType(this ProjectConfig config, CRH3C380BBase obj)
        {
            if (config == null)
            {
                return;
            }

            if (obj == null)
            {
                return;
            }

            var current = obj.DataPackage.Config.GetCurrentProjectType();
            if (config.ProjectType != current)
            {
                AppLog.Warn(
                    string.Format(
                        "system config's project type 【{0}】 is not same as project config 【{1}】, we will forcible make  project config's project type = {0}",
                        current, config.ProjectType));
                config.ProjectType = current;
            }
        }
    }
}