using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using CommonUtil.Model;
using CommonUtil.Util;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CommonUtil.Controls;
using CRH2MMI.Common.Util;


namespace CRH2MMI.PowerVoltage
{
    class PowerVoltagePageOne : PowerVoltagePageBase
    {
        private List<PowerVoltageModel> m_ModelConfigs;
        private int m_MaxVoltage;
        // ReSharper disable once InconsistentNaming
        private const float m_MaxHeight = 137.5f*2f;
        private List<Line> m_CoordinateLines;
        private Coordinate m_Coordinate;

        public PowerVoltagePageOne(Power power)
            : base(power)
        {
            Init();
        }

        private void Init()
        {
            m_ModelConfigs = GlobalInfo.CurrentCRH2Config.PowerVoltageConfig.FirstPageModels;
            m_MaxVoltage = m_ModelConfigs.Max(m => m.MaxVoltage);

            m_Coordinate = new Coordinate(m_CoordinateOrigin) { XOffsets = new[] { -5f, 680F }, YOffsets = new[] { 0, -320f } };

            m_CoordinateLines = new List<Line>();
            for (var i = 0; i < 3; i++)
            {
                var yoff = (int)m_MaxHeight * i / 2;
                m_CoordinateLines.Add(new Line(new Point(m_CoordinateOrigin.X - 5, 175 + yoff), new Point(m_CoordinateOrigin.X, 175 + yoff)));
            }

            InitModel();

            InitChangePageBtn();
        }

        private void InitChangePageBtn()
        {
            m_ChangePageBtn = new CRH2Button()
            {
                OutLineRectangle = new Rectangle(549, 525, 117, 43),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                ButtonDownEvent = (sender, args) => HandleUtil.OnHandle(ChangePage, this, null),
                TextColor = Color.White,
                Text = "下一页面",
            };
        }

        private void InitModel()
        {
            for (int index = 0; index < m_ModelConfigs.Count; index++)
            {
                var modelConfig = m_ModelConfigs[index];

                m_ConstInfo.Add(new GDIRectText()
                {
                    Text = modelConfig.VoltageType,
                    Location = new Point(modelConfig.FirstAbsX , 134),
                    Size = new Size(modelConfig.TextWidth* modelConfig.VoltageConfigs.Count, 38),
                    TextColor = Color.White,
                    TextFormat =
                        new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near }
                });

                for (int i = 0; i < modelConfig.VoltageConfigs.Count; i++)
                {
                    var voltageConfig = modelConfig.VoltageConfigs[i];
                    var carNameLocation = new Point((int) (modelConfig.FirstAbsX + (i + 0.5)*modelConfig.TextWidth - 15),
                        modelConfig.FirstAbsY - 25);
                    m_ConstInfo.Add(new GDIRectText()
                    {
                        Text = voltageConfig.CarName.ToString(),
                        Location = carNameLocation,
                        Size = new Size(30, 15),
                        TextColor = Color.White,
                        TextFormat =
                            new StringFormat() {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near}
                    });

                    m_PowerVoltageUnits.Add(new PowerVoltageUnit
                    {
                        ValueText = new GDIRectText()
                        {
                            Location = new Point(modelConfig.FirstAbsX + i*modelConfig.TextWidth, modelConfig.FirstAbsY),
                            Size = new Size(modelConfig.TextWidth, modelConfig.TextHeight),
                            TextColor = (Color.Yellow),
                            BkColor = Color.Black,
                            NeedDarwOutline = true,
                            TextFormat =
                                new StringFormat(StringFormatFlags.DirectionRightToLeft)
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                },
                            RefreshAction = o =>
                            {
                                var sen = (GDIRectText) o;
                                var idxs = GlobalResource.Instance.GetInFloatIndexs(voltageConfig.InFloatColoumNames);
                                var fValue = m_PowerView.FloatList[idxs[0]];
                                sen.Text = (fValue > modelConfig.MaxVoltage ? modelConfig.MaxVoltage : fValue).ToString("F0");
                            }
                        },
                        ValueBar = new Progress(Direction.Up)
                        {
                            Location =
                                new Point((int) (modelConfig.FirstAbsX + (i + 0.5)*modelConfig.TextWidth - 13),
                                    m_CoordinateOrigin.Y),
                            Size = new Size(25, (int) m_MaxHeight),
                            MaxDrawValue = m_MaxHeight,
                            MaxValue = m_MaxVoltage,
                            Color = Color.FromArgb(0, 255, 0),
                            RefreshAction = o =>
                            {
                                var sen = (Progress) o;
                                var idxs = GlobalResource.Instance.GetInFloatIndexs(voltageConfig.InFloatColoumNames);
                                var fValue = m_PowerView.FloatList[idxs[0]];
                                sen.CurrentValue = fValue > modelConfig.MaxVoltage ? modelConfig.MaxVoltage : fValue;
                            }
                        }
                    });
                }
            }
        }

        public override void OnPaint(Graphics g)
        {
            base.OnPaint(g);

            // 坐标系
            DrawCoordinate(g);
        }

        /// <summary>
        /// // 坐标系
        /// </summary>
        /// <param name="g"></param>
        private void DrawCoordinate(Graphics g)
        {

            var point = new Point {X = 45, Y = 141};
            g.DrawString("(V)", CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);

            point.X = 38;
            point.Y = 164;
            g.DrawString(m_MaxVoltage.ToString(CultureInfo.InvariantCulture), CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);

            point.X = 38;
            point.Y = 300;
            g.DrawString((m_MaxVoltage/2).ToString(CultureInfo.InvariantCulture), CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);

            //point.X = 58;
            //point.Y = 446;
            //g.DrawString("0", CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);

            point.X = 11;
            point.Y = 463;
            g.DrawString("车厢", CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);

            m_Coordinate.OnDraw(g);

            m_CoordinateLines.ForEach(e => e.OnDraw(g));
        }
    }
}
