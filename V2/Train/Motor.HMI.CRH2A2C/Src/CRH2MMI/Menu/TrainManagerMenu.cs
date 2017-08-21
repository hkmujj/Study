using System.Drawing;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.Menu
{
    internal class TrainManagerMenu : MenuBase
    {
        readonly RectangleF[] m_Recmenu = new RectangleF[9];//列车员模式 功能矩形框
        private readonly int[] m_Goto = new int[9] { 57, 0, 0, 0, 0, 0, 0, 0, 0 };//列车员模式

        public TrainManagerMenu(CRH2menu crh2Menu)
            : base(crh2Menu)
        {
            //列车员
            for (int i = 0; i < 9; i++)
            {
                m_Recmenu[i].Width = 175;
                m_Recmenu[i].Height = 50;
            }
            for (int i = 0; i < 3; i++)
            {
                m_Recmenu[i].X = 215 + i * 175;
                m_Recmenu[i].Y = 125;
            }

            m_Recmenu[3].X = 215;
            m_Recmenu[3].Y = 200;

            for (int i = 4; i < 7; i++)
            {
                m_Recmenu[i].X = 215 + (i - 4) * 175;
                m_Recmenu[i].Y = 275;
            }
            m_Recmenu[7].X = 215;
            m_Recmenu[7].Y = 350;

            m_Recmenu[8].X = 215;
            m_Recmenu[8].Y = 425;
        }

        public override bool OnMouseDown(Point point)
        {
            for (int i = 0; i < 9; i++)
            {
                if (m_Recmenu[i].Contains(point))
                {
                    if (i == 0)
                    {
                        m_CRH2Menu.append_postCmd(CmdType.ChangePage, m_Goto[i], 0, 0);
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
