namespace Subway.ShenZhenLine9.Interfaces
{
    /// <summary>
    /// 状态
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// 状态Key
        /// </summary>
        IStateKey Key { get; }
        /// <summary>
        /// 标题名称
        /// </summary>
        string TitleName { get; }
        /// <summary>
        /// 按钮1
        /// </summary>
        IBtnItem Btn01 { get; }
        /// <summary>
        /// 按钮1
        /// </summary>
        IBtnItem Btn02 { get; }
        /// <summary>
        /// 按钮1
        /// </summary>
        IBtnItem Btn03 { get; }
        /// <summary>
        /// 按钮1
        /// </summary>
        IBtnItem Btn04 { get; }
        /// <summary>
        /// 按钮1
        /// </summary>
        IBtnItem Btn05 { get; }
        /// <summary>
        /// 按钮1
        /// </summary>
        IBtnItem Btn06 { get; }
        /// <summary>
        /// 按钮1
        /// </summary>
        IBtnItem Btn07 { get; }
        /// <summary>
        /// 按钮1
        /// </summary>
        IBtnItem Btn08 { get; }
        /// <summary>
        /// 按钮1
        /// </summary>
        IBtnItem Btn09 { get; }
        /// <summary>
        /// 按钮1
        /// </summary>
        IBtnItem Btn10 { get; }

        /// <summary>
        /// 更新状态
        /// </summary>
        void UpdateSate();

    }
}