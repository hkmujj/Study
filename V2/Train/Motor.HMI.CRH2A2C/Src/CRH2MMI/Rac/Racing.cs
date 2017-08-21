using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Grid.Items;
using CommonUtil.Util;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View.Train;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid;
using CommonUtil.Controls.Grid.GridLine;


namespace CRH2MMI.Rac
{
    /// <summary>
    /// 空转滑行
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Racing : MMIBaseClass
    {
        private TrainView m_TrainView;

        private List<GDIRectText> m_ConstInfos;
        private GridLine m_GridLine;

        public override void paint(Graphics g)
        {
            m_ConstInfos.ForEach(e => e.OnDraw(g));
            m_GridLine.OnPaint(g);

        }

        public override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;

            m_TrainView = TrainView.Instance;

            var conf = GlobalInfo.CurrentCRH2Config.EmputyRollConfig;
            conf.Grid.RefreshRelation();
            conf.Grid.RelaceSpecialString();

            m_GridLine = GDIGridLineHelper.CreateGridLine(conf.Grid, CreateGridItemAction);

            var txtSize = new Size(100, 46);
            m_ConstInfos =
                conf.Grid.Rows.FindAll(f => !f.Name.IsNullOrWhiteSpace()).Select(s => new GDIRectText()
                {
                    OutLineRectangle =
                        new Rectangle(1, m_GridLine.GetIntersectionLocation(s.RowIndex, 0).Y - txtSize.Height / 2,
                            txtSize.Width, txtSize.Height),
                    Text = s.Name,
                    DrawFont = CRH2Resource.Font13,
                }).ToList();

            var x = m_GridLine.Location.X + m_GridLine.Size.Width / 2 - 300 / 2;
            var y = m_GridLine.OutLineRectangle.Bottom + 40;
            m_ConstInfos.Add(new GDIRectText()
            {
                BkColor = Color.Black,
                OutLineRectangle =
                    new Rectangle(new Point(x, y),
                        new Size(300, 24)),
                TextColor = Color.White,
                Text = " (注) 数字为本日的发生累计次数",
                NeedDarwOutline = false,
                Bold = true,
            });

            return true;
        }

        private void CreateGridItemAction(GridLine gridLine, GridColumnConfig gridColumnConfig, GridItemBase arg3)
        {
            var glInit = new GridLineInitialize() { ViewClass = this };
            glInit.InitInnerContrl(gridLine, gridColumnConfig, arg3);

            var txt = (GridTextItem)arg3;
            //TODO RefreshAction
            txt.RectText.Text = "0";
            txt.RectText.TextColor = CRH2Resource.YellowBrush.Color;

        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == 2)
            {
                m_TrainView.Location =
                    GlobalInfo.CurrentCRH2Config.EmputyRollConfig.TrainViewLocation.Rectangle.Location;
            }
        }

        public override string GetInfo()
        {
            return "空转滑行";
        }

    }
}