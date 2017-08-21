using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI.CommunicationOverview
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class CommOverview : HMIBase
    {
        private List<GDIRectText> m_StateTexts;

        private RectangleImage m_BkgroudRectangleImage;

        public override string GetInfo()
        {
            return "Communication Overview";
        }

        protected override bool Initalize()
        {
            m_BkgroudRectangleImage = new RectangleImage { OutLineRectangle = new Rectangle(120, 144, 632, 303), Image = Images[0] };

            var regions = GetStateRegions();

            m_StateTexts =
                GlobleParam.Instance.DetailConfig.CommunicationOverviewConfig.CommunicationOverviewItems.Where(
                    w => !string.IsNullOrWhiteSpace(w.LogicIndexName)).Select(s => new GDIRectText
                    {
                        OutLineRectangle = regions[s.Horizontal, s.Vertical],
                        Text = s.DisplayName,
                        TextColor = Color.Black,
                        BkColor = GdiCommon.OceanBluePen.Color,
                        TextFormat = GdiCommon.CenterFormat,
                        RefreshAction = o =>
                        {
                            var txt = (GDIRectText) o;
                            txt.BkColor = BoolList[GlobleParam.Instance.FindInBoolIndex(s.LogicIndexName)]
                                ? Color.Red : GdiCommon.OceanBluePen.Color;
                        }
                    }).ToList();


            UIObj.InBoolList.AddRange(GlobleParam.Instance.DetailConfig.CommunicationOverviewConfig.CommunicationOverviewItems.Where(
                    w => !string.IsNullOrWhiteSpace(w.LogicIndexName)).Select(s => GlobleParam.Instance.FindInBoolIndex(s.LogicIndexName)));
            

            return true;
        }

        private Rectangle[,] GetStateRegions()
        {
            var size = new Size(32 * 2, 22);
            var reigons = new Rectangle[7, 8];

            var ys = Enumerable.Range(0, 8).Select(s => 177 + 35 * s).ToList();

            var xs = new List<int> { 126 };

            var intervals = GetXInterval().GetEnumerator();
            for (int i = 0; i < 6; i++)
            {
                intervals.MoveNext();
                xs.Add(xs.Last() + intervals.Current);
            }


            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    reigons[i, j] = Rectangle.Inflate(new Rectangle(xs[i], ys[j], 0, size.Height), size.Width / 2, 0);
                }
            }

            return reigons;
        }

        public override void paint(Graphics g)
        {
            m_BkgroudRectangleImage.OnDraw(g);

            m_StateTexts.ForEach(e => e.OnPaint(g));
        }

        private IEnumerable<int> GetXInterval()
        {
            yield return 100;
            yield return 100;
            yield return 128;
            yield return 97;
            yield return 105;
            yield return 96;
        }
    }
}