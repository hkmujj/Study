using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using CommonUtil.Controls.Grid.Items;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.Util;
using CRH2MMI.WorkState;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid;
using CommonUtil.Controls.Grid.GridLine;


namespace CRH2MMI.Tow1.Pages
{
    abstract class Tow1CommonPageBase : Tow1InfoPageBase
    {
        protected Tow1CommonPageBase(TowInfo towInfo)
            : base(towInfo)
        {
        }

        // ReSharper disable once InconsistentNaming
        private static readonly List<int> m_TitleLocationYs = new List<int>() { 276, 480, 500 };
        private GridLine m_BrakeSigal;

        /// <summary>
        /// 条状图
        /// </summary>
        private readonly List<Progress> m_ValueProgresses = new List<Progress>();

        protected int MaxXValue { set; get; }

        // ReSharper disable once InconsistentNaming
        private const int m_MaxHeight = 75 * 2;

        public override void Init()
        {
            base.Init();

            InitBrakeInfo(GlobalInfo.CurrentCRH2Config.Tow1Config.Tow1PageConfigs.Find(f => f.PageName == "Common").Grid);

            var conf = GlobalInfo.CurrentCRH2Config.Tow1Config.Tow1PageConfigs.Find(f => f.PageName == PageName).Grid;

            m_GridLine = GDIGridLineHelper.CreateGridLine(conf, ValueControlInitAction);

            InitCoordinate();

            InitTitles(conf);
        }

        private void ValueControlInitAction(GridLine gridLine, GridColumnConfig gridColumnConfig, GridItemBase arg3)
        {
            var glInit = new GridLineInitialize() { ViewClass = m_TowInfo };
            glInit.InitInnerContrl(gridLine, gridColumnConfig, arg3);
            var textItem = ((GridTextItem)arg3);
            textItem.GetInnerContrl().TextColor = gridColumnConfig.Parent.Colors.First();

            if (gridColumnConfig.Parent.RowIndex == 0 && gridColumnConfig.Parent.Name.Equals("设定值"))
            {
                InitSetValue(gridColumnConfig, textItem, true);
            }
            else if (gridColumnConfig.Parent.RowIndex == 1 && gridColumnConfig.Parent.Name.Equals("反馈值"))
            {
                InitSetValue(gridColumnConfig, textItem, false);
            }
            else if (gridColumnConfig.Parent.RowIndex == 0)
            {
                var progress = new Progress(Direction.Up)
                {
                    Location =
                        new Point((textItem.Location.X + 11),
                            471),
                    Size = new Size(26, m_MaxHeight),
                    MaxDrawValue = m_MaxHeight,
                    MaxValue = MaxXValue,
                    Color = gridColumnConfig.Parent.Colors[0],
                    RefreshAction = o =>
                    {
                        var sen = (Progress)o;
                        var value =
                            m_TowInfo.FloatList[
                                GlobalResource.Instance.GetInFloatIndexs(
                                    gridColumnConfig.InFloatColoumNames)[0]];
                        sen.CurrentValue = value > MaxXValue ? MaxXValue : value;
                    }
                };
                m_ValueProgresses.Add(progress);
            }
        }

        private void InitSetValue(GridColumnConfig gridColumnConfig, ICommonInnerControl textItem, bool IsSeting)
        {
            var progress = new Progress(Direction.Up)
            {
                Location =
                    new Point((textItem.Location.X + (IsSeting ? 5 : 24)),
                        471),
                Size = new Size(13, m_MaxHeight),
                MaxDrawValue = m_MaxHeight,
                MaxValue = MaxXValue,
                Color = gridColumnConfig.Parent.Colors[0],
                RefreshAction = o =>
                {
                    var sen = (Progress)o;
                    var value =
                        m_TowInfo.FloatList[
                            GlobalResource.Instance.GetInFloatIndexs(
                                gridColumnConfig.InFloatColoumNames)[0]];
                    sen.CurrentValue = value > MaxXValue ? MaxXValue : value;
                }
            };
            m_ValueProgresses.Add(progress);
        }


        public override void OnPaint(Graphics g)
        {
            base.OnPaint(g);

            m_BrakeSigal.OnPaint(g);

            m_ValueProgresses.ForEach(e => e.OnPaint(g));
        }

        private void InitBrakeInfo(GridConfig conf)
        {
            m_BrakeSigal = GDIGridLineHelper.CreateGridLine(conf, (line, config, arg3) => BrakeSigalAction(line, conf, config, arg3));
            var glInit = new GridLineInitialize() { ViewClass = m_TowInfo };
            var consInfo = glInit.CreateTitles(m_BrakeSigal, conf);
            consInfo.ForEach(e =>
            {
                e.Size = new Size(e.Size.Width + 40, e.Size.Height);
                e.DrawFont = new Font(e.DrawFont.FontFamily, 13);
            });

            m_ConstInfos.AddRange(consInfo.Cast<CommonInnerControlBase>());
        }

        private void BrakeSigalAction(GridLine gridLine, GridConfig gridConfig, GridColumnConfig gridColumnConfig,
            GridItemBase arg3)
        {
            var txtItem = ((GridTextItem)arg3);
            txtItem.ContentSize = new Size(gridConfig.TextWidth, gridConfig.TextHeight);

            var txt = txtItem.GetInnerContrl();
            txt.BkColor = Color.Black;
            txt.TextColor = Color.Yellow;
            txt.NeedDarwOutline = true;
            txt.RefreshAction = o =>
            {
                var ctl = (GDIRectText)o;
                ctl.Text = WorkStateResource.Instance.GetLevel();
            };
        }

        private void InitCoordinate()
        {
            var x = GlobalInfo.CurrentCRH2Config.Tow1Config.TrainViewLocation.Rectangle.Left - 5;
            var carCount = GlobalInfo.CurrentCRH2Config.Tow1Config.Tow1PageConfigs[0].Grid.ColumnCount;

            var lines = new List<Line>
            {
                new Line(new Point(x, 309), new Point(x, 470)),
                new Line(new Point(x, 470), new Point(x + 44 * (carCount-1) + 43, 470))
            };
            for (int i = 0; i < 3; i++)
            {
                lines.Add(new Line(new Point(x - 5, 320 + 75 * i), new Point(x, 320 + 75 * i)));
            }
            m_ConstInfos.AddRange(lines.Cast<CommonInnerControlBase>());

            var xNameFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };

            var txtLocation = new Point(x - 105, 304);
            var xNamesSize = new Size(80, 20);
            var xNames = new List<GDIRectText>
            {
                new GDIRectText()
                {
                    Text = MaxXValue.ToString(CultureInfo.InvariantCulture),
                    NeedDarwOutline = false,
                    Location = new Point(txtLocation.X, txtLocation.Y),
                    Size = xNamesSize,
                    BkColor = Color.Black,
                    TextFormat = xNameFormat,
                },
                new GDIRectText()
                {
                    Text = (MaxXValue/2).ToString(CultureInfo.InvariantCulture),
                    NeedDarwOutline = false,
                    Location = new Point(txtLocation.X, txtLocation.Y + 75 ),
                    Size = xNamesSize,
                    BkColor = Color.Black,
                    TextFormat = xNameFormat,
                },
                new GDIRectText()
                {
                    Text = "0",
                    NeedDarwOutline = false,
                    Location = new Point(txtLocation.X, txtLocation.Y + 75 * 2),
                    Size = xNamesSize,
                    BkColor = Color.Black,
                    TextFormat = xNameFormat,
                },
            };
            m_ConstInfos.AddRange(xNames.Cast<CommonInnerControlBase>());
        }

        private void InitTitles(GridConfig conf)
        {
            var titleSize = new Size(150, 24);
            var xtitles = conf.Rows.Select((t, i) => new GDIRectText()
            {
                Text = t.Name,
                NeedDarwOutline = false,
                Location = new Point(1, m_TitleLocationYs[i]),
                Size = titleSize,
                BkColor = Color.Black,
            }).ToList();
            m_ConstInfos.AddRange(xtitles.Cast<CommonInnerControlBase>());
        }

        protected override void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            base.OnReversalChanged(sender, eventArgs);

            m_BrakeSigal.Reverse();

            var mid = ((float)m_ValueProgresses.Min(m => m.Location.X) + m_ValueProgresses.Max(m => m.Location.X)) / 2;
            var mat = MatrixHelper.CreateTurnMatrix(mid, TurnOrientation.Horizontal);
            m_ValueProgresses.ForEach(e => e.Location = mat.TransformPoint(e.Location));
        }
    }
}
