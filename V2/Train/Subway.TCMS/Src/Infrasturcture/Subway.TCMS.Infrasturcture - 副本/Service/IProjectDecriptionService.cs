using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;
using Subway.TCMS.Infrasturcture.Model.Constance;

namespace Subway.TCMS.Infrasturcture.Service
{
    public interface IProjectDecriptionService : IService
    {
        /// <summary>
        /// 车辆屏类型
        /// </summary>
        TCMSType Type { get; }
        /// <summary>
        /// 输入Bool接口描述字典
        /// </summary>
        IReadOnlyDictionary<string, int> InBoolDictionary { get; }
        /// <summary>
        /// 输入Float接口描述字典
        /// </summary>
        IReadOnlyDictionary<string, int> InFloatDictionary { get; }
        /// <summary>
        /// 输出Bool接口描述字典
        /// </summary>
        IReadOnlyDictionary<string, int> OutBoolDictionary { get; }
        /// <summary>
        /// 输出Float接口描述字典
        /// </summary>
        IReadOnlyDictionary<string, int> OutFloatDictionary { get; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config"></param>
        void Initaliza(IProjectIndexDescriptionConfig config);
    }
}
