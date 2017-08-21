using CommonUtil.Controls.Button;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI.Extention
{
    internal static class ButtonExtention
    {
        public static GDIButton SetHVACNoaml(this GDIButton button)
        {
            var btn = button as IranUnenableShadowButton;

            //btn.IranShadowTextButtonBehavier = new IranShadowTextButtonBehavier(btn) { ShadowTextBrush = GdiCommon.MediumGreyBrush };


            return btn;
        }
    }
}