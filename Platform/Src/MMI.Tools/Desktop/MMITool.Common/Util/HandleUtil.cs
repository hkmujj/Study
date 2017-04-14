using System;
using System.Diagnostics;

namespace MMITool.Common.Util
{
    /// <summary>
    /// HandleUtil
    /// </summary>
    public static class HandleUtil
    {
        /// <summary>
        /// 事件触发
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [DebuggerStepThrough]
        public static void OnHandle(EventHandler handler, object sender, EventArgs e)
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// 事件触发
        /// </summary>
        /// <param name="handler"></param>
        [DebuggerStepThrough]
        public static void OnHandle(EventHandler handler)
        {
            OnHandle(handler, null, null);
        }

        /// <summary>
        /// 事件触发
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [DebuggerStepThrough]
        public static void OnHandle<T>(EventHandler<T> handler, object sender, T e) where  T : EventArgs
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// 事件触发
        /// </summary>
        /// <param name="handler"></param>
        [DebuggerStepThrough]
        public static void OnHandle<T>(EventHandler<T> handler) where T : EventArgs
        {
            OnHandle(handler, null, null);
        }
    }
}
