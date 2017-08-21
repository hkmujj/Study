namespace Subway.ShenZhenLine7.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStateFactory
    {
        /// <summary>
        /// 获取State
        /// </summary>
        /// <param name="key">StateKey</param>
        /// <returns></returns>
        IState GetOrCreateState(IStateKey key);

        /// <summary>
        /// 获取State
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IState GetOrCreateState(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        string GetKey(string view);
    }
}