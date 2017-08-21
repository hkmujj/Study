using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Grid.Items;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CommonUtil.Controls;

namespace CRH2MMI.Tow1.Pages
{
    class Tow1Page : Tow1InfoPageBase
    {

        public Tow1Page(TowInfo towInfo)
            : base(towInfo)
        {
            PageName = "牵引变流器";
        }

        public override void Init()
        {
            base.Init();

            var conf = GlobalInfo.CurrentCRH2Config.Tow1Config.Tow1PageConfigs.Find(f => f.PageName == PageName).Grid;

            var glInit = new GridLineInitialize() { ViewClass = m_TowInfo };
            m_GridLine =
                GDIGridLineHelper.CreateGridLine(conf, glInit.InitInnerContrl);
            foreach (var it in m_GridLine.AllItems)
            {
                ((GridTextItem)it).GetInnerContrl().TextColor = TextColor;
            }

            var constInfos = glInit.CreateTitles(m_GridLine, conf);
            constInfos.ForEach(e =>
            {
                e.DrawFont = new Font(e.DrawFont.Name, 13);
                e.Size = new Size(e.Size.Width + 50, e.Size.Height);
            });
            m_ConstInfos.AddRange(constInfos.Cast<CommonInnerControlBase>());
        }
    }
}
