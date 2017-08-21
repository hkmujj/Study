using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Config.ConfigModel;
using CRH2MMI.Fault;
using CRH2MMI.Notify;
using log4net;
using MMI.Facility.Interface.Attribute;


namespace CRH2MMI.Common.Global
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GlobalInfo : CRH2BaseClass
    {

        public static GlobalInfo Instance { private set; get; }

        /// <summary>
        /// 资源
        /// </summary>
        public GlobalResource Resource { private set; get; }

        /// <summary>
        /// 参数
        /// </summary>
        public GlobalParam Param { private set; get; }

        /// <summary>
        /// 事件
        /// </summary>
        public GlobalEvent GlobalEvent { private set; get; }

        private FaultCreator m_FaultCreator;

        public CRH2ConfigAdpt CRH2ConfigAdpt { private set; get; }

        

        public override bool Init()
        {
            if (Resource == null)
            {
                NDC.Clear();
                NDC.Push(GetProjectName());

                using (NDC.Push("Global Inits"))
                {
                    Instance = this;
                    GlobalEvent = GlobalEvent.Instance;
                    Param = GlobalParam.Instance;
                    Resource = new GlobalResource(this);
                    PackingBrakeCutManager.Instance.SetDataPackage(this);
                    CRH2ConfigAdpt = new CRH2ConfigAdpt()
                    {
                        File = Path.Combine(GlobalParam.RootConfigPath, CRH2Config.FileName),
                        ConfigPath = Path.Combine(RecPath, "..\\Config"),
                    };
                    CRH2ConfigAdpt.Init();

                    Resource.Init(this.IndexDescriptionConfig);

                    Param.Init();

                    HandleUtil.OnHandle(GlobalEvent.Instance.ConfigLoadCompleteEvent, this, null);

                    LogMgr.Debug(string.Format("Init CRH2 Config({0}) success, current type is 【{1}】", CRH2ConfigAdpt.File, CRH2ConfigAdpt.CurrentCRH2Config.Type));

                    m_ReversalIndex =
                        GetInBoolIndex(
                            CRH2ConfigAdpt.CurrentCRH2Config.GlobalInfoConfig.Reversal.InBoolColoumNames.First());

                    InitUIObj(new[] {CRH2ConfigAdpt.CurrentCRH2Config.GlobalInfoConfig.Reversal});

                    InitNotify();

                    InitFault();

                    InitChangePagaManager();
                }
            }
            return true;
        }

        private void InitNotify()
        {
            if (NotifyManager.Instance.Initalize(this, CurrentCRH2Config.NotifyConfig))
            {
                UIObj.InBoolList.AddRange(NotifyManager.Instance.NotifyDictionary.Values.SelectMany(s => s.Select(si => si.InboolIndex)));
            }
        }

        private void InitChangePagaManager()
        {
            ChangePagaManager.Instance.Initalize();
        }

        private void InitFault()
        {
            FaultMgr.Instance.Init();
            UIObj.InBoolList.AddRange(FaultMgr.Instance.FaultNameInfoDic.Keys);
            LogMgr.Info(string.Format("All Fault (count={0}) logic index : 【{1}】", FaultMgr.Instance.FaultNameInfoDic.Keys.Count,
                string.Join(",", FaultMgr.Instance.FaultNameInfoDic.Keys.Select(s => s.ToString(CultureInfo.InvariantCulture)).ToArray())));
            var faultConfig = CRH2ConfigAdpt.CurrentCRH2Config.FaultConfig;
            var names = new List<string>
            {
                faultConfig.Distance,
                faultConfig.Speed
            };

            UIObj.InFloatList.AddRange(names.Select(GetInFloatIndex));
            m_FaultCreator = new FaultCreator(this, CRH2ConfigAdpt.CurrentCRH2Config.FaultConfig);
        }

        private int m_ReversalIndex;

        public override void paint(Graphics g)
        {
            var reverState = BoolList[m_ReversalIndex];
            if (reverState != Param.IsReversalTrain)
            {
                Param.IsReversalTrain = reverState;
            }

            if (m_FaultCreator.CheckHasFault())
            {
                m_FaultCreator.UpdateFaultMgr();
            }
            if (this == NotifyManager.Instance.ViewClass)
            {
                NotifyManager.Instance.UpdateNotifyState();
            }
        }

        /// <summary>
        /// Instance.CRH2ConfigAdpt.CurrentCRH2Config 的快捷访问
        /// </summary>
        public static CRH2ConfigData CurrentCRH2Config { get { return Instance.CRH2ConfigAdpt.CurrentCRH2Config; }}
    }

}
