using System.Collections.ObjectModel;

namespace Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISpeedDialPlate
    {
        /// <summary>
        /// 
        /// </summary>
        ReadOnlyCollection<ISpeedDialPlateDegree> AllSpeedDegrees { get; }

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

    /// <summary>
    /// 
    /// </summary>
    public interface ISpeedDialPlateDegree
    {
        /// <summary>
        /// 速度 
        /// </summary>
        float Speed { get; }

        /// <summary>
        /// 角度, 与绘图角度相同
        /// </summary>
        float Angle { get; }

        /// <summary>
        /// 刻度长度
        /// </summary>
        float Lenght { get; }

        /// <summary>
        /// 显示内容
        /// </summary>
        string Text { get; }
    }
}