namespace CommonUtil.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IChangedDataAdapter<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectName"></param>
        void RequestClearChanged(string projectName);

        /// <summary>
        /// 
        /// </summary>
        void ClearChanged();
    }
}