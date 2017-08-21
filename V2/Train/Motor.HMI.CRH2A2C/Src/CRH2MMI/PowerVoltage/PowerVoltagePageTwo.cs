using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using CommonUtil.Model;
using CommonUtil.Util;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CommonUtil.Controls;


namespace CRH2MMI.PowerVoltage
{
    class PowerVoltagePageTwo : PowerVoltagePageBase
    {
        private List<PowerVoltageModel> m_ModelConfigs;
        private int m_MaxVoltage;
        // ReSharper disable once InconsistentNaming
        private const float m_MaxHeight = 137.5f * 2f;
        private List<Line> m_CoordinateLines;

        private bool HasTwoCoordinate { get { return m_ModelConfigs.GroupBy(g => g.MaxVoltage).Count() > 1; }}

        public PowerVoltagePageTwo(Power power)
            : base(power)
        {
            Init();
        }

        private void InitChangePageBtn()
        {
            m_ChangePageBtn = new CRH2Button()
            {
                OutLineRectangle = new Rectangle(430, 525, 117, 43),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                ButtonDownEvent = (sender, args) => HandleUtil.OnHandle(ChangePage, this, null),
                TextColor = Color.White,
                Text = "上一页面",
            };
        }

        private void Init()
        {
            m_ModelConfigs = GlobalInfo.CurrentCRH2Config.PowerVoltageConfig.SecondPageModels;
            m_MaxVoltage = m_ModelConfigs.Max(m => m.MaxVoltage);

            InitCoordinate();

            InitModel();

            InitConstInfo1();
            if (HasTwoCoordinate)
            {
                InitConstInfo2();
            }

            InitChangePageBtn();

        }

   
        private void InitCoordinate()
        {
            if (HasTwoCoordinate)
            {
                Init2Coordinate();
            }
            else
            {
                Init1Coordinate();
            }
        }

        private void Init1Coordinate()
        {
            m_CoordinateLines = new List<Line>()
                                {
                                    new Line(new Point(m_CoordinateOrigin.X, 140), m_CoordinateOrigin),
                                    new Line(m_CoordinateOrigin,new Point(756, m_CoordinateOrigin.Y))
                                };

            for (int i = 0; i < 3; i++)
            {
                var yoff = (int) m_MaxHeight * i / 2;
                m_CoordinateLines.Add(new Line(new Point(m_CoordinateOrigin.X - 5, 175 + yoff), new Point(m_CoordinateOrigin.X, 175 + yoff)));
            }
        }

        private void Init2Coordinate()
        {
            m_CoordinateLines = new List<Line>()
                                {
                                    new Line(new Point(m_CoordinateOrigin.X, 140), m_CoordinateOrigin),
                                    new Line(m_CoordinateOrigin,
                                        new Point(m_ModelConfigs[0].FirstAbsX + m_ModelConfigs[0].TextWidth * m_ModelConfigs[0].VoltageConfigs.Count, m_CoordinateOrigin.Y))
                                };

            for (int i = 0; i < 3; i++)
            {
                var yoff = (int)m_MaxHeight * i / 2;
                m_CoordinateLines.Add(new Line(new Point(m_CoordinateOrigin.X - 5, 175 + yoff), new Point(m_CoordinateOrigin.X, 175 + yoff)));
            }

            // 第二个坐标系
            var origin = new Point(740, m_CoordinateOrigin.Y);
            m_CoordinateLines.Add(new Line(new Point(origin.X, 140), origin));
            m_CoordinateLines.Add(new Line(new Point(m_ModelConfigs[1].FirstAbsX, origin.Y), origin));
            for (int i = 0; i < 3; i++)
            {
                var yoff = (int)m_MaxHeight * i / 2;
                m_CoordinateLines.Add(new Line(new Point(origin.X + 5, 175 + yoff), new Point(origin.X, 175 + yoff)));
            }
        }


        private void InitConstInfo1()
        {
            var size = new Size(65, 30);
            var point = new Point { X = 45, Y = 141 };
            m_ConstInfo.Add(new GDIRectText()
                            {
                                TextColor = Color.White,
                                Text = "(V)",
                                OutLineRectangle = new Rectangle(point, size)
                            });

            point.X = 38;
            point.Y = 164;
            m_ConstInfo.Add(new GDIRectText()
                            {
                                TextColor = Color.White,
                                Text = m_ModelConfigs[0].MaxVoltage.ToString(CultureInfo.InvariantCulture),
                                OutLineRectangle = new Rectangle(point, size)
                            });


            point.X = 38;
            point.Y = 300;
            m_ConstInfo.Add(new GDIRectText()
                            {
                                TextColor = Color.White,
                                Text = ( m_ModelConfigs[0].MaxVoltage / 2 ).ToString(CultureInfo.InvariantCulture),
                                OutLineRectangle = new Rectangle(point, size)
                            });

            point.X = 58;
            point.Y = 440;
            m_ConstInfo.Add(new GDIRectText()
                            {
                                TextColor = Color.White,
                                Text = "0",
                                OutLineRectangle = new Rectangle(point, new Size(20,30))
                            });

            point.X = 11;
            point.Y = 463;
            m_ConstInfo.Add(new GDIRectText()
                            {
                                TextColor = Color.White,
                                Text = "车厢",
                                OutLineRectangle = new Rectangle(point, size)
                            });

        }

        private void InitConstInfo2()
        {
            var size = new Size(65, 30);
            var point = new Point { X = 748, Y = 141 };
            m_ConstInfo.Add(new GDIRectText()
            {
                TextColor = Color.White,
                Text = "(V)",
                OutLineRectangle = new Rectangle(point, size)
            });

            point.X = 748;
            point.Y = 164;
            m_ConstInfo.Add(new GDIRectText()
            {
                TextColor = Color.White,
                Text = m_ModelConfigs[1].MaxVoltage.ToString(CultureInfo.InvariantCulture),
                OutLineRectangle = new Rectangle(point, size)
            });


            point.X = 748;
            point.Y = 300;
            m_ConstInfo.Add(new GDIRectText()
            {
                TextColor = Color.White,
                Text = (m_ModelConfigs[1].MaxVoltage / 2).ToString(CultureInfo.InvariantCulture),
                OutLineRectangle = new Rectangle(point, size)
            });

            point.X = 758;
            point.Y = 440;
            m_ConstInfo.Add(new GDIRectText()
            {
                TextColor = Color.White,
                Text = "0",
                OutLineRectangle = new Rectangle(point, new Size(20, 30))
            });
        }


        private void InitModel()
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int index = 0; index < m_ModelConfigs.Count; index++)
            {
                var modelConfig = m_ModelConfigs[index];
                m_ConstInfo.Add(new GDIRectText()
                                {
                                    Text = modelConfig.VoltageType,
                                    Location = new Point(modelConfig.FirstAbsX - modelConfig.TextWidth * ( modelConfig.VoltageConfigs.Count < 3 ? 1 : 0 ), 134),
                                    Size = new Size(modelConfig.TextWidth * ( modelConfig.VoltageConfigs.Count < 3 ? 3 : modelConfig.VoltageConfigs.Count ), 38),
                                    TextColor = Color.White,
                                    TextFormat =
                                        new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near }
                                });

                for (int i = 0; i < modelConfig.VoltageConfigs.Count; i++)
                {
                    var voltageConfig = modelConfig.VoltageConfigs[i];
                    var carNameLocation = new Point((int) ( modelConfig.FirstAbsX + i * modelConfig.TextWidth ),
                        modelConfig.FirstAbsY - 25);
                    m_ConstInfo.Add(new GDIRectText()
                                    {
                                        Text = voltageConfig.CarName.ToString(),
                                        Location = carNameLocation,
                                        Size = new Size(modelConfig.TextWidth, 15),
                                        TextColor = Color.White,
                                        TextFormat =
                                            new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near }
                                    });

                    PowerVoltageModel config = modelConfig;
                    m_PowerVoltageUnits.Add(new PowerVoltageUnit
                                            {
                                                ValueText = new GDIRectText()
                                                            {
                                                                Location = new Point(modelConfig.FirstAbsX + i * modelConfig.TextWidth, modelConfig.FirstAbsY),
                                                                Size = new Size(modelConfig.TextWidth, modelConfig.TextHeight),
                                                                TextColor = Color.Yellow,
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
                                                                    var idxs =
                                                                        GlobalResource.Instance.GetInFloatIndexs(voltageConfig.InFloatColoumNames);
                                                                    var fValue = m_PowerView.FloatList[idxs[0]];
                                                                    sen.Text = ( fValue > config.MaxVoltage ? config.MaxVoltage : fValue ).ToString("F0");
                                                                }
                                                            },
                                                ValueBar = new Progress(Direction.Up)
                                                           {
                                                               Location =
                                                                   new Point((int) ( modelConfig.FirstAbsX + ( i + 0.5 ) * modelConfig.TextWidth - 13 ),
                                                                   m_CoordinateOrigin.Y),
                                                               Size = new Size(25, (int) m_MaxHeight),
                                                               MaxDrawValue = m_MaxHeight,
                                                               MaxValue = modelConfig.MaxVoltage,
                                                               Color = Color.FromArgb(0, 255, 0),
                                                               RefreshAction = o =>
                                                               {
                                                                   var sen = (Progress) o;
                                                                   var idxs =
                                                                       GlobalResource.Instance.GetInFloatIndexs(voltageConfig.InFloatColoumNames);
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
            //var point = new Point { X = 45, Y = 141 };
            //g.DrawString("(V)", CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);

            //point.X = 38;
            //point.Y = 164;
            //g.DrawString(m_MaxVoltage.ToString(CultureInfo.InvariantCulture), CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);

            //point.X = 38;
            //point.Y = 300;
            //g.DrawString((m_MaxVoltage / 2).ToString(CultureInfo.InvariantCulture), CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);

            //point.X = 58;
            //point.Y = 446;
            //g.DrawString("0", CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);

            //point.X = 11;
            //point.Y = 463;
            //g.DrawString("车厢", CRH2Resource.Font12, CRH2Resource.WhiteBrush, point);

            m_CoordinateLines.ForEach(e => e.OnDraw(g));
        }
    }
}
