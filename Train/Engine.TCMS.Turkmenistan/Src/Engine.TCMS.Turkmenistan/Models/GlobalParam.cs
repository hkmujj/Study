using MMI.Facility.Interface.Project;

namespace Engine.TCMS.Turkmenistan.Models
{
    internal class GlobalParam
    {
        static GlobalParam()
        {
            Instance = new GlobalParam();
        }
        /// <summary>
        /// GlobalParam 单例
        /// </summary>
        public static GlobalParam Instance { get; private set; }
        /// <summary>
        /// 子系统初始化参数
        /// </summary>
        public SubsystemInitParam InitParam { get; private set; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="initParam">子系统初始化参数</param>
        public void Initlization(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            Initlization(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory, initParam.AppConfig.AppPaths.ConfigDirectory);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="root"></param>
        /// <param name="app"></param>
        public void Initlization(string root, string app)
        {

        }
    }
}