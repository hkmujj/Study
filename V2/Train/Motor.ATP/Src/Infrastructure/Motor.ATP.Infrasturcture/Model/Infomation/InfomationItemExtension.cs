using System;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Model.Message;

namespace Motor.ATP.Infrasturcture.Model.Infomation
{
    public static class InfomationItemExtension
    {
        /// <summary>
        /// 转换成 IMessageItem
        /// </summary>
        /// <param name="infomation"></param>
        /// <returns></returns>
        public static IMessageItem ToMessageItem(this IInfomationItem infomation)
        {
            var msg = new MessageItem
            {
                Content = infomation.Content.MessageContent,
                InfomationItem = infomation,
                TimeStamp = infomation.Content.TimeShowType.IsTimeFromNow() ? infomation.StartTime : DateTime.MinValue,
                TimeDifference =
                    infomation.Content.TimeShowType == InfomationTimeShowType.TimeFrom0
                        ? -TimeSpan.FromTicks(infomation.StartTime.Ticks)
                        : TimeSpan.Zero,
                Style =
                    infomation.Content.ShowType == InfomationShowType.Flash ||
                    infomation.Content.ShowType == InfomationShowType.FlashAndEnsure
                        ? MessageStyle.FlashFrame
                        : MessageStyle.Ordinary,
            };

            return msg;
        }

        /// <summary>
        /// 获取消息的计时长度, 只针对需要计时的消息有意义
        /// </summary>
        /// <param name="messageItem"></param>
        /// <returns></returns>
        public static TimeSpan GetTimingSpan(this IMessageItem messageItem)
        {
            return messageItem.TimeStamp - (messageItem.InfomationItem.StartTime + messageItem.TimeDifference);
        }
    }
}