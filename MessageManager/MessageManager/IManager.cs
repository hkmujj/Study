using System;
using System.Collections.Generic;

namespace MessageManager
{
    public interface IManager<T> : IPaging<T> where T : IMessage
    {
        /// <summary>
        /// 当前信息条数
        /// </summary>
        int Count { get; }
        /// <summary>
        /// Messages Dictionary
        /// </summary>
        IDictionary<int, T> Messages { get; }
        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        void Initlization(string filePath);
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="id">信息id</param>
        /// <returns>返回<see cref="IMessage"/>数据</returns>
        T GetMessage(int id);
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns><see cref="IList{T}"/></returns>
        IList<T> GetAllMessage();
        /// <summary>
        /// 创建一条消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="dt">创建消息时间</param>
        /// <returns>true 创建成功 false 创建失败</returns>
        bool CreatMessage(int id, DateTime dt = default(DateTime));
        /// <summary>
        /// 移除消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <returns></returns>
        bool RemoveMessage(int id);


    }
}
