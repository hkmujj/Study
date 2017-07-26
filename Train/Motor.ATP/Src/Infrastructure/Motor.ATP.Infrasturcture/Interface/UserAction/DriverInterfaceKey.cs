using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 每一个状态的key
    /// </summary>
    public class DriverInterfaceKey
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(DriverInterfaceKey other)
        {
            return ToString() == other.ToString();
        }

        /// <summary>确定指定的 <see cref="T:System.Object" /> 是否等于当前的 <see cref="T:System.Object" />。</summary>
        /// <returns>如果指定的 <see cref="T:System.Object" /> 等于当前的 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
        /// <param name="obj">与当前的 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((DriverInterfaceKey)obj);
        }

        /// <summary>
        /// Root结点， 初始化状态
        /// </summary>
        public static readonly DriverInterfaceKey Root = new DriverInterfaceKey(new List<string>() { "Root" });

        /// <summary>
        /// 空
        /// </summary>
        public static readonly ReadOnlyCollection<string> EmptyPaths = Enumerable.Empty<string>().ToList().AsReadOnly();

        /// <summary>
        /// 路径集合
        /// </summary>
        public ReadOnlyCollection<string> Paths { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths"></param>
        public DriverInterfaceKey(IList<string> paths = null)
        {
            Paths = paths != null ? new ReadOnlyCollection<string>(paths) : EmptyPaths;
        }

        /// <summary>
        /// 解析一个key
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DriverInterfaceKey Parser(string content)
        {
            return string.IsNullOrWhiteSpace(content)
                ? new DriverInterfaceKey()
                : new DriverInterfaceKey(content.Split('-').ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Paths != null)
            {
                return string.Join("-", Paths);
            }
            return string.Empty;
        }

        /// <summary>用作特定类型的哈希函数。</summary>
        /// <returns>当前 <see cref="T:System.Object" /> 的哈希代码。</returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(DriverInterfaceKey a, DriverInterfaceKey b)
        {
            if (ReferenceEquals(null, a))
            {
                return false;
            }
            return a.Equals(b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(DriverInterfaceKey a, DriverInterfaceKey b)
        {
            return !(a == b);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DriverInterfaceKeyEqualityComparer : IEqualityComparer<DriverInterfaceKey>
    {
        /// <summary>确定指定的对象是否相等。</summary>
        /// <returns>如果指定的对象相等，则为 true；否则为 false。</returns>
        public bool Equals(DriverInterfaceKey x, DriverInterfaceKey y)
        {
            return x.ToString() == y.ToString();
        }

        /// <summary>返回指定对象的哈希代码。</summary>
        /// <returns>指定对象的哈希代码。</returns>
        /// <param name="obj">
        /// <see cref="T:System.Object" />，将为其返回哈希代码。</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="obj" /> 的类型为引用类型，<paramref name="obj" /> 为 null。</exception>
        public int GetHashCode(DriverInterfaceKey obj)
        {
            return obj.GetHashCode();
        }
    }
}