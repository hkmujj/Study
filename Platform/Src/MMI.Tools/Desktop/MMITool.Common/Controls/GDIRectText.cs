using System;
using System.Diagnostics;
using System.Drawing;

namespace MMITool.Common.Controls
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Text = {Text}")]
    public class GDIRectText : CommonInnerControlBase
    {

        private int m_Labelsize;
        private SolidBrush m_TextBrush;
        private SolidBrush m_BKBrush;

        private String m_StrLabel = "";
        private bool m_ShowLable = false;

        /// <summary>
        /// ����ͼ
        /// </summary>
        public Image BKImage { get; set; }

        private bool m_Bold;
        private bool m_BackColorVisible;
        private Color m_BkColor;

        /// <summary>
        /// �Ƿ������߿�
        /// </summary>
        [Obsolete("�������淶, use NeedDarwOutline")]
        public bool Isdrawrectfrm
        {
            set { NeedDarwOutline = value; }
            get { return NeedDarwOutline; }
        }


        /// <summary>
        /// �Ƿ���Ҫ����
        /// </summary>
        public bool NeedDarwOutline { set; get; }

        /// <summary>
        /// Ĭ������ "Arial", 12
        /// </summary>
        public Font DrawFont { set; get; }

        /// <summary>
        /// �ı�
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// ����ɫ, Ĭ�� Color.Black
        /// </summary>
        public Color BkColor
        {
            set
            {
                m_BkColor = value;
                if (m_BKBrush !=null)
                {
                    m_BKBrush.Color = value;
                }
            }
            get { return m_BkColor; }
        }

        /// <summary>
        /// ������pen, Ĭ��Color.White
        /// </summary>
        public Pen OutLinePen { get; set; }

        /// <summary>
        /// �����Ƿ�Ӵ�
        /// </summary>
        public bool Bold
        {
            set
            {
                m_Bold = value;
                if (this.DrawFont != null)
                {
                    var style = DrawFont.Style;
                    //DrawFont = new Font(DrawFont, value ? FontStyle.Bold : ~FontStyle.Bold);
                    if (value)
                    {
                        style |= FontStyle.Bold;
                    }
                    else
                    {
                        style &= ~FontStyle.Bold;
                    }
                    DrawFont = new Font(DrawFont, style);
                }
            }
            get { return m_Bold; }
        }


        /// <summary>
        /// �ı��������Ϣ
        /// </summary>
        public StringFormat TextFormat { set; get; }

        /// <summary>
        /// ������ɫ, Ĭ��(Color.White)
        /// </summary>
        public Color TextColor
        {
            set { m_TextBrush.Color = value; }
            get { return m_TextBrush.Color; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool BackColorVisible
        {
            set
            {
                m_BackColorVisible = value;
                m_BKBrush = value
                    ? new SolidBrush(BkColor)
                    : null;
            }
            get { return m_BackColorVisible; }
        }

        /// <summary>
        /// 
        /// </summary>
        public GDIRectText()
        {
            OutLinePen = new Pen(Color.White, 1);
            NeedDarwOutline = false;
            Visible = true;
            m_TextBrush = new SolidBrush(Color.White);
            TextFormat = new StringFormat() { LineAlignment = StringAlignment.Center };
            m_BKBrush = new SolidBrush(Color.Black);
            m_BkColor = Color.Black;
            m_BackColorVisible = true;
            DrawFont = new Font("Arial", 12);
        }



        #region set ����

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="format"></param>
        /// <param name="bold"></param>
        /// <param name="font"></param>
        public void SetTextStyle(int size, int format, bool bold, String font)
        {

            if (format != FormatStyle.DirectionLeftToRight)
            {

                if (format == FormatStyle.DirectionRightToLeft)
                {
                    TextFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                }
                else if (format == FormatStyle.Center)
                {
                    TextFormat.Alignment = StringAlignment.Center;

                }
            }

            Bold = bold;
            DrawFont = new Font(font, size);
            if (bold)
            {
                DrawFont = new Font(DrawFont, FontStyle.Bold);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public void SetBkColor(int r, int g, int b)
        {

            m_BKBrush = new SolidBrush(Color.FromArgb(r, g, b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public void SetBkColor(Color color)
        {
            m_BKBrush = new SolidBrush(color);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        public void SetDrawFrm(bool b)
        {
            NeedDarwOutline = b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="size"></param>
        public void SetLabel(String text, int size)
        {
            m_StrLabel = text;
            m_Labelsize = size;
            m_ShowLable = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        public void SetLabelOn(bool b)
        {
            m_ShowLable = b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public void SetTextColor(int r, int g, int b)
        {
            m_TextBrush = new SolidBrush(Color.FromArgb(r, g, b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theTextColor"></param>
        public void SetTextColor(SolidBrush theTextColor)
        {
            m_TextBrush = theTextColor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void SetText(String text)
        {
            Text = text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        [Obsolete("��ѡ��ʹ�� OutLineRectangle")]
        public void SetTextRect(int x, int y, int width, int height)
        {
            OutLineRectangle = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="weight"></param>
        public void SetLinePen(int r, int g, int b, int weight)
        {
            OutLinePen = new Pen(Color.FromArgb(r, g, b), weight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        public void SetImage(Image img)
        {
            BKImage = new Bitmap(img);

        }

        #endregion


        /// <summary>
        /// ��ʼ��
        /// </summary>
        public override void Init()
        {

        }

        /// <summary>
        /// ��갴��
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            //û����������Ӧ
            return false;
        }

        /// <summary>
        /// ��갴�º���
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            //û����������Ӧ
            return false;
        }

        /// <summary>
        /// ��ͼ
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            //Refresh();

            if (!Visible)
            {
                return;
            }

            // ����
            DrawBk(g);

            // �ı�
            DrawText(g);

            // ����
            DrawOutline(g);

        }

        internal void OnDraw(Graphics g, bool needDrawBk)
        {
            Refresh();

            if (!Visible)
            {
                return;
            }

            if (needDrawBk)
            {
                // ����
                DrawBk(g);
            }

            // �ı�
            DrawText(g);

            // ����
            DrawOutline(g);
        }

        private void DrawText(Graphics g)
        {
            if (m_TextBrush != null)
            {
                if (m_ShowLable)
                {
                    var tmpDrawFont = new Font("Arial", m_Labelsize);

                    RectangleF tmpOutLineRectangle = OutLineRectangle;
                    tmpOutLineRectangle.Width = OutLineRectangle.Width * 0.8f;
                    g.DrawString(Text, DrawFont, m_TextBrush, tmpOutLineRectangle, TextFormat);
                    tmpOutLineRectangle.Width = OutLineRectangle.Width * 0.4f;
                    tmpOutLineRectangle.X = OutLineRectangle.X + OutLineRectangle.Width * 0.65f;
                    g.DrawString(m_StrLabel, tmpDrawFont, m_TextBrush, tmpOutLineRectangle, TextFormat);
                }
                else
                {
                    g.DrawString(Text, DrawFont, m_TextBrush, OutLineRectangle, TextFormat);
                }
            }
        }

        private void DrawOutline(Graphics g)
        {
            if (NeedDarwOutline)
            {
                g.DrawRectangle(OutLinePen, OutLineRectangle);
            }
        }

        private void DrawBk(Graphics g)
        {
            if (m_BKBrush != null)
            {
                g.FillRectangle(m_BKBrush, OutLineRectangle);
            }

            if (BKImage != null)
            {
                g.DrawImage(BKImage, OutLineRectangle);
            }
        }
    }
}
