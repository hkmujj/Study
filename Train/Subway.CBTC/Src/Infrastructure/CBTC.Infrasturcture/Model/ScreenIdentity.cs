using System.Collections.Generic;
using System.Diagnostics;

namespace CBTC.Infrasturcture.Model
{
    /// <summary>
    /// 屏的标识符
    /// </summary>
    public class ScreenIdentity 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rectangle"></param>
        /// <param name="regionModel"></param>
        [DebuggerStepThrough]
        public ScreenIdentity(string name, Rect rectangle, RegionModel regionModel)
        {
            RegionModel = regionModel;
            Rectangle = rectangle;
            Name = name;
        }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { private set; get; }

        /// <summary>
        /// 位置,大小
        /// </summary>
        public Rect Rectangle { private set; get; }

        /// <summary>
        /// 区域信息
        /// </summary>
        public RegionModel RegionModel { private set; get; }

        /// <summary>返回表示当前 <see cref="T:System.Object" /> 的 <see cref="T:System.String" />。</summary>
        /// <returns>
        /// <see cref="T:System.String" />，表示当前的 <see cref="T:System.Object" />。</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>用作特定类型的哈希函数。</summary>
        /// <returns>当前 <see cref="T:System.Object" /> 的哈希代码。</returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        public static string NavigateParameterKey { get { return typeof(ScreenIdentity).Name; } }

        
    }

    /// <summary>
    /// 
    /// </summary>
    public class ScreenIdentityCompare : IEqualityComparer<ScreenIdentity>
    {
        /// <summary>确定指定的对象是否相等。</summary>
        /// <returns>如果指定的对象相等，则为 true；否则为 false。</returns>
        public bool Equals(ScreenIdentity x, ScreenIdentity y)
        {
            return x.GetHashCode() == y.GetHashCode();
        }

        /// <summary>返回指定对象的哈希代码。</summary>
        /// <returns>指定对象的哈希代码。</returns>
        /// <param name="obj">
        /// <see cref="T:System.Object" />，将为其返回哈希代码。</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="obj" /> 的类型为引用类型，<paramref name="obj" /> 为 null。</exception>
        public int GetHashCode(ScreenIdentity obj)
        {
            return obj.GetHashCode();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class Rect
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class RegionModel
    {
    }
}
