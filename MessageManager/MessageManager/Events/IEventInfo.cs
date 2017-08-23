using System;
using System.ComponentModel;

namespace MessageManager.Events
{
    /// <summary>
    /// 故障信息
    /// </summary>
    public interface IEventInfo : IMessage
    {
        /// <summary>
        /// 车
        /// </summary>
        string Car { get; }
        /// <summary>
        /// 代码
        /// </summary>
        string Code { get; }
        /// <summary>
        /// 等级
        /// </summary>
        EventLevel Level { get; }
        /// <summary>
        /// 故障发生时间
        /// </summary>
        DateTime HappenTime { get; }
        /// <summary>
        /// 确认时间
        /// </summary>
        DateTime ConfirmTime { get; }

    }

    /// <summary>
    /// 事件等级
    /// </summary>
    [Flags]
    public enum EventLevel
    {
        /// <summary>
        /// 事件信息
        /// </summary>
        Event = 1,
        /// <summary>
        /// 轻微
        /// </summary>
        Slight = 2,
        /// <summary>
        /// 中等
        /// </summary>
        Medium = 4,
        /// <summary>
        /// 严重
        /// </summary>
        Grave = 8,
        /// <summary>
        /// 所有故障
        /// </summary>
        All = Event | Slight | Medium | Grave
    }
}

