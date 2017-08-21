using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class LegendBrakesAir : HMIBase
    {
        private FjButtonEx m_BackBtn;

        private List<Tuple<Label, RectangleImage>> m_ConstInfoItems;

        public override string GetInfo()
        {
            return "空压、制动图例";
        }

        public override void paint(Graphics g)
        {
            GlobleParam.Instance.CurrentIranViewIndex = IranViewIndex.BrakeAirLegend;

            m_ConstInfoItems.ForEach(e =>
            {
                e.Item1.OnDraw(g);
                e.Item2.OnDraw(g);
            });
        }

        protected override bool Initalize()
        {
            InitalizeAllLegend();

            m_ConstInfoItems.RemoveAll(r => r.Item1.Text.Contains("nknown"));

            m_BackBtn = new FjButtonEx(1, GlobleParam.ResManager.GetString("String243"), GlobleRect.m_LegendbtnRect);
            m_BackBtn.MouseDown += (sender, pt) => ChangedPage(IranViewIndex.BrakeAir);

            return true;
        }

        private void InitalizeAllLegend()
        {
            var rectArr = new[]
            {
                new Rectangle(2, 70, 38, 22),
                new Rectangle(2, 99, 38, 22),
                new Rectangle(2, 128, 38, 22),
                new Rectangle(2, 157, 38, 22),
                new Rectangle(2, 186, 38, 22),
                new Rectangle(0, 214, 48, 48),
                new Rectangle(0, 268, 48, 48),
                new Rectangle(0, 322, 48, 48),
                new Rectangle(236, 70, 48, 72),
                new Rectangle(236, 148, 48, 72),
                new Rectangle(236, 226, 48, 72),
                new Rectangle(452, 70, 48, 48),
                new Rectangle(452, 121, 48, 48),
                new Rectangle(452, 172, 48, 48)
            };

            var txtRects = new[]
            {
                new Rectangle(55, 70, 170, 22),
                new Rectangle(55, 99, 170, 22),
                new Rectangle(55, 128, 170, 22),
                new Rectangle(55, 157, 170, 22),
                new Rectangle(55, 186, 170, 22),
                new Rectangle(55, 214, 170, 48),
                new Rectangle(55, 268, 170, 48),
                new Rectangle(55, 322, 170, 48),
                new Rectangle(297, 70, 155, 72),
                new Rectangle(297, 148, 155, 72),
                new Rectangle(297, 226, 155, 72),
                new Rectangle(511, 70, 155, 48),
                new Rectangle(511, 121, 155, 48),
                new Rectangle(511, 172, 155, 48),
            };

            var bitmaps = new[]
            {
                new Bitmap(RecPath + "\\frame/brakeOk.jpg"),
                new Bitmap(RecPath + "\\frame/brakeOff.jpg"),
                new Bitmap(RecPath + "\\frame/brakeCutout.jpg"),
                new Bitmap(RecPath + "\\frame/brakeFaulty.jpg"),
                new Bitmap(RecPath + "\\frame/brakeUnknown.jpg"),
                new Bitmap(RecPath + "\\frame/eBrakeApp.jpg"),
                new Bitmap(RecPath + "\\frame/eBrakeReleased.jpg"),
                new Bitmap(RecPath + "\\frame/eBrakeUnknown.jpg"),
                new Bitmap(RecPath + "\\frame/airCompRunning.jpg"),
                new Bitmap(RecPath + "\\frame/airCompStandby.jpg"),
                new Bitmap(RecPath + "\\frame/airCompFaulty.jpg"),
                new Bitmap(RecPath + "\\frame/pBrakeApp.jpg"),
                new Bitmap(RecPath + "\\frame/pBrakeIsolated.jpg"),
                new Bitmap(RecPath + "\\frame/pBrakeReleased.jpg")
            };

            var constImages =
                bitmaps.Select((s, i) => new RectangleImage() {Image = s, OutLineRectangle = rectArr[i]});

            var txts = Enumerable.Range(41, txtRects.Length).Select(s => GlobleParam.ResManager.GetString("String" + s));

            var constInfos =
                 txts.Select(
                     (s, i) =>
                         new Label()
                         {
                             OutLineRectangle = txtRects[i],
                             Brush = GdiCommon.MediumGreyBrush,
                             Font = GdiCommon.Txt14Font,
                             Text = s,
                         }).ToList();

            constInfos.ForEach(e => e.Format.Alignment = StringAlignment.Near);

            m_ConstInfoItems =
                constImages.Select((s, i) => new Tuple<Label, RectangleImage>(constInfos[i], s)).ToList();
        }
    }
}