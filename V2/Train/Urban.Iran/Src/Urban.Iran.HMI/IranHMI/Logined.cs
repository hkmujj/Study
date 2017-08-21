using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Logined : HMIBase
    {

        private List<FjButtonEx> m_ButtonList;

        private Dictionary<int, IranViewIndex> m_ViewID;

        private string[] m_Strings = { "Display intensity", "100", "100", "10" };

        private List<Rectangle> m_Rec;

        protected override bool Initalize()
        {
            m_ButtonList = new List<FjButtonEx>
                         {
                             new FjButtonEx(1, GlobleParam.ResManager.GetString("String182"), new Rectangle(100, 130, 97, 62)),
                             new FjButtonEx(2, GlobleParam.ResManager.GetString("String183"), new Rectangle(100, 240, 97, 62)),
                             new FjButtonEx(3, GlobleParam.ResManager.GetString("String186"), new Rectangle(100, 350, 97, 62)),
                             new FjButtonEx(4, GlobleParam.ResManager.GetString("String177"), new Rectangle(337, 130, 97, 62)),
                             new FjButtonEx(5, GlobleParam.ResManager.GetString("String185"), new Rectangle(337, 240, 97, 62)),
                             new FjButtonEx(6, GlobleParam.ResManagerText.GetString("Button0001"), new Rectangle(720, 130, 58, 58)),
                             new FjButtonEx(7, GlobleParam.ResManagerText.GetString("Button0002"), new Rectangle(720, 246, 58, 58))
                         };
            m_ViewID = new Dictionary<int, IranViewIndex>
                       {
                           { 1, (IranViewIndex)44 },
                           { 2, (IranViewIndex)45},
                           { 3, (IranViewIndex)46},
                           { 4, (IranViewIndex)1 },
                           { 5, (IranViewIndex)1 }, 
                           { 6, (IranViewIndex)1 },
                           { 7, (IranViewIndex)1 }
                       };
            m_Rec = new List<Rectangle>
                    {
                        new Rectangle(720,72,58,58), 
                        new Rectangle(720,188,58,58), 
                        new Rectangle(662,188,58,29), 
                        new Rectangle(662,217,58,28)
                    };
            m_ButtonList.ForEach(e => e.MouseDown += MouseDownMethod);
            return true;
        }

        private void MouseDownMethod(FjButtonEx btnsender, Point pt)
        {
            if (btnsender.BtnIndex <= 3)
            {
                ChangedPage(m_ViewID[btnsender.BtnIndex]);
            }
        }

        public override bool mouseUp(Point point)
        {
            foreach (var buttonEx in m_ButtonList.Where(buttonEx => buttonEx.IsVisible(point)))
            {
                buttonEx.OnMouseDown(point);
                break;
            }
            return true;
        }

        public override string GetInfo()
        {
            return "Logined";
        }

        public override void paint(Graphics dcGs)
        {
            m_ButtonList.ForEach(e => e.OnDraw(dcGs));
            dcGs.DrawString(m_Strings[0], GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, m_Rec[0], GdiCommon.CenterFormat);
            dcGs.FillRectangle(GdiCommon.DarkGreyBrush, m_Rec[1]);
            dcGs.DrawString(m_Strings[1], GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, m_Rec[1], GdiCommon.CenterFormat);
            dcGs.DrawString(m_Strings[2], GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, m_Rec[2], GdiCommon.CenterFormat);
            dcGs.DrawString(m_Strings[3], GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, m_Rec[3], GdiCommon.CenterFormat);

        }
    }
}
