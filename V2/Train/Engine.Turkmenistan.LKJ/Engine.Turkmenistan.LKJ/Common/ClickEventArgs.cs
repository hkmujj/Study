using System;

namespace Engine.Turkmenistan.LKJ.Common
{
    /// <summary>
    /// 功能描述：控件点击事件传输的参数类
    /// 创建人：唐林
    /// 创建时间：2014-07-14
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClickEventArgs<T> : EventArgs
    {
        #region 泛型属性
        /// <summary>
        /// 读取事件数据
        /// </summary>
        public T Message
        {
            get
            {
                return m_Message;
            }
        }
        private readonly T m_Message = default(T);
        #endregion

        #region 方法
        /// <summary>
        /// 空构造函数
        /// </summary>
        public ClickEventArgs()
        {
            //
        }

        /// <summary>
        /// 构造函数：获得事件数据状态
        /// </summary>
        /// <param name="message">数据</param>
        public ClickEventArgs(T message)
        {
            this.m_Message = message;
        }
        #endregion
    }
}