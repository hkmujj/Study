using System;
using System.Collections.ObjectModel;
using MMI.Facility.Interface.Service;

namespace Motor.ATP.Domain.Interface.Infomation
{
    /// <summary>
    /// 消息创建
    /// </summary>
    public interface IInformationCreater : IDataListener
    {
        /// <summary>
        /// 消息生发
        /// </summary>
        event Action<ReadOnlyCollection<IInfomationItemContent>> InfomationCreated;


        /// <summary>
        /// 消息结束
        /// </summary>
        event Action<ReadOnlyCollection<IInfomationItemContent>> InfomationDeleted;

        /// <summary>
        /// 消息清除
        /// </summary>
        event Action InfomationCleared;


        void Initalize();
    }
}