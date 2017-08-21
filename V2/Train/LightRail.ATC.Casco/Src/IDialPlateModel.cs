using System.Collections.ObjectModel;

namespace LightRail.ATC.Casco
{
    public interface IDialPlateModel
    {
        ReadOnlyCollection<DialPlateDegree> VisibleDialPlateDegreeCollection { get; }

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