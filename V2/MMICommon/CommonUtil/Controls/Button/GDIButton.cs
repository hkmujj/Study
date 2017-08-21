using System;
using System.Diagnostics;
using System.Drawing;
using CommonUtil.Util;

namespace CommonUtil.Controls.Button
{
    /// <summary>
    /// 类似 Button 的控件 
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Text = {Text}")]
    public class GDIButton : CommonInnerControlBase
    {
        /// <summary>
        /// 鼠标按下的事件
        /// </summary>
        [NonSerialized] public EventHandler ButtonDownEvent;

        /// <summary>
        /// 鼠标起的事件
        /// </summary>
        [NonSerialized] public EventHandler ButtonUpEvent;

        /// <summary>
        /// TODO 鼠标点击事件
        /// </summary>
        [NonSerialized] public EventHandler ButtonClickEvent;

        /// <summary>
        /// CurrentMouseStateChanged;
        /// </summary>
        [NonSerialized] public EventHandler CurrentMouseStateChanged;

        /// <summary>
        /// 当前按键区域的鼠标状态[DefaultValue(MouseState.MouseUp)]
        /// </summary>
        public virtual MouseState CurrentMouseState
        {
            set
            {
                if (value == m_CurrentMouseState)
                {
                    return;
                }
                m_CurrentMouseState = value;
                HandleUtil.OnHandle(CurrentMouseStateChanged, this, EventArgs.Empty);
            }
            get { return m_CurrentMouseState; }
        }

        /// <summary>
        /// 按键内容的文本控件
        /// </summary>
        public GDIRectText ContentTextControl
        {
            get { return m_ButtonText; }
        }

        /// <summary>
        /// 是否可用[DefaultValue(true)]
        /// </summary>
        public override bool IsEnable
        {
            get { return m_IsEnable; }
            set
            {
                m_IsEnable = value;
                if (m_ButtonText != null)
                {
                    m_ButtonText.TextColor = value ? TextColor : Color.Gray;
                }
            }
        }

        /// <summary>
        /// 按键上的文本
        /// </summary>
        public virtual string Text
        {
            set { m_ButtonText.Text = value; }
            get { return m_ButtonText.Text; }
        }

        private Color m_TextColor;

        /// <summary>
        /// 控件的字体颜色
        /// </summary>
        public virtual Color TextColor
        {
            set
            {
                m_ButtonText.TextColor = value;
                m_TextColor = value;
            }
            get { return m_TextColor; }
        }

        private GDIRectText m_ButtonText = new GDIRectText();

        /// <summary>
        /// 
        /// </summary>
        public GDIRectText TextControl
        {
            get { return m_ButtonText; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual CommonInnerControlBase InnnerControl
        {
            get { return m_ButtonText; }
            private set { m_ButtonText = (GDIRectText) value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Padding Padding
        {
            set
            {

                if (value != m_Padding)
                {
                    if (m_Padding != null)
                    {
                        m_Padding.PaddingChanged -= OnPaddingChanged;
                    }
                    m_Padding = value;
                    if (value == null)
                    {
                        m_Padding = Padding.Empty;
                    }
                    m_Padding.PaddingChanged += OnPaddingChanged;
                    OnPaddingChanged(m_Padding);
                }
            }
            get { return m_Padding; }
        }

        private void OnPaddingChanged(Padding padding)
        {
            ChangeInnerControlRegion(padding);
        }

        private void ChangeInnerControlRegion(Padding padding)
        {
            if (m_ButtonText != null)
            {
                var x = OutLineRectangle.X;
                var y = OutLineRectangle.Y;
                var weight = OutLineRectangle.Width;
                var height = OutLineRectangle.Height;
                m_ButtonText.OutLineRectangle = new Rectangle(x + padding.Left, y + padding.Top,
                    weight - padding.Horizontal,
                    height - padding.Vertical);
            }
        }

        private readonly Point[] m_Curpoint = new Point[4];
        private readonly Pen m_Darkpen = new Pen(Color.FromArgb(104, 112, 125), 3);
        private readonly Pen m_Linepen = new Pen(Color.FromArgb(221, 228, 234), 3);
        private Image m_Bgkimage;

        /// <summary>
        /// 背景图片
        /// </summary>
        public Image BackImage
        {
            get { return m_Bgkimage; }
            set { m_Bgkimage = value; }
        }

        private bool m_IsEnable;
        private IBtnBehavierStrategy m_BtnBehavierStrategy;
        private Padding m_Padding;
        private MouseState m_CurrentMouseState;

        /// <summary>
        /// 按键的使用策略
        /// </summary>
        public IBtnBehavierStrategy BtnBehavierStrategy
        {
            get { return m_BtnBehavierStrategy; }
            set { m_BtnBehavierStrategy = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public GDIButton()
        {
            Padding = new Padding(3);
            this.OutLineChanged =
                (sender, args) =>
                {
                    var x = OutLineRectangle.X;
                    var y = OutLineRectangle.Y;
                    var weight = OutLineRectangle.Width;
                    var height = OutLineRectangle.Height;
                    ChangeInnerControlRegion(m_Padding);
                    m_Curpoint[0].X = x;
                    m_Curpoint[0].Y = y + 1;
                    m_Curpoint[1].X = x;
                    m_Curpoint[1].Y = y + height - 1;
                    m_Curpoint[2].X = x + weight;
                    m_Curpoint[2].Y = y + 1;
                    m_Curpoint[3].X = x + weight;
                    m_Curpoint[3].Y = y + height - 1;
                };
            CurrentMouseState = MouseState.MouseUp;
            Visible = true;
            IsEnable = true;
            TextColor = Color.Black;
            m_BtnBehavierStrategy = new BtnBehavierNormalStrategy(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        public void SetButtonRect(int x, int y, int weight, int height)
        {
            OutLineRectangle = new Rectangle(x, y, weight, height);
            m_ButtonText.OutLineRectangle = new Rectangle(x + m_Padding.Left, y + m_Padding.Top,
                weight - m_Padding.Horizontal, height - m_Padding.Vertical);
            m_Curpoint[0].X = x;
            m_Curpoint[0].Y = y + 1;
            m_Curpoint[1].X = x;
            m_Curpoint[1].Y = y + height - 1;
            m_Curpoint[2].X = x + weight;
            m_Curpoint[2].Y = y + 1;
            m_Curpoint[3].X = x + weight;
            m_Curpoint[3].Y = y + height - 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public void SetButtonColor(int r, int g, int b)
        {
            m_ButtonText.SetBkColor(r, g, b);
            //m_EnableBtnBkColor = Color.FromArgb(r, g, b);
            m_ButtonText.SetTextColor(0, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        public void SetButtonText(String str)
        {
            m_ButtonText.SetTextStyle(12, FormatStyle.Center, true, "Arial");
            m_ButtonText.SetText(str);
        }

        /// <summary>
        /// 设置文本的格式 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="format"></param>
        /// <param name="bold"></param>
        /// <param name="font"></param>
        public void SetTextStyle(int size, int format, bool bold, String font)
        {
            m_ButtonText.SetTextStyle(size, format, bold, font);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textColor"></param>
        public void SetButtonTextColor(SolidBrush textColor)
        {
            //m_EnableTextColor = textColor.Color;
            m_ButtonText.SetTextColor(textColor);
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnButtonDown()
        {
            if (IsEnable)
            {
                CurrentMouseState = MouseState.MouseDown;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnButtonUp()
        {
            if (IsEnable)
            {
                CurrentMouseState = MouseState.MouseUp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picpath"></param>
        public void SetButtonPic(String picpath)
        {
            m_Bgkimage = Image.FromFile(picpath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        public void SetButtonPic(Image image)
        {
            m_Bgkimage = image;
        }


        /// <summary>
        /// 生成按钮的 Click 事件。
        /// </summary>
        [Obsolete("功能不完善")]
        public void PerformClick()
        {
            m_BtnBehavierStrategy = new BtnBehavierClickStrategy(this);
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            return m_BtnBehavierStrategy.OnMouseDown(point);
        }

        /// <summary>
        /// 鼠标按下后弹起
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            return m_BtnBehavierStrategy.OnMouseUp(point);
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            m_BtnBehavierStrategy.OnDraw(g);
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public override void Translate(float offsetX, float offsetY)
        {
            base.Translate(offsetX, offsetY);

            m_ButtonText.Translate(offsetX, offsetY);

            for (int i = 0; i < m_Curpoint.Length; i++)
            {
                m_Curpoint[i].Offset((int) Math.Ceiling(offsetX), (int) Math.Ceiling(offsetY));
            }

        }

        /// <summary>
        /// 将此  放大指定量。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public override void Inflate(float width, float height)
        {
            base.Inflate(width, height);

            m_ButtonText.Inflate(width, height);
        }

        /// <summary>
        /// 常规画图
        /// </summary>
        public virtual void DrawDefault(Graphics g)
        {
            if (!Visible)
            {
                return;
            }

            DrawText(g);

            DrawOutLine(g);

            DrawBackImage(g);
        }

        /// <summary>
        /// 画背景图片
        /// </summary>
        public virtual void DrawBackImage(Graphics g)
        {
            if (m_Bgkimage != null)
            {
                g.DrawImage(m_Bgkimage, m_ButtonText.OutLineRectangle);
            }
        }

        /// <summary>
        /// 画轮廓
        /// </summary>
        public virtual void DrawOutLine(Graphics g)
        {
            switch (CurrentMouseState)
            {
                case MouseState.MouseUp:
                    g.DrawLine(m_Linepen, m_Curpoint[0], m_Curpoint[1]);
                    g.DrawLine(m_Linepen, m_Curpoint[0], m_Curpoint[2]);
                    g.DrawLine(m_Darkpen, m_Curpoint[1], m_Curpoint[3]);
                    g.DrawLine(m_Darkpen, m_Curpoint[2], m_Curpoint[3]);
                    break;
                case MouseState.MouseDown:
                    g.DrawLine(m_Darkpen, m_Curpoint[0], m_Curpoint[1]);
                    g.DrawLine(m_Darkpen, m_Curpoint[0], m_Curpoint[2]);
                    g.DrawLine(m_Linepen, m_Curpoint[1], m_Curpoint[3]);
                    g.DrawLine(m_Linepen, m_Curpoint[2], m_Curpoint[3]);
                    break;
            }
        }

        /// <summary>
        /// 绘button的字
        /// </summary>
        public virtual void DrawText(Graphics g)
        {
            m_ButtonText.OnDraw(g);
        }



        /// <summary>
        /// 绘button的字
        /// </summary>
        /// <param name="g"></param>
        /// <param name="needDrawBk"></param>
        protected virtual void DrawText(Graphics g, bool needDrawBk)
        {
            m_ButtonText.OnDraw(g, needDrawBk);
        }
    }
}
