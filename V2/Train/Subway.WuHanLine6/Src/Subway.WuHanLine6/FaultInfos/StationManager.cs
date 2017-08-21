using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Subway.WuHanLine6.FaultInfos
{
    /// <summary>
    /// 站点管理类
    /// </summary>
    [Export]
    public class StationManager
    {
        /// <summary>
        ///
        /// </summary>
        public IDictionary<int, StationInfo> AllStation { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="info">数据</param>
        public void Initialize(IEnumerable<StationInfo> info)
        {
            AllStation = info.ToDictionary(t => t.Index, t => t);
        }

        /// <summary>
        /// 获取站点名称
        /// </summary>
        /// <param name="index">站点编号</param>
        /// <returns>retrue StationName</returns>
        public string GetStatioName(int index)
        {
            if (AllStation.ContainsKey(index))
            {
                return AllStation[index].Name;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取站点ID 
        /// </summary>
        /// <param name="name">站点名称</param>
        /// <returns></returns>
        public int GetStationIndex(string name)
        {
            return AllStation.Where(info => info.Value.Name == name).Select(info => info.Key).FirstOrDefault();
        }
    }
}