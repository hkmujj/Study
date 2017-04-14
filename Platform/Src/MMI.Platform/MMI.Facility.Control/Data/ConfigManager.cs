using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using CommonUtil.Util;
using Microsoft.Practices.Prism;
using MMI.Facility.DataType.Config;
using MMI.Facility.DataType.Config.Net;
using MMI.Facility.DataType.Extension;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.Control.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigManager : IConfigManager
    {
        /// <summary>
        /// 单
        /// </summary>
        public static ConfigManager Instance { private set; get; }

        private readonly Dictionary<SubsystemType, IAppConfigLoader> m_AppConfigLoaderDictionary;

        public Config Config { get; private set; }

        IConfig IConfigManager.Config
        {
            get { return Config; }
        }

        static ConfigManager()
        {
            Instance = new ConfigManager();
        }

        private ConfigManager()
        {
            Config = new Config();
            m_AppConfigLoaderDictionary = new Dictionary<SubsystemType, IAppConfigLoader>()
            {
                {SubsystemType.Addin, new AddinAppConfigLoader()},
                {SubsystemType.Subsys, new SubsystemAppConfigLoader()}
            };
        }

        public void LoadConfig(string baseDirectory, bool needInitalize = true)
        {

            LoadSysConfigIfNeed(baseDirectory);

            var rootConfigDirctory = Path.Combine(baseDirectory, "Config");

            var netFile = Path.Combine(rootConfigDirctory, NetConfig.FileName);
            SysLog.Info(string.Format("加载网络配置文件:{0}", netFile));
            LoadNetConfig(netFile);

            LoadDebugWindConfig(rootConfigDirctory);

            LoadAppConfig(baseDirectory);

            LoadIndexDescriptionConfig(rootConfigDirctory, needInitalize);

            ThreadPool.QueueUserWorkItem(state =>
            {
                LoadScreenTableConfig(rootConfigDirctory);
            });

            if (!ValidateConfig())
            {
                SysLog.Error("config has some error.");
            }
        }

        private void LoadScreenTableConfig(string configDirectory)
        {
            var data =
                DataSerialization.DeserializeFromXmlFile<ScreenTableConfig>(Path.Combine(configDirectory,
                    ScreenTableConfig.FileName));

            Config.ScreenTableConfig = data ?? new ScreenTableConfig() { ConcreateScreenItems = new List<ScreenItem>()};
        }

        private void LoadIndexDescriptionConfig(string configDirctory, bool needInitalize)
        {
            var serach = string.Format("{0}.*{1}", Path.GetFileNameWithoutExtension(IndexDescriptionConfig.FileName),
                Path.GetExtension(IndexDescriptionConfig.FileName));

            var files = Directory.GetFiles(configDirctory, serach);

            if (!files.Any())
            {
                SysLog.Info("There is not any 索引描述配置文件");
            }

            var totalConfig = new IndexDescriptionConfig();

            foreach (var file in files)
            {
                SysLog.Info(string.Format("加载索引描述配置文件:{0}", file));

                var config = DataSerialization.DeserializeFromXmlFile<IndexDescriptionConfig>(file);

                if (config != null)
                {
                    foreach (var descriptionConfig in config.ProjectIndexDescriptionConfigs)
                    {
                        descriptionConfig.ConfigFile = file;

                        FindThenSetProjectType(descriptionConfig);
                    }
                    totalConfig.ProjectIndexDescriptionConfigs.AddRange(config.ProjectIndexDescriptionConfigs);
                }
            }

            if (needInitalize)
            {
                totalConfig.Initalize(configDirctory);
            }

            Config.IndexDescriptionConfig = totalConfig;
        }

        private void FindThenSetProjectType(ProjectIndexDescriptionConfig descriptionConfig)
        {
            try
            {
                var projectType =
                    Config.AppConfigs.First(f => f.AppName == descriptionConfig.AppNames.First()).ProjectType;

                descriptionConfig.ProjectType = projectType;

                foreach (var c in descriptionConfig.AppNames.Skip(1))
                {
                    var t = Config.AppConfigs.FirstOrDefault(f => f.AppName == c);
                    if (t == null || t.ProjectType != projectType)
                    {
                        SysLog.Warn(
                            "We found the index description config 's appname={0} and appname={1} is not same projcet type.",
                            descriptionConfig.AppNames.First(), c);
                    }
                }
            }
            catch (Exception e)
            {
                SysLog.Error(
                    "Can not found app name in system config when parser index description files. the name={0}, Exception={1}",
                    descriptionConfig.AppNames.FirstOrDefault(), e);
            }

            SysLog.Info("Loaded index description info: File={0}, ProjectType={1}, AppNames={2}",
                descriptionConfig.ConfigFile, descriptionConfig.ProjectType,
                string.Join(",", descriptionConfig.AppNames));
        }


        private void LoadAppConfig(string baseDirectory)
        {
            Config.AllAppConfigs.Clear();

            foreach (var subsystemConfig in Config.SystemConfig.SubsystemConfigCollection)
            {
                var config = m_AppConfigLoaderDictionary[subsystemConfig.SubsystemType].LoadAppConfig(Config,
                    baseDirectory, subsystemConfig);
                Config.AllAppConfigs.Add(config);
            }
        }

        public void SaveDebugWindRunningConfig(System.Windows.Forms.Control sendser, Rectangle rectangle)
        {
            string baseDirectory = Config.SystemDicrectory.BaseDirectory;
            var file = Path.Combine(baseDirectory, "Config", DebugWindowConfig.RunningFileName);

            var windConfig = (DebugWindowConfig)Config.DebugWindowConfig;

            windConfig.SetOrUpdateRectangle(sendser.GetType(), rectangle);

            DataSerialization.SerializeToXmlFile(windConfig, file);
        }

        public void SaveConfig<T>(T data, string baseDirectory)
        {
            var file = Path.Combine(baseDirectory, "Config",
                (string)typeof(T).GetField("FileName").GetValue(null));
            DataSerialization.SerializeToXmlFile(data, file);
        }

        public void SaveConfig<T>(T data, string appName, string baseDirectory)
        {
            var file = Path.Combine(baseDirectory, string.Format("{0}\\Config", appName),
                (string)typeof(T).GetField("FileName").GetValue(null));
            DataSerialization.SerializeToXmlFile(data, file);
        }

        private void LoadDebugWindConfig(string rootConfigDirectory)
        {
            var configFile = Path.Combine(rootConfigDirectory, DebugWindowConfig.FileName);
            var runningFile = Path.Combine(rootConfigDirectory, DebugWindowConfig.RunningFileName);
            var targetFile = configFile;
            if (File.Exists(runningFile) && File.Exists(configFile))
            {
                try
                {
                    if (File.GetLastWriteTimeUtc(runningFile) > File.GetLastWriteTimeUtc(configFile))
                    {
                        targetFile = runningFile;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            SysLog.Info(string.Format("加载调试窗体配置文件:{0}", targetFile));

            var debugConfig = DataSerialization.DeserializeFromXmlFile<DebugWindowConfig>(targetFile);

            var config = Config;

            config.DebugWindowConfig = debugConfig;
        }


        private void LoadNetConfig(string netFile)
        {
            var netConfig = DataSerialization.DeserializeFromXmlFile<NetConfig>(netFile);
            if (netConfig.NetChannelConfig == null)
            {
                SysLog.Error("Can not found net channel config in {0}", NetConfig.FileName);
            }
            if (netConfig.NetDataProtocolConfig == null)
            {
                SysLog.Error("Can not found net data protocol config in {0}", NetConfig.FileName);
            }
            Config.NetConfig = netConfig;
        }

        private bool ValidateConfig()
        {
            if (Config.SystemConfig.SubsystemConfigCollection.All(c => c.SubsystemType != SubsystemType.Addin))
            {
                SysLog.Error("start project count msut larger than 0 by config.ini");
                return false;
            }

            if (Config.NetConfig.NetChannelConfig != null)
            {
                switch (Config.NetConfig.NetChannelConfig.NetType)
                {
                    case NetType.None:
                        LogMgr.Info("can not found any usable network in config, the selected nettype = {0}",
                            Config.NetConfig.NetChannelConfig.NetType);
                        break;
                    case NetType.B:
                    case NetType.C:
                    case NetType.PTT2D:

                        switch (Config.NetConfig.NetDataProtocolConfig.ProtocolType)
                        {
                            case NetDataProtocolType.PackageIdOnly:
                                SysLog.Info("Current net data protocol is only one package head type.");
                                OutputNetDataLogs(Config.NetConfig.NetDataProtocolConfig.PackageIdOnlyConfig.NetDataConfig);
                                break;
                            case NetDataProtocolType.BussnessIdAndPackageId:
                                SysLog.Info("Current net data protocol : head include bussiness and index.");
                                foreach (var config in Config.NetConfig.NetDataProtocolConfig.BussnessIdAndPackageIdConfig.ProjectDataConfigCollection)
                                {
                                    OutputNetDataLogs(config.NetDataConfig, config.ProjectType.ToString());
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                SysLog.Error("Can not found net channel config .");
            }

            // TODO ValidateConfig
            return true;
        }

        private static void OutputNetDataLogs(INetDataConfig netDataConfig, string pre = null)
        {
            var info = string.Format("{1} 接收到数据包的个数为【{0}】", netDataConfig.NetInputDataPackage.PackageCount, pre);
            SysLog.Info(info);
            info = string.Format("{2} 接收到数据包的float值起始byte为【{0}】, 大小为【{1}】",
                netDataConfig.NetInputDataPackage.FloatStartByte,
                netDataConfig.NetInputDataPackage.FloatCountOfByte, pre);
            SysLog.Info(info);
            info = string.Format("{2} 接收到数据包的bool值起始byte为【{0}】, 大小为【{1}】",
                netDataConfig.NetInputDataPackage.BoolStartByte,
                netDataConfig.NetInputDataPackage.BoolCountOfByte, pre);
            SysLog.Info(info);
            info = string.Format("{1} 发送数据包的个数为【{0}】", netDataConfig.NetInputDataPackage.PackageCount, pre);
            SysLog.Info(info);
            info = string.Format("{2} 发送数据包的float值的映射起始byte为【{0}】, 大小为【{1}】",
                netDataConfig.NetOutputDataPackage.FloatMappedStartIndex,
                netDataConfig.NetOutputDataPackage.FloatCountOfByte, pre);
            SysLog.Info(info);
            info = string.Format("{2} 发送数据包的bool值的映射起始byte为【{0}】, 大小为【{1}】",
                netDataConfig.NetOutputDataPackage.BoolMappedStartIndex,
                netDataConfig.NetOutputDataPackage.BoolCountOfByte, pre);
            SysLog.Info(info);
        }


        public void LoadSysConfigIfNeed(string baseDirectory)
        {
            if (Config.SystemConfig == null)
            {
                LoadSysConfig(baseDirectory);
            }
        }

        private void LoadSysConfig(string baseDirectory)
        {
            var file = Path.Combine(baseDirectory, "Config", SystemConfig.FileName);

            try
            {
                Config.SystemConfig =
                    DataSerialization.DeserializeFromXmlFile<SystemConfig>(file);

                SysLog.Info(string.Format("加载系统配置文件:{0} sucess.", file));
            }
            catch (Exception e)
            {
                var msg = string.Format("Load system config {0} error. {1}", file, e);
                LogMgr.Fatal(msg);
                throw new Exception(msg);
            }
        }
    }
}
