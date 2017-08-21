using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ARChangeValue : HMIBase
    {
        private Bitmap m_Bitmap;
        private Rectangle[] m_RectArr;
        private FjButtonEx[] m_BtnArr;
        private string[] m_StrActive = new[] { "", "", "" };
        private bool[] m_ActiveText = new[] { false, false, false };
        private List<Region> m_Rect;
        private void GetValue()
        {
            string tmp;
            if (KeyBoard.IsClr)
            {
                tmp = string.Empty;
                KeyBoard.PressedValue = string.Empty;
            }
            else
            {
                tmp = KeyBoard.PressedValue;
            }
            for (var i = 0; i < m_ActiveText.Length; i++)
            {
                if (!m_ActiveText[i]) continue;
                m_StrActive[i] = tmp;
                break;
            }
        }

        private void BtnMouseDown(FjButtonEx btnSender, Point pt)
        {
            if (btnSender.BtnIndex == (int) IranViewIndex.AccRetardationTest)
            {
                ChangedPage(IranViewIndex.AccRetardationTest);
            }
        }

        public override string GetInfo()
        {
            return "ARChangeValue";
        }

        protected override bool Initalize()
        {

            m_Bitmap = new Bitmap(RecPath + "\\frame/ChangeValue.jpg");
            m_RectArr = new[]
                      {
                          new Rectangle(0, 153, 800, 316),
                          new Rectangle(140, 160, 200, 25),
                          new Rectangle(15, 215, 170, 25),
                          new Rectangle(310, 215, 170, 25),
                          new Rectangle(200, 250, 100, 35),
                          new Rectangle(200, 315, 100, 35),

                          new Rectangle(16, 249, 72, 38),
                          new Rectangle(96, 249, 88, 38),
                          new Rectangle(16, 313, 72, 38),
                          new Rectangle(96, 313, 88, 38),

                          new Rectangle(312, 313, 88, 38),
                          new Rectangle(392, 313, 88, 38),

                          new Rectangle(98, 251, 84, 34),
                          new Rectangle(98, 315, 84, 34),
                          new Rectangle(394, 315, 84, 34),

                      };
            m_BtnArr = new[]
                     {
                         new FjButtonEx(1, GlobleParam.ResManager.GetString("String108"), new Rectangle(203, 385, 97, 62)),
                         new FjButtonEx(29,"◀ "+ GlobleParam.ResManager.GetString("String243"), new Rectangle(16, 385, 97, 62))
                     };
            m_Rect = new List<Region>
            {
                new Region(m_RectArr[7]),
                new Region(m_RectArr[9]),
                
                new Region(m_RectArr[11])
            };
            foreach (var btn in m_BtnArr)
            {
                btn.MouseDown += BtnMouseDown;
            }
            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            g.DrawImage(m_Bitmap, m_RectArr[0]);
            g.DrawString(GlobleParam.ResManagerText.GetString("Text0022"), GdiCommon.Txt12Font, GdiCommon.WhiteBrush, m_RectArr[1], GdiCommon.CenterFormat);
            g.DrawString(GlobleParam.ResManager.GetString("String146"), GdiCommon.Txt12Font, GdiCommon.WhiteBrush, m_RectArr[2], GdiCommon.CenterFormat);
            g.DrawString(GlobleParam.ResManager.GetString("String150"), GdiCommon.Txt12Font, GdiCommon.WhiteBrush, m_RectArr[3], GdiCommon.CenterFormat);
            g.DrawString(GlobleParam.ResManager.GetString("String147"), GdiCommon.Txt12Font, GdiCommon.WhiteBrush, m_RectArr[4], GdiCommon.CenterFormat);
            g.DrawString(GlobleParam.ResManager.GetString("String148"), GdiCommon.Txt12Font, GdiCommon.WhiteBrush, m_RectArr[5], GdiCommon.CenterFormat);

            g.FillRectangle(m_ActiveText[0] ? GdiCommon.WhiteBrush : GdiCommon.MediumGreyBrush, m_RectArr[12]);
            g.FillRectangle(m_ActiveText[1] ? GdiCommon.WhiteBrush : GdiCommon.MediumGreyBrush, m_RectArr[13]);
            g.FillRectangle(m_ActiveText[2] ? GdiCommon.WhiteBrush : GdiCommon.MediumGreyBrush, m_RectArr[14]);

            g.DrawString("20", GdiCommon.Txt12Font, GdiCommon.DarkBlueBrush, m_RectArr[6], GdiCommon.CenterFormat);
            g.DrawString(m_StrActive[0], GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, m_RectArr[7], GdiCommon.CenterFormat);
            g.DrawString("80", GdiCommon.Txt12Font, GdiCommon.DarkBlueBrush, m_RectArr[8], GdiCommon.CenterFormat);
            g.DrawString(m_StrActive[1], GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, m_RectArr[9], GdiCommon.CenterFormat);
            g.DrawString("20", GdiCommon.Txt12Font, GdiCommon.DarkBlueBrush, m_RectArr[10], GdiCommon.CenterFormat);
            g.DrawString(m_StrActive[2], GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, m_RectArr[11], GdiCommon.CenterFormat);

            foreach (var btn in m_BtnArr)
            {
                btn.OnDraw(g);
            }

        }

        private void Reset(int index)
        {
            KeyBoard.IsClr = false;
            KeyBoard.IsEnter = false;
            KeyBoard.PressedValue = string.Empty;
            for (int i = 0; i < m_ActiveText.Length; i++)
            {
                m_ActiveText[i] = false;
            }
            if (index == -1 || index >= m_ActiveText.Length)
            {
                return;
            }
            m_ActiveText[index] = true;
        }

        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView, IranViewIndex lastView)
        {
            if (naviType == NavigateType.SwitchFromOther)
            {
                Reset(-1);
            }
        }

        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < m_Rect.Count; index++)
            {
                if (m_Rect[index].IsVisible(point))
                {
                    break;
                }
            }
            if (index < m_Rect.Count)
            {
                Reset(index);
            }
            foreach (var btn in m_BtnArr.Where(btn => btn.IsVisible(point)))
            {
                btn.OnMouseDown(point);
                break;
            }
            return true;
        }
    }
}