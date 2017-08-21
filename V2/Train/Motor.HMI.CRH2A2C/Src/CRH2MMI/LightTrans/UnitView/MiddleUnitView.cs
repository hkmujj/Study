using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;

namespace CRH2MMI.LightTrans.UnitView
{
    class MiddleUnitView : UnitViewBase
    {
        public static Size DefaultSize = new Size(60, 130);

        public static Size DefaultInnerTextSize = new Size(32, 32);

        public UnitType Type { private set; get; }

        public enum UnitType
        {
            /// <summary>
            /// 1 在上面
            /// </summary>
            UpIs1,
            UpIs2,
        }

        public MiddleUnitView(UnitType type, Trans trans, List<string> inbools)
            : base(trans)
        {
            Type = type;
            m_OutLineRectangle = new Rectangle(0, 0, DefaultSize.Width, DefaultSize.Height);

            InitMiddlText();

            Init12Text(type);

            InitLine(inbools);
        }

        private void InitLine(List<string> inbools)
        {
            var y1 = m_ConstInfo.Min(m => m.OutLineRectangle.Bottom);
            var y2 = m_ConstInfo.Max(m => m.OutLineRectangle.Top);
            var x = DefaultSize.Width/2;

            m_Lines.Add(new Line()
            {
                StartPoint = new Point(x, y1),
                EndPoint = new Point(x, y2),
                RefreshAction = o => GetLinePen((Line) o, inbools)
            });
        }

        private void Init12Text(UnitType type)
        {
            List<string> texts;

            switch (type)
            {
                case UnitType.UpIs1:
                    texts = new List<string>() {"1", "2"};
                    break;
                case UnitType.UpIs2:
                    texts = new List<string>() {"2", "1"};
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
            Init12Text(texts);

        }

        private void Init12Text(List<string> texts)
        {
            var x = DefaultSize.Width/2 - DefaultInnerTextSize.Width/2;
            var interval = x;
            var y = DefaultSize.Height - interval*2 - DefaultInnerTextSize.Height*2;

            for (int i = 0; i < texts.Count; i++)
            {
                m_ConstInfo.Add(new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(new Point(x, y + i*(DefaultInnerTextSize.Height + interval)), DefaultInnerTextSize),
                    Text = texts[i],
                    NeedDarwOutline = true,
                    Bold = true,
                   TextFormat= new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                    },
                });
            }
        }

        private void InitMiddlText()
        {
            m_ConstInfo.Add(new GDIRectText()
            {
                Text = "\r\n中",
                TextFormat =
                    new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Near,
                    },
                OutLineRectangle = OutLineRectangle,
                NeedDarwOutline = true,
                Bold = true,
            });
        }

        public override List<Point> GetConnectionPoints()
        {
            var main = m_ConstInfo.First();

            var x1 = main.OutLineRectangle.Left + main.OutLineRectangle.Width/2 - DefaultLineInterval/2;
            var x2 = main.OutLineRectangle.Left + main.OutLineRectangle.Width/2 + DefaultLineInterval/2;
            var y1 = main.OutLineRectangle.Top;
            var y2 = main.OutLineRectangle.Bottom;

            return new List<Point>() {new Point(x1, y1), new Point(x2, y1), new Point(x2, y2), new Point(x1, y2)};
        }
    }
}
