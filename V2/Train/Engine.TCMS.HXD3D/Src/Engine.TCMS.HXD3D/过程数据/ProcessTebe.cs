using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ProcessTebe : HXD3BaseClass
    {
        // Fields
        private bool[] m_BValue;

        private float[] m_FValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList = new List<RectangleF>();
        private readonly NeedChangeLength[] m_TheChangeRectsArr = new NeedChangeLength[20];

        // Methods
        public void Draw(Graphics e)
        {
            PaintGroundImage(e);
            PaintValue(e);
        }

        private SolidBrush GetBrush(bool isUp)
        {
            if (isUp)
            {
                return SolidBrushsItems.BlueBrush1;
            }
            return SolidBrushsItems.RedBrush1;
        }

        public override string GetInfo()
        {
            return "牵引/制动力";
        }

        private void GetValue()
        {
            int num;
            for (num = 0; num < m_BValue.Length; num++)
            {
                m_BValue[num] = BoolList[UIObj.InBoolList[num]];
            }
            for (num = 0; num < m_FValue.Length; num++)
            {
                m_FValue[num] = FloatList[UIObj.InFloatList[num]];
            }
        }

        protected override  void Initalize()
        {
            InitData();
        }

        private void InitData()
        {
            int num;
            m_BValue = new bool[UIObj.InBoolList.Count];
            m_FValue = new float[UIObj.InFloatList.Count];
            m_ImgsList = new List<Image>();
            for (num = 0; num < UIObj.ParaList.Count; num++)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + UIObj.ParaList[num]));
            }
            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.ProcessTebe, ref m_RectsList))
            {
                m_HasPlan = true;
            }
            for (num = 0; num < 6; num++)
            {
                m_TheChangeRectsArr[num] = new NeedChangeLength(m_RectsList[13 + num], 100f, RectRiseDirection.上, false);
                m_TheChangeRectsArr[6 + num] = new NeedChangeLength(m_RectsList[0x13 + num], 100f, RectRiseDirection.下, false);
            }
            for (num = 0; num < 2; num++)
            {
                m_TheChangeRectsArr[12 + num] = new NeedChangeLength(m_RectsList[0x19 + num], 600f, RectRiseDirection.上, false);
                m_TheChangeRectsArr[14 + num] = new NeedChangeLength(m_RectsList[0x1b + num], 600f, RectRiseDirection.下, false);
            }
            for (num = 0; num < 2; num++)
            {
                m_TheChangeRectsArr[0x10 + num] = new NeedChangeLength(m_RectsList[0x1d + num], 600f, RectRiseDirection.上, false);
                m_TheChangeRectsArr[0x12 + num] = new NeedChangeLength(m_RectsList[0x1f + num], 600f, RectRiseDirection.下, false);
            }
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            Draw(dcGs);
        }

        private void PaintGroundImage(Graphics e)
        {
            int num;
            RectangleF ef;
            e.DrawImage(m_ImgsList[0], m_RectsList[0]);
            for (num = 0; num < 12; num++)
            {
                ef = m_RectsList[1 + num];
                ef = m_RectsList[1 + num];
                ef = m_RectsList[1 + num];
                ef = m_RectsList[1 + num];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            }
            for (num = 0; num < 8; num++)
            {
                ef = m_RectsList[0x21 + num];
                ef = m_RectsList[0x21 + num];
                ef = m_RectsList[0x21 + num];
                ef = m_RectsList[0x21 + num];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            }
        }

        private void PaintValue(Graphics e)
        {
            int num;
            for (num = 0; num < 9; num++)
            {
                e.DrawString(Convert.ToInt32(m_FValue[num]).ToString("0"), FontsItems.DefaultFont, SolidBrushsItems.YellowBrush1, m_RectsList[num + 1], FontsItems.TheAlignment(FontRelated.居中));
            }
            e.DrawString(Convert.ToInt32(m_FValue[9]) + "km/h", FontsItems.Fonts_Regular(FontName.宋体, 20f, true), SolidBrushsItems.YellowBrush1, m_RectsList[10], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(m_FValue[10] > 0f ? Convert.ToInt32(m_FValue[10]).ToString() : "-", FontsItems.Fonts_Regular(FontName.宋体, 20f, true), SolidBrushsItems.YellowBrush1, m_RectsList[11], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(m_FValue[11].ToString("0.0"), FontsItems.Fonts_Regular(FontName.宋体, 20f, true), SolidBrushsItems.YellowBrush1, m_RectsList[12], FontsItems.TheAlignment(FontRelated.居中));
            for (num = 0; num < 6; num++)
            {
                if (!(!m_BValue[0] || m_BValue[1]))
                {
                    m_TheChangeRectsArr[num].DrawRectangle(e, ref m_FValue[num], GetBrush(true));
                }
                else if (!(m_BValue[0] || !m_BValue[1]))
                {
                    m_TheChangeRectsArr[num + 6].DrawRectangle(e, ref m_FValue[num], GetBrush(false));
                }
            }
            for (num = 0; num < 2; num++)
            {
                if (!(!m_BValue[0] || m_BValue[1]))
                {
                    m_TheChangeRectsArr[12 + num].DrawRectangle(e, ref m_FValue[8 - 2 * num], num == 0 ? SolidBrushsItems.Grey3 : SolidBrushsItems.BlueBrush1);
                }
                else if (!(m_BValue[0] || !m_BValue[1]))
                {
                    m_TheChangeRectsArr[14 + num].DrawRectangle(e, ref m_FValue[8 - 2 * num], num == 0 ? SolidBrushsItems.Grey3 : SolidBrushsItems.RedBrush1);
                }
            }
            for (num = 0; num < 2; num++)
            {
                if (!(!m_BValue[0] || m_BValue[1]))
                {
                    m_TheChangeRectsArr[0x10 + num].DrawRectangle(e, ref m_FValue[8 - num], num == 0 ? SolidBrushsItems.Grey3 : SolidBrushsItems.BlueBrush1);
                }
                else if (!(m_BValue[0] || !m_BValue[1]))
                {
                    m_TheChangeRectsArr[0x12 + num].DrawRectangle(e, ref m_FValue[8 - num], num == 0 ? SolidBrushsItems.Grey3 : SolidBrushsItems.RedBrush1);
                }
            }
        }
    }
}