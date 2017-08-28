using System;
using System.Collections.Generic;
using System.Linq;
using MessageManager.Extention;

namespace MessageManager.Events
{
    public class EventManager : IManager<EventInfo>
    {
        public readonly List<EventInfo> CurrentEvent = new List<EventInfo>();
        public readonly List<EventInfo> HistoryEvent = new List<EventInfo>();

        /// <summary>
        /// CurrentPage
        /// </summary>
        public int CurrentPage { get; protected set; } = 1;

        /// <summary>
        /// MaxPage
        /// </summary>
        public int MaxPage { get; protected set; } = 1;

        /// <summary>
        /// 当前页信息
        /// </summary>
        public IList<EventInfo> CurrentPageInfo { get; protected set; } = null;

        /// <summary>
        /// Reset Page
        /// </summary>
        public void ResetPage()
        {
            CurrentPage = 1;
            MaxPage = 1;
        }

        /// <summary>
        /// 跳转到某一页
        /// </summary>
        /// <param name="page">页</param>
        public void JumpPage(int page)
        {
            if (page <= MaxPage)
            {
                CurrentPage = page;
            }
        }

        /// <summary>
        /// 首页
        /// </summary>
        public void FirstPage()
        {
            CurrentPage = 1;
        }

        /// <summary>
        /// 尾页
        /// </summary>
        public void EndPage()
        {
            CurrentPage = MaxPage;
        }

        public event Action<int> CurrentPageChanged;
        public event Action<int> MaxPageChanged;

        /// <summary>
        /// 当前信息条数
        /// </summary>
        public int Count => CurrentEvent.Count;

        /// <summary>
        /// Messages Dictionary
        /// </summary>
        public IDictionary<int, EventInfo> Messages { get; protected set; }

        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void Initlization(string filePath)
        {
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="id">信息id</param>
        /// <returns>返回<see cref="IMessage"/>数据</returns>
        public EventInfo GetMessage(int id)
        {
            if (Messages.ContainsKey(id))
            {
                return Messages[id];
            }

            return null;
        }

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns><see cref="IList{T}"/></returns>
        public IList<EventInfo> GetAllMessage()
        {
            return CurrentEvent;
        }

        /// <summary>
        /// 创建一条消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="dt">创建消息时间</param>
        /// <returns>true 创建成功 false 创建失败</returns>
        public bool CreatMessage(int id, DateTime dt = new DateTime())
        {
            if (Messages.ContainsKey(id))
            {
                var tmp = Messages[id].Clone().Cast<EventInfo>();
                CurrentEvent.Add(tmp);
                HistoryEvent.Add(tmp);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 移除消息
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="removeTime">移除信息时间</param>
        /// <returns></returns>
        public bool RemoveMessage(int id, DateTime removeTime = default(DateTime))
        {
            var tmp = CurrentEvent.FirstOrDefault(f => f.ID == id);
            if (tmp != null)
            {
                tmp.ConfirmTime = removeTime == default(DateTime) ? DateTime.Now : removeTime;
                CurrentEvent.Remove(tmp);
                return true;
            }
            return false;
        }
    }
}
