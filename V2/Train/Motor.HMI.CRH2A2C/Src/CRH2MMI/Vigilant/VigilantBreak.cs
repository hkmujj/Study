using CommonUtil.Controls;

namespace CRH2MMI.Vigilant
{
    internal class VigilantBreak
    {
        private int m_BreakCount;

        public VigilantBreak(GDIRectText gdiText)
        {
            GDIText = gdiText;
            BreakCount = 0;
        }

        public GDIRectText GDIText { private set; get; }

        /// <summary>
        /// 制动次数
        /// </summary>
        public int BreakCount
        {
            set
            {
                m_BreakCount = value;
                GDIText.Text = value == 0 ? "0" : value.ToString("###");
            }
            get { return m_BreakCount; }
        }
    }
}
