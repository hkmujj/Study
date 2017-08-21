using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Roundness;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;
using CRH2MMI.Title;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid.GridLine;

namespace CRH2MMI.Notify
{
    /// <summary>
    /// 通知状态
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Notify : CRH2BaseClass
    {
        private TrainView m_TrainView;

        private List<CommonInnerControlBase> m_ConstInfos;

        private GridLine m_GridLine;

        private Title.Title m_Title;


        public override void paint(Graphics g)
        {
            m_ConstInfos.ForEach(e => e.OnDraw(g));
            m_GridLine.OnPaint(g);
        }

        public override bool Init()
        {

            m_TrainView = TrainView.Instance;

            m_Title = Title.Title.Instance;

            var notifyConfig = GlobalInfo.CurrentCRH2Config.NotifyConfig;
            notifyConfig.Grid.RefreshRelation();

            InitUIObj(notifyConfig.Grid.Rows.SelectMany(s => s.ColumnConfigs).Cast<CRH2CommunicationPortModel>());

            var glInit = new GridLineInitialize() { ViewClass = this };
            m_GridLine = GDIGridLineHelper.CreateGridLine(notifyConfig.Grid, glInit.InitInnerContrl);

            InitConstInfo(glInit, notifyConfig);

            return true;
        }

        private void InitConstInfo(GridLineInitialize glInit, NotifyConfig notifyConfig)
        {
            var y = m_GridLine.OutLineRectangle.Bottom + 70;
            var mid = notifyConfig.Grid.AbsX + notifyConfig.Grid.Width / 2;
            var x = mid - 120;
            var textSize = new Size(130, 18);
            const int r = 8;
            const int interval = 20;

            m_ConstInfos = new List<CommonInnerControlBase>()
            {
                new GDIRectText()
                {
                    BkColor = Color.Black,
                    OutLineRectangle = new Rectangle(new Point(x, y), new Size(textSize.Width, textSize.Height)),
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
                    Center = new Point(x + interval + r, y + textSize.Height/2),
                    R = r,
                },
                new GDIRoundness()
                {
                    ContentColor = Color.Red,
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
                    Text = "  : 通知)",
                    NeedDarwOutline = false,
                    Bold = true,
                },
            };

            m_ConstInfos.AddRange(glInit.CreateTitles(m_GridLine, notifyConfig.Grid).Cast<CommonInnerControlBase>());
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                m_Title.TitleMenuClicked += OnTitleMenuClick;
                m_Title.TitleShowStrategy = new TitleMenuShowStrategy() { MenuType = TitleMenuType.Retrun };
                var view = (ViewConfig) nParaB;
                switch (view)
                {
                    case ViewConfig.Notify:
                        m_TrainView.Location = GlobalInfo.CurrentCRH2Config.NotifyConfig.TrainViewLocation.Rectangle.Location;
                        break;
                }
            }
        }

        private void OnTitleMenuClick(object sender, TitleMenuClickArgs e)
        {
            
            m_Title.TitleMenuClicking -= OnTitleMenuClick;

            m_Title.ResetTitleShowStrategy();
        }

        public override string GetInfo()
        {
            return "通知状态";
        }

    }
}