using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Domain;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DoorParam : HMIBase
    {
        private Rectangle[] m_RectArr;
        private Bitmap[] m_BmpArr;
        private bool[] m_IsDrProgrammed;
        private Rectangle[] m_TableRectArr;
        private Point[] m_LinePtArr;
        private int[] m_ForceArr;
        private float[] m_FloatArr;
        private int[] m_MemeryIndex;

        private List<GDIRectText> m_CarNames;

        public override string GetInfo()
        {
            return "DoorParam";
        }

        protected override bool Initalize()
        {
            //127,360,45,28
            m_CarNames = CarNameConstant.CarNameCollection.Select((s, i) => new GDIRectText()
            {
                OutLineRectangle = new Rectangle(122 +i*124, 356, 56, 28),
                Text = s,
                TextColor = GdiCommon.DarkBlueBrush.Color,
                BkColor = Color.FromArgb(82, 90, 109),
            }).ToList();

            m_MemeryIndex = new int[3];
            m_RectArr = new[]
                      {
                          new Rectangle(97, 313, 25, 10),
                          new Rectangle(127, 313, 25, 10),
                          new Rectangle(156, 313, 25, 10),
                          new Rectangle(185, 313, 25, 10),
                          new Rectangle(97, 412, 25, 10),
                          new Rectangle(127, 412, 25, 10),
                          new Rectangle(156, 412, 25, 10),
                          new Rectangle(185, 412, 25, 10),

                          new Rectangle(218, 313, 25, 10),
                          new Rectangle(247, 313, 25, 10),
                          new Rectangle(276, 313, 25, 10),
                          new Rectangle(305, 313, 25, 10),
                          new Rectangle(218, 412, 25, 10),
                          new Rectangle(247, 412, 25, 10),
                          new Rectangle(276, 412, 25, 10),
                          new Rectangle(305, 412, 25, 10),

                          new Rectangle(343, 313, 25, 10),
                          new Rectangle(372, 313, 25, 10),
                          new Rectangle(401, 313, 25, 10),
                          new Rectangle(430, 313, 25, 10),
                          new Rectangle(343, 412, 25, 10),
                          new Rectangle(372, 412, 25, 10),
                          new Rectangle(401, 412, 25, 10),
                          new Rectangle(430, 412, 25, 10),

                          new Rectangle(468, 313, 25, 10),
                          new Rectangle(497, 313, 25, 10),
                          new Rectangle(526, 313, 25, 10),
                          new Rectangle(555, 313, 25, 10),
                          new Rectangle(468, 412, 25, 10),
                          new Rectangle(497, 412, 25, 10),
                          new Rectangle(526, 412, 25, 10),
                          new Rectangle(555, 412, 25, 10),

                          new Rectangle(588, 313, 25, 10),
                          new Rectangle(617, 313, 25, 10),
                          new Rectangle(646, 313, 25, 10),
                          new Rectangle(675, 313, 25, 10),
                          new Rectangle(588, 412, 25, 10),
                          new Rectangle(617, 412, 25, 10),
                          new Rectangle(646, 412, 25, 10),
                          new Rectangle(675, 412, 25, 10),

                          new Rectangle(80, 327, 639, 81),
                          new Rectangle(561, 232, 25, 10),
                          new Rectangle(561, 264, 25, 10)
                      };

            m_BmpArr = new[]
                     {
                         new Bitmap(RecPath + "\\frame/train.jpg"),
                         new Bitmap(RecPath + "\\frame/drRed.jpg"),
                         new Bitmap(RecPath + "\\frame/drGreen.jpg"),
                     };

            m_IsDrProgrammed = new[]
                             {
                                 true, true, true, true,
                                 true, true, true, true,
                                 true, true, true, true,
                                 true, true, true, true,
                                 false, false, false, false,
                                 false, false, false, false,
                                 false, false, false, false,
                                 false, false, false, false,
                                 false, false, false, false,
                                 true, true, true, true
                             };

            m_TableRectArr = new[]
                           {
                               new Rectangle(6, 80, 175, 38),
                               new Rectangle(6, 119, 175, 38),
                               new Rectangle(6, 159, 175, 38),
                               new Rectangle(6, 199, 175, 38),
                               new Rectangle(6, 239, 175, 38),

                               new Rectangle(185, 80, 83, 38),
                               new Rectangle(185, 119, 83, 38),
                               new Rectangle(185, 159, 83, 38),
                               new Rectangle(185, 199, 83, 38),
                               new Rectangle(185, 239, 83, 38),

                               new Rectangle(270, 80, 162, 38),
                               new Rectangle(270, 119, 162, 38),
                               new Rectangle(270, 159, 162, 38),
                               new Rectangle(270, 199, 162, 38),
                               new Rectangle(270, 239, 162, 38),

                               new Rectangle(434, 80, 98, 38),
                               new Rectangle(434, 119, 98, 38),
                               new Rectangle(434, 159, 98, 38),
                               new Rectangle(434, 199, 98, 38),
                               new Rectangle(434, 239, 98, 38),

                               new Rectangle(534, 80, 162, 38),
                               new Rectangle(534, 119, 162, 38),
                               new Rectangle(534, 159, 162, 38),

                               new Rectangle(697, 80, 98, 38),
                               new Rectangle(697, 119, 98, 38),
                               new Rectangle(697, 159, 98, 38),
                           };

            m_LinePtArr = new[]
                        {
                            new Point(5, 80),
                            new Point(5, 280),
                            new Point(184, 80),
                            new Point(184, 280),
                            new Point(268, 80),
                            new Point(268, 280),
                            new Point(432, 80),
                            new Point(432, 280),
                            new Point(532, 80),
                            new Point(532, 280),
                            new Point(696, 80),
                            new Point(696, 200),
                            new Point(797, 80),
                            new Point(797, 200),

                            new Point(5, 80),
                            new Point(797, 80),
                            new Point(5, 119),
                            new Point(797, 119),
                            new Point(5, 159),
                            new Point(797, 159),
                            new Point(5, 199),
                            new Point(797, 199),
                            new Point(5, 239),
                            new Point(533, 239),
                            new Point(5, 280),
                            new Point(533, 280),
                            new Point(540, 205),
                            new Point(593, 228),
                            new Point(593, 260)
                        };

            m_ForceArr = new[] { 150, 180, 200, 230, 260 };

            m_FloatArr = new[] { 3.0f, 3.0f, 3.0f, 40.0f, 0.5f, 0.0f, 0.5f, 0.3f };

            if (UIObj.InBoolList.Count >= 1)
            {
                m_MemeryIndex[0] = UIObj.InBoolList[0];
            }
            else return false;

            if (UIObj.InFloatList.Count >= 2)
            {
                m_MemeryIndex[1] = UIObj.InFloatList[0];
                m_MemeryIndex[2] = UIObj.InFloatList[1];
            }
            else return false;
            return true;
        }

        public override void paint(Graphics g)
        {
            //////////////////////////////////////////////////////////////////////////

            RefreshValues();

            //////////////////////////////////////////////////////////////////////////
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex) 14;
            GdiCommon.DarkGreyPen.Width = 2;

            DrawGridLines(g);

            GdiCommon.DarkGreyPen.Width = 1;

            g.DrawImage(m_BmpArr[0], m_RectArr[40]);
            for (var index = 0; index < 20; ++index)
            {
                g.DrawImage(m_IsDrProgrammed[index] ? m_BmpArr[2] : m_BmpArr[1], m_RectArr[index]);
                g.DrawImage(m_IsDrProgrammed[index + 20] ? m_BmpArr[2] : m_BmpArr[1], m_RectArr[index + 20]);
            }

            g.DrawString(GlobleParam.ResManager.GetString("String32"), GdiCommon.Txt14Font, GdiCommon.MediumGreyBrush, m_LinePtArr[26]);
            g.DrawImage(m_BmpArr[1], m_RectArr[41]);
            g.DrawImage(m_BmpArr[2], m_RectArr[42]);
            g.DrawString(GlobleParam.ResManager.GetString("String132"), GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, m_LinePtArr[27]);
            g.DrawString(GlobleParam.ResManager.GetString("String133"), GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, m_LinePtArr[28]);

            GdiCommon.m_StrFormat.LineAlignment = StringAlignment.Center;
            for (var index = 0; index < 5; ++index)
            {
                GdiCommon.m_StrFormat.Alignment = StringAlignment.Near;
                g.DrawString(GlobleParam.ResManager.GetString("String142") + (index + 1),
                    GdiCommon.Txt10Font,
                    GdiCommon.MediumGreyBrush,
                    m_TableRectArr[index],
                    GdiCommon.m_StrFormat);
                GdiCommon.m_StrFormat.Alignment = StringAlignment.Center;
                g.DrawString(m_ForceArr[index].ToString() + "N", GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, m_TableRectArr[index + 5], GdiCommon.m_StrFormat);
            }

            GdiCommon.m_StrFormat.Alignment = StringAlignment.Near;
            var temp = 137;
            for (var index = 0; index < 5; ++index)
            {
                g.DrawString(GlobleParam.ResManager.GetString("String" + (temp++)),
                    GdiCommon.Txt10Font,
                    GdiCommon.MediumGreyBrush,
                    m_TableRectArr[index + 10],
                    GdiCommon.m_StrFormat);
            }

            GdiCommon.m_StrFormat.Alignment = StringAlignment.Center;
            g.DrawString(m_FloatArr[0].ToString("F1") + "s", GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, m_TableRectArr[15], GdiCommon.m_StrFormat);
            g.DrawString(m_FloatArr[1].ToString("F1") + "s", GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, m_TableRectArr[16], GdiCommon.m_StrFormat);
            g.DrawString(m_FloatArr[2].ToString("F1"), GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, m_TableRectArr[17], GdiCommon.m_StrFormat);
            g.DrawString(m_FloatArr[3].ToString("F1") + "cm", GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, m_TableRectArr[18], GdiCommon.m_StrFormat);
            g.DrawString(m_FloatArr[4].ToString("F1") + "s", GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, m_TableRectArr[19], GdiCommon.m_StrFormat);

            temp = 134;
            for (var index = 0; index < 3; ++index)
            {
                GdiCommon.m_StrFormat.Alignment = StringAlignment.Near;
                g.DrawString(GlobleParam.ResManager.GetString("String" + (temp++)),
                    GdiCommon.Txt10Font,
                    GdiCommon.MediumGreyBrush,
                    m_TableRectArr[index + 20],
                    GdiCommon.m_StrFormat);
                GdiCommon.m_StrFormat.Alignment = StringAlignment.Center;
                g.DrawString(m_FloatArr[5 + index].ToString("F1") + "s", GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, m_TableRectArr[index + 23], GdiCommon.m_StrFormat);
            }

            m_CarNames.ForEach(e => e.OnDraw(g));
        }

        private void DrawGridLines(Graphics g)
        {
            for (var index = 0; index < 7; ++index)
            {
                g.DrawLine(GdiCommon.DarkGreyPen, m_LinePtArr[index*2], m_LinePtArr[index*2 + 1]);
            }
            for (var index = 7; index < 13; ++index)
            {
                g.DrawLine(GdiCommon.DarkGreyPen, m_LinePtArr[index*2], m_LinePtArr[index*2 + 1]);
            }
        }

        private void RefreshValues()
        {
            for (var index = 0; index < 40; ++index)
            {
                m_IsDrProgrammed[index] = BoolList[index + m_MemeryIndex[0]];
            }
            for (var index = 0; index < 5; ++index)
            {
                m_ForceArr[index] = (int) FloatList[index + m_MemeryIndex[1]];
            }
            for (var index = 0; index < 8; ++index)
            {
                m_FloatArr[index] = FloatList[index + m_MemeryIndex[2]];
            }
        }
    }
}