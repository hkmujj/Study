using System;

namespace Urban.Domain.TrainState.Interface.Infomation
{
    /// <summary>
    /// 消息数据单元
    /// </summary>
    public interface IInfomationItem : IIdentityProvider<IInfomationItemContent>
    {
        /// <summary>
        /// 内容
        /// </summary>
        IInfomationItemContent Content { get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime StartTime { get; }

        /// <summary>
        /// 解决时间
        /// </summary>
        DateTime FixTime { get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime EndTime { get; }

    }
}