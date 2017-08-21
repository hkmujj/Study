using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class PISLegend : HMIBase
    {
        private Rectangle m_ImageRectangle;

        protected override bool Initalize()
        {
            m_ImageRectangle = new Rectangle(0, 70, 800, 400);

            return true;
        }

        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView, IranViewIndex lastView)
        {
            if (naviType == NavigateType.SwitchFromOther)
            {

            }
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(Images[0], m_ImageRectangle);
        }
    }
}