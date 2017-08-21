using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Models.ButtonModel
{
    /// <summary>
    /// 
    /// </summary>
    public class StateKey : IStateKey
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">Key名称</param>
        /// <param name="view">视图名称</param>
        public StateKey(string key, string view)
        {
            View = view;
            Key = key;
        }

        /// <summary>
        /// Key名称
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 视图名称
        /// </summary>
        public string View { get; }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>
        /// 当前 <see cref="T:Subway.ShenZhenLine9.Models.ButtonModel.StateKey"/> 的哈希代码。
        /// </returns>
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        /// <summary>
        /// 返回表示当前 <see cref="T:Subway.ShenZhenLine9.Models.ButtonModel.StateKey"/> 的 <see cref="T:System.String"/>。
        /// </summary>
        /// <returns>
        /// <see cref="T:System.String"/>，表示当前的 <see cref="T:Subway.ShenZhenLine9.Models.ButtonModel.StateKey"/>。
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

            return Equals(obj as IStateKey);
        }

        /// <summary>
        /// 确定指定的IStateKey实例等于当前的实例
        /// </summary>
        /// <param name="key">与当前的实例进行比较的IStateKey实例</param>
        /// <returns></returns>
        public bool Equals(IStateKey key)
        {
            return key.Key == (this.Key) && key.View == (this.View);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public static bool operator ==(StateKey key1, StateKey key2)
        {
            if (ReferenceEquals(null, key1) && ReferenceEquals(null, key2))
            {
                return true;
            }
            if (ReferenceEquals(null, key1) || ReferenceEquals(null, key2))
            {
                return false;
            }
            return key1.Equals(key2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public static bool operator !=(StateKey key1, StateKey key2)
        {
            return !(key1 == key2);
        }
    }
}