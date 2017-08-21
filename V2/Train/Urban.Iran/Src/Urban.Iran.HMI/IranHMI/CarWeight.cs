using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Model;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Controls;
using Urban.Iran.HMI.Domain;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class CarWeight : HMIBase
    {
        private Bitmap m_DegreeBmp;

        private List<Tuple<GDIRectText, GDIProgress, RectangleImage>> m_ValueProgresses;

        private List<Label> m_ConstInfos;

        public override string GetInfo()
        {
            return "CarWeight";
        }

        protected override bool Initalize()
        {

            InitalizeConstInfos();

            m_DegreeBmp = new Bitmap(RecPath + "\\frame/LoadWeightDegree.jpg");

            m_ValueProgresses = CarNameConstant.CarNameCollection.Select((s, i) =>
            {
                var it =
                    new Tuple<GDIRectText, GDIProgress, RectangleImage>(
                        new GDIRectText()
                        {
                            OutLineRectangle = new Rectangle(167 + 120 * i, 440, 46, 22),
                            DrawFont = GdiCommon.Txt12Font,
                            TextBrush = GdiCommon.MediumGreyBrush,
                            BkColor = Color.FromArgb(223, 223, 0),
                            Text = "0",
                            Tag = GlobleParam.Instance.FindInFloatIndex(string.Format("{0} 载重%", s)),
                            TextFormat = GdiCommon.CenterFormat,
                            RefreshAction = o =>
                            {
                                var txt = (GDIRectText)o;
                                var tag = (Tuple<GDIRectText, GDIProgress, int>)txt.Tag;
                                var value = Math.Max(0, Math.Min(100, FloatList[tag.Item3]));
                                tag.Item1.Text = value.ToString("0");
                                tag.Item2.CurrentValue = (int)value;
                            }
                        },
                        new GDIProgress(Direction.Up)
                        {
                            OutLineRectangle = new Rectangle(170 + 120 * i, 138, 40, 297),
                            BackgroundColor = GdiCommon.OceanBlueBrush.Color,
                            CurrentValue = 100,
                            MaxValue = 100,
                            Visible = true,
                            NeedOutLine = false,
                        },
                        new RectangleImage()
                        {
                            OutLineRectangle = new Rectangle(166 + 120 * i, 136, 48, 327),
                            Image = m_DegreeBmp
                        });
                it.Item1.Tag = new Tuple<GDIRectText, GDIProgress, int>(it.Item1, it.Item2, (int)it.Item1.Tag);
                return it;
            }).ToList();

            return true;
        }

        private void InitalizeConstInfos()
        {
            m_ConstInfos = new List<Label>();
            for (int i = 0; i < 5; i++)
            {
                m_ConstInfos.Add(new Label()
                {
                    OutLineRectangle = new Rectangle(710, 133 + 74 * i + (i == 4 ? 16 : 0), 40, 20),
                    Font = GdiCommon.Txt12Font,
                    Brush = GdiCommon.MediumGreyBrush,
                    Text = (25 * (4 - i)).ToString()
                });
            }
            m_ConstInfos.Last().Text = "%";
        }

        public override void paint(Graphics g)
        {
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex) 13;
            var temp = 34;


            m_ConstInfos.ForEach(e => e.OnDraw(g));

            m_ValueProgresses.ForEach(e =>
            {
                e.Item3.OnDraw(g);
                e.Item1.OnPaint(g);
                e.Item2.OnDraw(g);

            });
        }
    }
}