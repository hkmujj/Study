using System.Collections.Generic;
using System.ComponentModel;

namespace LightRail.HMI.GZYGDC.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnit : INotifyPropertyChanged
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        void DataChanged(IDictionary<int, bool> args);
    }
}