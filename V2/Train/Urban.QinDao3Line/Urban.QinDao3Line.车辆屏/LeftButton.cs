using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class LeftButton:NewQBaseclass
    {
        private readonly List<Rectangle> m_Rects = new List<Rectangle>();
        private readonly List<Region> m_Region = new List<Region>();
        private bool[] m_BtnIsDown = new bool[2];
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            Init();
            return true;
        }

        public override bool mouseDown(Point point)
        {
            for (int i = 0; i < 2; i++)
            {
                if (m_Region[i].IsVisible(point))
                {
                    m_BtnIsDown[i] = true;
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            for (int i = 0; i < 2; i++)
            {
                if (m_Region[i].IsVisible(point))
                {
                    m_BtnIsDown[i] = false;
                }
            }
            return true;
        }
        public override void paint(Graphics g)
        {

            DrawImgs(g);

        }

        private void DrawImgs(Graphics e)
        {
            for (int i=0;i<2;i++)
            {
                if (m_BtnIsDown[i])
                {
                    e.DrawImage(m_Imgs[3], m_Rects[i]);
                }
                else
                    e.DrawImage(m_Imgs[2], m_Rects[i]);
                e.DrawImage(m_Imgs[0 + i], m_Rects[2 + i]);
            }
        }

        private void Init()
        {
            //左侧2个按钮
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(712, 293 + i * 67, 58, 58));
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(715, 297 + i * 67, 50, 50));
            }
            for (int i = 0; i < 2; i++)
            {
                m_Region.Add(new Region(m_Rects[2 + i]));
            }
        }
    }
}
