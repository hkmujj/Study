using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ProcessAux : HXD3BaseClass
    {
        // Fields
        private bool[] m_BValue;

        private float[] m_FValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;

        // Methods
        private void Draw(Graphics e)
        {
            PaintGroundImage(e);
            PaintValue(e);
        }

        public override string GetInfo()
        {
            return "辅助";
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
            if (Coordinate.RectangleFLists(ViewIDName.ProcessAux, ref m_RectsList))
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
            e.DrawString(Convert.ToInt32(m_FValue[0]) + "V", FontsItems.Fonts_Regular(FontName.宋体, 9f, false), SolidBrushsItems.YellowBrush1, m_RectsList[1], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(Convert.ToInt32(m_FValue[1]) + "A", FontsItems.Fonts_Regular(FontName.宋体, 9f, false), SolidBrushsItems.YellowBrush1, m_RectsList[2], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(Convert.ToInt32(m_FValue[2]) + "Hz", FontsItems.Fonts_Regular(FontName.宋体, 9f, false), SolidBrushsItems.YellowBrush1, m_RectsList[3], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(Convert.ToInt32(m_FValue[3]) + "V", FontsItems.Fonts_Regular(FontName.宋体, 9f, false), SolidBrushsItems.YellowBrush1, m_RectsList[4], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(Convert.ToInt32(m_FValue[4]) + "A", FontsItems.Fonts_Regular(FontName.宋体, 9f, false), SolidBrushsItems.YellowBrush1, m_RectsList[5], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(Convert.ToInt32(m_FValue[5]) + "Hz", FontsItems.Fonts_Regular(FontName.宋体, 9f, false), SolidBrushsItems.YellowBrush1, m_RectsList[6], FontsItems.TheAlignment(FontRelated.居中));
            for (num = 0; num < 4; num++)
            {
                e.DrawImage(m_BValue[num] ? m_ImgsList[2] : m_ImgsList[1], m_RectsList[7 + num]);
            }
            e.DrawImage(m_BValue[4] ? m_ImgsList[4] : m_ImgsList[3], m_RectsList[11]);
            for (num = 0; num < 4; num++)
            {
                e.DrawImage(m_BValue[5 + num] ? m_ImgsList[6] : m_ImgsList[5], m_RectsList[12 + num]);
            }
            for (num = 0; num < 7; num++)
            {
                e.DrawImage(m_BValue[9 + num] ? m_ImgsList[6] : m_ImgsList[5], m_RectsList[0x10 + num]);
            }
            for (num = 0; num < 2; num++)
            {
                e.DrawImage(m_BValue[0x10 + num] ? m_ImgsList[6] : m_ImgsList[5], m_RectsList[0x17 + num]);
            }
        }
    }
}