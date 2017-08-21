namespace Engine._6A.Interface
{
    public interface ITabItemInfoProvider
    {
        /// <summary>
        /// 表头名
        /// </summary>
        string HeadName { get; }

        /// <summary>
        /// TabIndex
        /// </summary>
        int TabIndex { get; }
    }
}