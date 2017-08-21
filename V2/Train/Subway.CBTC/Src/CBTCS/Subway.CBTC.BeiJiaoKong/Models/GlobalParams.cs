using System;
using System.Collections.ObjectModel;
using System.Linq;
using CBTC.Infrasturcture.Model.Msg.Details;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Subway.CBTC.BeiJiaoKong.Models
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    public class GlobalParams
    {
        private IProjectIndexDescriptionConfig m_ProjectIndexDescription;

        static GlobalParams()
        {
            Instance = new GlobalParams();
        }
        /// <summary>
        /// GlobalParams 单例
        /// </summary>
        public static GlobalParams Instance { get; private set; }
        /// <summary>
        /// 子系统初始化参数
        /// </summary>
        public SubsystemInitParam InitParam { get; private set; }

        public bool IsDebug { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Lazy<ReadOnlyCollection<IInformationContent>> InformationContents { get; private set; }

        /// <summary>
        /// 项目接口描述
        /// </summary>
        public IProjectIndexDescriptionConfig ProjectIndexDescription
        {
            get
            {
                if (m_ProjectIndexDescription==null)
                {
                    if (InitParam!=null)
	                {
		                 m_ProjectIndexDescription=InitParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig( new CommunicationDataKey(InitParam.AppConfig));
	                }
                    
                }
                return m_ProjectIndexDescription;
            }
            private set { m_ProjectIndexDescription = value; }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="initParam">子系统参数</param>
        public void Initialize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            Initialize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory, initParam.AppConfig.AppPaths.ConfigDirectory);
            
            Initalize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                initParam.AppConfig.AppPaths.ConfigDirectory, initParam.DataPackage.Config.SystemConfig.IsDebugModel);
        }

        public void Initialize(string rootConfig, string appConfig)
        {

        }

        public void Initalize(string rootConfigPath, string appConfigPath, bool isDebug)
        {
            IsDebug = isDebug;

            InformationContents =
                new Lazy<ReadOnlyCollection<IInformationContent>>(
                    () =>
                        ExcelParser.Parser<InformationContent>(appConfigPath)
                            .Cast<IInformationContent>()
                            .ToList()
                            .AsReadOnly());
        }
    }
}