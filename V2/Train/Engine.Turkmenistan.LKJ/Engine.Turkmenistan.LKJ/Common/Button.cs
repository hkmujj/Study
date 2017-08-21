using System;
using System.Drawing;

namespace Engine.Turkmenistan.LKJ.Common
{
    /// <summary>
    /// 功能描述：自定义按钮
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    public class Button : IControl, IDisposable
    {
        private readonly Boolean m_SelfReplication;//按钮自复标识
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

        public event EventHandleClickEvent DownEvent
        {
            add { m_DownEvent += value; }
            remove { m_DownEvent -= value; }
        }
        private EventHandleClickEvent m_DownEvent = null;
        #endregion

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
        private readonly Int32 m_ID = -1;

        /// <summary>
        /// 读取按钮Style属性（使用时需转换为ButtonStyle）
        /// </summary>
        public IStyle Style
        {
            get { return m_Style; }
        }
        private readonly IStyle m_Style;

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

        /// <summary>
        /// 读取或设置是否复位属性（按钮是否弹起，用户程序使用该属性实现按钮的复位）
        /// </summary>
        public Boolean IsReplication
        {
            get { return m_IsReplication; }
            set
            {
                if (m_IsReplication == value)
                    return;

                m_IsReplication = value;
            }
        }
        private Boolean m_IsReplication = true;
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
        public Button(String text, RectangleF rect, Int32 id, ButtonStyle style, Boolean selfReplication = true)
        {
            m_Text = text;
            m_Rect = rect;
            m_ID = id;
            m_Style = style;
            m_SelfReplication = selfReplication;
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
                if (m_DownEvent != null)
                    m_DownEvent(this, new ClickEventArgs<int>(m_ID));

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

                if (m_ClickEvent != null)//触发按钮按下事件
                    m_ClickEvent(this, new ClickEventArgs<int>(m_ID));
                if (m_SelfReplication)//为自复按钮，并复位
                {
                    m_IsReplication = true;
                }
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

                if (((ButtonStyle)m_Style).DisableImage == null)
                    ((ButtonStyle)m_Style).DisableImage = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/Resource/Btn_Disable.png");

                dcGs.DrawImage(((ButtonStyle)m_Style).DisableImage, m_Rect);
                dcGs.DrawString(
                    m_Text,
                    ((ButtonStyle)m_Style).FontStyle.Font,
                    new SolidBrush(Color.FromArgb(150, 150, 150)),
                    new RectangleF(m_Rect.X, m_Rect.Y + 3, m_Rect.Width, m_Rect.Height),
                    ((ButtonStyle)m_Style).FontStyle.StringFormat
                );
                return;
            }

            if (!m_IsReplication)//按钮未复位，绘制按下图片背景
            {
                if (((ButtonStyle)m_Style).DownImage != null)
                    dcGs.DrawImage(((ButtonStyle)m_Style).DownImage, m_Rect);
            }
            else//按钮复位，绘制常规图片背景
            {
                if (m_Style.Background != null)
                    dcGs.DrawImage(m_Style.Background, m_Rect);
            }
            dcGs.DrawString(
                m_Text,
                ((ButtonStyle)m_Style).FontStyle.Font,
                ((ButtonStyle)m_Style).FontStyle.TextBrush,
                new RectangleF(m_Rect.X, m_Rect.Y + 3, m_Rect.Width, m_Rect.Height),
                ((ButtonStyle)m_Style).FontStyle.StringFormat
            );//绘制按钮文本
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