using ES.JCTMS.Common.Control.Common;
using System.Collections.Generic;
using System.Drawing;

namespace ES.JCTMS.Common.Control
{
    /// <summary>
    ///     功能描述：表格头
    ///     创建人：lih
    ///     创建时间：2014-07-22
    /// </summary>
    public struct GridHeader
    {
        /// <summary>
        ///     读取或设置Header文本属性
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     读取或设置Header文本字体属性
        /// </summary>
        public Font HeaderFont { get; set; }

        /// <summary>
        ///     读取或设置Header对应列数据文本字体属性
        /// </summary>
        public Font DataFont { get; set; }

        /// <summary>
        ///     读取或设置Header文本画刷属性
        /// </summary>
        public Brush TextBrush { get; set; }

        /// <summary>
        ///     读取或设置Header背景画刷属性
        /// </summary>
        public Brush BackgroundBrush { get; set; }

        /// <summary>
        ///     读取或设置Header宽度属性
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        ///     读取或设置Header高度属性
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        ///     读取或设置Header对应列显示列表对象的属性名称属性
        /// </summary>
        public string TProperty { get; set; }

        /// <summary>
        ///     读取或设置Header文本对齐方式属性
        /// </summary>
        public StringFormat SF_Header { get; set; }

        /// <summary>
        ///     读取或设置Header对应列数据文本对齐属性
        /// </summary>
        public StringFormat SF_Data { get; set; }
    }

    public class Grid<T>
    {
        private List<T> _currentDataList = new List<T>();
        private ListBoxHeader[] _headers;
        private int currentPage = 0;

        /// <summary>
        ///     读取或设置按钮文本属性
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        ///     读取按钮标识ID属性
        /// </summary>
        public int ID { get; } = -1;

        /// <summary>
        ///     读取按钮Style属性（使用时需转换为ButtonStyle）
        /// </summary>
        public IStyle Style { get; }

        /// <summary>
        ///     读取或设置按钮所在矩形区域属性
        /// </summary>
        public RectangleF Rect
        {
            get { return _rect; }
            set
            {
                if (_rect == value)
                {
                    return;
                }

                _rect = value;
            }
        }

        private RectangleF _rect;

        /// <summary>
        ///     读取或设置是否获得焦点属性（功能后续添加）
        /// </summary>
        public bool IsFocus
        {
            get { return _isFocus; }
            set
            {
                if (_isFocus == value)
                {
                    return;
                }

                _isFocus = value;
            }
        }

        private bool _isFocus;
    }
}