using System.Drawing;

namespace Subway.ATC.Casco.MMI_Message
{
    internal class StartView
    {
        private readonly Image m_StartViewImage;

        private int m_ShowTimes;

        private const int MaxShowTimes = 10;

        public StartView(Image startViewImage)
        {
            m_StartViewImage = startViewImage;
            CurrentState = StartState.Unstart;
        }

        public void Paint(Graphics g)
        {
            CurrentState = StartState.Starting;

            if (MaxShowTimes <= m_ShowTimes++)
            {
                CurrentState = StartState.Started;
            }

            g.DrawImage(m_StartViewImage, 0, 0, 800, 600);
        }

        public void Reset()
        {
            CurrentState = StartState.Unstart;
            m_ShowTimes = 0;
        }

        public StartState CurrentState { private set; get; }

        public enum StartState
        {
            Unstart,

            Starting,

            Started,
        }

    }
}