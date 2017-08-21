using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class SetDataTime : HMIBase
    {
        private readonly bool[] m_ActiveTextBox = new[] { false, false, false, false, false };
        private string[] m_TextBoxStrings = { "", "", "", "", "", "" };
        private List<Rectangle> m_Rec;
        private List<Region> m_Regions;
        private FjButtonEx m_Button;


        protected sealed override bool Initalize()
        {
            m_Rec = new List<Rectangle>
                    {
                        new Rectangle(80, 240, 40, 25),
                        new Rectangle(130, 240, 40, 25),
                        new Rectangle(180, 240, 40, 25),
                        new Rectangle(270, 240, 40, 25),
                        new Rectangle(320, 240, 40, 25),
                        new Rectangle(80, 210, 40, 25),
                        new Rectangle(130, 210, 40, 25),
                        new Rectangle(180, 210, 40, 25),
                        new Rectangle(270, 210, 40, 25),
                        new Rectangle(320, 210, 40, 25),
                        new Rectangle(80, 270, 40, 25),
                        new Rectangle(130, 270, 40, 25),
                        new Rectangle(180, 270, 40, 25),
                        new Rectangle(270, 270, 40, 25),
                        new Rectangle(320, 270, 40, 25),
                        new Rectangle(270, 100, 200, 25)
                    };

            m_Regions = new List<Region>();
            for (int i = 0; i < 5; i++)
            {
                m_Regions.Add(new Region(m_Rec[i]));
            }
            m_Button = new FjButtonEx(1, GlobleParam.ResManager.GetString("String118"), new Rectangle(195, 380, 97, 62));
            m_Button.MouseDown += (sender, arg) =>//TODO The Event
            {

            };
            return true;
        }

        public sealed override string GetInfo()
        {
            return "MasterRest";
        }

        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView, IranViewIndex lastView)
        {
            if (naviType == NavigateType.SwitchFromOther)
            {
                ResetActiveBox();
            }
        }

        private void ResetActiveBox()
        {
            for (int i = 0; i < m_ActiveTextBox.Length; i++)
            {
                m_ActiveTextBox[i] = false;
            }
            KeyBoard.PressedValue = string.Empty;
        }

        public sealed override bool mouseUp(Point point)
        {
            if (m_Button.IsVisible(point))
            {
                m_Button.OnMouseDown(point);
                return true;
            }
            int index = 0;
            for (; index < m_Regions.Count; index++)
            {
                if (m_Regions[index].IsVisible(point))
                {
                    break;
                }
            }
            if (index < m_Regions.Count)
            {
                ResetActiveBox();
                m_ActiveTextBox[index] = true;
            }
            return true;
        }

        public sealed override void paint(Graphics dcGs)
        {
            m_Button.OnDraw(dcGs);
            for (int i = 0; i < 5; i++)
            {
                dcGs.FillRectangle(m_ActiveTextBox[i] ? GdiCommon.WhiteBrush : GdiCommon.MediumGreyBrush, m_Rec[i]);
                dcGs.DrawRectangle(new Pen(GdiCommon.DarkGreyPen.Color, 3f), m_Rec[i]);
            }
            if (KeyBoard.IsEnter)
            {
                KeyBoard.PressedValue = string.Empty;
                KeyBoard.IsClr = false;
                KeyBoard.IsEnter = false;
            }
            else if (KeyBoard.IsClr)
            {
                SetTextBoxValue(true);
                KeyBoard.PressedValue = string.Empty;
            }
            else
            {
                SetTextBoxValue(false);
            }
            for (int i = 0; i < m_ActiveTextBox.Length; i++)
            {
                dcGs.DrawString(m_TextBoxStrings[i], GdiCommon.Txt12Font, GdiCommon.BlackBrush, m_Rec[i], GdiCommon.CenterFormat);
            }
            for (int i = 0; i < 11; i++)
            {
                dcGs.DrawString(GlobleParam.ResManagerText.GetString("Text" + (i + 10).ToString("0000")), i <= 9 ? GdiCommon.Txt8Font : GdiCommon.Txt14Font, GdiCommon.MediumGreyBrush, m_Rec[5 + i], GdiCommon.CenterFormat);
            }
        }
        /// <summary>
        /// 设置TextBox的值
        /// </summary>
        /// <param name="bPara">true 清空值，false 获取输入的值</param>
        private void SetTextBoxValue(bool bPara)
        {
            for (int i = 0; i < m_ActiveTextBox.Length; i++)
            {
                if (m_ActiveTextBox[i])
                {
                    m_TextBoxStrings[i] = bPara ? string.Empty : (KeyBoard.PressedValue == string.Empty ? m_TextBoxStrings[i] : (i == 0 ? KeyBoard.PressedValue.PadLeft(2, '0') : KeyBoard.PressedValue));
                }
            }
        }
    }
}
