using System.Collections.Generic;

namespace Subway.XiaMenLine1.Interface
{
    /// <summary>
    /// 分页类
    /// </summary>
    public interface IPaging<T>
    {
        /// <summary>
        /// 最大页
        /// </summary>
        int MaxPage { get; }
        /// <summary>
        /// 当前页
        /// </summary>
        int CurrentPage { get; }
        /// <summary>
        /// 每页条数
        /// </summary>
        int PageNum { get; }
        /// <summary>
        /// 当前数据列表
        /// </summary>
        IList<T> CurrentData { get; }
        /// <summary>
        /// 首页
        /// </summary>
        void FristPage();
        /// <summary>
        /// 下一页
        /// </summary>
        void NextPage();
        /// <summary>
        /// 上一页
        /// </summary>
        void LastPage();
        /// <summary>
        /// 获取当前页数据列表
        /// </summary>
        /// <returns>当前页数据列表</returns>
        IList<T> GetCurrent();
    }
}
