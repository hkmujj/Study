using System.Drawing;

namespace Motor.HMI.CRH1A
{
    ///<summary>
    ///菜单标题的设置 根据视图的不同  
    ///</summary>
    public class GT_MenuTitle
    {
        private readonly Rectangle m_Recposition = new Rectangle(400, 5, 700, 40);
        public string Title { set; get; }
        private readonly SolidBrush m_Blackbrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        private readonly Font m_Titlefont = new Font("Arial", 12);

        public GT_MenuTitle()
        {
            Title = string.Empty;
        }

        public GT_MenuTitle(string menutitle)
        {
            Title = menutitle;
        }
        public void OnDraw(Graphics g)
        {
            g.DrawString(Title, m_Titlefont, m_Blackbrush, m_Recposition);
        }
    }
}