using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class KeyBoard : HMIBase
    {
        private List<FjButtonEx> m_KeyBtnArr;
        public static string PressedValue { set; get; }
        public static bool IsEnter { set; get; }
        public static bool IsClr { set; get; }

        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView, IranViewIndex lastView)
        {
            if (naviType == NavigateType.SwitchFromOther)
            {
                PressedValue = string.Empty;
            }
        }

        private static void BtnMouseDown(FjButtonEx btnSender, Point pt)
        {
            if (btnSender.BtnIndex < (int) IranViewIndex.HVACPage2)
            {
                IsClr = false;
                IsEnter = false;
                if (PressedValue.Length<4)
                {
                    PressedValue += btnSender.BtnIndex;
                }
            }
            else if (btnSender.BtnIndex == (int) IranViewIndex.HVACLegend)
            {
                IsClr = false;
                IsEnter = true;
                PressedValue = string.Empty;
            }
            else
            {
                IsClr = true;
                IsEnter = false;
                PressedValue = string.Empty;
            }
        }

        public override string GetInfo()
        {
            return "KeyBoard";
        }

        protected override bool Initalize()
        {
            PressedValue = string.Empty;
            var keyBoardRect = new[]
                                       {
                                           new Rectangle(505, 169, 63, 64),
                                           new Rectangle(577, 169, 63, 64),
                                           new Rectangle(649, 169, 63, 64),

                                           new Rectangle(505, 241, 63, 64),
                                           new Rectangle(577, 241, 63, 64),
                                           new Rectangle(649, 241, 63, 64),

                                           new Rectangle(505, 313, 63, 64),
                                           new Rectangle(577, 313, 63, 64),
                                           new Rectangle(649, 313, 63, 64),

                                           new Rectangle(505, 385, 63, 64),
                                           new Rectangle(577, 385, 63, 64),
                                           new Rectangle(649, 385, 99, 64)
                                       };

            m_KeyBtnArr = new List<FjButtonEx>
                        {
                            new FjButtonEx(7, "7", keyBoardRect[0]),
                            new FjButtonEx(8, "8", keyBoardRect[1]),
                            new FjButtonEx(9, "9", keyBoardRect[2]),
                            new FjButtonEx(4, "4", keyBoardRect[3]),
                            new FjButtonEx(5, "5", keyBoardRect[4]),
                            new FjButtonEx(6, "6", keyBoardRect[5]),
                            new FjButtonEx(1, "1", keyBoardRect[6]),
                            new FjButtonEx(2, "2", keyBoardRect[7]),
                            new FjButtonEx(3, "3", keyBoardRect[8]),
                            new FjButtonEx(10, "Clr", keyBoardRect[9]),
                            new FjButtonEx(0, "0", keyBoardRect[10]),
                            new FjButtonEx(11, "Enter", keyBoardRect[11]),

                        };

            foreach (var btn in m_KeyBtnArr)
            {
                btn.BtnFont = GdiCommon.Txt14Font;
                btn.MouseDown += BtnMouseDown;
            }

            return true;
        }

        public override void paint(Graphics g)
        {
            m_KeyBtnArr.ForEach(e => e.OnDraw(g));
        }

        public override bool mouseDown(Point point)
        {
            foreach (var btn in m_KeyBtnArr)
            {
                if (btn.IsVisible(point))
                {
                    btn.Status = BtnStatus.Active;
                    btn.OnMouseDown(point);
                }
                else
                {
                    btn.Status = BtnStatus.Normal;
                }
            }

            return true;
        }
    }
}