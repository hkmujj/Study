using System.Diagnostics;

namespace MMI.Facility.WPFInfrastructure.Model
{
    /// <summary>
    /// 数据变化模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataChangedModel<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        [DebuggerStepThrough]
        public DataChangedModel(T oldValue, T newValue)
        {
            NewValue = newValue;
            OldValue = oldValue;
        }

        /// <summary>
        /// 老的值
        /// </summary>
        public T OldValue { internal set; get; }

        /// <summary>
        /// 新的值
        /// </summary>
        public T NewValue { internal set; get; }
    }
}
