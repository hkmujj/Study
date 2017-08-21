namespace Subway.ShenZhenLine9.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITabItemInfoProvider
    {
        /// <summary>
        /// 标题名称
        /// </summary>
        string HeadName { get; }

        /// <summary>
        /// 所在Tab索引
        /// </summary>
        int TabIndex { get; }
    }
}