using System;
using System.Diagnostics;
using System.Drawing;

namespace MMITool.Common.Controls.Button
{
    /// <summary>
    /// ���� Button �Ŀؼ� 
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Text = {Text}")]
    public class GDIButton : CommonInnerControlBase
    {
        /// <summary>
        /// ��갴�µ��¼�
        /// </summary>
        [NonSerialized]
        public EventHandler ButtonDownEvent;

        /// <summary>
        /// �������¼�
        /// </summary>
        [NonSerialized]
        public EventHandler ButtonUpEvent;

        /// <summary>
        /// TODO ������¼�
        /// </summary>
        [NonSerialized]
        public EventHandler ButtonClickEvent;

        /// <summary>
        /// ��ǰ������������״̬[DefaultValue(MouseState.MouseUp)]
        /// </summary>
        public MouseState CurrentMouseState { set; get; }

        /// <summary>
        /// �Ƿ����[DefaultValue(true)]
        /// </summary>
        public bool IsEnable
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
        /// �����ϵ��ı�
        /// </summary>
        public string Text
        {
            set { m_ButtonText.Text = value; }
            get { return m_ButtonText.Text; }
        }

        /// <summary>
        /// �ؼ���������ɫ
        /// </summary>
        public Color TextColor
        {
            set { m_ButtonText.TextColor = value; }
            get { return m_ButtonText.TextColor; }
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
            private set
            {
                m_ButtonText = (GDIRectText) value;
            }
        }

        private const int Gap = 3;
        private readonly Point[] m_Curpoint = new Point[4];
        private readonly Pen m_Darkpen = new Pen(Color.FromArgb(104, 112, 125), 3);
        private readonly Pen m_Linepen = new Pen(Color.FromArgb(221, 228, 234), 3);
        private Image m_Bgkimage;

        /// <summary>
        /// ����ͼƬ
        /// </summary>
        public Image BackImage
        {
            get { return m_Bgkimage; }
            set { m_Bgkimage = value; }
        }

        private bool m_IsEnable;
        private IBtnBehavierStrategy m_BtnBehavierStrategy;

        /// <summary>
        /// ������ʹ�ò���
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
            this.OutLineChanged =
                (sender, args) =>
                {
                    var x = OutLineRectangle.X;
                    var y = OutLineRectangle.Y;
                    var weight = OutLineRectangle.Width;
                    var height = OutLineRectangle.Height;
                    m_ButtonText.OutLineRectangle = new Rectangle(x + Gap, y + Gap, weight - Gap * 2, height - Gap * 2);
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
            m_ButtonText.OutLineRectangle = new Rectangle(x + Gap, y + Gap, weight - Gap * 2, height - Gap * 2);
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
        /// �����ı��ĸ�ʽ 
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
        /// ���ɰ�ť�� Click �¼���
        /// </summary>
        [Obsolete("���ܲ�����")]
        public void PerformClick()
        {
            m_BtnBehavierStrategy = new BtnBehavierClickStrategy(this);
        }

        /// <summary>
        /// ��갴��
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            return m_BtnBehavierStrategy.OnMouseDown(point);
        }

        /// <summary>
        /// ��갴�º���
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            return m_BtnBehavierStrategy.OnMouseUp(point);
        }

        /// <summary>
        /// ��ͼ
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            m_BtnBehavierStrategy.OnDraw(g);
        }

        /// <summary>
        /// ���滭ͼ
        /// </summary>
        public void DrawDefault(Graphics g)
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
        /// ������ͼƬ
        /// </summary>
        public void DrawBackImage(Graphics g)
        {
            if (m_Bgkimage != null)
            {
                g.DrawImage(m_Bgkimage, m_ButtonText.OutLineRectangle);
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public void DrawOutLine(Graphics g)
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
        /// ��button����
        /// </summary>
        public void DrawText(Graphics g)
        {
            m_ButtonText.OnDraw(g);
        }

        /// <summary>
        /// ��button����
        /// </summary>
        /// <param name="g"></param>
        /// <param name="needDrawBk"></param>
        protected void DrawText(Graphics g, bool needDrawBk)
        {
            m_ButtonText.OnDraw(g, needDrawBk);   
        }
    }
}
