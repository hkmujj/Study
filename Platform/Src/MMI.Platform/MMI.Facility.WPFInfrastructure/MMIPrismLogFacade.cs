using System;
using log4net;
using Microsoft.Practices.Prism.Logging;

namespace MMI.Facility.WPFInfrastructure
{
    /// <summary>
    /// 
    /// </summary>
    internal class MMIPrismLogFacade : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    m_Log.DebugFormat("{0} : {1}", priority, message);
                    break;
                case Category.Exception:
                    m_Log.ErrorFormat("{0} : {1}", priority, message);
                    break;
                case Category.Info:
                    m_Log.InfoFormat("{0} : {1}", priority, message);
                    break;
                case Category.Warn:
                    m_Log.WarnFormat("{0} : {1}", priority, message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("category", category, null);
            }
        }

        // ReSharper disable once InconsistentNaming
        private static readonly ILog m_Log;

        static MMIPrismLogFacade()
        {
            m_Log = LogManager.GetLogger("Prism");
            m_Log.Info("Create Prism logger success.");
        }
    }
}