using System;
using System.Collections.Generic;
using CommonUtil.Util;
using Motor.ATP.Infrasturcture.Model.Config;

namespace Motor.ATP.Infrasturcture.Model.Extension
{
    public static class RunningConfigExtension
    {
        public static Version GetCurrentVersion(this RunningConfig runningConfig)
        {
            Version v = null;
            if (runningConfig != null)
            {
                if (!Version.TryParse(runningConfig.Version, out v))
                {
                    AppLog.Error("Can not parser current version , {0}", runningConfig.Version);
                }
                else
                {
                    AppLog.Info("Parse version sucess, the version = {0}", v);
                }
            }
            else
            {
                AppLog.Error("Can not parser current version , because the RunningConfig = null");
            }



            return v;
        }

        public static List<Version> GetKnownVersions(this RunningConfig runningConfig)
        {
            var vs = new List<Version>();
            
            if (runningConfig != null)
            {
                foreach (var s in runningConfig.KnownVersions)
                {
                    Version v = null;
                    if (!Version.TryParse(s, out v))
                    {
                        AppLog.Error("Can not parser known versions ,item = {0}", runningConfig.Version);
                    }
                    else
                    {
                        AppLog.Info("Parse known versions sucess, the version item = {0}", v);
                        vs.Add(v);
                    }
                }
            }
            else
            {
                AppLog.Error("Can not parser known versions , because the RunningConfig = null");
            }

            return vs;
        }
    }
}