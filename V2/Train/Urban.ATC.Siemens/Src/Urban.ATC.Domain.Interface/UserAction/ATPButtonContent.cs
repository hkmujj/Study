using System;
using System.Diagnostics;


namespace Motor.ATP.Domain.Interface.UserAction
{
    /// <summary>
    /// 按键内容
    /// </summary>
    public class ATPButtonContent 
    //public class ATPButtonContent : NotificationObject
    {
        private ATPButtonContentStyle m_Style;
        private object m_Content;
        private object m_ImageSource;

        [DebuggerStepThrough]
        public ATPButtonContent(ATPButtonContentStyle style = ATPButtonContentStyle.Text, object content = null, object imageSource = null)
        {
            ImageSource = imageSource;
            Style = style;
            Content = content;
        }

        /// <summary>
        /// 样式
        /// </summary>
        public ATPButtonContentStyle Style
        {
            private set { m_Style = value;  }
            get { return m_Style; }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public object Content
        {
            private set { m_Content = value; }
            get { return m_Content; }
        }

        public object ImageSource
        {
            private set { m_ImageSource = value; }
            get { return m_ImageSource; }
        }
    }

    [Flags]
    public enum ATPButtonContentStyle
    {
        /// <summary>
        /// 未设置
        /// </summary>
        None = 0,

        /// <summary>
        /// 显示文本
        /// </summary>
        Text = 1,

        /// <summary>
        /// 返回, 画叉
        /// </summary>
        Cross = 2,

        /// <summary>
        /// 图片
        /// </summary>
        Image = 4,

        ImageText = Text | Image,

    }
}
