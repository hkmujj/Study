using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.MainInstance
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class MainControlTest : HXD3BaseClass
    {
        // Fields
        private readonly Image[] m_BaseImage = new Image[2];

        private readonly bool[] m_BtnIsDown = new bool[1];
        private readonly string[] m_BtnNames = { "试验项目", "调速手柄零位确认", "制动12级手柄位确认", "结果" };
        private readonly List<Region> m_BtnRegion = new List<Region>();
        private float[] m_FValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;

        // Methods
        private void Draw(Graphics e)
        {
            PaintButtonsState(e);
            PaintValue(e);
        }

        public override string GetInfo()
        {
            return "司控器试验";
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
            if (Coordinate.RectangleFLists(ViewIDName.MainControlTest, ref m_RectsList))
            {
                m_BtnRegion.Add(new Region(m_RectsList[7]));
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
            e.FillRectangle(new SolidBrush(Color.Blue), m_RectsList[8]);
            e.DrawString("确认", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[7], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawImage(m_BtnIsDown[0] ? m_ImgsList[0] : m_ImgsList[1], m_RectsList[7]);
        }

        private void PaintValue(Graphics e)
        {
            int num;
            for (num = 0; num < 4; num++)
            {
                e.DrawString(m_BtnNames[num], FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[num], FontsItems.TheAlignment(FontRelated.靠左));
            }
            for (num = 0; num < 7; num++)
            {
                var ef = m_RectsList[num];
                ef = m_RectsList[num];
                ef = m_RectsList[num];
                ef = m_RectsList[num];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            }
            for (num = 0; num < 2; num++)
            {
                e.DrawString(Convert.ToInt32(m_FValue[num]).ToString("#"), FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsList[4 + num], FontsItems.TheAlignment(FontRelated.靠左));
            }
            e.DrawString("试验是否开始", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[6], FontsItems.TheAlignment(FontRelated.居中));
        }
    }
}