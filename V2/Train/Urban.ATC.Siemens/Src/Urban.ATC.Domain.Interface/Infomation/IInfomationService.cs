using System;
using MMI.Facility.Interface.Service;

namespace Motor.ATP.Domain.Interface.Infomation
{
    /// <summary>
    /// 消息通知服务
    /// </summary>
    public interface IInfomationService : IService, IATPPartial
    {
        /// <summary>
        /// 消息生发
        /// </summary>
        event Action<IInfomationItem> InfomationBegin;


        /// <summary>
        /// 消息结束
        /// </summary>
        event Action<IInfomationItem> InfomationEnd;

        /// <summary>
        /// 当前正在确认的消息
        /// </summary>
        IInfomationItem CurrentEnsureingInfomation { get; }

        /// <summary>
        /// 确认当前需要确认的消息
        /// </summary>
        void EnsureCurrentInfomation();


        /// <summary>
        /// 设置新的消息产生类
        /// </summary>
        /// <param name="infomationCreater"></param>
        void SetInformationCreater(IInformationCreater infomationCreater);


        /// <summary>
        /// 获取消息产生类
        /// </summary>
        IInformationCreater GetInformationCreater();

        void Initalize();

    }
}