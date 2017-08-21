using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ProcessBattery : HXD3BaseClass
    {
        // Fields
        private bool[] m_BValue;

        private float[] m_FValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;
        private readonly string[] m_Str = { "CCT1", "CCT2" };

        // Methods
        private void Draw(Graphics e)
        {
            PaintGroundImage(e);
            PaintValue(e);
        }

        public override string GetInfo()
        {
            return "蓄电池";
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
            m_BValue = new bool[UIObj.InBoolList.Count];
            m_FValue = new float[UIObj.InFloatList.Count];
            m_ImgsList = new List<Image>();
            for (var i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + UIObj.ParaList[i]));
            }
            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.ProcessBattery, ref m_RectsList))
            {
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

        private void PaintValue(Graphics e)
        {
            int num;
            for (num = 0; num < 3; num++)
            {
                e.DrawString(Convert.ToInt32(m_FValue[num]) + "A", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[1 + num], FontsItems.TheAlignment(FontRelated.居中));
            }
            e.DrawString(Convert.ToInt32(m_FValue[3]) + "V", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[4], FontsItems.TheAlignment(FontRelated.居中));
            for (num = 0; num < 2; num++)
            {
                e.DrawString(Convert.ToInt32(m_FValue[4]) + "V", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[5 + 4 * num], FontsItems.TheAlignment(FontRelated.居中));
            }
            for (num = 0; num < 2; num++)
            {
                e.DrawString(Convert.ToInt32(m_FValue[5 + num]) + "A", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[6 + num], FontsItems.TheAlignment(FontRelated.居中));
            }
            e.DrawString(Convert.ToInt32(m_FValue[7]) + "V", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[8], FontsItems.TheAlignment(FontRelated.居中));
            for (num = 0; num < 2; num++)
            {
                if (m_BValue[num])
                {
                    e.FillRectangle(new SolidBrush(Color.Green), m_RectsList[10 + num]);
                    e.DrawImage(m_ImgsList[1], m_RectsList[10 + num]);
                    e.DrawString(m_Str[num], FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.BlackBrush, m_RectsList[12 + num], FontsItems.TheAlignment(FontRelated.居中));
                }
                else
                {
                    e.FillRectangle(new SolidBrush(Color.Red), m_RectsList[10 + num]);
                    e.DrawImage(m_ImgsList[2], m_RectsList[10 + num]);
                    e.DrawString(m_Str[num], FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.BlackBrush, m_RectsList[12 + num], FontsItems.TheAlignment(FontRelated.居中));
                }
            }
            e.DrawImage(m_BValue[2] ? m_ImgsList[3] : m_ImgsList[4], m_RectsList[14]);
            if (m_BValue[0] || m_BValue[1])
            {
                e.DrawImage(m_ImgsList[5], m_RectsList[15]);
            }
            else
            {
                e.DrawImage(m_ImgsList[6], m_RectsList[15]);
            }
        }
    }
}