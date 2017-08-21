using System.Collections.ObjectModel;

namespace Tram.CBTC.Infrasturcture.Model.Signal.Speed
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISpeedDialPlate
    {
        /// <summary>
        /// 最小值
        /// </summary>
        SpeedDialPlateDegree MinDegree { get; }

        /// <summary>
        /// 最大值
        /// </summary>
        SpeedDialPlateDegree MaxDegree { get; }

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyCollection<SpeedDialPlateDegree> AllSpeedDegrees { get; }

        /// <summary>
        /// 转换到表盘角度
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        float ConvertSpeedToAngle(float speed);

        /// <summary>
        /// 转换到画圆孤角度
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        float ConvertSpeedToDrawArcAngle(float speed);
    }
}