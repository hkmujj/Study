using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.HMI.CRH1A.Common.EventArg;
using Motor.HMI.CRH1A.Common.Train;
using CommonUtil.Util;

namespace Motor.HMI.CRH1A.Common.Global
{
    /// <summary>
    /// 全局事件
    /// </summary>
    internal class GlobalEvent
    {
        public static GlobalEvent Instance { private set; get; }

        static GlobalEvent()
        {
            Instance = new GlobalEvent();
        }

        private GlobalEvent()
        {

        }

        public void OnRestartCompleted(EventArgs args = null)
        {
            if (RestartCompleted != null)
            {
                RestartCompleted(this, args);
            }
        }

        public void OnStartCompleted(EventArgs args = null)
        {
            if (StartCompleted != null)
            {
                StartCompleted(this, args);
            }
        }

        /// <summary>
        /// 是否反转 IsReversalTrain 的值改变了的事件
        /// </summary>
        public EventHandler ReversalChanged;

        /// <summary>
        /// 列车状态变化
        /// </summary>
        public EventHandler<TrainStateChangedArgs> TrainStateChanged;

        /// <summary>
        /// 司机室位置变化 
        /// </summary>
        public EventHandler<EventArgs<ChangedArgs<DriverLocation>>> DriverLocationChanged;

        /// <summary>
        /// 重启中.
        /// </summary>
        public EventHandler RestartCompleted;

        /// <summary>
        /// 启动完成
        /// </summary>
        public EventHandler StartCompleted;
    }
}
