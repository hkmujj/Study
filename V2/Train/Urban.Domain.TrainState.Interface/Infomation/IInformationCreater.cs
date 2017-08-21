using System;
using System.Collections.ObjectModel;

namespace Urban.Domain.TrainState.Interface.Infomation
{
    /// <summary>
    /// 消息创建
    /// </summary>
    public interface IInformationCreater
    {
        /// <summary>
        /// 消息生发
        /// </summary>
        event Action<ReadOnlyCollection<IInfomationItemContent>> InfomationCreated;


        /// <summary>
        /// 消息结束
        /// </summary>
        event Action<ReadOnlyCollection<IInfomationItemContent>> InfomationDeleted;


        void Initalize();
    }
}