using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Grid.Items;
using CommonUtil.Controls.Roundness;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid;
using CommonUtil.Controls.Grid.GridLine;
using MMICommon.Controls.Grid;

namespace CRH2MMI.BPRescue
{
    [GksDataType(DataType.isMMIObjectClass)]
    class BPRescueView : CRH2BaseClass
    {
        private BPResource m_Resource;

        private GridLine m_GridLine;

        private List<GDIRectText> m_TitleTexts;

        private TrainView m_TrainView;

        /// <summary>
        /// 固定信息
        /// </summary>
        private List<CommonInnerControlBase> m_ConstInfo;

        public override bool Init()
        {

            var bpConfig = GlobalInfo.CurrentCRH2Config.BPRescueConfig;
            if (bpConfig == null || GlobalInfo.CurrentCRH2Config.BPRescueConfig.BPGridLine == null)
            {
                return true;
            }

            m_TrainView = TrainView.Instance;

            m_Resource = new BPResource(this);
            m_Resource.Init();

            InitUIObj(bpConfig.BPGridLine.ViewItems.SelectMany(s => s.Items).Cast<CRH2CommunicationPortModel>());

            m_TitleTexts = new List<GDIRectText>();
            m_GridLine = new GridLine(
                new Point(m_Resource.BPConfig.BPGridLine.AbsX, m_Resource.BPConfig.BPGridLine.AbsY),
                new Size(m_Resource.BPConfig.BPGridLine.Width, m_Resource.BPConfig.BPGridLine.Height), m_Resource.BPConfig.BPGridLine.RowCount - 1
                , m_Resource.BPConfig.BPGridLine.ColumnCount - 1);

            InitMatrix(m_GridLine, 0, m_Resource.BPConfig.BPGridLine.ViewItems.Count);

            InitConstInfo();

            GlobalEvent.Instance.ReversalChanged += OnReversalChanged;

            return true;

        }

        private void InitConstInfo()
        {
            var y = m_GridLine.OutLineRectangle.Bottom + 50;
            var x = m_GridLine.GetIntersectionLocation(0, 1).X + 10;
            var textSize = new Size(50, 20);
            var r = 8;
            const int interval = 20;

            m_ConstInfo = new List<CommonInnerControlBase>()
            {

                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle = new Rectangle(new Point(x, y), new Size(textSize.Width, textSize.Height)),
                    TextColor = Color.White,
                    Text = "( 注 : ",
                    NeedDarwOutline = false,
                    Bold = true,
                },

                new GDIRoundness()
                {
                    ContentColor = CRH2Resource.GreenPen.Color,
                    NeedDrawArc = false,
                    NeedDrawContent = true,
                    Center = new Point(x + textSize.Width + interval + r, y + textSize.Height/2),
                    R = r,
                },

                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle =
                        new Rectangle(new Point(x + interval + r*2  + textSize.Width, y),
                            new Size(textSize.Width, textSize.Height)),
                    TextColor = Color.White,
                    Text = ": 有",
                    NeedDarwOutline = false,
                    Bold = true,
                },
                new GDIRoundness()
                {
                    ContentColor = Color.White,
                    NeedDrawArc = false,
                    NeedDrawContent = true,
                    Center = new Point(x + (textSize.Width + interval + r)*2, y + textSize.Height/2),
                    R = r,
                },

                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle =
                        new Rectangle(new Point(x + (interval + r*2  + textSize.Width)*2, y),
                            new Size(textSize.Width, textSize.Height)),
                    TextColor = Color.White,
                    Text = ": 无 )",
                    NeedDarwOutline = false,
                    Bold = true,
                },
            };
        }

        private void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            m_GridLine.Reverse();
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                m_TrainView.Location = GlobalInfo.CurrentCRH2Config.BPRescueConfig.TrainViewLocation.Rectangle.Location;
            }
        }

        public override string GetInfo()
        {
            return "BP 救援";
        }

        public override void paint(Graphics g)
        {
            m_GridLine.OnPaint(g);

            m_TitleTexts.ForEach(e => e.OnDraw(g));

            m_ConstInfo.ForEach(e => e.OnDraw(g));
        }

        private void InitMatrix(GridLine gridLine, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                var it = m_Resource.BPConfig.BPGridLine.ViewItems[i];

                var loc = gridLine.GetIntersectionLocation(it.ColumnIndex, 0);

                var text = new GDIRectText
                {
                    Text = it.Name,
                    OutLineRectangle = new Rectangle(1, loc.Y - 10, 150, 20),
                    BkColor = Color.Black,
                    TextColor = Color.White,
                };
                m_TitleTexts.Add(text);

                if (it.ItemType == GridItemType.Text)
                {
                    InitTextBox(gridLine, start, it, i);
                }

                else
                {
                    InitRoundness(gridLine, start, it, i);
                }
            }

        }

        private void InitTextBox(GridLine gridLine, int start, BPViewItem it, int i)
        {
            var textSize = new Size(44, 20);
            var format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            var center = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
            foreach (var item in it.Items)
            {
                var cen = gridLine.GetIntersectionLocation(i - start, item.CarNo);
                MatrixViewConfigItem item1 = item;

                var dataItem = gridLine.AddIntersectionItem(i - start, item.CarNo, GridItemType.Text) as GridTextItem;

                Debug.Assert(dataItem != null, "dataItem != null");
                dataItem.ContentSize = new Size(textSize.Width, textSize.Height);
                dataItem.RectText.BkColor = Color.Black;
                dataItem.RectText.TextColor = Color.White;
                dataItem.RectText.NeedDarwOutline = true;
                dataItem.RectText.TextFormat = it.ItemValues != null && it.ItemValues.Any() ? center : format;
                if (item1.InBoolColoumNames.Any())
                {
                    dataItem.RectText.RefreshAction = it.ItemValues != null && it.ItemValues.Any()
                    ? o =>
                    {
                        var sen = (GDIRectText)o;
                        sen.Text = BoolList[GetInBoolIndex(item1.InBoolColoumNames.First())]
                            ? it.ItemValues[1]
                            : it.ItemValues[0];
                    }
                    : (Action<object>)null;
                }
                else if (item1.InFloatColoumNames.Any())
                {
                    dataItem.RectText.RefreshAction = o =>
                    {
                        var sen = (GDIRectText)o;
                        sen.Text = FloatList[GetInFloatIndex(item1.InFloatColoumNames.First())].ToString("F0");
                    };
                }

            }
        }

        private void InitRoundness(GridLine gridLine, int start, MatrixViewConfig it, int i)
        {
            foreach (var item in it.Items)
            {
                MatrixViewConfigItem item1 = item;
                var dataItem = gridLine.AddIntersectionItem(i - start, item.CarNo, GridItemType.Roudness) as GridRoundnessItem;
                Debug.Assert(dataItem != null, "dataItem != null");
                dataItem.Roundness.Center = gridLine.GetIntersectionLocation(i - start, item.CarNo);
                dataItem.Roundness.R = 8;
                dataItem.Roundness.RefreshAction =
                    roundness =>
                    {
                        ((GDIRoundness)roundness).ContentColor = BoolList[GetInBoolIndex(item1.InBoolColoumNames.First())]
                            ? it.Region == 1 ? Color.Red : CRH2Resource.GreenPen.Color
                            : Color.White;
                    };
                dataItem.Roundness.NeedDrawArc = false;
                dataItem.Roundness.NeedDrawContent = true;
            }
        }
    }
}
