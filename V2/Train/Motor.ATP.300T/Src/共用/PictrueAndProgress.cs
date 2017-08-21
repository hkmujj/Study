using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Model;

namespace Motor.ATP._300T.共用
{
    public class PictrueAndProgress
    {
        private readonly Image[] m_ImgArr;
        private readonly RectangleF[] m_RectangleF;
        private RectangleF[] m_StrRec;
        private RectangleF[] m_ImgRec;
        private List<GDIProgress> m_Progresses;
        private Color m_BackGroundColor;
        private bool m_IsLoad;
        public int Interval { get; set; }

        // ReSharper disable once UnusedParameter.Local
        public PictrueAndProgress(ATPBaseClass obj, Image[] img, RectangleF[] rec, float maxValue)
        {
            m_ImgArr = img;
            m_RectangleF = rec;
            MaxValue = maxValue;
            ProspectColor = Color.LawnGreen;
            Interval = 5;
        }
        public float CurrentValue { get; set; }
        public float MaxValue { get;private set; }
        void LoadStrRec()
        {
            m_IsLoad = true;
            m_StrRec = new RectangleF[m_RectangleF.Length];
            m_ImgRec = new RectangleF[m_RectangleF.Length];
            for (var i = 0; i < m_RectangleF.Length; i++)
            {
                m_StrRec[i] = new RectangleF(m_RectangleF[i].X + m_RectangleF[i].Height + Interval+16, m_RectangleF[i].Y, m_RectangleF[i].Width - m_RectangleF[i].Height - Interval-16, m_RectangleF[i].Height);
                m_ImgRec[i] = new RectangleF(m_RectangleF[i].X, m_RectangleF[i].Y-4, m_RectangleF[i].Height+8, m_RectangleF[i].Height+8);
            }
            LoadProgress();
        }

        void LoadProgress()
        {
            m_Progresses = new List<GDIProgress>();
            for (var i = 0; i < m_RectangleF.Length; i++)
            {
                m_Progresses.Add(new GDIProgress(Direction.Right)
                {
                    MaxValue = MaxValue,
                    CurrentValue = CurrentValue,
                    Location = Rectangle.Round(m_StrRec[i]).Location,
                    Size = Rectangle.Round(m_StrRec[i]).Size,
                    NeedOutLine = false,
                    BackgroundColor = ProspectColor,RefreshAction = o=>RefreshValue((GDIProgress)o)
                });
            }
        }

        private void RefreshValue(GDIProgress item)
        {
            item.CurrentValue = CurrentValue;
        }

        public Color BackGroundColor
        {
            get { return m_BackGroundColor; }
            set
            {
                if (value == m_BackGroundColor)
                {
                    return;
                }
                m_BackGroundColor = value;
                m_Brush = new SolidBrush(value);
            }
        }

        private SolidBrush m_Brush;
        public Color ProspectColor { get; set; }
        public void OnDraw(Graphics g)
        {
            if (!m_IsLoad)
            {
                LoadStrRec();
            }
            for (var i = 0; i < m_RectangleF.Length; i++)
            {
                g.DrawImage(m_ImgArr[i], m_ImgRec[i]);
                if (m_Brush != null)
                {
                    g.FillRectangle(m_Brush, m_StrRec[i]);
                }
               
            }
            m_Progresses.ForEach(f => f.OnPaint(g));
            for (var i = 0; i < m_RectangleF.Length; i++)
            {
                g.DrawString((CurrentValue / MaxValue).ToString("0%"), FontsItems.Font12DefB, SolidBrushsItems.BlackBrush, m_Progresses[i].OutLineRectangle, FontsItems.TheAlignment(FontRelated.居中)); 
            }
        }
    }
}