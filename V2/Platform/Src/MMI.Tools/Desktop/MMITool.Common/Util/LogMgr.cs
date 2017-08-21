using System;
using System.Diagnostics;
using System.IO;
using log4net;

namespace MMITool.Common.Util
{
    /// <summary>
    /// 日志管理
    /// </summary>
    public static class LogMgr
    {
        // ReSharper disable once InconsistentNaming
        private static readonly ILog m_Log;

        static LogMgr()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\LogConfig.xml")));
            m_Log = LogManager.GetLogger("LogMgr");
            Info("Create LogMgr success.");
        }

        /// <summary>
        /// 当前的 logger
        /// </summary>
        public static ILog CurrentLogger { get { return m_Log; }}

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msg"></param>
        public static void Fatal(string msg)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Fatal(GetDetailMsg(msg));
            }
        }


        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Error(GetDetailMsg(msg));
            }
        }


        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Warn(GetDetailMsg(msg));
            }
        }


        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Info(GetDetailMsg(msg));
            }
        }


        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Debug(GetDetailMsg(msg));
            }
        }



        private static string GetDetailMsg(string msg)
        {
            var stack = (new StackTrace(2, true)).GetFrame(0);
            return string.Format("{0},{1} : {2}", stack.GetFileName(), stack.GetFileLineNumber(), msg);
        }
    }
}

