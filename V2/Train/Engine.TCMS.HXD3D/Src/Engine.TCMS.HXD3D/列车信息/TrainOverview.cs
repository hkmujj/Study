using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.Title;
using Engine.TCMS.HXD3D.底层共用;
using Engine.TCMS.HXD3D.过程数据;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.列车信息
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class TrainOverview : HXD3BaseClass
    {
        // Fields
        private bool[] m_BValue;

        private float[] m_FValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private bool[] m_LastStates;
        private List<RectangleF> m_RectsList;

        // Methods
        private void Draw(Graphics e)
        {
            PaintGroundImage(e);
            PaintState(e);
        }

        public override string GetInfo()
        {
            return "机车纵览";
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
            m_LastStates = m_BValue.ToArray<bool>();
            m_ImgsList = new List<Image>();
            for (var i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + UIObj.ParaList[i]));
            }
            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.TrainOverview, ref m_RectsList))
            {
                m_HasPlan = true;
            }
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            Draw(dcGs);
            m_LastStates = m_BValue.ToArray<bool>();
        }

        private void PaintGroundImage(Graphics e)
        {
            e.DrawImage(m_ImgsList[0], m_RectsList[0x11]);
        }

        private void PaintState(Graphics e)
        {
            int num;
            int num2;
            if (!(m_LastStates[0] || !m_BValue[0x22]))
            {
                var instance = ProcessCounters.Instance;
                instance.CbCount++;
            }
            if (m_BValue[0])
            {
                var counters2 = ProcessCounters.Instance;
                counters2.ShouD1Count++;
            }
            for (num = 0; num < 3; num++)
            {
                if (m_BValue[num + 30])
                {
                    e.DrawImage(m_ImgsList[num + 1], m_RectsList[0]);
                }
            }
            e.DrawString(Convert.ToInt32(m_FValue[0]).ToString(), FontsItems.Fonts_Regular(FontName.宋体, 11.5f, false), SolidBrushsItems.YellowBrush1, m_RectsList[1], FontsItems.TheAlignment(FontRelated.居中));
            for (num = 0; num < 2; num++)
            {
                num2 = 0;
                while (num2 < 4)
                {
                    if (m_BValue[num2 + num * 4])
                    {
                        e.DrawImage(m_ImgsList[4 + num2 + num * 4], m_RectsList[2 + num]);
                    }
                    num2++;
                }
                if (m_BValue[8 + num])
                {
                    e.DrawImage(m_ImgsList[12], m_RectsList[5 + num]);
                }
                if (m_BValue[10 + num])
                {
                    e.DrawImage(m_ImgsList[13], m_RectsList[7 + num]);
                }
                if (m_BValue[36 + num])
                {
                    e.DrawImage(m_ImgsList[20], m_RectsList[7 + num]);
                }
                if (m_BValue[0x21 + num])
                {
                    e.DrawImage(m_ImgsList[0x12 + num], m_RectsList[9]);
                }
            }
            for (num = 0; num < 3; num++)
            {
                for (num2 = 0; num2 < 6; num2++)
                {
                    if (m_BValue[12 + num2 + num * 6])
                    {
                        e.DrawImage(m_ImgsList[15 + num], m_RectsList[11 + num2]);
                    }
                }
            }
            if (m_BValue[0x23])
            {
                e.DrawImage(m_ImgsList[14], m_RectsList[10]);
            }
            if (m_BValue[0x26])
            {
                e.DrawImage(m_ImgsList[21], m_RectsList[10]);
            }
            e.DrawString(TopTitleScreen.TrainID1, FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsList[4], FontsItems.TheAlignment(FontRelated.居中));
        }
    }
}