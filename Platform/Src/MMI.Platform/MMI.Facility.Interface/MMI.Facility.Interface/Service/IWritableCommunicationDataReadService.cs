using System.Collections.Generic;
using MMI.Facility.Interface.IndexDescription;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 测试用的通信
    /// </summary>
    public interface IWritableCommunicationDataReadService : ICommunicationDataReadService, ICommunicationDataWriteService
    {
        /// <summary>
        /// 是否可写
        /// </summary>
        bool IsReadonly { get; }

        /// <summary>
        /// 
        /// </summary>
        IList<IndexValueDescriptionModel<int, bool>> BoolList { get; }


        /// <summary>
        /// 
        /// </summary>
        IList<IndexValueDescriptionModel<int, float>> FloatList { get; }
    }
}