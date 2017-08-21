using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.MainInstance
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class AuxPowerTest : HXD3BaseClass
    {
        // Fields
        private readonly Image[] m_BaseImage = new Image[2];

        private readonly bool[] m_BtnIsDown = new bool[1];
        private readonly string[] m_BtnNames = { "输出电压(V)", "输出电流(A)", "输出频率(Hz)", "结果" };
        private readonly List<Region> m_BtnRegion = new List<Region>();
        private float[] m_FValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;
        private readonly string[] m_Str = { "辅助电源装置1", "辅助电源装置2" };

        // Methods
        private void Draw(Graphics e)
        {
            PaintButtonsState(e);
            PaintValue(e);
        }

        public override string GetInfo()
        {
            return "辅助电源试验";
        }

        private void GetValue()
        {
            for (var i = 0; i < m_FValue.Length; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[0] + i];
            }
        }

        protected override  void Initalize()
        {
            InitData();
        }

        private void InitData()
        {
            m_FValue = new float[UIObj.InFloatList[1] - UIObj.InFloatList[0] + 1];
            m_ImgsList = new List<Image>();
            for (var i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + UIObj.ParaList[i]));
            }
            m_BaseImage[0] = m_ImgsList[1];
            m_BaseImage[1] = m_ImgsList[0];
            if (Coordinate.RectangleFLists(ViewIDName.AuxPowerTest, ref m_RectsList))
            {
                m_BtnRegion.Add(new Region(m_RectsList[0x10]));
                m_HasPlan = true;
            }
        }

        public override bool mouseDown(Point nPoint)
        {
            if (m_BtnRegion[0].IsVisible(nPoint))
            {
                m_BtnIsDown[0] = true;
                return true;
            }
            return false;
        }

        public override bool mouseUp(Point nPoint)
        {
            if (m_BtnRegion[0].IsVisible(nPoint))
            {
                m_BtnIsDown[0] = false;
                return true;
            }
            return false;
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            Draw(dcGs);
        }

        private void PaintButtonsState(Graphics e)
        {
            e.FillRectangle(new SolidBrush(Color.Blue), m_RectsList[0x11]);
            e.DrawString("确认", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[0x10], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawImage(m_BtnIsDown[0] ? m_ImgsList[0] : m_ImgsList[1], m_RectsList[0x10]);
        }

        private void PaintValue(Graphics e)
        {
            int num;
            for (num = 0; num < 2; num++)
            {
                e.DrawString(m_Str[num], FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[1 + num], FontsItems.TheAlignment(FontRelated.靠左));
            }
            for (num = 0; num < 4; num++)
            {
                e.DrawString(m_BtnNames[num], FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[3 * num + 3], FontsItems.TheAlignment(FontRelated.靠左));
            }
            for (num = 1; num < 4; num++)
            {
                e.DrawString("123", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[3 * num + 1], FontsItems.TheAlignment(FontRelated.居中));
            }
            for (num = 1; num < 4; num++)
            {
                e.DrawString("123", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[3 * num + 2], FontsItems.TheAlignment(FontRelated.居中));
            }
            for (num = 0; num < 0x10; num++)
            {
                var ef = m_RectsList[num];
                ef = m_RectsList[num];
                ef = m_RectsList[num];
                ef = m_RectsList[num];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            }
            for (num = 0; num < 2; num++)
            {
                e.DrawString(Convert.ToInt32(m_FValue[num]).ToString("#"), FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsList[14 + num], FontsItems.TheAlignment(FontRelated.靠左));
            }
            e.DrawString("试验是否开始", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[15], FontsItems.TheAlignment(FontRelated.居中));
        }
    }
}