using System;
using System.Collections.Generic;
using System.Drawing;
using ES.Facility.Common.Control.Common;

namespace ES.Facility.Common.Control
{
    /// <summary>
    /// 功能描述：表格头
    /// 创建人：lih
    /// 创建时间：2014-07-22
    /// </summary>
    public struct GridHeader
    {
        /// <summary>
        /// 读取或设置Header文本属性
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 读取或设置Header文本字体属性
        /// </summary>
        public Font HeaderFont { get; set; }
        /// <summary>
        /// 读取或设置Header对应列数据文本字体属性
        /// </summary>
        public Font DataFont { get; set; }
        /// <summary>
        /// 读取或设置Header文本画刷属性
        /// </summary>
        public Brush TextBrush { get; set; }
        /// <summary>
        /// 读取或设置Header背景画刷属性
        /// </summary>
        public Brush BackgroundBrush { get; set; }
        /// <summary>
        /// 读取或设置Header宽度属性
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// 读取或设置Header高度属性
        /// </summary>
        public float Height { get; set; }
        /// <summary>
        /// 读取或设置Header对应列显示列表对象的属性名称属性
        /// </summary>
        public string Property { get; set; }
        /// <summary>
        /// 读取或设置Header文本对齐方式属性
        /// </summary>
        public StringFormat SfHeader { get; set; }
        /// <summary>
        /// 读取或设置Header对应列数据文本对齐属性
        /// </summary>
        public StringFormat SfData { get; set; }
    }

    public class Grid<T> 
    {
        private ListBoxHeader[] m_Headers;
        private int m_CurrentPage = 0;
        private List<T> m_CurrentDataList = new List<T>();

        #region IControl接口属性
        /// <summary>
        /// 读取或设置按钮文本属性
        /// </summary>
        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
        private string m_Text = string.Empty;

        /// <summary>
        /// 读取按钮标识ID属性
        /// </summary>
        public int ID
        {
            get { return m_ID; }
        }
        private int m_ID = -1;

        /// <summary>
        /// 读取按钮Style属性（使用时需转换为ButtonStyle）
        /// </summary>
        public IStyle Style
        {
            get { return m_Style; }
        }
        private IStyle m_Style;

        /// <summary>
        /// 读取或设置按钮所在矩形区域属性
        /// </summary>
        public RectangleF Rect
        {
            get { return m_Rect; }
            set
            {
                if (m_Rect == value)
                    return;

                m_Rect = value;
            }
        }
        private RectangleF m_Rect;

        /// <summary>
        /// 读取或设置是否获得焦点属性（功能后续添加）
        /// </summary>
        public bool IsFocus
        {
            get { return m_IsFocus; }
            set
            {
                if (m_IsFocus == value)
                    return;

                m_IsFocus = value;
            }
        }
        private bool m_IsFocus = false;
        #endregion
    }
}
