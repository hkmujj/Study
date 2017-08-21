namespace Subway.WuHanLine6.Models.BtnModel
{
    /// <summary>
    ///
    /// </summary>
    public class StateKeys
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        public StateKeys(string key)
        {
            Key = key;
        }

        /// <summary>
        ///
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>
        /// 当前 <see cref="T:System.Object"/> 的哈希代码。
        /// </returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
        /// </summary>
        /// <returns>
        /// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
        /// </returns>
        public override string ToString()
        {
            return Key;
        }

        /// <summary>
        /// 确定指定的 <see cref="T:System.Object"/> 是否等于当前的 <see cref="T:System.Object"/>。
        /// </summary>
        /// <returns>
        /// 如果指定的 <see cref="T:System.Object"/> 等于当前的 <see cref="T:System.Object"/>，则为 true；否则为 false。
        /// </returns>
        /// <param name="obj">与当前的 <see cref="T:System.Object"/> 进行比较的 <see cref="T:System.Object"/>。</param>
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
            return Equals((StateKeys)obj);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(StateKeys other)
        {
            return ToString() == (other.ToString());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(StateKeys a, StateKeys b)
        {
            if (ReferenceEquals(null, a) && ReferenceEquals(null, b))
            {
                return true;
            }
            if (ReferenceEquals(null, a) || ReferenceEquals(null, b))
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
        public static bool operator !=(StateKeys a, StateKeys b)
        {
            return !(a == b);
        }
    }
}