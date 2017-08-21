using System.Windows.Threading;

namespace MMI.Facility.Interface.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppExtension
    {

        private static Dispatcher m_MainDispatcher;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="dispatcher"></param>
        public static void SetMainDispatcher(this App app, Dispatcher dispatcher)
        {
            m_MainDispatcher = dispatcher;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static Dispatcher GetMainDispatcher(this App app)
        {
            return m_MainDispatcher;
        }
    }
}
