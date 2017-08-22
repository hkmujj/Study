using System;
using System.ComponentModel;
using System.IO;
using CommonUtil.Util;
using MMI.Facility.Interface.Project;
using Motor.ATP.Infrasturcture.Model.Config;

namespace Motor.ATP.Infrasturcture.Model
{
    public class GlobalParamBase
    {
        protected GlobalParamBase()
        {
            
        }

        public virtual void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            RunningConfig =
                new Lazy<RunningConfig>(
                    () =>
                        DataSerialization.DeserializeFromXmlFile<RunningConfig>(
                            Path.Combine(initParam.AppConfig.AppPaths.ConfigDirectory,
                                Infrasturcture.Model.Config.RunningConfig.FileName)));
        }

        public bool IsDebugModel
        {
            get
            {
                return InitParam != null && InitParam.DataPackage.Config.SystemConfig.IsDebugModel;
            }
        }

        [Browsable(false)]
        public SubsystemInitParam InitParam { private set; get; }

        public Lazy<RunningConfig> RunningConfig { get; private set; }

        [Browsable(false)]
        public GlobalTimerBase GlobalTimer { get; protected set; }
    }
}