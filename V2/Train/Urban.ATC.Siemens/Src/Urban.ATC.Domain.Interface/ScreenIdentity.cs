using System.Collections.Generic;
using System.Diagnostics;


namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 屏的标识符
    /// </summary>
    public class ScreenIdentity 
    {
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

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static string NavigateParameterKey { get { return typeof(ScreenIdentity).Name; } }

        
    }

    public class ScreenIdentityCompare : IEqualityComparer<ScreenIdentity>
    {
        public bool Equals(ScreenIdentity x, ScreenIdentity y)
        {
            return x.GetHashCode() == y.GetHashCode();
        }

        public int GetHashCode(ScreenIdentity obj)
        {
            return obj.GetHashCode();
        }
    }


    public class Rect
    {
    }

    public class RegionModel
    {
    }
}
