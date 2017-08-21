using System.Collections.Generic;
using System.Drawing;

namespace Engine.TCMS.HXD3D.HXD3D
{
    public class ButtonState1 : ButtonState
    {
        // Methods
        public ButtonState1(RectangleF baseRect, Image[] baseImg, RectangleF midRect, List<Image> midImgs, bool canBeLocked) : this(baseRect, baseImg, midRect, midImgs, null, canBeLocked)
        {
        }

        public ButtonState1(RectangleF baseRect, Image[] baseImg, RectangleF midRect, string strSkin, bool canBeLocked) : this(baseRect, baseImg, midRect, null, strSkin, canBeLocked)
        {
        }

        public ButtonState1(RectangleF baseRect, Image[] baseImg, RectangleF midRect, List<Image> midImgs, string strSkin, bool canBeLocked) : base(baseRect, baseImg, midRect, null, strSkin, canBeLocked)
        {
        }

        public override void DrawMidState(Graphics g, SolidBrush theMidBrush)
        {
            if (IsLocked)
            {
                g.FillRectangle(theMidBrush, RectSkin);
            }
        }
    }
}