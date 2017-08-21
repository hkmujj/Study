using System;
using System.Drawing;
using System.IO;
using CommonUtil.Util;
using Motor.HMI.CRH1A.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;

namespace Motor.HMI.CRH1A.Common.Global
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GlobalInfo : CRH1BaseClass
    {
        private bool m_ButtonEnable;

        public static GlobalInfo Instance { private set; get; }

        public CRH1AConfig Crh1AConfig { private set; get; }

        public CRH1ADetailConfig CRH1ADetailConfig { private set; get; }

        public GlobalViewNavigate GlobalViewNavigate { private set; get; }

        public bool ButtonEnable
        {
            get { return m_ButtonEnable; }
            private set
            {
                if (value == m_ButtonEnable)
                {
                    return;
                }
                m_ButtonEnable = value;
                OnButtonStatusChange();
            }
        }

        public event Action ButtonStatusChange;

        /// <summary>
        /// 参数
        /// </summary>
        public GlobalParam Param { private set; get; }

        /// <summary>
        /// 事件
        /// </summary>
        public GlobalEvent GlobalEvent { private set; get; }

        public void Init(string configPath)
        {
            LogMgr.Debug("Parser CRH1AConfig.xml");
            Crh1AConfig = DataSerialization.DeserializeFromXmlFile<CRH1AConfig>(Path.Combine(configPath, "CRH1AConfig.xml"));
            if (Crh1AConfig.AdaptConfig == null)
            {
                Crh1AConfig.AdaptConfig = new AdaptConfig();
            }

            var detailFile = Path.Combine(configPath, string.Format("CRH1A{0}Config.xml", Crh1AConfig.Type));
            LogMgr.Debug(string.Format("Parser {0}", detailFile));
            CRH1ADetailConfig = DataSerialization.DeserializeFromXmlFile<CRH1ADetailConfig>(detailFile);
            CRH1ADetailConfig.File = detailFile;
        }

        public override bool Initialize()
        {
            Init(Path.Combine(RecPath, "..\\config"));

            Instance = this;

            GlobalEvent = GlobalEvent.Instance;
            Param = GlobalParam.Instance;
            GlobalViewNavigate = GlobalViewNavigate.Instance;

            Param.Init();

            return true;
        }

        public override void paint(Graphics g)
        {
            Param.Refresh();
            ButtonEnable = !BoolList[685];
        }

        protected virtual void OnButtonStatusChange()
        {
            var handler = ButtonStatusChange;
            if (handler != null)
            {
                handler();
            }
        }
    }
}
