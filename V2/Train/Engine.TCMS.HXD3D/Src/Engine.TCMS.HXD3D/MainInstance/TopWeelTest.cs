using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.MainInstance
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class TopWeelTest : HXD3BaseClass
    {
        // Fields
        private readonly bool[] m_BtnIsdown = new bool[6];

        private bool[] m_Bvalue;
        private float[] m_Fvalue;
        private readonly List<Rectangle> m_RectList = new List<Rectangle>();
        private readonly List<Region> m_RegionList = new List<Region>();

        // Methods
        private void AxleSpeed(Graphics e)
        {
            e.DrawString(CommonRes.Str1[2], CommonRes.Font14B, CommonRes.WhiteBrush, m_RectList[7], CommonRes.MFormat);
            for (var i = 0; i < 6; i++)
            {
                if (m_BtnIsdown[i])
                {
                    e.DrawString(m_Fvalue[i].ToString("0.0") + "km/h", CommonRes.Font14B, CommonRes.WhiteBrush, m_RectList[8], CommonRes.MFormat);
                }
                if (BtnIsTrue())
                {
                    e.DrawString("0.0km/h", CommonRes.Font14B, CommonRes.WhiteBrush, m_RectList[8], CommonRes.MFormat);
                }
            }
        }

        private void BtnIsover(int index)
        {
            m_BtnIsdown[index] = true;
            for (var i = 0; i < m_RegionList.Count; i++)
            {
                if (i != index)
                {
                    m_BtnIsdown[i] = false;
                }
            }
        }

        private bool BtnIsTrue()
        {
            for (var i = 0; i < 6; i++)
            {
                if (m_BtnIsdown[i])
                {
                    return false;
                }
            }
            return true;
        }

        private void DrawButton(Graphics e)
        {
            for (var i = 0; i < 6; i++)
            {
                e.DrawRectangle(CommonRes.WhitePen1, m_RectList[i]);
                e.DrawString(CommonRes.Str1[0] + (i + 1), CommonRes.Font12B, CommonRes.WhiteBrush, m_RectList[i], CommonRes.MFormat);
            }
        }

        private void DrawShowContent(Graphics e)
        {
            e.DrawRectangle(CommonRes.WhitePen1, m_RectList[6]);
            for (var i = 0; i < 6; i++)
            {
                if (m_BtnIsdown[i])
                {
                    e.DrawString(CommonRes.Str1[0] + (i + 1) + CommonRes.Str1[1], CommonRes.Font12B, CommonRes.WhiteBrush, m_RectList[6], CommonRes.MFormat);
                }
                else if (BtnIsTrue())
                {
                    e.DrawString(CommonRes.Str1[0] + CommonRes.Str1[1], CommonRes.Font12B, CommonRes.WhiteBrush, m_RectList[6], CommonRes.MFormat);
                }
            }
        }

        public override string GetInfo()
        {
            return "顶轮实验";
        }

        private void GetValue()
        {
            int num;
            for (num = 0; num < m_Bvalue.Length; num++)
            {
                m_Bvalue[num] = BoolList[UIObj.InBoolList[num]];
            }
            for (num = 0; num < m_Fvalue.Length; num++)
            {
                m_Fvalue[num] = FloatList[UIObj.InFloatList[num]];
            }
        }

        protected override  void Initalize()
        {
            InitData();
        }

        private void InitData()
        {
            int num;
            m_Bvalue = new bool[UIObj.InBoolList.Count];
            m_Fvalue = new float[UIObj.InFloatList.Count];
            for (num = 0; num < 2; num++)
            {
                for (var i = 0; i < 3; i++)
                {
                    m_RectList.Add(new Rectangle(0x34 + i * 0x5c, 0x8a + num * 0x3e, 0x53, 0x2e));
                }
            }
            m_RectList.Add(new Rectangle(0x89, 0x170, 450, 0x69));
            m_RectList.Add(new Rectangle(500, 0xc3, 0x5e, 0x2d));
            m_RectList.Add(new Rectangle(580, 0xc3, 0x70, 0x2d));
            for (num = 0; num < 6; num++)
            {
                m_RegionList.Add(new Region(m_RectList[num]));
            }
        }

        public override bool mouseDown(Point nPoint)
        {
            for (var i = 0; i < m_RegionList.Count; i++)
            {
                if (m_RegionList[i].IsVisible(nPoint))
                {
                    BtnIsover(i);
                }
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            return base.mouseUp(nPoint);
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            DrawButton(dcGs);
            DrawShowContent(dcGs);
            AxleSpeed(dcGs);
            base.paint(dcGs);
        }
    }
}