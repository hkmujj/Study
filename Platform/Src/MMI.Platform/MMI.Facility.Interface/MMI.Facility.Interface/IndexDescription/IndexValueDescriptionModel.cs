using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace MMI.Facility.Interface.IndexDescription
{
    /// <summary>
    /// IndexValueDescriptionModelProxy 代理值和索引， 新建描述
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class IndexValueDescriptionModelWrapper<TIndex, TValue> : IndexValueDescriptionModel<TIndex, TValue>
        where TIndex : IEquatable<TIndex>
    {
        private readonly IndexValueDescriptionModel<TIndex, TValue> m_ConcreateModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="concreateModel"></param>
        public IndexValueDescriptionModelWrapper(IndexValueDescriptionModel<TIndex, TValue> concreateModel)
        {
            if (concreateModel == null)
            {
                throw new ArgumentNullException();
            }
            m_ConcreateModel = concreateModel;

            concreateModel.PropertyChanged += ConcreateModelOnPropertyChanged;
        }

        private void ConcreateModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RaisePropertyChanged(propertyChangedEventArgs.PropertyName);
        }

        public override TValue Value
        {
            get { return m_ConcreateModel.Value; }
            set
            {
                if (Equals(value, m_ConcreateModel.Value))
                {
                    return;
                }
                m_ConcreateModel.Value = value;
                RaisePropertyChanged(ValuePropertyName);
            }
        }


        public override TIndex Index
        {
            get { return m_ConcreateModel.Index; }
            set
            {
                if (Equals(value, m_ConcreateModel.Index))
                {
                    return;
                }
                m_ConcreateModel.Index = value;
                RaisePropertyChanged("Index");
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TIndex"></typeparam>
    [DebuggerDisplay("Index={Index}, Value={Value}, Description={Description}")]
    public class IndexValueDescriptionModel<TIndex, TValue> : INotifyPropertyChanged, IIndexDescriptionProvider<TIndex>,
        IEqualityComparer<IndexValueDescriptionModel<TIndex, TValue>> where TIndex : IEquatable<TIndex>
    {
        private string m_Description;
        private TIndex m_Index;
        private TValue m_Value;


        /// <summary>
        /// 
        /// </summary>
        public virtual string Description
        {
            set
            {
                if (value == m_Description)
                {
                    return;
                }
                m_Description = value;
                RaisePropertyChanged("Description");
            }
            get { return m_Description; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual TIndex Index
        {
            set
            {
                if (Equals(value, m_Index))
                {
                    return;
                }
                m_Index = value;
                RaisePropertyChanged("Index");
            }
            get { return m_Index; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual TValue Value
        {
            set
            {
                if (Equals(value, m_Value))
                {
                    return;
                }
                m_Value = value;
                RaisePropertyChanged(ValuePropertyName);
            }
            get { return m_Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ValuePropertyName
        {
            get { return "Value"; }
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
            var other = obj as IndexValueDescriptionModel<TIndex, TValue>;
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
        public bool Equals(IndexValueDescriptionModel<TIndex, TValue> x, IndexValueDescriptionModel<TIndex, TValue> y)
        {
            return y.Index != null && (x.Index != null && x.Index.Equals(y.Index));
        }

        /// <summary>
        /// 返回指定对象的哈希代码。
        /// </summary>
        /// <returns>
        /// 指定对象的哈希代码。
        /// </returns>
        /// <param name="obj"><see cref="T:System.Object"/>，将为其返回哈希代码。</param><exception cref="T:System.ArgumentNullException"><paramref name="obj"/> 的类型为引用类型，<paramref name="obj"/> 为 null。</exception>
        public int GetHashCode(IndexValueDescriptionModel<TIndex, TValue> obj)
        {
            return obj.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}