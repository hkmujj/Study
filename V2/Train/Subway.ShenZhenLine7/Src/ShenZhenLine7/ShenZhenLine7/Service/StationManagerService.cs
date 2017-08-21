using System.Collections.Generic;
using System.Linq;
using MMI.Facility.Interface.Service;
using Subway.ShenZhenLine7.Interfaces;

namespace Subway.ShenZhenLine7.Service
{
    /// <summary>
    /// 站点管理
    /// </summary>
    public class StationManagerService : IService
    {
        static StationManagerService()
        {
            m_AllStation = new Dictionary<int, IStation>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="allStation">站点枚举器</param>
        public StationManagerService(IEnumerable<IStation> allStation)
        {
            m_AllStation = allStation.ToDictionary(t => t.Index, t => t);
        }
        private static IDictionary<int, IStation> m_AllStation;

        /// <summary>
        /// 获取车站
        /// </summary>
        /// <param name="name">车站名称</param>
        /// <returns>返回车站  不存在返回Null</returns>
        public IStation GetStation(string name)
        {
            return m_AllStation.Values.FirstOrDefault(station => station.Name.Equals(name));
        }

        /// <summary>
        /// 获取车站
        /// </summary>
        /// <param name="index">车站编号</param>
        /// <returns>返回车站  不存在返回Null</returns>
        public IStation GetStation(int index)
        {
            return m_AllStation.Values.FirstOrDefault(station => station.Index == index);
        }

        /// <summary>
        /// 获取所有车站
        /// </summary>
        /// <returns>返回所有车站</returns>
        public IEnumerable<IStation> GetAllStation()
        {
            return m_AllStation.Values;
        }
    }
}