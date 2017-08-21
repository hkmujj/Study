using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;

namespace Urban.Iran.HMI.Common
{
    public class IranUnenableShadowText : GDIRectText
    {
        private readonly IRectTextBehavierStrategy m_IranShadowTextButtonBehavier;

        private readonly IRectTextBehavierStrategy m_DefaultButtonBehavier;

        public IranUnenableShadowText()
        {
            m_IranShadowTextButtonBehavier = new IranShadowRectTextBehavier(this)
            {
                ShadowTextBrush = Brushes.Black,
                LocationTraslateAction = point => point.Translate(-2, -2)
            };

            m_DefaultButtonBehavier = new DefaultRectTextBehavierStrategy(this);

            IsEnable = true;
        }

        public override bool IsEnable
        {
            get { return base.IsEnable; }
            set
            {
                base.IsEnable = value;
                RectTextBehavierStrategy = IsEnable ? m_DefaultButtonBehavier : m_IranShadowTextButtonBehavier;
            }
        }
    }
}