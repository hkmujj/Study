using System.Collections.Generic;
using Microsoft.Practices.Prism.ViewModel;

namespace MMI.Facility.WPFInfrastructure.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IndexValueModel<T> : NotificationObject, IEqualityComparer<IndexValueModel<T>>
    {
        private uint m_Index;
        private T m_Value;

        /// <summary>
        /// 
        /// </summary>
        public uint Index
        {
            set
            {
                m_Index = value;
                RaisePropertyChanged(() => Index);
            }
            get { return m_Index; }
        }

        /// <summary>
        /// 
        /// </summary>
        public T Value
        {
            set
            {
                m_Value = value;
                RaisePropertyChanged(() => Value);
            }
            get { return m_Value; }
        }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>
        /// 当前 <see cref="T:System.Object"/> 的哈希代码。
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return Index.GetHashCode();
        }

        /// <summary>
        /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
        /// </summary>
        /// <returns>
        /// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("Indx = {0}, Value={1}", Index, Value);
        }

        /// <summary>
        /// 确定指定的 <see cref="T:System.Object"/> 是否等于当前的 <see cref="T:System.Object"/>。
        /// </summary>
        /// <returns>
        /// 如果指定的 <see cref="T:System.Object"/> 等于当前的 <see cref="T:System.Object"/>，则为 true；否则为 false。
        /// </returns>
        /// <param name="obj">与当前的 <see cref="T:System.Object"/> 进行比较的 <see cref="T:System.Object"/>。</param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            var other = obj as IndexValueModel<T>;
            return other != null && Index.Equals(other.Index);
        }

        /// <summary>
        /// 确定指定的对象是否相等。
        /// </summary>
        /// <returns>
        /// 如果指定的对象相等，则为 true；否则为 false。
        /// </returns>
        /// <param name="x">要比较的第一个类型为 <paramref>
        ///         <name>T</name>
        ///     </paramref>
        ///     的对象。</param><param name="y">要比较的第二个类型为 <paramref>
        ///             <name>T</name>
        ///         </paramref>
        ///         的对象。</param>
        public bool Equals(IndexValueModel<T> x, IndexValueModel<T> y)
        {
            return x.Index == y.Index;
        }

        /// <summary>
        /// 返回指定对象的哈希代码。
        /// </summary>
        /// <returns>
        /// 指定对象的哈希代码。
        /// </returns>
        /// <param name="obj"><see cref="T:System.Object"/>，将为其返回哈希代码。</param><exception cref="T:System.ArgumentNullException"><paramref name="obj"/> 的类型为引用类型，<paramref name="obj"/> 为 null。</exception>
        public int GetHashCode(IndexValueModel<T> obj)
        {
            return obj.GetHashCode();
        }
    }
}
