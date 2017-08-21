using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.Infomation;
using Motor.ATP.Domain.Model.Message;

namespace Motor.ATP.Domain.Model.Infomation
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
                TimeStamp = infomation.StartTime,
                Style =
                    infomation.Content.ShowType == InfomationShowType.Flash || infomation.Content.ShowType == InfomationShowType.FlashAndEnsure
                        ? MessageStyle.FlashFrame
                        : MessageStyle.Ordinary,
            };

            return msg;
        }
    }
}