using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using CRH2MMI.Common.Global;

namespace CRH2MMI.DoorInfo
{
    /// <summary>
    /// 车门信息背景
    /// </summary>
    class DoorInfoBack : CommonInnerControlBase
    {
        private Arrows m_DirectionArrows;

        private List<GDIRectText> m_ConstInfos;

        private List<GraphicsPath> m_BkRegions;
        private List<GraphicsPath> m_BkRegions1;

        public static readonly Size DefaultSize = new Size(800, 626);

        public static readonly Size DefaultInfoSize = new Size(80, 27);

        public static readonly Size DefaultBkRegionSizeUnit = new Size(42, 286);

        public DoorInfoBack()
        {
            InitalizeConstInfos();

            InitalizeBkRegions();

            InitalizeArrow();

            OutLineChanged += OnOutLineChanged;

        }


        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            SetOutLine(new Rectangle(Location, DefaultSize));
            var mat = new Matrix();
            mat.Translate(Location.X, Location.Y);
            m_BkRegions.ForEach(e => e.Transform(mat));
            m_BkRegions1.ForEach(e => e.Transform(mat));
            m_ConstInfos.ForEach(e => e.Location = mat.TransformPoint(e.Location));

            m_DirectionArrows.Location = mat.TransformPoint(m_DirectionArrows.Location);
        }

        private void InitalizeBkRegions()
        {
            m_BkRegions = new List<GraphicsPath>();
            m_BkRegions1 = new List<GraphicsPath>();
            var config = GlobalInfo.CurrentCRH2Config.DoorConfig;
            var is8Car = config.DoorInBoolModels.Max(m => m.CarNo) <= 7;
            InitalizeBkRegionsOf(is8Car ? 8 : 16);
            InitalizeBkRegionsOf1(is8Car ? 8 : 16);
            var mat = new Matrix();
            mat.Translate(is8Car ? 210 : 80, 70);
            m_BkRegions.ForEach(e => e.Transform(mat));
            m_BkRegions1.ForEach(e => e.Transform(mat));
        }
        private void InitalizeBkRegionsOf1(int carCount)
        {
            const int r = 8;
            var tmp = new Rectangle(new Point(r, 0), new Size(DefaultBkRegionSizeUnit.Width - r, DefaultBkRegionSizeUnit.Height));
            var location = Point.Empty;
            var carFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            var gp1 = new GraphicsPath();
            gp1.AddLine(tmp.Left, tmp.Top, tmp.Right, tmp.Top);
            gp1.AddLine(tmp.Right, tmp.Top, tmp.Right, tmp.Bottom);
            gp1.AddLine(tmp.Right, tmp.Bottom, tmp.Left, tmp.Bottom);
            gp1.AddArc(0, DefaultBkRegionSizeUnit.Height - r * 2, r * 2, r * 2, 90, 90);
            gp1.AddLine(0, r, 0, DefaultBkRegionSizeUnit.Height - r);
            gp1.AddArc(0, 0, r * 2, r * 2, 180, 90);
            gp1.AddString(carCount.ToString(CultureInfo.InvariantCulture), SystemFonts.DefaultFont.FontFamily, 1, 20, new Point(DefaultBkRegionSizeUnit.Width / 2 + location.X, DefaultBkRegionSizeUnit.Height / 2 + location.Y), carFormat);
            m_BkRegions1.Add(gp1);

            for (int i = carCount - 2; i > 0; i--)
            {
                location.Offset(DefaultBkRegionSizeUnit.Width + 2, 0);
                var gp = new GraphicsPath();
                gp.AddRectangle(new Rectangle(location, DefaultBkRegionSizeUnit));
                gp.AddString((i + 1).ToString(CultureInfo.InvariantCulture), SystemFonts.DefaultFont.FontFamily, 1, 20, new Point(DefaultBkRegionSizeUnit.Width / 2 + location.X, DefaultBkRegionSizeUnit.Height / 2 + location.Y), carFormat);
                m_BkRegions1.Add(gp);
            }

            location.Offset(DefaultBkRegionSizeUnit.Width + 2, 0);
            var tmp2 = new Rectangle(new Point(location.X, location.Y), new Size(DefaultBkRegionSizeUnit.Width - r, DefaultBkRegionSizeUnit.Height));
            var gp2 = new GraphicsPath();
            gp2.AddLine(tmp2.Right, tmp2.Bottom, tmp2.Left, tmp2.Bottom);
            gp2.AddLine(tmp2.Left, tmp2.Bottom, tmp2.Left, tmp2.Top);
            gp2.AddLine(tmp2.Left, tmp2.Top, tmp2.Right, tmp2.Top);
            gp2.AddArc(location.X + DefaultBkRegionSizeUnit.Width - r * 2, location.Y, r * 2, r * 2, -90, 90);
            gp2.AddLine(location.X + DefaultBkRegionSizeUnit.Width, location.Y + r, location.X + DefaultBkRegionSizeUnit.Width, location.Y + DefaultBkRegionSizeUnit.Height - r);
            gp2.AddArc(location.X + DefaultBkRegionSizeUnit.Width - r * 2, location.Y + DefaultBkRegionSizeUnit.Height - r * 2, r * 2, r * 2, 0, 90);
            gp2.AddString("1", SystemFonts.DefaultFont.FontFamily, 1, 20, new Point(DefaultBkRegionSizeUnit.Width / 2 + location.X, DefaultBkRegionSizeUnit.Height / 2 + location.Y), carFormat);
            m_BkRegions1.Add(gp2);
        }
        private void InitalizeBkRegionsOf(int carCount)
        {
            const int r = 8;
            var tmp = new Rectangle(new Point(r, 0), new Size(DefaultBkRegionSizeUnit.Width - r, DefaultBkRegionSizeUnit.Height));
            var location = Point.Empty;
            var carFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            var gp1 = new GraphicsPath();
            gp1.AddLine(tmp.Left, tmp.Top, tmp.Right, tmp.Top);
            gp1.AddLine(tmp.Right, tmp.Top, tmp.Right, tmp.Bottom);
            gp1.AddLine(tmp.Right, tmp.Bottom, tmp.Left, tmp.Bottom);
            gp1.AddArc(0, DefaultBkRegionSizeUnit.Height - r * 2, r * 2, r * 2, 90, 90);
            gp1.AddLine(0, r, 0, DefaultBkRegionSizeUnit.Height - r);
            gp1.AddArc(0, 0, r * 2, r * 2, 180, 90);
            gp1.AddString("1", SystemFonts.DefaultFont.FontFamily, 1, 20, new Point(DefaultBkRegionSizeUnit.Width / 2 + location.X, DefaultBkRegionSizeUnit.Height / 2 + location.Y), carFormat);
            m_BkRegions.Add(gp1);

            for (int i = 1; i < carCount - 1; i++)
            {
                location.Offset(DefaultBkRegionSizeUnit.Width + 2, 0);
                var gp = new GraphicsPath();
                gp.AddRectangle(new Rectangle(location, DefaultBkRegionSizeUnit));
                gp.AddString((i + 1).ToString(CultureInfo.InvariantCulture), SystemFonts.DefaultFont.FontFamily, 1, 20, new Point(DefaultBkRegionSizeUnit.Width / 2 + location.X, DefaultBkRegionSizeUnit.Height / 2 + location.Y), carFormat);
                m_BkRegions.Add(gp);
            }

            location.Offset(DefaultBkRegionSizeUnit.Width + 2, 0);
            var tmp2 = new Rectangle(new Point(location.X, location.Y), new Size(DefaultBkRegionSizeUnit.Width - r, DefaultBkRegionSizeUnit.Height));
            var gp2 = new GraphicsPath();
            gp2.AddLine(tmp2.Right, tmp2.Bottom, tmp2.Left, tmp2.Bottom);
            gp2.AddLine(tmp2.Left, tmp2.Bottom, tmp2.Left, tmp2.Top);
            gp2.AddLine(tmp2.Left, tmp2.Top, tmp2.Right, tmp2.Top);
            gp2.AddArc(location.X + DefaultBkRegionSizeUnit.Width - r * 2, location.Y, r * 2, r * 2, -90, 90);
            gp2.AddLine(location.X + DefaultBkRegionSizeUnit.Width, location.Y + r, location.X + DefaultBkRegionSizeUnit.Width, location.Y + DefaultBkRegionSizeUnit.Height - r);
            gp2.AddArc(location.X + DefaultBkRegionSizeUnit.Width - r * 2, location.Y + DefaultBkRegionSizeUnit.Height - r * 2, r * 2, r * 2, 0, 90);
            gp2.AddString(carCount.ToString(CultureInfo.InvariantCulture), SystemFonts.DefaultFont.FontFamily, 1, 20, new Point(DefaultBkRegionSizeUnit.Width / 2 + location.X, DefaultBkRegionSizeUnit.Height / 2 + location.Y), carFormat);
            m_BkRegions.Add(gp2);
        }


        private void InitalizeArrow()
        {
            var bound = m_BkRegions[0].GetBounds();
            m_DirectionArrows = new Arrows()
            {
                OutLineRectangle = new Rectangle(new Point((int)(bound.Left - 10), (int)(bound.Top + bound.Height / 2 - 13)), new Size(10, 20)),
                Direction = Direction.Left,
            };
        }

        public void RefreshDriverRoom(Direction direction)
        {
            if (m_DirectionArrows.Direction != direction)
            {
                var bound = m_BkRegions[m_BkRegions.Count / 2].GetBounds();
                var mat = MatrixHelper.CreateTurnMatrix(bound.Left - 5, TurnOrientation.Horizontal);
                var dap = new[] { m_DirectionArrows.Location };
                mat.TransformPoints(dap);
                m_DirectionArrows.Direction = direction;
                m_DirectionArrows.Location = dap[0];
            }
        }

        private void InitalizeConstInfos()
        {
            var location = Point.Empty;
            m_ConstInfos = new List<GDIRectText>()
                           {
                               new GDIRectText()
                               {
                                   Text = "压紧 1 位",
                                   OutLineRectangle = new Rectangle(location, DefaultInfoSize),
                                   TextColor = Color.White,
                                   BkColor = Color.Black,
                               },
                               new GDIRectText()
                               {
                                   Text = "压紧 3 位",
                                   OutLineRectangle = new Rectangle(location = location.Translate(0, DefaultInfoSize.Height), DefaultInfoSize),
                                   TextColor = Color.White,
                                   BkColor = Color.Black,
                               },
                               new GDIRectText()
                               {
                                   Text = "车门 1 位",
                                   OutLineRectangle = new Rectangle(location = location.Translate(0, DefaultInfoSize.Height+35), DefaultInfoSize),
                                   TextColor = Color.White,
                                   BkColor = Color.Black,
                               },
                               new GDIRectText()
                               {
                                   Text = "车门 3 位",
                                   OutLineRectangle = new Rectangle(location = location.Translate(0, DefaultInfoSize.Height+25), DefaultInfoSize),
                                   TextColor = Color.White,
                                   BkColor = Color.Black,
                               },
                               new GDIRectText()
                               {
                                   Text = "车门 2 位",
                                   OutLineRectangle = new Rectangle(location = location.Translate(0, DefaultInfoSize.Height+70), DefaultInfoSize),
                                   TextColor = Color.White,
                                   BkColor = Color.Black,
                               },
                               new GDIRectText()
                               {
                                   Text = "车门 4 位",
                                   OutLineRectangle = new Rectangle(location = location.Translate(0, DefaultInfoSize.Height+25), DefaultInfoSize),
                                   TextColor = Color.White,
                                   BkColor = Color.Black,
                               },
                               new GDIRectText()
                               {
                                   Text = "压紧 2 位",
                                   OutLineRectangle = new Rectangle(location = location.Translate(0, DefaultInfoSize.Height+55), DefaultInfoSize),
                                   TextColor = Color.White,
                                   BkColor = Color.Black,
                               },
                               new GDIRectText()
                               {
                                   Text = "压紧 4 位",
                                   OutLineRectangle = new Rectangle( location.Translate(0, DefaultInfoSize.Height), DefaultInfoSize),
                                   TextColor = Color.White,
                                   BkColor = Color.Black,
                               },
                           };
        }


        public override void OnDraw(Graphics g)
        {
            m_ConstInfos.ForEach(e => e.OnDraw(g));
            //if (GlobalParam.Instance.IsReversalTrain)
            //{
            //    m_BkRegions1.ForEach(e => g.FillPath(Brushes.White, e));
            //}
            //else
            {
                m_BkRegions.ForEach(e => g.FillPath(Brushes.White, e));
            }


            m_DirectionArrows.OnDraw(g);
        }

        public Point GetLocationOf(DoorInBoolModel model)
        {
            var bound = m_BkRegions[model.CarNo].GetBounds();

            string name = model.Type == DoorOperType.OpenClose ? "车门 {0} 位" : "压紧 {0} 位";

            name = string.Format(name, (int)model.DoorLocation + 1);

            var txt = m_ConstInfos.Find(f => f.Text == name);

            return new Point((int)bound.Left, txt.OutLineRectangle.Top);

        }
    }
}
