using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class TrainPower : HXD3BaseClass
    {
        // Fields
        private bool[] m_BValue;

        private float[] m_FValue;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;

        // Methods
        private void Draw(Graphics e)
        {
            PaintGroundImage(e);
            PaintState(e);
        }

        public override string GetInfo()
        {
            return "列车供电";
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

            m_ImgsList = UIObj.ParaList.Select(s => Image.FromFile(Path.Combine(RecPath, s))).ToList();

            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.TrainPower, ref m_RectsList))
            {
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

        private void PaintState(Graphics e)
        {
            int num;
            RectangleF ef;
            for (num = 0; num < 6; num++)
            {
                e.DrawImage(m_BValue[num] ? m_ImgsList[2] : m_ImgsList[1], m_RectsList[3 + num]);
            }
            if (m_BValue[6])
            {
                ef = m_RectsList[11];
                ef = m_RectsList[11];
                ef = m_RectsList[11];
                ef = m_RectsList[11];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.FillRectangle(new SolidBrush(Color.Red), m_RectsList[13]);
            }
            else
            {
                ef = m_RectsList[11];
                ef = m_RectsList[11];
                ef = m_RectsList[11];
                ef = m_RectsList[11];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.FillRectangle(new SolidBrush(Color.Green), m_RectsList[13]);
            }
            if (m_BValue[12])
            {
                ef = m_RectsList[11];
                ef = m_RectsList[11];
                ef = m_RectsList[11];
                ef = m_RectsList[11];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.FillRectangle(new SolidBrush(Color.Black), m_RectsList[13]);
            }
            if (m_BValue[7])
            {
                ef = m_RectsList[12];
                ef = m_RectsList[12];
                ef = m_RectsList[12];
                ef = m_RectsList[12];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.FillRectangle(new SolidBrush(Color.Red), m_RectsList[14]);
            }
            else
            {
                ef = m_RectsList[12];
                ef = m_RectsList[12];
                ef = m_RectsList[12];
                ef = m_RectsList[12];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.FillRectangle(new SolidBrush(Color.Green), m_RectsList[14]);
            }
            if (m_BValue[13])
            {
                ef = m_RectsList[12];
                ef = m_RectsList[12];
                ef = m_RectsList[12];
                ef = m_RectsList[12];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.FillRectangle(new SolidBrush(Color.Black), m_RectsList[14]);
            }
            if (m_BValue[8])
            {
                ef = m_RectsList[0x13];
                ef = m_RectsList[0x13];
                ef = m_RectsList[0x13];
                ef = m_RectsList[0x13];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.FillRectangle(new SolidBrush(Color.Red), m_RectsList[20]);
                e.DrawString("供电柜1", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.BlackBrush, m_RectsList[0x13], FontsItems.TheAlignment(FontRelated.居中));
            }
            else
            {
                ef = m_RectsList[0x13];
                ef = m_RectsList[0x13];
                ef = m_RectsList[0x13];
                ef = m_RectsList[0x13];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.FillRectangle(new SolidBrush(Color.Green), m_RectsList[20]);
                e.DrawString("供电柜1", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.BlackBrush, m_RectsList[0x13], FontsItems.TheAlignment(FontRelated.居中));
            }
            if (m_BValue[10])
            {
                ef = m_RectsList[0x13];
                ef = m_RectsList[0x13];
                ef = m_RectsList[0x13];
                ef = m_RectsList[0x13];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.FillRectangle(new SolidBrush(Color.Black), m_RectsList[20]);
                e.DrawString("供电柜1", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[0x13], FontsItems.TheAlignment(FontRelated.居中));
            }
            if (m_BValue[9])
            {
                ef = m_RectsList[0x15];
                ef = m_RectsList[0x15];
                ef = m_RectsList[0x15];
                ef = m_RectsList[0x15];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.FillRectangle(new SolidBrush(Color.Red), m_RectsList[0x16]);
                e.DrawString("供电柜2", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.BlackBrush, m_RectsList[0x15], FontsItems.TheAlignment(FontRelated.居中));
            }
            else
            {
                ef = m_RectsList[0x15];
                ef = m_RectsList[0x15];
                ef = m_RectsList[0x15];
                ef = m_RectsList[0x15];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.FillRectangle(new SolidBrush(Color.Green), m_RectsList[0x16]);
                e.DrawString("供电柜2", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.BlackBrush, m_RectsList[0x16], FontsItems.TheAlignment(FontRelated.居中));
            }
            if (m_BValue[11])
            {
                ef = m_RectsList[0x15];
                ef = m_RectsList[0x15];
                ef = m_RectsList[0x15];
                ef = m_RectsList[0x15];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.FillRectangle(new SolidBrush(Color.Black), m_RectsList[0x16]);
                e.DrawString("供电柜2", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[0x15], FontsItems.TheAlignment(FontRelated.居中));
            }
            num = 0;
            while (num < 2)
            {
                ef = m_RectsList[9 + num];
                ef = m_RectsList[9 + num];
                ef = m_RectsList[9 + num];
                ef = m_RectsList[9 + num];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.DrawString(Convert.ToInt32(m_FValue[0]) + "V", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.YellowBrush1, m_RectsList[9], FontsItems.TheAlignment(FontRelated.靠右));
                e.DrawString(Convert.ToInt32(m_FValue[1]) + "V", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.YellowBrush1, m_RectsList[10], FontsItems.TheAlignment(FontRelated.靠右));
                num++;
            }
            for (num = 0; num < 4; num++)
            {
                ef = m_RectsList[15 + num];
                ef = m_RectsList[15 + num];
                ef = m_RectsList[15 + num];
                ef = m_RectsList[15 + num];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.DrawString(Convert.ToInt32(m_FValue[2]) + "A", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.YellowBrush1, m_RectsList[15], FontsItems.TheAlignment(FontRelated.靠右));
                e.DrawString(Convert.ToInt32(m_FValue[3]) + "A", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.YellowBrush1, m_RectsList[0x10], FontsItems.TheAlignment(FontRelated.靠右));
                e.DrawString(Convert.ToInt32(m_FValue[4]) + "A", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.YellowBrush1, m_RectsList[0x11], FontsItems.TheAlignment(FontRelated.靠右));
                e.DrawString(Convert.ToInt32(m_FValue[5]) + "A", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.YellowBrush1, m_RectsList[0x12], FontsItems.TheAlignment(FontRelated.靠右));
            }
            for (num = 0; num < 2; num++)
            {
                ef = m_RectsList[1 + num];
                ef = m_RectsList[1 + num];
                ef = m_RectsList[1 + num];
                ef = m_RectsList[1 + num];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.DrawString(Convert.ToInt32(m_FValue[6]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.YellowBrush1, m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠右));
                e.DrawString(Convert.ToInt32(m_FValue[7]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.YellowBrush1, m_RectsList[2], FontsItems.TheAlignment(FontRelated.靠右));
            }
            for (num = 0; num < 2; num++)
            {
                ef = m_RectsList[0x19 + num];
                ef = m_RectsList[0x19 + num];
                ef = m_RectsList[0x19 + num];
                ef = m_RectsList[0x19 + num];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.DrawString(Convert.ToInt32(m_FValue[10]) + "V", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.YellowBrush1, m_RectsList[0x19], FontsItems.TheAlignment(FontRelated.靠右));
                e.DrawString(Convert.ToInt32(m_FValue[11]) + "V", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.YellowBrush1, m_RectsList[0x1a], FontsItems.TheAlignment(FontRelated.靠右));
            }
        }
    }
}