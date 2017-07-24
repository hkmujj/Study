using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data.TimeTable;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 时刻表服务
    /// </summary>
    public interface ITimeTableProviderService : IService
    {
        /// <summary>
        /// 时刻表字典
        /// </summary>
        IReadOnlyDictionary<int, ITimeTableItem> TimeTableItemsDictionary { get; }
        /// <summary>
        /// 时刻表改变
        /// </summary>
        event EventHandler TimeTableChanged;
    }
}
