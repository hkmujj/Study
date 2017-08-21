using System.Drawing;

namespace Engine.Turkmenistan.LKJ.Common
{
    public class ButtonStyle : IStyle
    {
        /// <summary>
        /// 读取或设置字体样式属性
        /// </summary>
        public FontStyleEs FontStyle { get; set; }

        /// <summary>
        /// 读取或设置控件响应鼠标左键按下时的背景图片属性
        /// </summary>
        public Image DownImage { get; set; }

        /// <summary>
        /// 读取或设置控件无效时的背景图片属性
        /// </summary>
        public Image DisableImage { get; set; }

        /// <summary>
        /// 读取或设置按钮背景图片属性
        /// </summary>
        public Image Background { get; set; }
    }
}