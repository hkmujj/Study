using System;
using System.IO;
using CommonUtil.Util;

namespace CRH2MMI.Common.Global
{
    internal class GlobalParam
    {
        private static GlobalParam m_Instance;
        private bool m_IsReversalTrain;

        public static GlobalParam Instance
        {
            get { return m_Instance ?? (m_Instance = new GlobalParam()); }
        }

        public ObjectManager ObjectManager{ get { return ObjectManager.Instance; }}

        private GlobalParam()
        {
            IsReversalTrain = false;
        }

        public bool ConfigLoadCompleted { private set; get; }

        /// <summary>
        /// 根配置文件路径 
        /// </summary>
        public static readonly string RootConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

        public void Init()
        {
            GlobalEvent.Instance.ConfigLoadCompleteEvent += (sender, args) => ConfigLoadCompleted = true;
            ApplicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ResourceDirectory = Path.Combine(ApplicationDirectory, "Resource");
        }

        public string ApplicationDirectory { private set; get; }

        /// <summary>
        /// 根资源文件目录 
        /// </summary>
        public string ResourceDirectory { private set; get; }

        /// <summary>
        /// 是否反转车辆, 默认不反, 即最左边为0号车
        /// </summary>
        public bool IsReversalTrain
        {
            set
            {
                m_IsReversalTrain = value;
                HandleUtil.OnHandle(GlobalEvent.Instance.ReversalChanged, this, null);
            }
            get { return m_IsReversalTrain; }
        }

        /// <summary>
        /// 车厢个数
        /// </summary>
        public int CarCount
        {
            get { return GlobalInfo.CurrentCRH2Config.TrainConfig.CarConfigs.Count; }
        }
    }
}
