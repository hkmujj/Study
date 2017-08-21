using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ProcessDrives : HXD3BaseClass
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
            return "驱动";
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
            if (Coordinate.RectangleFLists(ViewIDName.ProcessDrives, ref m_RectsList))
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
            for (num = 0; num < 8; num++)
            {
                e.DrawString(Convert.ToInt32(m_FValue[num]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[1 + num], FontsItems.TheAlignment(FontRelated.居中));
            }
            e.DrawString(Convert.ToInt32(m_FValue[8]).ToString("0.0"), FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[9], FontsItems.TheAlignment(FontRelated.居中));
            for (num = 0; num < 4; num++)
            {
                e.DrawString(Convert.ToInt32(m_FValue[9 + num]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[10 + num], FontsItems.TheAlignment(FontRelated.居中));
            }
            for (num = 0; num < 0x12; num++)
            {
                e.DrawString(Convert.ToInt32(m_FValue[13 + num]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[14 + num], FontsItems.TheAlignment(FontRelated.居中));
            }
            for (num = 0; num < 12; num++)
            {
                e.DrawImage(m_BValue[num] ? m_ImgsList[1] : m_ImgsList[2], m_RectsList[0x20 + num]);
            }
            for (num = 0; num < 12; num++)
            {
                e.DrawString(Convert.ToInt32(m_FValue[0x1f + num]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[0x2c + num], FontsItems.TheAlignment(FontRelated.居中));
            }
        }
    }
}