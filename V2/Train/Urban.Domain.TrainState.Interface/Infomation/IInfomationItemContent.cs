using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Interface.Infomation
{
    public interface IInfomationItemContent
    {
        /// <summary>
        /// ID
        /// </summary>
        int Id { get; }

        /// <summary>
        /// 优先级
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// 消息内容
        /// </summary>
        string MessageContent { get; }

        /// <summary>
        /// 其它
        /// </summary>
        object Other { get; }

    }
}