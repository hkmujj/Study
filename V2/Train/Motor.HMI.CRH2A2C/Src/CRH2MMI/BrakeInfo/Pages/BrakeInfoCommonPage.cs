using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using CommonUtil.Controls.Grid.Items;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.Util;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid;
using CommonUtil.Controls.Grid.GridLine;
using CRH2MMI.Config.ConfigModel;


namespace CRH2MMI.BrakeInfo.Pages
{
    class BrakeInfoCommonPage : BrakeInfoPageBase
    {
        protected GridLine m_GridLine;

        // ReSharper disable once InconsistentNaming
        private static readonly List<int> m_TitleLocationYs = new List<int>() { 250, 458, 478 };
        // ReSharper disable once InconsistentNaming
        private static readonly List<Size> m_TitileSizes = new List<Size>() { new Size(170, 48), new Size(150, 24), new Size(150, 24) };

        /// <summary>
        /// 条状图
        /// </summary>
        private readonly List<Progress> m_ValueProgresses = new List<Progress>();

        // ReSharper disable once InconsistentNaming
        private const int m_MaxHeight = 75 * 2;
        protected float MaxXValue { set; get; }

        public BrakeInfoCommonPage(BrakeInfo brakeInfo)
            : base(brakeInfo)
        {
        }

        public override void Init()
        {
            base.Init();

            var conf = GlobalInfo.CurrentCRH2Config.BrakeInfoConfig.BrakePageConfigs.Find(f => f.PageName == PageName).Grid;

            m_GridLine = GDIGridLineHelper.CreateGridLine(conf, CreateGDITextAction);

            InitTitles(conf);

            InitCoordinate();

        }

        private void InitTitles(GridConfig conf)
        {
            var xtitles = conf.Rows.Select((t, i) => new GDIRectText()
            {
                Text = t.Name,
                NeedDarwOutline = false,
                Location = new Point(1, m_TitleLocationYs[i]),
                Size = m_TitileSizes[i],
                BkColor = Color.Black,
            }).ToList();
            m_ConstInfos.AddRange(xtitles.Cast<CommonInnerControlBase>());
        }

        protected void CreateGDITextAction(GridLine gridLine, GridColumnConfig columnConfig, GridItemBase arg3)
        {
            var textItem = arg3 as GridTextItem;
            Debug.Assert(textItem != null, "dataItem != null");
            var rowConfig = columnConfig.Parent;
            var config = rowConfig.Parent;
            textItem.ContentSize = new Size(config.TextWidth, config.TextHeight);
            textItem.RectText.BkColor = Color.Black;
            textItem.RectText.TextColor = columnConfig.Parent.Colors.First();
            textItem.RectText.NeedDarwOutline = true;
            textItem.RectText.TextFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            if (rowConfig.RowIndex == 0 && rowConfig.Name.Equals("BCU"))
            {
                InitSetValue(columnConfig, textItem, rowConfig, true);
            }
            else if (rowConfig.RowIndex == 1 && rowConfig.Name.Equals("C1"))
            {
                InitSetValue(columnConfig, textItem, rowConfig, false);
            }
            else if (rowConfig.RowIndex == 0)
            {
                var progress = new Progress(Direction.Up)
                {
                    Location =
                        new Point((textItem.Location.X + 11),
                            451),
                    Size = new Size(26, m_MaxHeight),
                    MaxDrawValue = m_MaxHeight,
                    MaxValue = MaxXValue,
                    Color = rowConfig.Colors[0],
                    RefreshAction = o =>
                    {
                        var sen = (Progress)o;
                        var value = m_BrakeInfo.FloatList[GlobalResource.Instance.GetInFloatIndexs(columnConfig.InFloatColoumNames).First()];
                        sen.CurrentValue = value > MaxXValue ? MaxXValue : value;
                    }
                };
                m_ValueProgresses.Add(progress);
            }

            var format = rowConfig.FloatToStringFormat.IsNullOrWhiteSpace()
                ? "F0"
                : rowConfig.FloatToStringFormat;
            textItem.RectText.RefreshAction = o =>
            {
                var sen = (GDIRectText)o;
                var value = m_BrakeInfo.FloatList[GlobalResource.Instance.GetInFloatIndexs(columnConfig.InFloatColoumNames).First()];
                sen.Text = value.ToString(format);
            };
        }

        private void InitSetValue(CRH2CommunicationPortModel columnConfig, ICommonInnerControl textItem, GridRowConfig rowConfig, bool isSet)
        {
            var progress = new Progress(Direction.Up)
            {
                Location =
                    new Point((textItem.Location.X + (isSet ? 5 : 24)),
                        451),
                Size = new Size(13, m_MaxHeight),
                MaxDrawValue = m_MaxHeight,
                MaxValue = MaxXValue,
                Color = rowConfig.Colors[0],
                RefreshAction = o =>
                {
                    var sen = (Progress)o;
                    var value =
                        m_BrakeInfo.FloatList[
                            GlobalResource.Instance.GetInFloatIndexs(columnConfig.InFloatColoumNames)
                                .First()];
                    sen.CurrentValue = value > MaxXValue ? MaxXValue : value;
                }
            };
            m_ValueProgresses.Add(progress);
        }


        protected override void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            m_BrakeSigal.Reverse();
            m_GridLine.Reverse();

            var mid = ((float)m_ValueProgresses.Min(m => m.Location.X) + m_ValueProgresses.Max(m => m.Location.X)) / 2;
            var mat = MatrixHelper.CreateTurnMatrix(mid, TurnOrientation.Horizontal);
            m_ValueProgresses.ForEach(e => e.Location = mat.TransformPoint(e.Location));
        }

        public override void OnPaint(Graphics g)
        {
            base.OnPaint(g);

            m_GridLine.OnPaint(g);

            m_ValueProgresses.ForEach(e => e.OnPaint(g));
        }

        private void InitCoordinate()
        {
            var x = GlobalInfo.CurrentCRH2Config.BrakeInfoConfig.TrainViewLocation.Rectangle.Left - 5;
            var carCount = GlobalInfo.CurrentCRH2Config.BrakeInfoConfig.BrakePageConfigs[0].Grid.ColumnCount;


            var lines = new List<Line>
            {
                new Line(new Point(x, 289), new Point(x, 450)),
                new Line(new Point(x, 450), new Point(x + 44 * (carCount-1) + 30, 450))
            };
            for (int i = 0; i < 3; i++)
            {
                lines.Add(new Line(new Point(x - 5, 300 + 75 * i), new Point(x, 300 + 75 * i)));
            }
            m_ConstInfos.AddRange(lines);

            var xNameFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };

            var txtX = x - 65;
            var xNamesSize = new Size(60, 20);
            var xNames = new List<GDIRectText>
            {
                new GDIRectText()
                {
                    Text = MaxXValue.ToString(CultureInfo.InvariantCulture),
                    NeedDarwOutline = false,
                    Location = new Point(txtX, 290),
                    Size = xNamesSize,
                    BkColor = Color.Black,
                    TextFormat = xNameFormat,
                },
                new GDIRectText()
                {
                    Text = (MaxXValue/2).ToString(CultureInfo.InvariantCulture),
                    NeedDarwOutline = false,
                    Location = new Point(txtX, 290 + 75 ),
                    Size = xNamesSize,
                    BkColor = Color.Black,
                    TextFormat = xNameFormat,
                },
                new GDIRectText()
                {
                    Text = "0",
                    NeedDarwOutline = false,
                    Location = new Point(txtX, 290 + 75 * 2),
                    Size = xNamesSize,
                    BkColor = Color.Black,
                    TextFormat = xNameFormat,
                },
            };
            m_ConstInfos.AddRange(xNames);
        }
    }
}
