
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;

namespace Motor.ATP.Domain.Interface.Service
{

    public interface IInterfaceAdapterService : IService
    {
        /// <summary>
        /// 输入bool
        /// </summary>
        IReadOnlyDictionary<string, int> InBoolDictionary { get; }

        /// <summary>
        /// 输入float
        /// </summary>
        IReadOnlyDictionary<string, int> InFloatDictionary { get; }

        /// <summary>
        /// 输出bool
        /// </summary>
        IReadOnlyDictionary<string, int> OutBoolDictionary { get; }

        /// <summary>
        /// 输出float
        /// </summary>
        IReadOnlyDictionary<string, int> OutFloatDictionary { get; }
    }
}