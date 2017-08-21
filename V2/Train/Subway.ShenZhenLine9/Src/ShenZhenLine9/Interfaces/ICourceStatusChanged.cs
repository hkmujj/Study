namespace Subway.ShenZhenLine9.Interfaces
{
    /// <summary>
    /// 课程结束
    /// </summary>
    public interface ICourceStatusChanged
    {
        /// <summary>
        /// 清除运行数据
        /// </summary>
        void Clear();

        /// <summary>
        /// 初始化运行数据
        /// </summary>
        void Start();
    }
}