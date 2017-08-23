using System;
using MessageManager.Extention;

namespace MessageManager.Events
{
    public class EventInfo : Bindingable, IEventInfo
    {
        /// <summary>Creates a new object that is a copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            var result = MessageExtention.Cast<EventInfo>(MemberwiseClone());
            result.HappenTime = default(DateTime);
            result.ConfirmTime = default(DateTime);
            return result;
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(IMessage other)
        {
            return other.ID == ID && other.Content == Content;
        }

        /// <summary>
        /// Message's ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Message's Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 车
        /// </summary>
        public string Car { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public EventLevel Level { get; set; }

        /// <summary>
        /// 故障发生时间
        /// </summary>
        public DateTime HappenTime { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime ConfirmTime { get; set; }
    }
}