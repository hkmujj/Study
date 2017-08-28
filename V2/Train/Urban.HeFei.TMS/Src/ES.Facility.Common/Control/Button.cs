﻿using System;
using System.Drawing;
using ES.Facility.Common.Control.Common;

namespace ES.Facility.Common.Control
{
    /// <summary>
    /// 功能描述：按钮Style
    /// 创建人：lih
    /// 创建时间：2014-07-14
    /// </summary>
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

    /// <summary>
    /// 功能描述：自定义按钮
    /// 创建人：lih
    /// 创建时间：2014-07-15
    /// </summary>
    public class Button : IControl, IDisposable
    {
        private bool m_SelfReplication;//按钮自复标识

        #region 事件/代理
        /// <summary>
        /// 添加或移除按钮点击事件响应函数属性
        /// </summary>
        public event EventHandleClickEvent ClickEvent
        {
            add { m_ClickEvent += value; }
            remove { m_ClickEvent -= value; }
        }
        private EventHandleClickEvent m_ClickEvent;

        public event EventHandleClickEvent MouseDownEvent
        {
            add { m_MouseDownEvent += value; }
            remove { m_MouseDownEvent -= value; }
        }
        private EventHandleClickEvent m_MouseDownEvent;
        #endregion

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
        /// 读取按钮标识ID属性
        /// </summary>
        public int ID
        {
            get { return m_ID; }
        }
        private int m_ID = -1;
        /// <summary>
        /// 设置按钮ID
        /// </summary>
        /// <param name="id"></param>
        public void SetID(int id)
        {
            m_ID = id;
        }
        /// <summary>
        /// 读取按钮Style属性（使用时需转换为ButtonStyle）
        /// </summary>
        public IStyle Style
        {
            get { return m_Style; }
            set { m_Style = value; }
        }
        private IStyle m_Style;

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

        /// <summary>
        /// 读取或设置是否复位属性（按钮是否弹起，用户程序使用该属性实现按钮的复位）
        /// </summary>
        public bool IsReplication
        {
            get { return m_IsReplication; }
            set
            {
                if (m_IsReplication == value)
                    return;

                m_IsReplication = value;
            }
        }
        private bool m_IsReplication = true;

        private SolidBrush m_TempdownTextBrush;
        private SolidBrush m_TempupTextBrush;

        private Font m_TextFont;

        private RectangleF m_TextRect;
        private StringFormat m_TextSf;

        private Image m_ButtonDownImage;
        private SolidBrush m_EnableSolibrush = new SolidBrush(Color.FromArgb(150, 150, 150));
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数：根据按钮文本信息、所在矩形区域、按钮标识、按钮Style、自复标识等信息构造按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="rect">按钮矩形区域</param>
        /// <param name="id">按钮ID</param>
        /// <param name="style">按钮样式</param>
        /// <param name="selfReplication">自复标识，默认为自复按钮</param>
        public Button(string text, RectangleF rect, int id, ButtonStyle style, bool selfReplication = true)
        {
            m_Text = text;
            m_Rect = rect;
            m_ID = id;
            if (style.DisableImage == null) style.DisableImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"/Resource/Btn_Disable.png");
            m_Style = style;
            m_SelfReplication = selfReplication;

            m_TempdownTextBrush = (((ButtonStyle)m_Style).FontStyle.TextDownBrush != null) ? ((ButtonStyle)m_Style).FontStyle.TextDownBrush : ((ButtonStyle)m_Style).FontStyle.TextBrush;
            m_TempupTextBrush = (((ButtonStyle)m_Style).FontStyle.TextUpBrush != null) ? ((ButtonStyle)m_Style).FontStyle.TextUpBrush : ((ButtonStyle)m_Style).FontStyle.TextBrush;


            m_TextFont = ((ButtonStyle)m_Style).FontStyle.Font;

            m_TextRect = new RectangleF(m_Rect.X, m_Rect.Y + 3, m_Rect.Width, m_Rect.Height);
            m_TextSf = ((ButtonStyle)m_Style).FontStyle.StringFormat;

            m_ButtonDownImage = ((ButtonStyle)m_Style).DownImage;
        }

        public static void Test()
        {
            var btn = new Button("", new RectangleF(), 1, new ButtonStyle());
        }
        #endregion

        #region 鼠标检测函数
        /// <summary>
        /// 鼠标按下功能函数（该函数需在界面检测鼠标按下函数中调用）
        /// </summary>
        /// <param name="p">光标所在坐标</param>
        public void MouseDown(Point p)
        {
            if (!m_Enable)//按钮不可用，函数返回
                return;

            if (m_Rect.Contains(p))//光标坐标在按钮矩形区域内，实现按钮按下功能
            {
                if (m_MouseDownEvent != null)
                    m_MouseDownEvent(this, new ClickEventArgs<int>(m_ID));
                m_IsFocus = true;//按钮获得焦点（后续可能会使用到）
                m_IsReplication = !m_IsReplication;//按钮复位属性取反
            }
        }

        /// <summary>
        /// 鼠标弹起功能函数（该函数需在界面检测鼠标弹起函数中调用）
        /// </summary>
        /// <param name="p">光标所在坐标</param>
        public void MouseUp(Point p)
        {
            if (!m_Enable)//按钮不可用，函数返回
                return;

            if (m_Rect.Contains(p))//光标坐标在按钮矩形区域内，实现按钮弹起功能
            {
                if (m_SelfReplication)//为自复按钮，并复位
                {
                    m_IsReplication = true;
                }

                if (m_ClickEvent != null)//触发按钮按下事件
                    m_ClickEvent(this, new ClickEventArgs<int>(m_ID));
            }
        }
        #endregion

        #region 按钮绘制函数
        /// <summary>
        /// 按钮绘制（需在界面绘制函数中实时调用）
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs)
        {
            if (!m_Enable)//按钮不能用，绘制灰色背景
            {
                dcGs.DrawImage(((ButtonStyle)m_Style).DisableImage, m_Rect);

                dcGs.DrawString(m_Text, ((ButtonStyle)m_Style).FontStyle.Font, m_EnableSolibrush, m_TextRect, m_TextSf);

                m_IsReplication = true;
                return;
            }


            if (!m_IsReplication)//按钮未复位，绘制按下图片背景
            {
                dcGs.DrawImage(((ButtonStyle)m_Style).DownImage, m_Rect);

                dcGs.DrawString(m_Text, ((ButtonStyle)m_Style).FontStyle.Font, m_TempdownTextBrush, m_TextRect, m_TextSf);//绘制按钮文本
            }
            else//按钮复位，绘制常规图片背景
            {
                dcGs.DrawImage(m_Style.Background, m_Rect);
                dcGs.DrawString(m_Text, ((ButtonStyle)m_Style).FontStyle.Font, m_TempupTextBrush, m_TextRect, m_TextSf);//绘制按钮文本
            }
        }
        #endregion

        #region IDisposable接口函数
        /// <summary>
        /// IDisposable接口函数：根据需要添加功能
        /// </summary>
        public void Dispose()
        {
            //按钮释放资源
        }
        #endregion
    }
}