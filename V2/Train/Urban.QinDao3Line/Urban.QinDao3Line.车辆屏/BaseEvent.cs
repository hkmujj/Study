using System;

namespace Urban.QingDao3Line.MMI
{
    class BaseEvent
    {
        /// <summary>
        /// 事件逻辑号
        /// </summary>
        public int EventLogicId { get; set; }
        /// <summary>
        /// 事件的内容
        /// </summary>
        public string EventContent { get; set; }
        /// <summary>
        /// 事件发生的位置
        /// </summary>
        public string EventLocation { get; set; }
        /// <summary>
        /// 事件的名字
        /// </summary>
        public string EventName { get; set; }
        /// <summary>
        /// 事件发生的时间
        /// </summary>
        public DateTime EventTime { get; set; }
        /// <summary>
        /// 事件结束的时间
        /// </summary>
        public DateTime EventOverTime { get; set; }
    }
}
