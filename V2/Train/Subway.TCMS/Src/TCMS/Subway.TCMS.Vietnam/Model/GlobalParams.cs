using MMI.Facility.Interface.Project;

namespace Subway.TCMS.Vietnam.Model
{
    public class GlobalParams
    {
        static GlobalParams()
        {
            Instance = new GlobalParams();
        }
        /// <summary>
        /// GlobalParams单例
        /// </summary>
        public static GlobalParams Instance { get; private set; }
        /// <summary>
        /// 子系统参数
        /// </summary>
        public SubsystemInitParam InitParam { get; private set; }
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="initParam">子系统参数</param>
        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
        }
    }
}
