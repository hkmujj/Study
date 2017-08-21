using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Grid.Items;
using CommonUtil.Controls.Roundness;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid;
using CommonUtil.Controls.Grid.GridLine;
using MMICommon.Controls.Grid;

namespace CRH2MMI.Vigilant
{
    /// <summary>
    /// 警惕救援界面
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class VigilantView : CRH2BaseClass
    {

        private VigilantResource m_Resource;

        private List<GDIRectText> m_TitleTexts;

        private List<GDIRoundness> m_StateRoundnesses;
        private List<VigilantBreak> m_ContentTexts;

        private GridLine m_UpGridLine;
        private GridLine m_DownGridLine;
        private TrainView m_TrainView;

        /// <summary>
        /// 固定信息
        /// </summary>
        private List<CommonInnerControlBase> m_ConstInfo;

        public override bool Init()
        {

            var vigiCfg = GlobalInfo.CurrentCRH2Config.VigilantConfig;
            if (vigiCfg == null || vigiCfg.ViewItems == null || !vigiCfg.ViewItems.Any())
            {
                return true;
            }

            m_TrainView = TrainView.Instance;

            m_Resource = new VigilantResource(this);
            m_Resource.Init();

            InitUIObj(vigiCfg.ViewItems.SelectMany(s => s.Items).Cast<CRH2CommunicationPortModel>());

            m_UpGridLine = new GridLine(new Point(240, 206), new Size(300, 30), 1, 7);

            m_DownGridLine = new GridLine(new Point(240, 330), new Size(300, 90), 3, 7);

            InitMatrix();

            InitConstInfo();

            GlobalEvent.Instance.ReversalChanged += (sender, args) =>
            {
                m_UpGridLine.Reverse();
                m_DownGridLine.Reverse();
            };

            return true;
        }

        private void InitMatrix()
        {

            m_TitleTexts = new List<GDIRectText>();
            m_StateRoundnesses = new List<GDIRoundness>();
            m_ContentTexts = new List<VigilantBreak>();

            InitMatrix(m_UpGridLine, 0, 2);

            InitMatrix(m_DownGridLine, 2, m_Resource.VigilantConfig.ViewItems.Count);

        }

        private void InitMatrix(GridLine gridLine, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                var it = m_Resource.VigilantConfig.ViewItems[i];

                var loc = gridLine.GetIntersectionLocation(it.ColumnIndex, 0);

                var text = new GDIRectText()
                {
                    Text = it.Name,
                    OutLineRectangle = new Rectangle(1, loc.Y - 10, 120, 20),
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

        private void InitTextBox(GridLine gridLine, int start, MatrixViewConfig it, int i)
        {
            var textSize = new Size(30, 16);
            var format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            foreach (var item in it.Items)
            {
                var cen = gridLine.GetIntersectionLocation(i - start, item.CarNo);
                var dataItem = gridLine.AddIntersectionItem(i - start, item.CarNo, GridItemType.Text) as GridTextItem;
                Debug.Assert(dataItem != null, "dataItem != null");
                //dataItem.RectText.OutLineRectangle = new Rectangle(cen.X - textSize.Width / 2, cen.Y - textSize.Height / 2, textSize.Width, textSize.Height);
                dataItem.ContentSize = new Size(textSize.Width, textSize.Height);
                dataItem.RectText.BkColor = Color.Black;
                dataItem.RectText.TextColor = Color.White;
                dataItem.RectText.NeedDarwOutline = true;
                dataItem.RectText.TextFormat = format;
                //dataItem.RectText.RefreshAction = it.ItemValues != null && it.ItemValues.Any()
                //    ? o =>
                //    {
                //        var sen = (GDIRectText) o;
                //        sen.Text = BoolList[UIObj.InBoolList[item1.InBoolIndexs[0]]]
                //            ? it.ItemValues[1]
                //            : it.ItemValues[0];
                //    }
                //    : (Action<object>) null;
                m_ContentTexts.Add(new VigilantBreak(dataItem.RectText));
            }
        }

        private void InitRoundness(GridLine gridLine, int start, MatrixViewConfig it, int i)
        {
            for (int index = 0; index < it.Items.Count; index++)
            {
                var item = it.Items[index];
                var cen = gridLine.GetIntersectionLocation(i - start, item.CarNo);
                var dataItem = gridLine.AddIntersectionItem(i - start, item.CarNo, GridItemType.Roudness) as GridRoundnessItem;
                dataItem.Roundness.Center = cen;
                dataItem.Roundness.R = 8;
                dataItem.Roundness.RefreshAction =
                    roundness =>
                    {
                        ((GDIRoundness)roundness).ContentColor = BoolList[GetInBoolIndex(item.InBoolColoumNames.First())]
                                ? it.Region == 1 ? Color.Red : Color.GreenYellow
                                : Color.White;
                    };
                dataItem.Roundness.NeedDrawArc = false;
                dataItem.Roundness.NeedDrawContent = true;

                if (it.Name == "警惕制动")
                {
                    int index1 = index;
                    dataItem.Roundness.ContentColorChanged = (sender, args) =>
                    {
                        var arg = args as ColorChangedEventArgs;
                        if (arg != null && arg.NewColor != Color.White)
                        {
                            ++m_ContentTexts[index1].BreakCount;
                        }
                    };
                }
            }
        }

        private void InitConstInfo()
        {
            var y = m_UpGridLine.OutLineRectangle.Bottom + 30;
            var y2 = m_DownGridLine.OutLineRectangle.Bottom + 30;
            var x = m_UpGridLine.GetIntersectionLocation(0, 0).X + 30;
            var textSize = new Size(130, 18);
            var y3 = y2 + textSize.Height + 10;
            const int r = 8;
            const int interval = 20;

            m_ConstInfo = new List<CommonInnerControlBase>()
            {
                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle = new Rectangle(new Point(x, y), new Size(textSize.Width, textSize.Height)),
                    TextColor = Color.White,
                    Text = "(        : 不动作",
                    NeedDarwOutline = false,
                    Bold = true,
                },

                new GDIRoundness()
                {
                    ContentColor = Color.White,
                    NeedDrawArc = false,
                    NeedDrawContent = true,
                    Center = new Point(x + interval + r, y + textSize.Height/2),
                    R = r,
                },

                new GDIRoundness()
                {
                    ContentColor = Color.GreenYellow,
                    NeedDrawArc = false,
                    NeedDrawContent = true,
                    Center = new Point(x + (textSize.Width + interval + r), y + textSize.Height/2),
                    R = r,
                },

                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle =
                        new Rectangle(new Point(x + interval + r*2 + textSize.Width, y),
                            new Size(textSize.Width, textSize.Height)),
                    TextColor = Color.White,
                    Text = "  : 动作)",
                    NeedDarwOutline = false,
                    Bold = true,
                },



                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle = new Rectangle(new Point(x, y2), new Size(textSize.Width, textSize.Height)),
                    TextColor = Color.White,
                    Text = "(        : 正常",
                    NeedDarwOutline = false,
                    Bold = true,
                },

                new GDIRoundness()
                {
                    ContentColor = Color.White,
                    NeedDrawArc = false,
                    NeedDrawContent = true,
                    Center = new Point(x + interval + r, y2 + textSize.Height/2),
                    R = r,
                },

                new GDIRoundness()
                {
                    ContentColor = Color.Red,
                    NeedDrawArc = false,
                    NeedDrawContent = true,
                    Center = new Point(x + (textSize.Width + interval + r), y2 + textSize.Height/2),
                    R = r,
                },

                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle =
                        new Rectangle(new Point(x + interval + r*2 + textSize.Width, y2),
                            new Size(textSize.Width, textSize.Height)),
                    TextColor = Color.White,
                    Text = "  : 异常)",
                    NeedDarwOutline = false,
                    Bold = true,
                },

                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle =
                        new Rectangle(new Point(x-20 , y3),
                            new Size(textSize.Width *3, textSize.Height)),
                    TextColor = Color.White,
                    Text = " (注) 数字为本日的发生累计次数",
                    NeedDarwOutline = false,
                    Bold = true,
                },
            };
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                m_TrainView.Location = GlobalInfo.CurrentCRH2Config.VigilantConfig.TrainViewLocation.Rectangle.Location;
            }
        }

        public override void paint(Graphics g)
        {
            m_UpGridLine.OnPaint(g);

            m_DownGridLine.OnPaint(g);

            m_TitleTexts.ForEach(e => e.OnDraw(g));

            m_ConstInfo.ForEach(e => e.OnDraw(g));

        }
    }
}
