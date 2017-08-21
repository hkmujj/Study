using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class StartView : HMIBase
    {
        private GraphicsPath m_GraphicsPath;
        private List<Rectangle> m_Rec;
        private List<CommonInnerControlBase> m_Collection;
        private Rectangle m_LocRct;
        private int m_Index = 0;
        private string DisplayStr = string.Empty;
        private StringFormat leftTop = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Near
        };
        protected override bool Initalize()
        {
            m_Rec = new List<Rectangle>();
            m_Rec.Add(new Rectangle(0, 0, 800, 600));
            m_Rec.Add(new Rectangle(273, 55, 250, 30));
            m_Rec.Add(new Rectangle(175, 375, 425, 145));
            m_Rec.Add(new Rectangle(175, 160, 55, 180));
            m_Rec.Add(new Rectangle(545, 160, 55, 180));
            m_Rec.Add(new Rectangle(235, 175, 305, 50));
            m_Rec.Add(new Rectangle(235, 230, 305, 110));
            m_Rec.Add(new Rectangle(273, 55, 30, 30));
            m_Rec.Add(new Rectangle(175, 385, 425, 135));
            m_LocRct = m_Rec[7];

            m_Collection = new List<CommonInnerControlBase>();
            m_GraphicsPath = new GraphicsPath();
            m_GraphicsPath.AddString("【", new FontFamily("宋体"), (int)FontStyle.Regular, 100f, new Point(120, 75), new StringFormat());
            m_GraphicsPath.AddString("】", new FontFamily("宋体"), (int)FontStyle.Regular, 100f, new Point(520, 75), new StringFormat());
            Matrix m = new Matrix();
            m.Scale(1, 2);
            m_GraphicsPath.Transform(m);
            m_Collection.Add(new GDIRectText()
            {
                Text = "M I T R A C",
                OutLineRectangle = m_Rec[5],
                NeedDarwOutline = false,
                BackColorVisible = false,
                DrawFont = new Font("Arial", 32),
                TextFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
                TextColor = Color.Black,
                Bold = true
            });
            m_Collection.Add(new GDIRectText()
            {
                Text = "TCMS",
                OutLineRectangle = m_Rec[6],
                NeedDarwOutline = false,
                BackColorVisible = false,
                DrawFont = new Font("Arial", 70),
                TextFormat = new StringFormat() { Alignment = StringAlignment.Center },
                TextColor = Color.Black,
                Bold = true
            });
            LoadAssbleVersion();
            return true;
        }

        public override void paint(Graphics g)
        {
            OffsetRec();
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.FillRegion(GdiCommon.StartViewBk, g.Clip);
            g.FillRectangles(GdiCommon.StartViewGray, m_Rec.Skip(1).Take(2).ToArray());
            m_Collection.ForEach(f => f.OnDraw(g));
            g.FillPath(Brushes.Black, m_GraphicsPath);
            g.FillRectangle(GdiCommon.WhiteBrush, m_Rec[7]);
            if (m_Index > 20)
            {
                g.DrawString(DisplayStr, SystemFonts.SmallCaptionFont, GdiCommon.WhiteBrush, m_Rec[8], leftTop);
            }
        }

        private void OffsetRec()
        {
            m_Index++;
            if (m_Rec[7].X + m_Rec[7].Width == m_Rec[1].X + m_Rec[1].Width)
            {
                m_Rec[7] = m_LocRct;
            }
            else
            {
                var tmp = m_Rec[7];
                tmp.Offset(10, 0);
                m_Rec[7] = tmp;
            }

        }


        private void LoadAssbleVersion()
        {
            if (string.IsNullOrEmpty(DisplayStr))
            {
                Type type = GetType();
                var str = type.Assembly.FullName.Split(',');
                foreach (var s in str)
                {
                    if (s.Trim().StartsWith("Version", StringComparison.InvariantCultureIgnoreCase))
                    {
                        DisplayStr = string.Format("The HMI {0}", s.Replace('=', ' '));
                    }
                }

            }
        }
    }
}