using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class SetWheelDiameter : HMIBase
    {

        private List<Rectangle> m_Rec;
        private readonly bool[] m_ActiveTextbox = { false, false };
        private List<Region> m_Regions;
        string m_InputValue1;
        string m_InputValue2;
        protected sealed override bool Initalize()
        {
            m_Rec = new List<Rectangle>
                    {
                        new Rectangle(80, 170, 300, 40),
                        new Rectangle(80, 310, 300, 40),
                        new Rectangle(175, 215, 75, 25),
                        new Rectangle(175, 355, 75, 25)
                    };
            m_Regions = new List<Region>
                        {
                            new Region(m_Rec[2]),
                            new Region(m_Rec[3])
                        };
            return true;
        }

        public sealed override string GetInfo()
        {
            return "MasterRest";
        }

        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < m_Regions.Count; index++)
            {
                if (m_Regions[index].IsVisible(point))
                {
                    break;
                }
            }
            if (index >= m_Regions.Count)
            {
                return true;
            }
            KeyBoard.PressedValue = string.Empty;
            m_ActiveTextbox[0] = false;
            m_ActiveTextbox[1] = false;
            m_ActiveTextbox[index] = true;
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawString(GlobleParam.ResManagerText.GetString("Text0008"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, m_Rec[0], GdiCommon.CenterFormat);
            dcGs.DrawString(GlobleParam.ResManagerText.GetString("Text0009"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, m_Rec[1], GdiCommon.CenterFormat);

            dcGs.FillRectangle(m_ActiveTextbox[0] ? GdiCommon.WhiteBrush : GdiCommon.MediumGreyBrush, m_Rec[2]);
            dcGs.FillRectangle(m_ActiveTextbox[1] ? GdiCommon.WhiteBrush : GdiCommon.MediumGreyBrush, m_Rec[3]);

            if (m_ActiveTextbox[0])
            {
                dcGs.DrawRectangle(new Pen(GdiCommon.DarkGreyPen.Color, 3f), m_Rec[2]);
            }
            else if (m_ActiveTextbox[1])
            {
                dcGs.DrawRectangle(new Pen(GdiCommon.DarkGreyPen.Color, 3f), m_Rec[3]);
            }

            if (KeyBoard.IsEnter)
            {
                KeyBoard.PressedValue = string.Empty;
                KeyBoard.IsClr = false;
                KeyBoard.IsEnter = false;
            }
            else if (KeyBoard.IsClr)
            {
                m_InputValue1 = string.Empty;
                KeyBoard.PressedValue = string.Empty;
            }
            else
            {
                if (m_ActiveTextbox[0])
                {
                    m_InputValue1 = KeyBoard.PressedValue;
                }
                if (m_ActiveTextbox[1])
                {
                    m_InputValue2 = KeyBoard.PressedValue;
                }
            }
            if (m_ActiveTextbox[0] || m_ActiveTextbox[1])
            {
                dcGs.DrawString(m_InputValue1, GdiCommon.Txt12Font, GdiCommon.BlackBrush, m_Rec[2], GdiCommon.CenterFormat);
                dcGs.DrawString(m_InputValue2, GdiCommon.Txt12Font, GdiCommon.BlackBrush, m_Rec[3], GdiCommon.CenterFormat);
            }

        }
    }
}
