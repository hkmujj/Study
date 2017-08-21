using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MMI.Facility.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP.Infrasturcture.Interface.Service
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
        /// 消息被确认
        /// </summary>
        event Action<IInfomationItem> InfomationEnsured;

        /// <summary>
        /// 所有消息被确认完了
        /// </summary>
        event Action AllInfomationEnsureCompleted;

        /// <summary>
        /// 当前正在确认的消息
        /// </summary>
        IInfomationItem CurrentEnsureingInfomation { get; }

        /// <summary>
        /// 确认当前需要确认的消息
        /// </summary>
        /// <param name="ensureParam"></param>
        /// <returns>已被确认的消息</returns>
        IInfomationItem EnsureCurrentInfomation(InformationEnsureParam ensureParam = null);

        /// <summary>
        /// 确认指定消息
        /// </summary>
        /// <param name="informations"></param>
        /// <returns></returns>
        ReadOnlyCollection<IInfomationItem> EnsureInfomationItems(IEnumerable<IInfomationItemContent> informations);

        /// <summary>
        /// 设置新的消息产生类
        /// </summary>
        /// <param name="infomationCreater"></param>
        void SetInformationCreater(IInformationCreater infomationCreater);


        /// <summary>
        /// 获取消息产生类
        /// </summary>
        IInformationCreater GetInformationCreater();

        /// <summary>
        /// 初始化
        /// </summary>
        void Initalize();

    }
}