using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CRH2MMI.Common.Util;

namespace CRH2MMI.Common
{
    public class GDIConnectHood : CommonInnerControlBase
    {
        public List<CommonInnerControlBase> Connect { get; set; }
        public CommonInnerControlBase TitleConnect { get; set; }
        public string Text { get; set; }
        public Font TextFont { get; set; }
        public Brush TextBrush { get; set; }
        public override void OnDraw(Graphics g)
        {
            g.DrawRectangle(CRH2Resource.WhitePen, OutLineRectangle);
            TitleConnect.OnDraw(g);
            Connect.ForEach(f => f.OnPaint(g));
        }
    }
}