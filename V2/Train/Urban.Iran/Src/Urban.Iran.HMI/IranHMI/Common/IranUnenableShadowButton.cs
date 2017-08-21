using System.Drawing;
using CommonUtil.Controls.Button;
using CommonUtil.Util.Extension;

namespace Urban.Iran.HMI.Common
{
    public class IranUnenableShadowButton : GDIButton
    {
        public IBtnBehavierStrategy IranShadowTextButtonBehavier { get; internal set; }

        public IBtnBehavierStrategy DefaultButtonBehavier { get; internal set; }

        public IranUnenableShadowButton()
        {
            IranShadowTextButtonBehavier = new IranShadowTextButtonBehavier(this)
            {
                ShadowTextBrush = Brushes.Black,
                LocationTraslateAction = point => point.Translate(-2, -2)
            };

            DefaultButtonBehavier = new IranHmiDefaultButtonBehavier(this);

            IsEnable = true;
        }

        public override bool IsEnable
        {
            get { return base.IsEnable; }
            set
            {
                base.IsEnable = value;
                BtnBehavierStrategy = IsEnable ? DefaultButtonBehavier : IranShadowTextButtonBehavier;
            }
        }

        public override bool OnMouseDown(Point point)
        {
            if (OutLineRectangle.Contains(point) && IsEnable)
            {
                BtnBehavierStrategy = IranShadowTextButtonBehavier;
            }
            return base.OnMouseDown(point);
        }

        public override bool OnMouseUp(Point point)
        {
            if (OutLineRectangle.Contains(point) && IsEnable)
            {
                BtnBehavierStrategy = DefaultButtonBehavier;
            }
            return base.OnMouseUp(point);
        }
    }
}