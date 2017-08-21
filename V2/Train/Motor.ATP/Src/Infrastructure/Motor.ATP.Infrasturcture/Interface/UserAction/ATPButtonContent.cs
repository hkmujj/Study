using System;
using System.Diagnostics;

namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 按键内容
    /// </summary>
    public class ATPButtonContent 
    //public class ATPButtonContent : NotificationObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="style"></param>
        /// <param name="content"></param>
        /// <param name="imageSource"></param>
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
        public ATPButtonContentStyle Style { private set; get; }

        /// <summary>
        /// 内容
        /// </summary>
        public object Content { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public object ImageSource { private set; get; }
    }

    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        ImageText = Text | Image,

    }
}
