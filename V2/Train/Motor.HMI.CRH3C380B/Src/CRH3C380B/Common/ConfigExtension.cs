using System;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.Interface.Data.Config;

namespace Motor.HMI.CRH3C380B.Common
{
    // ReSharper disable once InconsistentNaming
    public static class IConfigExtension
    {
        public static ProjectType GetCurrentProjectType(this IConfig iconfig)
        {
            var activedAddinconfig =
                iconfig.SystemConfig.SubsystemConfigCollection.Where(
                    w => w.NeedLoad && w.SubsystemType == SubsystemType.Addin).ToList();

            var first =
                activedAddinconfig.FirstOrDefault(
                    f => f.Name.Contains("380BL") || f.Name.Contains("380B") || f.Name.Contains("CRH3C"));
            if (first.Name.Contains("380BL"))
            {
                return ProjectType.CRH380BL;
            }
            if (first.Name.Contains("380B"))
            {
                return ProjectType.CRH380B;
            }
            if (first.Name.Contains("CRH3C"))
            {
                return ProjectType.CRH3C;
            }
            //if (activedAddinconfig.FirstOrDefault(f => f.Name.Contains("380BL")) != null)
            //{
            //    if (activedAddinconfig.FirstOrDefault(f => f.Name.Contains("CRH3C")) != null)
            //    {
            //        msg = "系统配置参数错误，配置中即有380B，又有CRH3C";
            //        AppLog.Fatal(msg);
            //        throw new ArgumentException(msg);
            //    }


            //    return ProjectType.CRH380B;
            //}
            //if (activedAddinconfig.FirstOrDefault(f => f.Name.Contains("CRH3C")) != null)
            //{
            //    return ProjectType.CRH3C;
            //}
            const string msg = "系统配置参数错误，系统配置中即没找到380B，也没有找到3C和380BL的配置";
            AppLog.Fatal(msg);
            throw new ArgumentException(msg);
        }
    }
}