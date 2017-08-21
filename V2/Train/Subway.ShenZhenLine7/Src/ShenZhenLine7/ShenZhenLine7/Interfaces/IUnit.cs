using System.Collections.Generic;
using System.ComponentModel;

namespace Subway.ShenZhenLine7.Interfaces
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