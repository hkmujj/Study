using System.Drawing;
using CommonUtil.Controls.Button;
using CommonUtil.Util.Extension;

namespace Urban.Iran.HMI.Common
{
    public static class GDIButtonExtention
    {
        public static IBtnBehavierStrategy GetNormalButtonBehavierStrategy(this GDIButton btn)
        {
            return new IranHmiDefaultButtonBehavier(btn);
        }

        public static IBtnBehavierStrategy GetShadowTextButtonBehavier(this GDIButton btn, Color textColor)
        {
            btn.TextColor = textColor;
            return new IranShadowTextButtonBehavier(btn)
            {
                ShadowTextBrush = Brushes.Black,
                LocationTraslateAction = point => point.Translate(-2, -2)
            };
        }
    }
}