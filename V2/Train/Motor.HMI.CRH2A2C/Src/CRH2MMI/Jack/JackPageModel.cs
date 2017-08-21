using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common.Global;
using CommonUtil.Controls;

namespace CRH2MMI.Jack
{
    class JackPageModel 
    {
        public int CarNo { private set; get; }

        public int Page { set; get; }

        public List<GDIRectText> PageTexts { get; private set; }

        public List<Line> Lines { get; private set; }

        protected static readonly Point PageFirstPoint = new Point(20, 114);

        /// <summary>
        /// 文本框的大小 
        /// </summary>
        protected static readonly Size Interval = new Size(2, 30);
        protected static readonly Pen LinePen = new Pen(Color.White, 2);

        public JackPageModel(int carNo)
        {
            CarNo = carNo;
            Lines = new List<Line>();
            PageTexts = new List<GDIRectText>();
        }

        public void OnPaint(Graphics g)
        {
            Lines.ForEach(e => e.OnDraw(g));

            PageTexts.ForEach(e => e.OnPaint(g));
        }


        public void Init(List<JackUnit> jackUnits,JackInfo jackInfo, Size textBoxSize)
        {

            PageTexts = jackUnits.Select(s => new GDIRectText()
            {
                OutLineRectangle =
                    new Rectangle(
                        PageFirstPoint.X + s.X*(textBoxSize.Width + Interval.Width),
                        PageFirstPoint.Y + s.Y*(textBoxSize.Height + Interval.Height),
                        textBoxSize.Width,
                        textBoxSize.Height),
                BkColor = Color.WhiteSmoke,
                TextColor = Color.Black,
                TextFormat = new StringFormat()
                {
                    FormatFlags = StringFormatFlags.DirectionRightToLeft,
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                },
                Text = s.Name,
                RefreshAction = o =>
                {
                    var t = (GDIRectText) o;
                    var idx =
                        GlobalResource.Instance.GetInBoolIndexs(s).First();
                    t.BkColor = jackInfo.BoolList[idx]
                        ? s.TextBkColors[1]
                        : s.TextBkColors[0];
                }
            }).ToList();

            var midY = PageFirstPoint.Y + textBoxSize.Height + Interval.Height / 2;
            var ys = new List<int>() { PageFirstPoint.Y + textBoxSize.Height, PageFirstPoint.Y + textBoxSize.Height + Interval.Height };
            foreach (var jup in jackUnits.FindAll(s => s.RelationShip != 0 ).GroupBy(g => g.RelationShip))
            {
                var tmpX = new List<int>(jup.Count());
                foreach (var jackUnit in jup)
                {
                    var x = (int) (PageFirstPoint.X + (jackUnit.X + 0.5f)*(textBoxSize.Width + Interval.Width));
                    var y = jackUnit.Y == 1 ? ys[1] : ys[0];
                    tmpX.Add(x);
                    Lines.Add(new Line(new Point(x, y), new Point(x, midY)));
                }
                if (tmpX.Count >= 2)
                {
                    Lines.Add(new Line(new Point(tmpX.Min(), midY), new Point(tmpX.Max(), midY)));
                }
            }
        }
    }
}
