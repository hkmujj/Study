using System.Collections.ObjectModel;

namespace Engine.TPX21F.HXN5B.Model.Interface
{
    public interface IValueDialPlate
    {
        ReadOnlyCollection<IValueDialPlateDegree> AllValueDegrees { get; }

        /// <summary>
        /// 转换到表盘角度
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        float ConvertValueToAngle(float value);

        /// <summary>
        /// 转换到画圆孤角度
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        float ConvertValueToDrawArcAngle(float value);
    }
}