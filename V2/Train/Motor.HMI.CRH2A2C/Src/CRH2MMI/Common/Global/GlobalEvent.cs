using System;

namespace CRH2MMI.Common.Global
{
    /// <summary>
    /// 全局事件
    /// </summary>
    class GlobalEvent
    {
        public static GlobalEvent Instance { private set; get; }

        static GlobalEvent()
        {
            Instance = new GlobalEvent();
        }

        private GlobalEvent()
        {
            
        }

        /// <summary>
        /// 是否反转 IsReversalTrain 的值改变了的事件
        /// </summary>
        public EventHandler ReversalChanged;

        public EventHandler ConfigLoadCompleteEvent;

        /// <summary>
        /// 重启事件
        /// </summary>
        public EventHandler RestartEvent;

        /// <summary>
        /// 开机
        /// </summary>
        public EventHandler StartEvent;

        /// <summary>
        /// 关机
        /// </summary>
        public EventHandler ShutdownEvent;

    }
}
