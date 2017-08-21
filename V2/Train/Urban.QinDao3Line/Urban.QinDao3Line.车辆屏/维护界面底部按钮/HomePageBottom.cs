using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class HomePageBottom : NewQBaseclass
    {
        private readonly bool[] m_BtnIsdown = new bool[3];
        public override bool init(ref int nErrorObjectIndex)
        {

            Mbd.InitData();
            IntiData();
            return true;
        }

        public override void paint(Graphics g)
        {
            DrawLines(g);
            DrawImgs(g);
        }
        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 3; index++)
            {
                if (Mbd.Region[1+index].IsVisible(point))
                {
                    m_BtnIsdown[index] = true;
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 3; index++)
            {
                if (Mbd.Region[1+index].IsVisible(point))
                {
                    m_BtnIsdown[index] = false;
                    switch (index)
                    {
                        case 0:
                            append_postCmd(CmdType.ChangePage, 10, 0, 0);
                            break;
                        case 1:
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        case 2:
                            append_postCmd(CmdType.ChangePage, 54, 0, 0);
                            break;
                    }
                }
            }
            return true;
        }

        private void DrawLines(Graphics e)
        {
            e.DrawLine(Common.m_BlackPen2, Mbd.Points[0], Mbd.Points[1]);
        }

        private void DrawImgs(Graphics e)
        {
            for (int i = 0; i < 3; i++)
            {

                if (m_BtnIsdown[i])
                {
                    e.DrawImage(m_Imgs[4], Mbd.Rects[i + 5]);
                }
                else
                {
                    e.DrawImage(m_Imgs[3], Mbd.Rects[i + 5]);
                }
                e.DrawImage(m_Imgs[i], Mbd.Rects[1+i]);
            }
        }
    }
}