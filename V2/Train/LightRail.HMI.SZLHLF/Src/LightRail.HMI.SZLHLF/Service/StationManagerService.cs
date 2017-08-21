using System.Collections.Generic;
using System.Linq;
using LightRail.HMI.SZLHLF.Interface;
using MMI.Facility.Interface.Service;

namespace LightRail.HMI.SZLHLF.Service
{
    /// <summary>
    /// 站点管理服务
    /// </summary>
    public class StationManagerService : IService
    {

        private static IDictionary<int, IStation> m_AllStations;


        static StationManagerService()
        {
            m_AllStations = new Dictionary<int, IStation>();
        }

        public StationManagerService(IEnumerable<IStation> allStations)
        {
            m_AllStations = allStations.ToDictionary(t => t.Index, t => t);
        }

        /// <summary>
        /// 获取车站
        /// </summary>
        /// <param name="Index">编号</param>
        /// <returns></returns>
        public IStation GetStation(int index)
        {
            return m_AllStations.Values.FirstOrDefault(station => station.Index == index);
        }

        /// <summary>
        /// 获取车站
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public IStation GetStation(string name)
        {
            return m_AllStations.Values.FirstOrDefault(station => station.Name.Equals(name));
        }

        /// <summary>
        /// 获取所有车站
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IStation> GetAllStaions()
        {
            return m_AllStations.Values;
        }
    }
}
