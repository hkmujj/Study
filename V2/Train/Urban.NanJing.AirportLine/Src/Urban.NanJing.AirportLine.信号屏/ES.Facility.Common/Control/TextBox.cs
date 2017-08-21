using System;
using System.Drawing;
using ES.Facility.Common.Control.Common;

namespace ES.Facility.Common.Control
{
    /// <summary>
    /// 功能描述：TextBoxStyle
    /// 创建人：唐林
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
    }

    /// <summary>
    /// 功能描述：自定义TextBox
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    public class TextBox : IControl, IDisposable
    {
        private Int32 m_CursorCount = 0;

        #region 属性/变量
        /// <summary>
        /// 读取或设置按钮文本属性
        /// </summary>
        public String Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
        private String m_Text = String.Empty;

        /// <summary>
        /// 读取按钮标识ID属性
        /// </summary>
        public Int32 ID
        {
            get { return m_ID; }
        }
        private Int32 m_ID = -1;

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
        public Boolean IsFocus
        {
            get { return m_IsFocus; }
            set
            {
                if (m_IsFocus == value)
                    return;

                m_IsFocus = value;
            }
        }
        private Boolean m_IsFocus = false;
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
        public Boolean Enable
        {
            get { return m_Enable; }
            set
            {
                if (m_Enable == value)
                    return;

                m_Enable = value;
            }
        }
        private Boolean m_Enable = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="style"></param>
        /// <param name="id"></param>
        public TextBox(RectangleF rect, TextBoxStyle style, Int32 id)
        {
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
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            if (m_IsFocus)
            {
                g.DrawImage(((TextBoxStyle)Style).BackgroundFocus, m_Rect);
                g.DrawString(
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
                    g.DrawImage(((TextBoxStyle)Style).Background, m_Rect);
                g.DrawString(
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
