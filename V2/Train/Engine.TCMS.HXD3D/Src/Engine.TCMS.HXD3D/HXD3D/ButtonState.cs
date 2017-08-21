using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.底层共用;

namespace Engine.TCMS.HXD3D.HXD3D
{
    public class ButtonState
    {
        // Fields
        private SolidBrush[] m_AllTheBrush;

        private readonly Image[] m_ImgBase;
        private readonly List<Image> m_ImgsSkin;
        private readonly RectangleF m_RectBase;
        protected RectangleF RectSkin;
        private string m_StrSkin;
        private readonly bool m_TheBtnCouldBeLock;
        private Font m_TheFont;

        // Methods
        public ButtonState(RectangleF baseRect, Image[] baseImg, RectangleF midRect, List<Image> midImgs, bool canBeLocked) : this(baseRect, baseImg, midRect, midImgs, null, canBeLocked)
        {
        }

        public ButtonState(RectangleF baseRect, Image[] baseImg, RectangleF midRect, string strSkin, bool canBeLocked) : this(baseRect, baseImg, midRect, null, strSkin, canBeLocked)
        {
        }

        public ButtonState(RectangleF baseRect, Image[] baseImg, RectangleF midRect, List<Image> midImgs, string strSkin, bool canBeLocked)
        {
            m_TheFont = FontsItems.Fonts_Regular(FontName.黑体, 10.5f, true);
            m_RectBase = baseRect;
            m_ImgBase = baseImg;
            RectSkin = midRect;
            if (midImgs != null)
            {
                m_ImgsSkin = midImgs;
            }
            if (strSkin != null)
            {
                m_StrSkin = strSkin;
            }
            m_TheBtnCouldBeLock = canBeLocked;
        }

        public void BtnRecover()
        {
            IsLocked = false;
        }

        public void BtnStateChange(bool isDown)
        {
            if (isDown && m_TheBtnCouldBeLock)
            {
                IsLocked = true;
            }
            else if (!(isDown || !m_TheBtnCouldBeLock))
            {
                BtnRecover();
            }
        }

        private void DrawBaseState(Graphics gs, bool isDown)
        {
            if ((m_ImgBase != null) && (m_ImgBase.Length > 1))
            {
                gs.DrawImage(isDown ? m_ImgBase[1] : m_ImgBase[0], m_RectBase);
            }
        }

        private void DrawMidPicState(Graphics gs)
        {
            if (m_ImgsSkin != null)
            {
                if (m_ImgsSkin.Count > 1)
                {
                    gs.DrawImage(IsLocked ? m_ImgsSkin[1] : m_ImgsSkin[0], RectSkin);
                }
                else
                {
                    gs.DrawImage(m_ImgsSkin[0], RectSkin);
                }
            }
        }

        public virtual void DrawMidState(Graphics g, SolidBrush theMidBrush)
        {
            if (IsLocked)
            {
                g.FillRectangle(theMidBrush, RectSkin);
            }
        }

        private void DrawSkinState(Graphics gs, SolidBrush[] strBrush)
        {
            if (strBrush.Length == 1)
            {
                gs.FillRectangle(CommonRes.BlueBrush, RectSkin);
            }
            if (m_StrSkin != null)
            {
                gs.DrawRectangle(new Pen(Color.White), new Rectangle((int)RectSkin.X, (int)RectSkin.Y, (int)RectSkin.Width, (int)RectSkin.Height));
                if (strBrush.Length == 5)
                {
                    gs.FillRectangle(strBrush[4], RectSkin);
                }
                if (!(IsLocked || (strBrush.Length == 0)))
                {
                    gs.DrawString(m_StrSkin, m_TheFont, strBrush[0], RectSkin, FontsItems.TheAlignment(FontRelated.居中));
                }
                else if (IsLocked && (strBrush.Length != 0))
                {
                    gs.DrawString(m_StrSkin, m_TheFont, strBrush[1], RectSkin, FontsItems.TheAlignment(FontRelated.居中));
                }
                if (strBrush.Length == 3)
                {
                    gs.DrawString(m_StrSkin, m_TheFont, strBrush[1], RectSkin, FontsItems.TheAlignment(FontRelated.居中));
                }
            }
        }

        public void DrawTheBtn(Graphics e, bool isPress, SolidBrush[] allBrush)
        {
            DrawTheGeliBtn(e, isPress, null, allBrush);
        }

        public void DrawTheGeliBtn(Graphics e, bool isPress, Image midImg, SolidBrush[] allBrush)
        {
            SolidBrush[] brushArray;
            DrawBaseState(e, isPress);
            if (allBrush.Length == 2)
            {
                DrawMidState(e, CommonRes.YellowBrush);
            }
            if (allBrush.Length == 5)
            {
                DrawMidState(e, CommonRes.YellowBrush);
            }
            if (allBrush.Length == 1)
            {
                DrawMidState(e, CommonRes.BlueBrush);
            }
            DrawMidPicState(e);
            if (allBrush.Length == 6)
            {
                brushArray = new SolidBrush[] { allBrush[1], allBrush[2], allBrush[3], allBrush[4], allBrush[5] };
            }
            else if (allBrush.Length == 5)
            {
                brushArray = new SolidBrush[] { allBrush[1], allBrush[2], allBrush[3], allBrush[4] };
            }
            else if (allBrush.Length == 2)
            {
                brushArray = new SolidBrush[] { allBrush[1], allBrush[0] };
            }
            else
            {
                brushArray = allBrush;
            }
            DrawSkinState(e, brushArray);
            if (midImg != null)
            {
                e.DrawImage(midImg, RectSkin);
            }
        }

        private RectangleF[] SpecialFontArea(RectangleF rect)
        {
            return new RectangleF[] { new RectangleF(rect.X - 1f, rect.Y - 1f, rect.Width, rect.Height), new RectangleF(rect.X, rect.Y, rect.Width, rect.Height), new RectangleF(rect.X + 1f, rect.Y + 1f, rect.Width, rect.Height) };
        }

        // Properties
        public bool IsLocked { protected set; get; }

        public string StrSkin
        {
            get
            {
                return m_StrSkin;
            }

            set
            {
                m_StrSkin = value;
            }
        }

        public Font TheFont
        {
            get
            {
                return m_TheFont;
            }

            set
            {
                m_TheFont = value;
            }
        }
    }
}