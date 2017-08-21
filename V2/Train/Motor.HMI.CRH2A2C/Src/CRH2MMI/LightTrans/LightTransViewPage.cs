using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util;using CommonUtil.Util.Extension;
using CRH2MMI.LightTrans.UnitView;

namespace CRH2MMI.LightTrans
{
    class LightTransViewPage : CommonInnerControlBase
    {
        private List<UnitViewBase> m_UnitViewBases;

        private List<Line> m_Lines;

        private List<CommonInnerControlBase> m_ConstInfos;
        private const int ConstLineLegth = 50;
        private const int ConstLineHeight = ConstInfoHeigth + 10;
        private const int ConstInfoHeigth = 540;

        private readonly Trans m_Trans;

        public LightTransPageID PageID { private set; get; }

        public LightTransPage PageConfig { get; private set; }

        public string PageText { private set; get; }

        public LightTransViewPage(Trans trans,LightTransPage pageConfigConfig)
        {
            m_Trans = trans;
            PageConfig = pageConfigConfig;
            PageText = "后 8 车厢";
            if (pageConfigConfig.Page == LightTransPageID.Page2)
            {
                PageText = "前 8 车厢";
            }
            PageID = pageConfigConfig.Page;
            m_UnitViewBases = new List<UnitViewBase>();
            m_Lines = new List<Line>();
            InitConstInfos();
            InitUnits();

        }

        public void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            var mids = m_UnitViewBases.FindAll(f => f is MiddleUnitView);
            var midx = ((float)mids.First().Location.X + mids.Last().Location.X) / 2 ;
            var mat1 = MatrixHelper.CreateTurnMatrix(midx, TurnOrientation.Horizontal);
            midx = ((float) mids.First().Location.X + mids.Last().Location.X + mids.Last().Size.Width)/2 ;
            var mat2 = MatrixHelper.CreateTurnMatrix(midx, TurnOrientation.Horizontal);

            m_UnitViewBases.ForEach(e =>
                                    {
                                        var mat = mat1;
                                        if (e is EndPointUnitView)
                                        {
                                            mat = mat2.Clone();
                                            mat.Translate(-e.Size.Width, 0, MatrixOrder.Append);
                                        }
                                        e.Location = mat.TransformPoint(e.Location);
                                    });
            m_Lines.ForEach(e =>
            {
                e.StartPoint = mat2.TransformPoint(e.StartPoint);
                e.EndPoint = mat2.TransformPoint(e.EndPoint);
            });

        }

        private void InitConstInfos()
        {
            var startx = 140;
            var txtSize = new Size(60, 20);
            const int interval = 20;
            m_ConstInfos = new List<CommonInnerControlBase>()
            {
                new GDIRectText() {Text = "(", OutLineRectangle = new Rectangle(new Point(startx, ConstInfoHeigth), new Size(20, 20))},
                new Line(new Point(startx += interval, ConstLineHeight), new Point(startx += ConstLineLegth, ConstLineHeight))
                {
                    LinePen = TransResource.GreenPen
                },
                new GDIRectText()
                {
                    Text = ":  正常",
                    Bold = true,
                    OutLineRectangle = new Rectangle(new Point(startx += interval, ConstInfoHeigth), txtSize)
                },
                new Line(new Point(startx += (interval + txtSize.Width), ConstLineHeight), new Point(startx += ConstLineLegth, ConstLineHeight))
                {
                    LinePen = TransResource.RedPen
                },
                new GDIRectText()
                {
                    Text = ":  异常",
                    Bold = true,
                    OutLineRectangle = new Rectangle(new Point(startx += interval, ConstInfoHeigth), txtSize)
                },
                new Line(new Point(startx +=  (interval + txtSize.Width), ConstLineHeight), new Point(startx += ConstLineLegth, ConstLineHeight))
                {
                    LinePen = TransResource.DotPen
                },
                new GDIRectText()
                {
                    Text = ":  不知 )",
                    Bold = true,
                    OutLineRectangle = new Rectangle(new Point(startx + interval, ConstInfoHeigth), new Size(txtSize.Width + interval,txtSize.Height))
                },
            };
        }

        private void InitUnits()
        {
            m_Trans.MoveTrainLocation(PageConfig.TrainViewLocation.Rectangle.Location);
            var config = PageConfig.LightUnitConfigs;
            var cars = m_Trans.TrainView.Cars;

            var builder = new LightUnitBuilder(m_Trans, cars, config);
            m_UnitViewBases = builder.BuildUnitViews();
            m_Lines = builder.BuildConnetionLines(m_UnitViewBases);
        }

        public override void OnDraw(Graphics g)
        {
            m_ConstInfos.ForEach(e => e.OnDraw(g));
            m_UnitViewBases.ForEach(e => e.OnPaint(g));
            m_Lines.ForEach(e => e.OnPaint(g));
        }

    }
}
