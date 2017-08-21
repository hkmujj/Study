using System;

namespace CommonUtil.Util
{
    /// <summary>
    /// EventArgs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventArgs<T> : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        public EventArgs(T arg)
        {
            Arg = arg;
        }

        /// <summary>
        /// 参数信息
        /// </summary>
        public T Arg { private set; get; }
    }
}
