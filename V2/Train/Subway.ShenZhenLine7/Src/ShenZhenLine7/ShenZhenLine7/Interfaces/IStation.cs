namespace Subway.ShenZhenLine7.Interfaces
{
    public interface IStation
    {
        /// <summary>
        /// 编号
        /// </summary>
        int Index { get; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 是否选中
        /// </summary>
        bool IsChecked { get; set; }
    }
}