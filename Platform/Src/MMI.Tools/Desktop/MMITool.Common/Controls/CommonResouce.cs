using System.Drawing;

namespace MMITool.Common.Controls
{
    /// <summary>
    /// 公共资源 
    /// </summary>
    public static class CommonResouce
    {
        /// <summary>
        /// //背景画刷 119, 136, 153
        /// </summary>
        public static readonly SolidBrush BackgroudBrush = new SolidBrush(Color.FromArgb(119, 136, 153));
        /// <summary>
        /// 绿色画刷 0, 255, 0
        /// </summary>
        public static readonly SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));//
        /// <summary>
        /// 红色画刷 255, 0, 0)
        /// </summary>
        public static readonly SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));//
        /// <summary>
        /// 蓝色画刷 0, 0, 255
        /// </summary>
        public static readonly SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255));//
        /// <summary>
        /// 黑色画刷 0, 0, 0
        /// </summary>
        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));//

        /// <summary>
        /// 白色画刷 new SolidBrush(Color.FromArgb(255, 255, 255));//
        /// </summary>
        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));//白色画刷

        /// <summary>
        /// 灰色画刷  121, 121, 121
        /// </summary>
        public static readonly SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(121, 121, 121));//
        /// <summary>
        /// 背景画笔
        /// </summary>
        public static readonly Pen BackgroudPen = new Pen(Color.FromArgb(209, 226, 242), 3);//

        /// <summary>
        /// 黑色画笔, 用于选择时的边框
        /// </summary>
        public static readonly Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 2);

        /// <summary>
        /// 选中时的边框
        /// </summary>
        public static readonly Pen SelectedPen = BlackPen;

        /// <summary>
        /// 粗条画笔
        /// </summary>
        public static readonly Pen LinePen = new Pen(Color.FromArgb(121, 121, 121), 3);//
        /// <summary>
        /// 细线条画笔
        /// </summary>
        public static readonly Pen SLinePen = new Pen(Color.FromArgb(121, 121, 121), 1);//细线条画笔
        /// <summary>
        /// 中线条画笔
        /// </summary>
        public static readonly Pen MLinePen = new Pen(Color.FromArgb(121, 121, 121), 2);//中线条画笔


        /// <summary>
        /// 默认字体 
        /// </summary>
        public static readonly Font DefalutFont = new Font("Arial", 11);
    }
}
