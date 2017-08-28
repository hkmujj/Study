using System;
using System.Collections.Generic;

namespace MessageManager
{
    /// <summary>
    /// Paging
    /// </summary>
    public interface IPaging<T>
    {
        /// <summary>
        /// CurrentPage
        /// </summary>
        int CurrentPage { get; }
        /// <summary>
        /// MaxPage
        /// </summary>
        int MaxPage { get; }
        /// <summary>
        /// 当前页信息
        /// </summary>
        IList<T> CurrentPageInfo { get; }
        /// <summary>
        /// Reset Page
        /// </summary>
        void ResetPage();
        /// <summary>
        /// 跳转到某一页
        /// </summary>
        /// <param name="page">页</param>
        void JumpPage(int page);
        /// <summary>
        /// 首页
        /// </summary>
        void FirstPage();
        /// <summary>
        /// 尾页
        /// </summary>
        void EndPage();
        /// <summary>
        /// 当前页更改
        /// </summary>
        event Action<int> CurrentPageChanged;
        /// <summary>
        /// 最大页更改
        /// </summary>
        event Action<int> MaxPageChanged;
    }
}
