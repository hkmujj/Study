using System;
using System.Diagnostics;
using System.IO;
using log4net;

namespace CommonUtil.Util
{
    /// <summary>
    /// 日志管理
    /// </summary>
    public static class LogMgr
    {
        // ReSharper disable once InconsistentNaming
        private static readonly ILog m_Log;

        /// <summary>
        /// 
        /// </summary>
        public static ILog SysLog { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static ILog AppLog { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerName"></param>
        /// <returns></returns>
        public static ILog GetLogger(string loggerName)
        {
            return LogManager.GetLogger(loggerName);
        }

        static LogMgr()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\LogConfig.xml")));
            SysLog = LogManager.GetLogger("System");
            AppLog = LogManager.GetLogger("AppLog");
            m_Log = LogManager.GetLogger("LogMgr");
            Info("Create LogMgr success.");
        }

        /// <summary>
        /// 当前的 logger
        /// </summary>
        public static ILog CurrentLogger { get { return m_Log; } }

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msgFormat"></param>
        [DebuggerStepThrough]
        public static void Fatal(string msgFormat)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Fatal(GetDetailMsg(msgFormat));
            }
        }

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msgFormat"></param>
        /// <param name="args"></param>
        [DebuggerStepThrough]
        public static void Fatal(string msgFormat, params object[] args)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Fatal(string.Format(GetDetailMsg(msgFormat), args));
            }
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg"></param>
        [DebuggerStepThrough]
        public static void Error(string msg)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Error(GetDetailMsg(msg));
            }
        }

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msgFormat"></param>
        /// <param name="args"></param>
        [DebuggerStepThrough]
        public static void Error(string msgFormat, params object[] args)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Error(string.Format(GetDetailMsg(msgFormat), args));
            }
        }


        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="msg"></param>
        [DebuggerStepThrough]
        public static void Warn(string msg)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Warn(GetDetailMsg(msg));
            }
        }

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msgFormat"></param>
        /// <param name="args"></param>
        [DebuggerStepThrough]
        public static void Warn(string msgFormat, params object[] args)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Warn(string.Format(GetDetailMsg(msgFormat), args));
            }
        }


        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg"></param>
        [DebuggerStepThrough]
        public static void Info(string msg)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Info(GetDetailMsg(msg));
            }
        }

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msgFormat"></param>
        /// <param name="args"></param>
        [DebuggerStepThrough]
        public static void Info(string msgFormat, params object[] args)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Info(string.Format(GetDetailMsg(msgFormat), args));
            }
        }


        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="msg"></param>
        [DebuggerStepThrough]
        public static void Debug(string msg)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Debug(GetDetailMsg(msg));
            }
        }

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msgFormat"></param>
        /// <param name="args"></param>
        [DebuggerStepThrough]
        public static void Debug(string msgFormat, params object[] args)
        {
            if (m_Log.IsErrorEnabled)
            {
                m_Log.Debug(string.Format(GetDetailMsg(msgFormat), args));
            }
        }


        [DebuggerStepThrough]
        private static string GetDetailMsg(string msg)
        {
            var stack = (new StackTrace(2, true)).GetFrame(0);
            return string.Format("{0},{1} : ", stack.GetFileName(), stack.GetFileLineNumber()) + msg;
        }
    }
}