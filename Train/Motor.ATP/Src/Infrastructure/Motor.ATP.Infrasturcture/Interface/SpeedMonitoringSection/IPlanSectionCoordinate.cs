using System.Collections.ObjectModel;
using System.Drawing;

namespace Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection
{
    /// <summary>
    /// 计划区坐标系。支持深copy 
    /// </summary>
    public interface IPlanSectionCoordinate 
    {
        /// <summary>
        /// X 轴集合
        /// </summary>
        ReadOnlyCollection<ICoordinateAxis>  XAxises { get; }

        /// <summary>
        /// Y 轴集合
        /// </summary>
        ReadOnlyCollection<ICoordinateAxis> YAxises { get; }

        /// <summary>
        /// 
        /// </summary>
        float MaxSpeedValue { get; }

        /// <summary>
        /// 
        /// </summary>
        float MaxDistanceValue { get; }

        /// <summary>
        /// 是否能放大
        /// </summary>
        bool CanZoomIn { get; }

        /// <summary>
        /// 是否能缩小
        /// </summary>
        bool CanZoomOut { get; }

        /// <summary>
        /// 获得距离的刻度
        /// </summary>
        /// <param name="distanceValue">距离, 单位1</param>
        /// <returns></returns>
        float GetDistanceScal(double distanceValue);

        /// <summary>
        /// 获得速度的刻度
        /// </summary>
        /// <param name="speedValue"></param>
        /// <returns></returns>
        float GetSpeedScal(double speedValue);

        /// <summary>
        /// 根据值计算出点
        /// </summary>
        /// <param name="distanceSpeedPoint"></param>
        /// <returns></returns>
        PointF GetPointF(DistanceSpeedPoint distanceSpeedPoint);

        /// <summary>
        /// 根据点计算出值
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        DistanceSpeedPoint GetDistanceSpeedPoint(PointF point);
    }
}
