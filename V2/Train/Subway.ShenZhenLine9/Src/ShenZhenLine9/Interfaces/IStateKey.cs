namespace Subway.ShenZhenLine9.Interfaces
{
    /// <summary>
    /// 状态Key
    /// </summary>
    public interface IStateKey
    {
        /// <summary>
        /// Key名称
        /// </summary>
        string Key { get; }
        /// <summary>
        /// 视图名称
        /// </summary>
        string View { get; }
    }
}