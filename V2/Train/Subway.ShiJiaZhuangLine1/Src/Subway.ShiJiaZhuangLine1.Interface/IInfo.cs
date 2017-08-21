using System.Collections.Generic;

namespace Subway.ShiJiaZhuangLine1.Interface
{
    public interface IInfo<TKey, TValue>
    {
        /// <summary>
        /// 文件名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 信息数据字典
        /// </summary>
        Dictionary<TKey, TValue> AllData { get; }
        /// <summary>
        /// 文件名
        /// </summary>
        /// <param name="file"></param>
        void Load(string file);
    }
}
