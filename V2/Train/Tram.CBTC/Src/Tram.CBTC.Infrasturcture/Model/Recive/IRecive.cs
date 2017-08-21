namespace Tram.CBTC.Infrasturcture.Model.Recive
{
    /// <summary>
    /// 适配层回掉View层方法
    /// </summary>
    public interface IRecive
    {
        /// <summary>
        /// 初始化线路号和站点列表
        /// </summary>
        /// <returns></returns>
        bool InitEndStationAndRoadID();
        /// <summary>
        /// 初始化可选执行计划，车次号，单程号
        /// </summary>
        /// <returns></returns>
        bool InitPlan();
    }
}
