using System.Threading;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public class App
    {
        /// <summary>
        /// 
        /// </summary>
        public static App Current { private set; get; }

        static App()
        {
            Current = new App();
        }

        /// <summary>
        /// 主线程
        /// </summary>
        public Thread MainThread { set; get; }

        /// <summary>
        /// 主窗口
        /// </summary>
        public IMainBaseForm MainForm { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IServiceManager ServiceManager { set; get; }

    }
}
