using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.空气制动系统
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class BrakeSetting : HXD3BaseClass
    {
        // Fields
        private float[] m_FValue;

        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;
        private readonly int[] m_Shuzi = { 0x3e8, 700, 700, 0x9c4, 700, 700, 10 };
        private readonly NeedChangeLength[] m_TheChangeRectsArr = new NeedChangeLength[7];

        // Methods
        private void Draw(Graphics e)
        {
            PaintGroundImage(e);
            PaintState(e);
            PaintRectangles(e);
        }

        public override string GetInfo()
        {
            return "制动设置";
        }

        private SolidBrush GetRectangleBrush(int numb, float valueNow)
        {
            switch (numb)
            {
                case 0:
                    if (((valueNow <= 0f) || (valueNow >= 750f)) && ((valueNow <= 950f) || (valueNow > 1000f)))
                    {
                        if ((valueNow >= 750f) && (valueNow <= 850f))
                        {
                            return SolidBrushsItems.YellowBrush1;
                        }
                        if ((valueNow <= 850f) || (valueNow > 950f))
                        {
                            break;
                        }
                        return SolidBrushsItems.BlueBrush1;
                    }
                    return SolidBrushsItems.RedBrush1;

                case 1:
                    if (((valueNow <= 0f) || (valueNow >= 500f)) && ((valueNow <= 600f) || (valueNow >= 700f)))
                    {
                        if ((valueNow < 500f) || (valueNow > 600f))
                        {
                            break;
                        }
                        return SolidBrushsItems.YellowBrush1;
                    }
                    return SolidBrushsItems.GreenBrush1;

                case 2:
                    if (((valueNow <= 0f) || (valueNow >= 500f)) && ((valueNow <= 600f) || (valueNow >= 700f)))
                    {
                        if ((valueNow < 500f) || (valueNow > 600f))
                        {
                            break;
                        }
                        return SolidBrushsItems.YellowBrush1;
                    }
                    return SolidBrushsItems.GreenBrush1;

                case 3:
                    return SolidBrushsItems.YellowBrush1;

                case 4:
                    if ((valueNow <= 0f) || (valueNow >= 320f))
                    {
                        if ((valueNow > 450f) && (valueNow <= 700f))
                        {
                            return SolidBrushsItems.RedBrush1;
                        }
                        if ((valueNow < 320f) || (valueNow > 450f))
                        {
                            break;
                        }
                        return SolidBrushsItems.BlueBrush1;
                    }
                    return SolidBrushsItems.YellowBrush1;

                case 5:
                    if ((valueNow <= 0f) || (valueNow >= 320f))
                    {
                        if ((valueNow > 450f) && (valueNow <= 700f))
                        {
                            return SolidBrushsItems.RedBrush1;
                        }
                        if ((valueNow >= 320f) && (valueNow <= 450f))
                        {
                            return SolidBrushsItems.BlueBrush1;
                        }
                        break;
                    }
                    return SolidBrushsItems.YellowBrush1;

                case 6:
                    return SolidBrushsItems.YellowBrush1;
            }
            return SolidBrushsItems.BlackBrush;
        }

        private void GetValue()
        {
            for (var i = 0; i < m_FValue.Length; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
        }

        protected override  void Initalize()
        {
            InitData();
        }

        private void InitData()
        {
            int num;
            m_FValue = new float[UIObj.InFloatList.Count];
            m_ImgsList = new List<Image>();
            for (num = 0; num < UIObj.ParaList.Count; num++)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + UIObj.ParaList[num]));
            }
            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.BrakeSetting, ref m_RectsList))
            {
                for (num = 0; num < 7; num++)
                {
                    m_TheChangeRectsArr[num] = new NeedChangeLength(m_RectsList[8 + num], m_Shuzi[num], RectRiseDirection.上, false);
                }
                m_HasPlan = true;
            }
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            Draw(dcGs);
        }

        private void PaintGroundImage(Graphics e)
        {
            e.DrawImage(m_ImgsList[0], m_RectsList[0]);
        }

        private void PaintRectangles(Graphics e)
        {
            for (var i = 0; i < 7; i++)
            {
                m_TheChangeRectsArr[i].DrawRectangle(e, ref m_FValue[i], GetRectangleBrush(i, m_FValue[i]));
            }
        }

        private void PaintState(Graphics e)
        {
            e.DrawString(Convert.ToInt32(m_FValue[0]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true), SolidBrushsItems.YellowBrush1, m_RectsList[1], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(Convert.ToInt32(m_FValue[1]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true), SolidBrushsItems.YellowBrush1, m_RectsList[2], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(Convert.ToInt32(m_FValue[2]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true), SolidBrushsItems.YellowBrush1, m_RectsList[3], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(Convert.ToInt32(m_FValue[3]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true), SolidBrushsItems.YellowBrush1, m_RectsList[4], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(Convert.ToInt32(m_FValue[4]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true), SolidBrushsItems.YellowBrush1, m_RectsList[5], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(Convert.ToInt32(m_FValue[5]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true), SolidBrushsItems.YellowBrush1, m_RectsList[6], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(Convert.ToInt32(m_FValue[6]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 8f, true), SolidBrushsItems.YellowBrush1, m_RectsList[7], FontsItems.TheAlignment(FontRelated.居中));
            for (var i = 0; i < 14; i++)
            {
                var ef = m_RectsList[1 + i];
                ef = m_RectsList[1 + i];
                ef = m_RectsList[1 + i];
                ef = m_RectsList[1 + i];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            }
        }
    }
}