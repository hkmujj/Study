using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MaintanceBottomButton:NewQBaseclass
    {
        private readonly bool[] m_BtnIsdown = new bool[4];
        public override bool init(ref int nErrorObjectIndex)
        {

            Mbd.InitData();
            IntiData();
            return true;
        }
        private int CurrentViewId = 0;
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                CurrentViewId = nParaB;
            }
            base.setRunValue(nParaA, nParaB, nParaC);
        }

        public override void paint(Graphics g)
        {
            DrawLines(g);
            DrawImgs(g);
        }
        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 4; index++)
            {
                if (Mbd.Region[index].IsVisible(point))
                {
                    m_BtnIsdown[index] = true;
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 4; index++)
            {
                if (Mbd.Region[index].IsVisible(point))
                {
                    m_BtnIsdown[index] = false;
                    switch (index)
                    {
                        case 0:
                            if (CurrentViewId == 16)
                            {
                                append_postCmd(CmdType.ChangePage, 15, 0, 0);
                            }
                            else
                            {
                                append_postCmd(CmdType.ChangePage, 10, 0, 0);
                            }
                            break;
                        case 1:
                            append_postCmd(CmdType.ChangePage, 10, 0, 0);
                            break;
                        case 2:
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        case 3:
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
            for (int i=0;i<4;i++)
            {

                if (m_BtnIsdown[i])
                {
                    e.DrawImage(m_Imgs[5], Mbd.Rects[i+4]);
                }
                else
                {
                    e.DrawImage(m_Imgs[4], Mbd.Rects[i+4]);
                }
                e.DrawImage(m_Imgs[i], Mbd.Rects[i]);
            }
        }

        /// <summary>
        /// 把所有btnisDown的值制为false
        /// </summary>
    }
}
