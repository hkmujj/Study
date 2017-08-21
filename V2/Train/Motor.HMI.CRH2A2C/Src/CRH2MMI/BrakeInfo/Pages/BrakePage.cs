using System;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Grid.Items;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid.GridLine;

namespace CRH2MMI.BrakeInfo.Pages
{
    /// <summary>
    /// 制动页面
    /// </summary>
    class BrakePage : BrakeInfoPageBase
    {
        private GridLine m_GridLine;

        public BrakePage(BrakeInfo brakeInfo) : base(brakeInfo)
        {
            PageName = "制动";
        }

        public override void Init()
        {
            var conf = GlobalInfo.CurrentCRH2Config.BrakeInfoConfig.BrakePageConfigs.Find(f => f.PageName == PageName).Grid;

            var glInit = new GridLineInitialize() { ViewClass = m_BrakeInfo };
            m_GridLine =
                GDIGridLineHelper.CreateGridLine(conf, glInit.InitInnerContrl);
            foreach (var it in m_GridLine.AllItems)
            {
                ((GridTextItem) it).GetInnerContrl().TextColor = TextColor;
            }

            var consInfo = glInit.CreateTitles(m_GridLine, conf);
            consInfo.ForEach(e =>
            {
                e.DrawFont = new Font(e.DrawFont.Name, 13);
                e.Size = new Size(e.Size.Width + 30, e.Size.Height);
            });
            m_ConstInfos = consInfo.Cast<CommonInnerControlBase>().ToList();
        }

        public override void OnPaint(Graphics g)
        {
            m_ConstInfos.ForEach(e => e.OnDraw(g));

            m_GridLine.OnPaint(g);
        }

        protected override void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            m_GridLine.Reverse();
        }
    }
}
