using System;
using System.Drawing;
using ES.Facility.Common.Control.Common;

namespace ES.Facility.Common.Control
{
    /// <summary>
    /// 功能描述：TextBoxStyle
    /// 创建人：lih
    /// 创建时间：2014-07-15
    /// </summary>
    public class TextBoxStyle : IStyle
    {
        /// <summary>Group
        /// 读取或设置按钮背景图片属性
        /// </summary>
        public Image Background { get; set; }

        /// <summary>
        /// 读取或设置按钮获取焦点时的背景图片属性
        /// </summary>
        public Image BackgroundFocus { get; set; }

        /// <summary>
        /// 读取或设置字体样式属性
        /// </summary>
        public FontStyleEs FontStyle { get; set; }

        /// <summary>
        /// 背景画笔颜色
        /// </summary>
        public SolidBrush BackgroundBrushes { get; set; }


    }

    /// <summary>
    /// 功能描述：自定义TextBox
    /// 创建人：lih
    /// 创建时间：2014-07-15
    /// </summary>
    public class TextBox : IControl, IDisposable
    {
        private int m_CursorCount = 0;
        private Pen m_FramePen;
        
        #region 属性/变量
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
        /// 边框颜色
        /// </summary>
        public SolidBrush FrameBrushes
        {
            get { return m_FrameBrushes; }
            set 
            { 
                m_FrameBrushes = value;
                m_FramePen.Brush = m_FrameBrushes;
            }
        }
        private SolidBrush m_FrameBrushes = new SolidBrush(Color.Black);

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

        /// <summary>
        /// 添加或移除Text点击事件
        /// </summary>
        public event EventHandleClickEvent ClickEvent
        {
            add { m_ClickEvent += value; }
            remove { m_ClickEvent -= value; }
        }
        private EventHandleClickEvent m_ClickEvent;

        /// <summary>
        /// 读取或设置按钮使能开关属性
        /// </summary>
        public bool Enable
        {
            get { return m_Enable; }
            set
            {
                if (m_Enable == value)
                    return;

                m_Enable = value;
            }
        }
        private bool m_Enable = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="style"></param>
        /// <param name="id"></param>
        public TextBox(RectangleF rect, TextBoxStyle style, int id)
        {
            m_FramePen = new Pen(Brushes.Black, 1.6f);
            m_Rect = rect;
            m_Style = style;
            m_ID = id;
        }

        /// <summary>
        /// 鼠标按下处理函数（需在界面MouseDown函数中调用）
        /// </summary>
        /// <param name="p">点击点</param>
        public void MouseDown(Point p)
        {
            if (!m_Enable)
                return;

            if (m_Rect.Contains(p))
            {
                m_IsFocus = true;
                if (m_ClickEvent != null)
                    m_ClickEvent(this, new ClickEventArgs<int>(m_ID));
            }
        }

        /// <summary>
        /// TextBox控件绘制函数（需在界面Paint函数中调用）
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs)
        {
            if (((TextBoxStyle)Style).BackgroundBrushes != null)
            {
                dcGs.FillRectangle(((TextBoxStyle)Style).BackgroundBrushes, m_Rect);
            }

            if (m_IsFocus)
            {
                if (((TextBoxStyle)Style).BackgroundFocus != null)
                {
                    dcGs.DrawImage(((TextBoxStyle)Style).BackgroundFocus, m_Rect);
                }
                dcGs.DrawString(
                    m_Text + Cursor(),
                    ((TextBoxStyle)Style).FontStyle.Font,
                    ((TextBoxStyle)Style).FontStyle.TextBrush,
                    m_Rect,
                    ((TextBoxStyle)Style).FontStyle.StringFormat
                    );
            }
            else
            {
                if (((TextBoxStyle)Style).Background != null)
                    dcGs.DrawImage(((TextBoxStyle)Style).Background, m_Rect);
                dcGs.DrawString(
                    m_Text,
                    ((TextBoxStyle)Style).FontStyle.Font,
                    ((TextBoxStyle)Style).FontStyle.TextBrush,
                    m_Rect,
                    ((TextBoxStyle)Style).FontStyle.StringFormat
                    );
            }
        }

        /// <summary>
        /// 光标计数器方法
        /// </summary>
        /// <returns></returns>
        private string Cursor()
        {
            m_CursorCount++;
            if (m_CursorCount < 4)
            {
                m_CursorCount++;
                return "|";
            }
            else if (m_CursorCount >= 4 && m_CursorCount < 8)
            {
                m_CursorCount++;
                return "";
            }
            else
            {
                m_CursorCount = 0;
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }
    }
}
