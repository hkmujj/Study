using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using CommonUtil.Util;
using CRH2MMI.Config.ConfigModel;
using CRH2MMI.Resource.Images;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;


namespace CRH2MMI.Common.Global
{
    class GlobalResource 
    {
        private Image[] m_Images;
        private readonly GlobalInfo m_GlobalInfo;

        public GlobalResource(GlobalInfo globalInfo)
        {
            m_GlobalInfo = globalInfo;
        }

        private Dictionary<string, Image> m_FileImageDic;

        private Image[] Images
        {
            get
            {
                return m_Images;
            }
        }
        public static GlobalResource Instance { private set; get; }

        /// <summary>
        /// 鼠标按下的图片
        /// </summary>
        public Image BtnDownImage
        {
            get { return Images[0]; }
        }

        /// <summary>
        /// 鼠标弹起的图片
        /// </summary>
        public Image BtnUpImage
        {
            get { return Images[1]; }
        }

        public bool Init(IProjectIndexDescriptionConfig indexDescriptionConfig)
        {
            Instance = this;

            m_FileImageDic = new Dictionary<string, Image>();

            m_Images = new Image[] { ImageResource.bluedown, ImageResource.blueb };

            m_GlobalInfo.CRH2ConfigAdpt.CurrentPortReaderConfig.Correction();
            var adpt = new ExcelAdapter();

            m_GlobalInfo.CRH2ConfigAdpt.CurrentFaultReaderConfig.Correction();
            FaultInfoDataTable = adpt.Adapter(m_GlobalInfo.CRH2ConfigAdpt.CurrentFaultReaderConfig).Tables[0];

            ICommunicationPortFacade = new ProjectCommunicationPortFacade(indexDescriptionConfig);
            IFaultInfoFacade = new FaultInfoFacade(this);

            return true;
        }

        public Image GetOrLoadImage(string fullFile)
        {
            if (string.IsNullOrEmpty(fullFile) || !File.Exists(fullFile))
            {
                LogMgr.Warn(string.Format("File not found error, file name is {0}", fullFile));
                return null;
            }
            if (!m_FileImageDic.ContainsKey(fullFile))
            {
                m_FileImageDic[fullFile] = Image.FromFile(fullFile);
            }
            return m_FileImageDic[fullFile];
        }

        /// <summary>
        /// 界面配置的xml文件 的根结点名字
        /// </summary>
        public const string ViewConfigXmlRootName = "ViewConfig";

        public DataTable FaultInfoDataTable { private set; get; }

        /// <summary>
        /// 获得数据接口的方法
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public ICommunicationPortFacade ICommunicationPortFacade { private set; get; }

        /// <summary>
        /// 获取故障信息的接口
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public IFaultInfoFacade IFaultInfoFacade { private set; get; }


       
    }

    static class GlobalResourceExtension
    {
        public static List<int> GetInBoolIndexs(this GlobalResource res,IEnumerable<string> rowNames)
        {
            return res.ICommunicationPortFacade.GetInBoolIndexs(rowNames);
        }

        public static List<int> GetOutBoolIndexs(this GlobalResource res, IEnumerable<string> rowNames)
        {
            return res.ICommunicationPortFacade.GetOutBoolIndexs(rowNames);
        }

        public static List<int> GetInFloatIndexs(this GlobalResource res, IEnumerable<string> rowNames)
        {
            return res.ICommunicationPortFacade.GetInFloatIndexs(rowNames);
        }

        public static List<int> GetOutFloatIndexs(this GlobalResource res, IEnumerable<string> rowNames)
        {
            return res.ICommunicationPortFacade.GetOutFloatIndexs(rowNames);
        }

        public static List<int> GetInBoolIndexs(this GlobalResource res, CRH2CommunicationPortModel configModel)
        {
            return res.ICommunicationPortFacade.GetInBoolIndexs(configModel);
        }

        public static List<int> GetOutBoolIndexs(this GlobalResource res, CRH2CommunicationPortModel configModel)
        {
            return res.ICommunicationPortFacade.GetOutBoolIndexs(configModel);
        }

        public static List<int> GetInFloatIndexs(this GlobalResource res, CRH2CommunicationPortModel configModel)
        {
            return res.ICommunicationPortFacade.GetInFloatIndexs(configModel);
        }

        public static List<int> GetOutFloatIndexs(this GlobalResource res, CRH2CommunicationPortModel configModel)
        {
            return res.ICommunicationPortFacade.GetOutFloatIndexs(configModel);
        }

        public static int GetInBoolIndex(this GlobalResource res, string name)
        {
            return res.ICommunicationPortFacade.GetInBoolIndex(name);
        }

        public static int GetOutBoolIndex(this GlobalResource res, string name)
        {
            return res.ICommunicationPortFacade.GetOutBoolIndex(name);
        }

        public static int GetInFloatIndex(this GlobalResource res, string name)
        {
            return res.ICommunicationPortFacade.GetInFloatIndex(name);
        }

        public static int GetOutFloatIndex(this GlobalResource res, string name)
        {
            return res.ICommunicationPortFacade.GetOutFloatIndex(name);
        }
    }
}
