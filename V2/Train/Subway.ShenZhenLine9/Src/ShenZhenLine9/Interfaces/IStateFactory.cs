using System.Collections.Generic;

namespace Subway.ShenZhenLine9.Interfaces
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
        /// 获取所有State
        /// </summary>
        /// <returns></returns>
        IEnumerable<IState> GetAllState();
    }
}