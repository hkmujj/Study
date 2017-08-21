using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml.Serialization;


namespace Urban.NanJing.AirportLine.DDU
{

    public class RectText
    {
        private bool m_IsSetValue;

        private bool m_IsFilllAll;

        private Point m_ImagePoint;

        public bool IsImmutable
        {
            get
            {
                return m_IsImmutable;
            }
            set
            {
                m_IsImmutable = value;
            }
        }
        private bool m_IsImmutable = true;

        private Rectangle m_FillRect;

        public Rectangle RectPosition
        {
            set
            {
                m_Rect = value;
            }
            get
            {
                return m_Rect;
            }
        }
        protected Rectangle m_Rect = new Rectangle();

        public String Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
                m_IsSetValue = true;
            }
        }
        private string m_Text;

        public float FontSize
        {
            get
            {
                return m_FontSize;
            }
            set
            {
                m_FontSize = value;
                m_Font = new Font("Arial", m_FontSize);
            }
        }
        private float m_FontSize;

        public FontStyle FontTextStyle
        {
            get
            {
                return m_FontStyle;
            }
            set
            {
                m_FontStyle = value;
                m_Font = new Font("Arial", m_FontSize, value);
            }
        }
        private FontStyle m_FontStyle;

        public int AlignmentTextFormat
        {
            get
            {
                return m_AlignmentTextFormat;
            }
            set
            {
                m_AlignmentTextFormat = value;
                m_StringFormat.Alignment = (StringAlignment)m_AlignmentTextFormat;
            }
        }
        private int m_AlignmentTextFormat;

        public int LineAlignmentTextFormat
        {
            get
            {
                return m_LineAlignmentTextFormat;
            }
            set
            {
                m_LineAlignmentTextFormat = value;
                m_StringFormat.LineAlignment = (StringAlignment)m_LineAlignmentTextFormat;
            }
        }
        private int m_LineAlignmentTextFormat;

        public Color TextColor
        {
            get
            {
                return m_TextColor;
            }
            set
            {
                m_TextColor = value;
                m_TextBrush.Color = m_TextColor;
            }
        }
        private Color m_TextColor;


        public Color BackgroundColor
        {
            get
            {
                return m_BackgroundColor;
            }
            set
            {
                if (m_BackgroundColor != value)
                {
                    m_BackgroundColor = value;
                    m_BackGroundBrush.Color = m_BackgroundColor;
                }
            }
        }
        private Color m_BackgroundColor;

        public Color PenColor
        {
            get
            {
                return m_PenColor;
            }
            set
            {
                m_PenColor = value;
                m_Pen.Color = m_PenColor;
            }
        }
        private Color m_PenColor;


        public int PenWide
        {
            get
            {
                return m_PenWide;
            }
            set
            {
                m_PenWide = value;
                m_Pen.Width = m_PenWide;
            }
        }
        private int m_PenWide;

        public bool IsDrawRectFrm
        {
            get
            {
                return m_IsDrawRectFrm;
            }
            set
            {
                m_IsDrawRectFrm = value;
            }
        }
        private bool m_IsDrawRectFrm;

        public Image ImagePicture
        {
            get
            {
                return m_ImagePicture;
            }
            set
            {
                m_ImagePicture = value;
                m_IsSetValue = true;
            }
        }
        private Image m_ImagePicture;

        private SolidBrush m_TextBrush;
        protected SolidBrush m_BackGroundBrush;
        private Font m_Font;
        private Pen m_Pen;
        private StringFormat m_StringFormat;
        private bool m_IsFillImageRect;

        public RectText(Rectangle rect, string text, float labelSize, int textFormat, Color textColor, Color backcolor, Color penColor, int penWide, bool isDrawFrame, Image image = null, bool isImmutable = false, bool isFillAll = true, bool isFillImageRect = true)
        {
            m_Rect = rect;
            m_Text = text;
            m_FontSize = labelSize;
            m_AlignmentTextFormat = textFormat;
            m_LineAlignmentTextFormat = 1;
            m_TextColor = textColor;
            m_BackgroundColor = backcolor;
            m_PenColor = penColor;
            m_PenWide = penWide;
            m_IsDrawRectFrm = isDrawFrame;
            m_ImagePicture = image;
            m_IsImmutable = isImmutable;
            m_IsFilllAll = isFillAll;
            m_IsFillImageRect = isFillImageRect;

            m_FillRect = new Rectangle(m_Rect.X + 2, m_Rect.Y + 2, m_Rect.Width - 4, m_Rect.Height - 4);
            m_TextBrush = new SolidBrush(m_TextColor);
            if (m_BackgroundColor != null)
                m_BackGroundBrush = new SolidBrush(m_BackgroundColor);
            m_Font = new Font("Arial", m_FontSize);
            m_Pen = new Pen(m_PenColor, m_PenWide);
            m_StringFormat = new StringFormat() { LineAlignment = (StringAlignment)m_LineAlignmentTextFormat, Alignment = (StringAlignment)m_AlignmentTextFormat };
        }

        public virtual void OnDraw(Graphics e)
        {
            if (m_BackGroundBrush != null)
            {
                if (!m_IsFilllAll)
                    e.FillRectangle(m_BackGroundBrush, m_FillRect);
                else
                    e.FillRectangle(m_BackGroundBrush, m_Rect);
            }
            if (!m_IsImmutable)
            {
                if (m_IsSetValue)
                {
                    if (m_ImagePicture != null)
                    {
                        if (!m_IsFillImageRect)
                        {
                            m_ImagePoint = new Point(m_Rect.X + (m_Rect.Width - m_ImagePicture.Width) / 2, m_Rect.Y);//可能会出现问题 由于图片尺寸过大
                            e.DrawImage(m_ImagePicture, new Rectangle(m_ImagePoint.X, m_ImagePoint.Y, m_ImagePicture.Width, m_ImagePicture.Height));
                        }
                        else
                        {
                            e.DrawImage(m_ImagePicture, m_Rect);
                        }
                    }

                    if (m_TextBrush != null && !string.IsNullOrEmpty(m_Text))
                    {
                        e.DrawString(m_Text, m_Font, m_TextBrush, m_Rect, m_StringFormat);
                    }
                    m_IsSetValue = false;
                }
            }
            else
            {
                if (m_ImagePicture != null)
                {
                    if (!m_IsFillImageRect)
                    {
                        m_ImagePoint = new Point(m_Rect.X + (m_Rect.Width - m_ImagePicture.Width) / 2, m_Rect.Y);//可能会出现问题 由于图片尺寸过大
                        e.DrawImage(m_ImagePicture, new Rectangle(m_ImagePoint.X, m_ImagePoint.Y, m_ImagePicture.Width, m_ImagePicture.Height));
                    }
                    else
                    {
                        e.DrawImage(m_ImagePicture, m_Rect);
                    }
                }

                if (m_TextBrush != null && !string.IsNullOrEmpty(m_Text))
                {
                    e.DrawString(m_Text, m_Font, m_TextBrush, m_Rect, m_StringFormat);
                }
            }

            if (IsDrawRectFrm)
            {
                e.DrawRectangle(m_Pen, m_Rect);
            }
        }

        public event Action<string> MouseDownEvent;

        public void OnMouseUp(Point p)
        {
            if (m_Rect.Contains(p))
            {
                if (MouseDownEvent != null)
                {
                    MouseDownEvent(m_Text);
                }
            }
        }
    }
}
