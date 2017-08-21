using System.Collections.Generic;
using System.ComponentModel;

namespace Motor.HMI.CRH380D.Interfaces
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