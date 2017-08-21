using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid.GridLine;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using MMI.Facility.Interface;

namespace CRH2MMI.CutState
{
    abstract class RemovalStateContentViewBase
    {
        protected List<GDIRectText> m_StateTitles;

        public List<GridLine> StateGrid { get; protected set; }

        public abstract void Initalize(baseClass viewClass);

        public void OnDraw(Graphics g)
        {
            StateGrid.ForEach(e => e.OnPaint(g));
            m_StateTitles.ForEach(e => e.OnDraw(g));
        }


        protected void CreateGridAndTitle(baseClass viewClass)
        {
            var glInit = new GridLineInitialize() { ViewClass = viewClass };
            var conf = GlobalInfo.CurrentCRH2Config.CutInfoConfig;
            conf.GridLine.ForEach(e => e.RefreshRelation());
            StateGrid = conf.GridLine.Select(s => GDIGridLineHelper.CreateGridLine(s, glInit.InitInnerContrl)).ToList();
            m_StateTitles = glInit.CreateTitles(StateGrid.First(), conf.GridLine.First());
        }

    }
}
