using System.Drawing;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.Menu
{
    internal class RecorderMenu : MenuBase
    {
        private readonly RectangleF[] m_Recmenu = new RectangleF[5];//记录模式 功能矩形框


        public RecorderMenu(CRH2menu crh2Menu)
            : base(crh2Menu)
        {

            //记录
            for (int i = 0; i < 5; i++)
            {
                m_Recmenu[i].Width = 175;
                m_Recmenu[i].Height = 50;
            }
            m_Recmenu[0].X = 215;
            m_Recmenu[0].Y = 125;

            m_Recmenu[1].X = 215;
            m_Recmenu[1].Y = 200;

            m_Recmenu[2].X = 385;
            m_Recmenu[2].Y = 200;

            m_Recmenu[3].X = 395;
            m_Recmenu[3].Y = 450;

            m_Recmenu[4].X = 565;
            m_Recmenu[4].Y = 450;
        }

        public override bool OnMouseDown(Point point)
        {
            //记录

            for (int i = 0; i < 5; i++)
            {
                if (m_Recmenu[i].Contains(point))
                {
                    if (i == 0)
                    {
                        m_CRH2Menu.append_postCmd(CmdType.ChangePage, 30, 0, 0);
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
