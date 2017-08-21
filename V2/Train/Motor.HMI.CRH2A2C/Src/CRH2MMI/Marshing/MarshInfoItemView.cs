using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CRH2MMI.Common.Util;
using CRH2MMI.TrainLineNo;

namespace CRH2MMI.Marshing
{
    class MarshInfoItemView : MarshItemView
    {
        private readonly RectangleF m_BrakeActionRectangleF = new RectangleF(320, 530, 160, 40);

        private Rectangle m_NextPageButtonRegion = new Rectangle(672, 529, 115, 42);

        private List<CommonInnerControlBase> m_ConstInfoCollection;

        private List<CommonInnerControlBase> m_ActiveTextCollection;

        private List<GDIRectText> m_SensorInfoCollection;

        public MarshInfoItemView(Marsh parentView)
            : base(MarshingPage.MarshingInfo, parentView)
        {
            InitConstInfoCollection();

            InitActiveTextCollection();

            InitSensorInfoCollection();
        }

        public override void SwitchToThis()
        {
            Title.TitleText = "联解编组信息";
        }

        public override void OnDraw(Graphics g)
        {
            var point = Point.Empty;
            g.DrawImage(Img[0], 0, 147, Img[0].Width, Img[0].Height);

            if (BValue[0])
            {
                point.X = 318;
                point.Y = 383;
                g.DrawString("联挂准备", CRH2Resource.Font24, CRH2Resource.YellowBrush, point);
            }
            if (BValue[1])
            {
                g.FillRectangle(CRH2Resource.YellowBrush, new RectangleF(320, 530, 160, 40));
                g.DrawString("制动动作", CRH2Resource.Font24, CRH2Resource.BlackBrush, m_BrakeActionRectangleF, CRH2Resource.DrawFormat);
            }

            m_ConstInfoCollection.ForEach(e => e.OnDraw(g));

            m_ActiveTextCollection.ForEach(e => e.OnPaint(g));

            m_SensorInfoCollection.ForEach(e => e.OnPaint(g));
        }

        public override bool OnMouseDown(Point point)
        {
            if (m_NextPageButtonRegion.Contains(point))
            {
                OnMarshButtonClick(this, MarshButtonType.TurnNext);
                return true;
            }

            return false;
        }

        private void InitSensorInfoCollection()
        {
            m_SensorInfoCollection = new List<GDIRectText>();
            for (int i = 0; i < 4; i++)
            {
                m_SensorInfoCollection.Add(new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(610, 240 + 27*i, 165, 27),
                    Text = "传感器故障" + i*27,
                    TextColor = Color.Red,
                });
            }
        }

        private void InitConstInfoCollection()
        {
            m_ConstInfoCollection = new List<CommonInnerControlBase>()
            {
                new GDIRectText {OutLineRectangle = new Rectangle(28, 149, 48, 27), Text = "车次", Bold = true},
                new GDIRectText {OutLineRectangle = new Rectangle(233, 185, 73, 28), Text = "km/h/s"},
                new GDIRectText {OutLineRectangle = new Rectangle(25, 181, 94, 36), Text = "加/减速度", Bold = true},
                new GDIRectText
                {
                    OutLineRectangle = new Rectangle(20, 310, 57, 50),
                    Text = "距离",
                    DrawFont = CRH2Resource.Font16,
                    Bold = true
                },
                new GDIRectText {OutLineRectangle = new Rectangle(549, 321, 30, 30), Text = "m"},
                new GDIRectText {OutLineRectangle = new Rectangle(631, 213, 110, 32), Text = "传感器信息",}
            };
        }

        private void InitActiveTextCollection()
        {
            m_ActiveTextCollection = new List<CommonInnerControlBase>()
            {
                new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(106, 148, 181, 34),
                    TextColor = CRH2Resource.YellowBrush.Color,
                    RefreshAction = o => { ((GDIRectText) o).Text = TNSET.TrainLine; }
                },
                new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(121, 185, 117, 34),
                    DrawFont = CRH2Resource.Font12,
                    TextColor = CRH2Resource.YellowBrush.Color,
                    TextFormat = CRH2Resource.DrawFormat,
                    RefreshAction = o => ((GDIRectText) o).Text = FValue[0].ToString("#,#0.0"),
                },
                new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(108, 232, 429, 112),
                    DrawFont = CRH2Resource.Font64,
                    TextColor = CRH2Resource.YellowBrush.Color,
                    TextFormat = CRH2Resource.DrawFormat,
                    RefreshAction = o => ((GDIRectText) o).Text = FValue[1].ToString("#,#0.0"),
                },
            };
        }
    }
}