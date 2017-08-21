using System.Drawing;
using CRH2MMI.Common.Util;

namespace CRH2MMI.Marshing
{
    class MarshingItemView : MarshItemView
    {
        public RectTextInfo[] m_MarshingTexts;

        public Rectangle m_TurnBackBtnRegion = new Rectangle(672, 529, 115, 42);

        public MarshingItemView(MarshingPage currentPage, Marsh parentView) : base(currentPage, parentView)
        {
        }

        public override bool OnMouseDown(Point point)
        {
            if (m_TurnBackBtnRegion.Contains(point))
            {
                OnMarshButtonClick(this, MarshButtonType.TurnBack);
                return true;
            }
            return false;
        }
    }
}