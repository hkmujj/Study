using System;

namespace Motor.ATP.Infrasturcture.Interface.Infomation
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
        /// 
        /// </summary>
        DateTime StartTime { get; }

    }

    /// <summary>
    /// 
    /// </summary>
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
        /// 时间显示方式
        /// </summary>
        InfomationTimeShowType TimeShowType { get; }

        /// <summary>
        /// 显示类型
        /// </summary>
        InfomationShowType ShowType { get; }

        /// <summary>
        /// 系统响应类型
        /// </summary>
        InfomationSystemResonseType SystemResonseType { get; }

        /// <summary>
        /// 响应类型
        /// </summary>
        InfomationResponseType ResponseType { get; }

        /// <summary>
        /// 自动取消
        /// </summary>
        InfomationAutoDeleteType AutoCancelType { get; }

        /// <summary>
        /// 下一控制模式
        /// </summary>
        ControlType NextControlType { get; }

        /// <summary>
        /// 消息内容
        /// </summary>
        string MessageContent { get; }

        /// <summary>
        /// 弹框标题
        /// </summary>
        string PopupTitle { get; }


        /// <summary>
        /// 弹框内容
        /// </summary>
        string PopupContent { get; }

        /// <summary>
        /// 弹出框内容编码方式
        /// </summary>
        int PopupContentCodePage { get; }

        /// <summary>
        /// 
        /// </summary>
        int OkReturnIndex { get; }

        /// <summary>
        /// 
        /// </summary>
        int CancelReturnIndex { get; }
    }
}
