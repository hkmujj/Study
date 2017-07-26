using System;
using System.Collections.ObjectModel;

namespace Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection
{
    /// <summary>
    /// 速度曲线
    /// </summary>
    public interface ISpeedCurve
    {
        /// <summary>
        /// 
        /// </summary>
        event EventHandler CurvePointCollectionChanged;

            /// <summary>
        /// 曲线点集合
        /// </summary>
        ObservableCollection<DistanceSpeedPoint> CurvePointCollection { get; }

        /// <summary>
        /// 通知 RaiseCurvePointCollectionChanged
        /// </summary>
        void RaiseCurvePointCollectionChanged();
    }
}