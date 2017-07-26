using System;
using System.Collections.ObjectModel;
using MMI.Facility.Interface.Service;

namespace Motor.ATP.Infrasturcture.Interface.Infomation
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
        /// 消息确认
        /// </summary>
        event Action<ReadOnlyCollection<IInfomationItemContent>> InfomationEnsured;

        /// <summary>
        /// 消息清除
        /// </summary>
        event Action InfomationCleared;

        /// <summary>
        /// 
        /// </summary>
        string ConfigFileName { get; }

        /// <summary>
        /// 
        /// </summary>
        void Initalize();

        /// <summary>
        /// 是否有需要计时 的消息
        /// </summary>
        /// <returns></returns>
        bool HasAnyNeedTimeInfomation();

        /// <summary>
        /// 更新消息
        /// </summary>
        /// <param name="infoID">消息ID</param>
        /// <param name="updateType">是否为添加消息</param>
        void UpdateInfomation(int infoID, InformationUpdateType updateType= InformationUpdateType.Add);
    }
}