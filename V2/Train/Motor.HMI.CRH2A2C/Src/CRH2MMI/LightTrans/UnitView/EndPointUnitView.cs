using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util;

namespace CRH2MMI.LightTrans.UnitView
{
    /// <summary>
    /// 端
    /// </summary>
    internal class EndPointUnitView : UnitViewBase
    {
        /// <summary>
        /// 默认整体大小
        /// </summary>
        public static Size DefaultSize = new Size(42, 150);

        /// <summary>
        /// 默认端的大小 = new Size(42, 40);
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static Size DefaultEndSize;

        /// <summary>
        /// = new Size(20, 78);
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static Size DefaultBCUCISize;


        static EndPointUnitView()
        {
            DefaultEndSize = new Size(DefaultSize.Width, 40);
            DefaultBCUCISize = new Size(DefaultSize.Width / 2 - 1, 78);
        }



        public EndPointUnitView(Trans parent, List<List<string>> inbools)
            : base(parent)
        {
            m_OutLineRectangle = new Rectangle(0, 0, DefaultSize.Width, DefaultSize.Height);

            m_Lines = new List<Line>();
            m_ConstInfo = new List<CommonInnerControlBase>()
            {
                new GDIRectText()
                {
                    Text = "端",
                    TextFormat = new StringFormat() {LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center},
                    DrawFont = new Font(CommonResouce.DefalutFont.FontFamily, 16, FontStyle.Bold),
                    // Location = (0,0)
                    Size = DefaultEndSize,
                    NeedDarwOutline = true,
                }
            };

            if (inbools == null || !inbools.Any())
            {
                LogMgr.Error("Can't build 端 without bcu or ci.");
                return;
            }

            InitBCUAndCI(inbools);

        }

        /// <summary>
        /// 获得连接线的点
        /// </summary>
        /// <returns></returns>
        public override List<Point> GetConnectionPoints()
        {
            var endp = m_ConstInfo.First();

            var x1 = endp.OutLineRectangle.Left;
            var x2 = endp.OutLineRectangle.Right;
            var y1 = endp.OutLineRectangle.Height/2 - DefaultLineInterval/2 + endp.Location.Y;
            var y2 = endp.OutLineRectangle.Height/2 + DefaultLineInterval/2 + endp.Location.Y;

            return new List<Point>() {new Point(x1, y1), new Point(x1, y2), new Point(x2, y1), new Point(x2, y2)};
        }

        private void InitBCUAndCI(List<List<string>> inbools)
        {
            var bcuLocation = new Point(0, DefaultSize.Height - DefaultBCUCISize.Height);

            var bcu = CreateBCU();

            var endpoint = m_ConstInfo[0];

            if (inbools.Count == 1)
            {
                bcu.OutLineRectangle = new Rectangle(new Point(bcuLocation.X + DefaultBCUCISize.Width / 2, bcuLocation.Y), DefaultBCUCISize);
                var linePoint = new Point(DefaultEndSize.Width / 2 - DefaultLineInterval / 2, endpoint.OutLineRectangle.Bottom);
                m_Lines.AddRange(CreateLines(linePoint, DefaultSize.Height - DefaultBCUCISize.Height - DefaultEndSize.Height, inbools[0]));
            }

            else if (inbools.Count == 2)
            {
                bcu.OutLineRectangle = new Rectangle(new Point(bcuLocation.X, bcuLocation.Y), DefaultBCUCISize);
                var ci = CreateCi(bcuLocation);
                m_ConstInfo.Add(ci);

                var linePoint = new Point(DefaultBCUCISize.Width / 2 - DefaultLineInterval / 2, endpoint.OutLineRectangle.Bottom);
                m_Lines.AddRange(CreateLines(linePoint, DefaultSize.Height - DefaultBCUCISize.Height - DefaultEndSize.Height, inbools[0]));

                linePoint = new Point(DefaultBCUCISize.Width+ DefaultBCUCISize.Width / 2 - DefaultLineInterval/2 + 2 , endpoint.OutLineRectangle.Bottom);
                m_Lines.AddRange(CreateLines(linePoint, DefaultSize.Height - DefaultBCUCISize.Height - DefaultEndSize.Height, inbools[1]));
            }
            else
            {
                LogMgr.Warn("Ignore inbools where index larger than 2");
            }

            m_ConstInfo.Add(bcu);
        }

        private IEnumerable<Line> CreateLines(Point startPoint, int length, List<string> inbools)
        {
            var line = new Line()
            {
                StartPoint = startPoint,
                EndPoint = new Point(startPoint.X, startPoint.Y + length),
                RefreshAction = o => GetLinePen((Line)o, inbools),
            };

            return new[]
            {
                line, new Line()
                {
                    StartPoint = new Point(startPoint.X + DefaultLineInterval, startPoint.Y),
                    EndPoint = new Point(startPoint.X + DefaultLineInterval, startPoint.Y + length),
                    RefreshAction = o => ((Line) o).LinePen = line.LinePen
                },
            };
        }

        private static GDIRectText CreateBCU()
        {
            return new GDIRectText()
            {
                Text = "B C U",
                TextFormat =
                    new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                    },
                NeedDarwOutline = true,
            };
        }

        private GDIRectText CreateCi(Point bcuLocation)
        {
            return new GDIRectText()
            {
                Text = "C   I",
                TextFormat =
                    new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                    },
                NeedDarwOutline = true,
                OutLineRectangle = new Rectangle(new Point(bcuLocation.X + DefaultBCUCISize.Width + 2, bcuLocation.Y), DefaultBCUCISize)
            };
        }



    }
}
