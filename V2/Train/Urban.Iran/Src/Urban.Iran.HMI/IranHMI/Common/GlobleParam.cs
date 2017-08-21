using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using Mmi.Communication.Index.Adapter;
using Urban.Iran.HMI.Config.ConfigModel;

namespace Urban.Iran.HMI.Common
{
    public class GlobleParam
    {
        private static volatile CommunicationIndexFacade m_CommunicationIndexFacade;

        private static IranDetailConfig m_DetailConfig;

        public static ResourceManager ResManager { private set; get; }

        public static ResourceManager ResManagerText { private set; get; }

        public IranViewIndex CurrentIranViewIndex { set; get; }

        public IReadOnlyDictionary<int, string> StationList { private set; get; }

        public ReadOnlyCollection<AnnounceInfo> AnnounceInfoCollectionService { private set; get; }
        public ReadOnlyCollection<AnnounceInfo> AnnounceInfoCollectionEmergency { private set; get; }

        public static GlobleParam Instance { private set; get; }

        public HMIWorkModel WorkModel { set; get; }

        /// <summary>
        /// 根配置文件夹
        /// </summary>
        public static readonly string RootConfigDirctory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\");

        public IranRootConfig RootConfig { private set; get; }

        public IranDetailConfig DetailConfig
        {
            get { return m_DetailConfig ?? (m_DetailConfig = LoadDetailConfig()); }
        }

        public static int CurrentSelectedMenuBtn { get; set; }

        public IReadOnlyDictionary<string, int> InBoolDictionary 
        {
            get
            {
                return CommunicationIndexFacade.InBoolDictionary;
            }
        }
        public IReadOnlyDictionary<string, int> OutBoolDictionary 
        {
            get
            {
                return CommunicationIndexFacade.OutBoolDictionary;
            }
        }
        public IReadOnlyDictionary<string, int> InFloatDictionary 
        {
            get
            {
                return CommunicationIndexFacade.InFloatDictionary;
            }
        }
        public IReadOnlyDictionary<string, int> OutFloatDictionary 
        {
            get
            {
                return CommunicationIndexFacade.OutFloatDictionary;
            }
        }
        public CommunicationIndexFacade CommunicationIndexFacade
        {
            get
            {
                if (m_CommunicationIndexFacade == null)
                {
                    m_CommunicationIndexFacade=InitalizeCommunicationIndexFacade();
                }
                return m_CommunicationIndexFacade;
            }
        }

        public int FindInBoolIndex(string name)
        {
            try
            {
                return InBoolDictionary[name];
            }
            catch (Exception e)
            {
                LogMgr.Error(string.Format("Can not found {0} in InBoolTable.", name));
                return int.MaxValue;
            }
        }

        public int FindInFloatIndex(string name)
        {
            try
            {
                return InFloatDictionary[name];
            }
            catch (Exception e)
            {
                LogMgr.Error(string.Format("Can not found {0} in InFloatTable ", name));
                return int.MaxValue;
            }
        }

        public int FindOutBoolIndex(string name)
        {
            try
            {
                return OutBoolDictionary[name];
            }
            catch (Exception e)
            {
                LogMgr.Error(string.Format("Can not found {0} in OutBoolTable.", name));
                return int.MaxValue;
            }
        }

        public int FindOutFloatIndex(string name)
        {
            try
            {
                return OutFloatDictionary[name];
            }
            catch (Exception e)
            {
                LogMgr.Error(string.Format("Can not found {0} in OutFloatTable.", name));
                return int.MaxValue;
            }
        }


        static GlobleParam()
        {
            Instance = new GlobleParam();
            ResManager = new ResourceManager("Urban.Iran.HMI.Resource_GB", typeof(BottomButton).Assembly);
            ResManagerText = new ResourceManager("Urban.Iran.HMI.Resource_AR", typeof(BottomButton).Assembly);
        }

        public GlobleParam()
        {
            WorkModel = HMIWorkModel.Maintenance;
        }


        public void InitalizeStationInfos(string file)
        {
            LoadStationFile(file);
        }

        /// <summary>
        /// 加载车站信息
        /// </summary>
        private void LoadStationFile(string file)
        {
            var allLine = File.ReadAllLines(file, Encoding.Default);
            StationList =
                allLine.Select(line => line.Split(';'))
                    .Where(slip => slip.Length == 2)
                    .ToDictionary(slip => Convert.ToInt32(slip[0]), slip => slip[1]).AsReadOnlyDictionary();

        }


        public void InitalizeFaultFixEnchiridion(string file)
        {
            var all = File.ReadAllLines(file, Encoding.Default);

            var lst = new List<AnnounceInfo>();
            var lst1 = new List<AnnounceInfo>();
            foreach (var tmp in all.Select(s => s.Split('\t')))
            {
                if (tmp.Length < 3)
                {
                    LogMgr.Warn(string.Format("{0} parser error. it has not enough coloumn.", file));
                }
                var info = new AnnounceInfo(Convert.ToInt32(tmp[1]), tmp[2]);
                switch ((PISType)Convert.ToInt32(tmp[0]))
                {
                    case PISType.Service:
                        lst.Add(info);
                        break;
                    case PISType.Emergency:
                        lst1.Add(info);
                        break;
                }

              
            }
            AnnounceInfoCollectionEmergency = lst1.AsReadOnly();
            AnnounceInfoCollectionService = lst.AsReadOnly();
        }


        private CommunicationIndexFacade InitalizeCommunicationIndexFacade()
        {
           var  indexFacade = new CommunicationIndexFacade();
            var rootConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\IranRootConfig.xml");
            RootConfig = DataSerialization.DeserializeFromXmlFile<IranRootConfig>(rootConfigFile);
            if (RootConfig == null)
            {
                LogMgr.Error(string.Format("Can not deserialize root config file \"{0}\"", rootConfigFile));
            }
            else
            {
                RootConfig.Correct(RootConfigDirctory);
                indexFacade.Initalize(RootConfig.CommunicationConfigs);
            }

            return indexFacade;
        }

        private static IranDetailConfig LoadDetailConfig()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\IranDetailConfig.xml");
            var config = DataSerialization.DeserializeFromXmlFile<IranDetailConfig>(file);
            if (config == null)
            {
                LogMgr.Error(string.Format("Can not deserialize detail config file \"{0}\"", file));
            }
            return config;
        }
    }
}